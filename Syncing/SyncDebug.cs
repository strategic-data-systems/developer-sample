using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DeveloperSample.Syncing
{
    public class SyncDebug
    {
        public async Task<List<string>> InitializeList(IEnumerable<string> items)
        {
            //Problem:
            // I believe, Parallel.ForEach is not waiting for the asynchronous operations to complete,
            // that might be the reason only 1 item being added to the bag.
            //Solution:
            //I used Parallel.ForEachAsync along with await, for the async operations to complete.
            var bag = new ConcurrentBag<string>();
            await Parallel.ForEachAsync(items, async (item, cancellationToken) =>
            {
                var r = await Task.Run(() => item).ConfigureAwait(false);
                bag.Add(r);
            });
            var list = bag.ToList();
            return list;
        }

        //Problem: This method will initialize the concurrentDictionary with keys ranging from 1 to 100
        //based on the values provided by the getItem func delegate using 3 different threads.
        //The unit test is expecting the getItem to be called 100 times, which is the count of the total keys.
        //I believe, the threads are not implemented propelry and they are duplicating work and
        //all of the threads are working on the same set of keys.
        //Solution:
        //I think we may need to distribute the load across the threads uniformly so that we can leverage the threads 
        //and distribute the load across the threads uniformly and make sure each thread does chunk of work.
        //So, I broken the total itemsToInitialize in to Chunks and created seperate thread for each chunk.
        public Dictionary<int, string> InitializeDictionary(Func<int, string> getItem)
        {
            var itemsToInitialize = Enumerable.Range(0, 100).ToList();
            var noOfThreads = 3;
            var chunks = SplitListIntoChunks(itemsToInitialize, noOfThreads);

            var concurrentDictionary = new ConcurrentDictionary<int, string>();

            var threads =
                chunks.Select(chunk => new Thread(() =>
                {
                    foreach (var item in chunk)
                    {
                        concurrentDictionary.AddOrUpdate(item, getItem, (_, s) => s);
                    }
                }))
                .ToList();

            foreach (var thread in threads)
            {
                thread.Start();
            }
            foreach (var thread in threads)
            {
                thread.Join();
            }

            return concurrentDictionary.ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        private List<List<T>> SplitListIntoChunks<T>(List<T> list, int chunksCount)
        {
            var chunks = new List<List<T>>();
            int chunkSize = list.Count / chunksCount;
            int extraItems = list.Count % chunksCount;

            for (int i = 0; i < chunksCount; i++)
            {
                var chunk = list.Skip(i * chunkSize).Take(chunkSize).ToList();
                //If there are more extra items, we add them uniformly across the chunks.
                //I mean, if we divide 100 items to  3 parts, there is 1 extra item, which we add to the 1st chunk.
                if (i < extraItems) 
                {
                    chunk.Add(list[chunkSize * chunksCount + i]);
                }
                chunks.Add(chunk);
            }

            return chunks;
        }
    }
}
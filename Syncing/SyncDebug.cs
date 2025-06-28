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
        public List<string> InitializeList(IEnumerable<string> items)
        {
            var bag = new ConcurrentBag<string>();

            var tasks = items.Select(async i =>
            {
                var r = await Task.Run(() => i).ConfigureAwait(false);
                bag.Add(r);
            });

            Task.WaitAll(tasks.ToArray());

            return bag.ToList();
        }
        public Dictionary<int, string> InitializeDictionary(Func<int, string> getItem)
        {
            var itemsToInitialize = Enumerable.Range(0, 100).ToList();
            var concurrentDictionary = new ConcurrentDictionary<int, Lazy<string>>();

            var threads = Enumerable.Range(0, 3)
                .Select(_ => new Thread(() =>
                {
                    foreach (var item in itemsToInitialize)
                    {
                        concurrentDictionary.GetOrAdd(
                            item,
                            _ => new Lazy<string>(() => getItem(item), LazyThreadSafetyMode.ExecutionAndPublication)
                        );
                    }
                }))
                .ToList();

            threads.ForEach(t => t.Start());
            threads.ForEach(t => t.Join());

            // Extract actual values from Lazy<string>
            return concurrentDictionary.ToDictionary(kv => kv.Key, kv => kv.Value.Value);
        }



    }
}
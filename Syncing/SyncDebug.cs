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
        // Basha - Here,  we are moving asynchronous programming, and we are waiting all tasks to be completed, so it wont block at here, it moves forward 
        public async Task<List<string>> InitializeList(IEnumerable<string> items)
        {
            var bag= items.Select(async i => {
                 var r = await Task.Run(() => i);
                 return r;
            })
            
            var list = await  Task.WhenAll(bag);
            return list.ToList();
        }
        
      // Basha - we are looping on each item and doing manipulation of items and making the dictionary available.
        public Dictionary<int, string> InitializeDictionary(Func<int, string> getItem)
        {
            var itemsToInitialize = Enumerable.Range(0, 100).ToList();
            var concurrentDictionary = new ConcurrentDictionary<int, string>();

            Parallel.ForEach(itemsToInitialize, item =>{
                 concurrentDictionary.AddOrUpdate(item, getItem, (_, __) => getItem(item));
            });

            return concurrentDictionary.ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }
}

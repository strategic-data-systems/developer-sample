using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace DeveloperSample.Syncing
{
    public class SyncTest
    {
        [Fact]
        public async void CanInitializeCollection()
        {
            //Arrange
            var debug = new SyncDebug();
            var items = new List<string> { "one", "two" };

            //Act
            var result = await debug.InitializeList(items);
            
            //Assert
            Assert.Equal(items.Count, result.Count);
            Assert.True(!items.Except(result).Any());//Verify that both lists have same content
        }

        [Fact()]
        public void ItemsOnlyInitializeOnce()
        {
            var debug = new SyncDebug();
            var count = 0;
            //The func delegate is doing 2 things before returning the value
            //1. Sleeping for 1ms
            //2. Incrementing the local count variable in threadsafe way using Interlocked.Increment
            var dictionary = debug.InitializeDictionary(i =>
            {
                Thread.Sleep(1);
                Interlocked.Increment(ref count); 
                return i.ToString();
            });

            Assert.Equal(100, count);
            Assert.Equal(100, dictionary.Count);
        }
    }
}
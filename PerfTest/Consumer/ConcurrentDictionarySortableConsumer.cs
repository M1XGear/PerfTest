using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerfTest.Consumer
{
    /// <summary>
    /// 
    /// </summary>
    public class ConcurrentDictionarySortableConsumer : SortableConsumerBase
    {
        private readonly ConcurrentDictionary<int, int> _memory;

        /// <summary>
        /// 
        /// </summary>
        public ConcurrentDictionarySortableConsumer()
        {
            _memory = new ConcurrentDictionary<int, int>();
        }

        /// <inheritdoc cref="ISortableConsumer{T}.ConsumeAsync"/>
        public override Task ConsumeAsync(int val)
        {
            _memory.AddOrUpdate(val, i => 1, (key, oldValue) => oldValue + 1);

            return Task.CompletedTask;
        }

        /// <inheritdoc cref="ISortableConsumer{T}.GetOrdered"/>
        public override IEnumerable<int> GetOrdered()
        {
            KeyValuePair<int, int>[] copy = _memory.ToArray();

            var list = new List<int>(copy.Length);
            foreach (var keyValuePair in copy)
            {
                for (var i = 0; i < keyValuePair.Value; i++)
                {
                    list.Add(keyValuePair.Key);
                }
            }

            return list;
        }
    }
}
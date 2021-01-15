using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PerfTest.Consumer
{
    /// <summary>
    /// 
    /// </summary>
    public class DictionarySortableConsumer : SortableConsumerBase
    {
        private readonly Dictionary<int, int> _memory;
        private readonly ReaderWriterLockSlim _memoryLock = new ReaderWriterLockSlim();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="minInputValue"></param>
        /// <param name="maxInputValue"></param>
        public DictionarySortableConsumer(int minInputValue, int maxInputValue)
        {
            _memory = new Dictionary<int, int>(maxInputValue - minInputValue + 1);
            for (var i = minInputValue; i <= maxInputValue; i++)
            {
                _memory[i] = 0;
            }
        }

        /// <inheritdoc cref="ISortableConsumer{T}.ConsumeAsync"/>
        public override Task ConsumeAsync(int val)
        {
            _memoryLock.EnterWriteLock();
            try
            {
                _memory[val] = _memory[val] + 1;
            }
            finally
            {
                _memoryLock.ExitWriteLock();
            }

            return Task.CompletedTask;
        }

        /// <inheritdoc cref="ISortableConsumer{T}.GetOrdered"/>
        public override int[] GetOrdered()
        {
            IOrderedEnumerable<KeyValuePair<int, int>> copy;
            _memoryLock.EnterReadLock();
            try
            {
                copy = _memory.Where(x => x.Value != 0).OrderBy(x => x.Key);
            }
            finally
            {
                _memoryLock.ExitReadLock();
            }

            var list = new List<int>();
            foreach (var keyValuePair in copy)
            {
                for (var i = 0; i < keyValuePair.Value; i++)
                {
                    list.Add(keyValuePair.Key);
                }
            }

            return list.ToArray();
        }
    }
}
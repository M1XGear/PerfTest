using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PerfTest.Consumer
{
    /// <summary>
    /// 
    /// </summary>
    public class SortedListSortableConsumer : ISortableConsumer<int>
    {
        private readonly SortedList<int, int> _memory;
        private readonly ReaderWriterLockSlim _memoryLock = new ReaderWriterLockSlim();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="minInputValue"></param>
        /// <param name="maxInputValue"></param>
        public SortedListSortableConsumer(int minInputValue, int maxInputValue)
        {
            _memory = new SortedList<int, int>(maxInputValue - minInputValue + 1);
            for (var i = minInputValue; i <= maxInputValue; i++)
            {
                _memory[i] = 0;
            }
        }

        /// <inheritdoc cref="ISortableConsumer{T}.ConsumeAsync"/>
        public Task ConsumeAsync(int val)
        {
            _memoryLock.EnterWriteLock();
            try
            {
                _memory[val]++;
            }
            finally
            {
                _memoryLock.ExitWriteLock();
            }

            return Task.CompletedTask;
        }

        /// <inheritdoc cref="ISortableConsumer{T}.GetOrdered"/>
        public int[] GetOrdered()
        {
            _memoryLock.EnterReadLock();
            KeyValuePair<int, int>[] copy;
            try
            {
                copy = _memory.Where(x => x.Value != 0).ToArray();
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
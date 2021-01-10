using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PerfTest.Consumer
{
    /// <summary>
    /// 
    /// </summary>
    public class ListSortableConsumer : ISortableConsumer<int>
    {
        private readonly List<int> _memory;
        private readonly ReaderWriterLockSlim _memoryLock = new ReaderWriterLockSlim();

        /// <summary>
        /// 
        /// </summary>
        public ListSortableConsumer()
        {
            _memory = new List<int>();
        }

        /// <inheritdoc cref="ISortableConsumer{T}.ConsumeAsync"/>
        public Task ConsumeAsync(int val)
        {
            _memoryLock.EnterWriteLock();
            try
            {
                _memory.Add(val);
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
            IOrderedEnumerable<int> copy;
            try
            {
                copy = _memory.OrderBy(x => x);
            }
            finally
            {
                _memoryLock.ExitReadLock();
            }

            return copy.ToArray();
        }
    }
}
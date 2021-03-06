﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PerfTest.Consumer
{
    /// <summary>
    /// 
    /// </summary>
    public class ListSortableConsumer : SortableConsumerBase
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
        public override Task ConsumeAsync(int val)
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
        public override IEnumerable<int> GetOrdered()
        {
            int[] copy;
            _memoryLock.EnterReadLock();
            try
            {
                copy = _memory.ToArray();
            }
            finally
            {
                _memoryLock.ExitReadLock();
            }

            return copy.OrderBy(x=>x);
        }
    }
}
﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PerfTest.Consumer
{
    /// <summary>
    /// ToDo: Optimize (sortOnInsert), or it's no different from simpleList
    /// </summary>
    public class LinkedListSortableConsumer : SortableConsumerBase
    {
        private readonly LinkedList<int> _memory;
        private readonly ReaderWriterLockSlim _memoryLock = new ReaderWriterLockSlim();

        /// <summary>
        /// 
        /// </summary>
        public LinkedListSortableConsumer()
        {
            _memory = new LinkedList<int>();
        }

        /// <inheritdoc cref="ISortableConsumer{T}.ConsumeAsync"/>
        public override Task ConsumeAsync(int val)
        {
            _memoryLock.EnterWriteLock();
            try
            {
                _memory.AddLast(val);
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

            return copy.OrderBy(x => x);
        }
    }
}
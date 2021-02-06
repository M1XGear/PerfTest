﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PerfTest.Consumer
{
    /// <summary>
    /// 
    /// </summary>
    public class ArraySortableConsumerBase : SortableConsumerBase
    {
        private readonly int[] _memory;
        private readonly ReaderWriterLockSlim _memoryLock = new ReaderWriterLockSlim();

        private readonly int _minInputValue;
        private readonly int _maxInputValue;
        private readonly int _size;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="minInputValue"></param>
        /// <param name="maxInputValue"></param>
        public ArraySortableConsumerBase(int minInputValue, int maxInputValue)
        {
            _minInputValue = minInputValue;
            _maxInputValue = maxInputValue;
            _size = maxInputValue - minInputValue;

            _memory = new int[_size];

            for (int i = 0; i < _size; i++)
            {
                _memory[i] = 0;
            }
        }

        /// <inheritdoc cref="ISortableConsumer{T}.ConsumeAsync"/>
        public override Task ConsumeAsync(int val)
        {
            var idx = val - _minInputValue;
            _memory[idx]++;
            return Task.CompletedTask;
        }

        /// <inheritdoc cref="ISortableConsumer{T}.GetOrdered"/>
        public override int[] GetOrdered()
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

            var list = new List<int>(_size);
            var value = _minInputValue;
            for (int i = 0; i < _size; i++)
            {
                for (int count = 0; count < copy[i]; count++)
                {
                    list.Add(value);
                }

                value++;
            }

            return list.ToArray();
        }
    }
}
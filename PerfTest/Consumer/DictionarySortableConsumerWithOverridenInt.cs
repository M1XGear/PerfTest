using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PerfTest.Consumer
{
    /// <summary>
    /// 
    /// </summary>
    public class DictionarySortableConsumerWithOverridenInt: SortableConsumerBase
    {
        private readonly Dictionary<MyInt, int> _memory;
        private readonly ReaderWriterLockSlim _memoryLock = new ReaderWriterLockSlim();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="minInputValue"></param>
        /// <param name="maxInputValue"></param>
        public DictionarySortableConsumerWithOverridenInt(int minInputValue, int maxInputValue)
        {
            _memory = new Dictionary<MyInt, int>(maxInputValue - minInputValue + 1);
            for (var i = minInputValue; i <= maxInputValue; i++)
            {
                var val = new MyInt(i);
                _memory[val] = 0;
            }
        }

        /// <inheritdoc cref="ISortableConsumer{T}.ConsumeAsync"/>
        public override Task ConsumeAsync(int val)
        {
            var myVal = new MyInt(val);
            _memoryLock.EnterWriteLock();
            try
            {
                _memory[myVal] = _memory[myVal] + 1;
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
            KeyValuePair<MyInt, int>[] copy;
            _memoryLock.EnterReadLock();
            try
            {
                copy = _memory.ToArray();
            }
            finally
            {
                _memoryLock.ExitReadLock();
            }

            var list = new List<int>(copy.Length);
            foreach (var keyValuePair in copy)
            {
                for (var i = 0; i < keyValuePair.Value; i++)
                {
                    list.Add(keyValuePair.Key.Value);
                }
            }

            return list.ToArray();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal readonly struct MyInt
    {
        private readonly int _val;

        public MyInt(int val)
        {
            _val = val;
        }

        public int Value => _val;

        public override int GetHashCode()
        {
            return _val;
        }
    }
}
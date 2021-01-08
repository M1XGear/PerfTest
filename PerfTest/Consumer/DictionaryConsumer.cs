using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerfTest.Consumer
{
    /// <summary>
    /// 
    /// </summary>
    public class DictionaryConsumer : IConsumer<int>
    {
        private readonly Dictionary<int, int> _memory;

        /// <inheritdoc cref="IConsumer{T}"/>
        public DictionaryConsumer(int minInputValue, int maxInputValue)
        {
            _memory = new Dictionary<int, int>(maxInputValue - minInputValue + 1);
            for (var i = minInputValue; i <= maxInputValue; i++)
            {
                _memory[i] = 0;
            }
        }

        /// <inheritdoc cref="IConsumer{T}.ConsumeAsync"/>
        public Task ConsumeAsync(int val)
        {
            _memory[val] = _memory[val] + 1;
            return Task.CompletedTask;
        }
    }
}
using System.Threading.Tasks;

namespace PerfTest.Consumer
{
    /// <summary>
    /// 
    /// </summary>
    public class VoidConsumer : IConsumer<int>
    {
        /// <inheritdoc cref="IConsumer{T}.ConsumeAsync"/>
        public Task ConsumeAsync(int val)
        {
            return Task.CompletedTask;
        }
    }
}
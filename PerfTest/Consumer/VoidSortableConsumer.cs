using System.Threading.Tasks;

namespace PerfTest.Consumer
{
    /// <summary>
    /// 
    /// </summary>
    public class VoidSortableConsumer : ISortableConsumer<int>
    {
        /// <inheritdoc cref="ISortableConsumer{T}.ConsumeAsync"/>
        public Task ConsumeAsync(int val)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc cref="ISortableConsumer{T}.GetOrdered"/>
        public int[] GetOrdered()
        {
            return new int[] { };
        }
    }
}
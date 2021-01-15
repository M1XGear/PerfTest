using System.Threading.Tasks;

namespace PerfTest.Consumer
{
    /// <summary>
    /// Simpliest consumer, to test Channel impact on benchmark
    /// </summary>
    public class VoidSortableConsumer : SortableConsumerBase
    {
        /// <inheritdoc cref="ISortableConsumer{T}.ConsumeAsync"/>
        public override Task ConsumeAsync(int val)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc cref="ISortableConsumer{T}.GetOrdered"/>
        public override int[] GetOrdered()
        {
            return new int[] { };
        }
    }
}
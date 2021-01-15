using System.Threading.Tasks;

namespace PerfTest.Consumer
{
    /// <summary>
    /// Consumes values, and values can be sorted
    /// </summary>
    public interface ISortableConsumer<in T>
    {
        /// <summary>
        /// Consumes value
        /// </summary>
        Task ConsumeAsync(T val);

        /// <summary>
        /// Sort saved values
        /// </summary>
        int[] GetOrdered();
    }
}
using System.Collections.Generic;
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
        /// Sort saved values.
        /// Should not interupt <see cref="ConsumeAsync"/>
        /// </summary>
        IEnumerable<int> GetOrdered();
    }
}
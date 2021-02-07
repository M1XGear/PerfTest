using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerfTest.Consumer
{
    /// <summary>
    /// Base
    /// </summary>
    public abstract class SortableConsumerBase : ISortableConsumer<int>
    {
        public abstract Task ConsumeAsync(int val);

        public abstract IEnumerable<int> GetOrdered();

        /// <summary>
        /// Used in benchmark result table
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return GetType().Name.Replace("SortableConsumer", "");
        }
    }
}
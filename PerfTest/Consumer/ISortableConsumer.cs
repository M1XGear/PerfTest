using System.Threading.Tasks;

namespace PerfTest.Consumer
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISortableConsumer<in T>
    {
        /// <summary>
        /// 
        /// </summary>
        Task ConsumeAsync(T val);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int[] GetOrdered();
    }
}
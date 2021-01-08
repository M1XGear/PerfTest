using System.Threading.Tasks;

namespace PerfTest.Consumer
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConsumer<in T>
    {
        /// <summary>
        /// 
        /// </summary>
        Task ConsumeAsync(T val);
    }
}
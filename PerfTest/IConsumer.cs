using System.Threading.Tasks;

namespace PerfTest
{
    interface IConsumer<T>
    {
        Task Counsume(T val);
    }
}
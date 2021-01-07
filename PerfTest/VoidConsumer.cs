using System.Threading.Tasks;

namespace PerfTest
{
    class VoidConsumer : IConsumer<int>
    {
        public Task Counsume(int val)
        {
            return Task.CompletedTask;
        }
    }
}
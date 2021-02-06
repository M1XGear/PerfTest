using System.Threading.Tasks;

namespace PerfTest.Consumer
{
    public abstract class SortableConsumerBase : ISortableConsumer<int>
    {
        public abstract Task ConsumeAsync(int val);

        public abstract int[] GetOrdered();

        public override string ToString()
        {
            return GetType().Name.Replace("SortableConsumer", "");
        }
    }
}
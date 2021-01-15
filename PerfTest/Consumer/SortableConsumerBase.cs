using System.Threading.Tasks;

namespace PerfTest.Consumer
{
    public abstract class SortableConsumerBase : ISortableConsumer<int>
    {
        public virtual Task ConsumeAsync(int val)
        {
            throw new System.NotImplementedException();
        }

        public virtual int[] GetOrdered()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return GetType().Name.Replace("SortableConsumer", "");
        }
    }
}
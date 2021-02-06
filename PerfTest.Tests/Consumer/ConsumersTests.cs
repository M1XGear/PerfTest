using System.Threading.Tasks;
using NUnit.Framework;
using PerfTest.Consumer;

namespace PerfTest.Tests.Consumer
{
    public class ConsumersTests
    {
        private static object[] _testCaseConsumeAsync3DifferentItems =
        {
            new object[] { new ConcurrentDictionarySortableConsumer() },
            new object[] { new DictionarySortableConsumer(1, 10) },
            new object[] { new DictionarySortableConsumerWithOverridenInt(1, 10) },
            new object[] { new LinkedListSortableConsumer() },
            new object[] { new ListSortableConsumer() },
            new object[] { new ArraySortableConsumerBase(1, 10) }
        };

        private static object[] _testCaseConsumeAsyncMultipleSimilarItems =
        {
            new object[] { new ConcurrentDictionarySortableConsumer() },
            new object[] { new DictionarySortableConsumer(-2, 5) },
            new object[] { new DictionarySortableConsumerWithOverridenInt(-2, 5) },
            new object[] { new LinkedListSortableConsumer() },
            new object[] { new ListSortableConsumer() },
            new object[] { new ArraySortableConsumerBase(-2, 5) }
        };

        [TestCaseSource(nameof(_testCaseConsumeAsync3DifferentItems))]
        public async Task ConsumeAsync_3DifferentItems(ISortableConsumer<int> consumer)
        {
            await consumer.ConsumeAsync(3);
            await consumer.ConsumeAsync(2);
            await consumer.ConsumeAsync(1);

            var ordered = consumer.GetOrdered();
            Assert.AreEqual(3, ordered.Length);
            for (var i = 1; i <= 3; i++)
            {
                Assert.AreEqual(i, ordered[i-1]);
            }
        }

        [TestCaseSource(nameof(_testCaseConsumeAsyncMultipleSimilarItems))]
        public async Task ConsumeAsync_MultipleSimilarItems(ISortableConsumer<int> consumer)
        {
            await consumer.ConsumeAsync(1);
            await consumer.ConsumeAsync(2);
            await consumer.ConsumeAsync(2);
            await consumer.ConsumeAsync(1);

            var ordered = consumer.GetOrdered();
            Assert.AreEqual(4, ordered.Length);
            Assert.AreEqual(1, ordered[0]);
            Assert.AreEqual(1, ordered[1]);
            Assert.AreEqual(2, ordered[2]);
            Assert.AreEqual(2, ordered[3]);
        }
    }
}
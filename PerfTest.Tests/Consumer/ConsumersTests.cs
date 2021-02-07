using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using PerfTest.Consumer;

namespace PerfTest.Tests.Consumer
{
    public class ConsumersTests
    {
        private static ISortableConsumer<int>[] TestCaseSource => new ISortableConsumer<int>[]
        {
            new ConcurrentDictionarySortableConsumer(),
            new DictionarySortableConsumer(-5, 10),
            new LinkedListSortableConsumer(),
            new ListSortableConsumer() ,
            new ArraySortableConsumer(-5, 10),
            new SortedListSortableConsumer(-5,10),
        };

        [TestCaseSource(nameof(TestCaseSource))]
        public async Task ConsumeAndSort_BaseTest(ISortableConsumer<int> consumer)
        {
            // Act
            await consumer.ConsumeAsync(3);
            await consumer.ConsumeAsync(2);
            await consumer.ConsumeAsync(1);

            var ordered = consumer.GetOrdered().ToArray();

            // Assert
            Assert.AreEqual(3, ordered.Length);
            for (var i = 0; i < 3; i++)
            {
                Assert.AreEqual(i+1, ordered[i]);
            }
        }

        [TestCaseSource(nameof(TestCaseSource))]
        public async Task ConsumeAndSort_SimilarItems(ISortableConsumer<int> consumer)
        {
            // Act
            await consumer.ConsumeAsync(1);
            await consumer.ConsumeAsync(2);
            await consumer.ConsumeAsync(2);
            await consumer.ConsumeAsync(1);

            var ordered = consumer.GetOrdered().ToArray();

            // Assert
            Assert.AreEqual(4, ordered.Length);
            Assert.AreEqual(1, ordered[0]);
            Assert.AreEqual(1, ordered[1]);
            Assert.AreEqual(2, ordered[2]);
            Assert.AreEqual(2, ordered[3]);
        }

        [TestCaseSource(nameof(TestCaseSource))]
        public async Task ConsumeAndSort_MultipleTimes(ISortableConsumer<int> consumer)
        {
            // Act
            await consumer.ConsumeAsync(3);
            await consumer.ConsumeAsync(2);
            await consumer.ConsumeAsync(1);
            var ordered = consumer.GetOrdered().ToArray();

            await consumer.ConsumeAsync(4);
            await consumer.ConsumeAsync(5);
            await consumer.ConsumeAsync(6);
            var ordered1 = consumer.GetOrdered().ToArray();

            // Assert
            Assert.AreEqual(3, ordered.Length);
            for (var i = 0; i < 3; i++)
            {
                Assert.AreEqual(i+1, ordered[i]);
            }
            Assert.AreEqual(6, ordered1.Length);
            for (var i = 0; i < 6; i++)
            {
                Assert.AreEqual(i+1, ordered1[i]);
            }
        }
    }
}
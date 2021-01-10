using System.Threading.Tasks;
using NUnit.Framework;
using PerfTest.Consumer;

namespace PerfTest.Tests.Consumer
{
    public class LinkedListSortableConsumerTests
    {
        private LinkedListSortableConsumer _consumer;

        [SetUp]
        public void SetUp()
        {
            _consumer = new LinkedListSortableConsumer();
        }

        [Test]
        public async Task ConsumeAsync_3DifferentItems()
        {
            await _consumer.ConsumeAsync(3);
            await _consumer.ConsumeAsync(2);
            await _consumer.ConsumeAsync(1);

            var ordered = _consumer.GetOrdered();
            Assert.AreEqual(3, ordered.Length);
            for (var i = 1; i <= 3; i++)
            {
                Assert.AreEqual(i, ordered[i-1]);
            }
        }

        [Test]
        public async Task ConsumeAsync_MultipleSimilarItems()
        {
            await _consumer.ConsumeAsync(1);
            await _consumer.ConsumeAsync(2);
            await _consumer.ConsumeAsync(2);
            await _consumer.ConsumeAsync(1);

            var ordered = _consumer.GetOrdered();
            Assert.AreEqual(4, ordered.Length);
            Assert.AreEqual(1, ordered[0]);
            Assert.AreEqual(1, ordered[1]);
            Assert.AreEqual(2, ordered[2]);
            Assert.AreEqual(2, ordered[3]);
        }
    }
}
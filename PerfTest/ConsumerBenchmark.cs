using BenchmarkDotNet.Attributes;
using System;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace PerfTest
{
    [MemoryDiagnoser]
    public class ConsumerBenchmark
    {
        private Channel<int> _channel;

        [Benchmark]
        [Arguments(10000)]
        [Arguments(20000)]
        [Arguments(30000)]
        [Arguments(40000)]
        [Arguments(50000)]
        public Task VoidConsumer(int inputSize)
        {
            IConsumer<int> consumer = new VoidConsumer();
            var writer = Task.Run(() => WriteDataToChannel(_channel.Writer, inputSize));
            var reader = Task.Run(() => ReadDataFromChannel(_channel.Reader, consumer));

            return Task.WhenAll(writer, reader);
        }

        [IterationSetup]
        public void IterationSetup()
        {
            _channel = Channel.CreateUnbounded<int>(new UnboundedChannelOptions() { SingleReader = true, SingleWriter = true });
        }

        [IterationCleanup]
        public void IterationCleanup()
        {
            _channel = null;
            GC.Collect();
        }

        private async Task ReadDataFromChannel(ChannelReader<int> channelReader, IConsumer<int> consumer)
        {
            while (await channelReader.WaitToReadAsync())
            {
                var val = await channelReader.ReadAsync();
                await consumer.Counsume(val);
            }
        }

        private async Task WriteDataToChannel(ChannelWriter<int> channelWriter, int count)
        {
            var rnd = new Random(777);
            for (var i = 0; i < count; i++)
            {
                var num = rnd.Next(0, 1000);
                await channelWriter.WriteAsync(num);
            }

            channelWriter.Complete();
        }
    }
}

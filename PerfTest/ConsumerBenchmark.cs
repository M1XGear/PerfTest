using System;
using System.Collections.Generic;
using System.Threading.Channels;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using PerfTest.Consumer;

namespace PerfTest
{
    /// <summary>
    /// Benchmark class
    /// </summary>
    [MemoryDiagnoser]
    public class ConsumerBenchmark
    {
        /// <summary>
        /// 
        /// </summary>
        private const int MaxInputSize = 100000;

        /// <summary>
        /// 
        /// </summary>
        private const int MinInputValue = 0;
        /// <summary>
        /// 
        /// </summary>
        private const int MaxInputValue = 100000-1;

        /// <summary>
        /// Channel for data transportation
        /// </summary>
        private Channel<int> _channel;

        private List<int> _input;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputSize">Input size</param>
        /// <returns></returns>
        [Benchmark]
        [Arguments(50000)]
        [Arguments(100000)]
        public Task VoidConsumer(int inputSize)
        {
            IConsumer<int> consumer = new VoidConsumer();
            var writer = Task.Run(() => WriteDataToChannelAsync(_channel.Writer, inputSize));
            var reader = Task.Run(() => ReadDataFromChannelAsync(_channel.Reader, consumer));

            return Task.WhenAll(writer, reader);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputSize">Input size</param>
        /// <returns></returns>
        [Benchmark]
        [Arguments(50000)]
        [Arguments(100000)]
        public Task DictionaryConsumer(int inputSize)
        {
            IConsumer<int> consumer = new DictionaryConsumer(MinInputValue, MaxInputValue);
            var writer = Task.Run(() => WriteDataToChannelAsync(_channel.Writer, inputSize));
            var reader = Task.Run(() => ReadDataFromChannelAsync(_channel.Reader, consumer));

            return Task.WhenAll(writer, reader);
        }

        [GlobalSetup]
        public void GlobalSetup()
        {
            _input = GenerateValues();
        }

        /// <summary>
        /// 
        /// </summary>
        [IterationSetup]
        public void IterationSetup()
        {
            _channel = Channel.CreateUnbounded<int>(new UnboundedChannelOptions
            {
                SingleReader = true, SingleWriter = true
            });
        }

        /// <summary>
        /// 
        /// </summary>
        [IterationCleanup]
        public void IterationCleanup()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [GlobalCleanup]
        public void GlobalCleanup()
        {
            _input = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="channelReader"></param>
        /// <param name="consumer"></param>
        /// <returns></returns>
        private async Task ReadDataFromChannelAsync(ChannelReader<int> channelReader, IConsumer<int> consumer)
        {
            while (await channelReader.WaitToReadAsync().ConfigureAwait(false))
            {
                var val = await channelReader.ReadAsync().ConfigureAwait(false);
                await consumer.ConsumeAsync(val).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="channelWriter"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private async Task WriteDataToChannelAsync(ChannelWriter<int> channelWriter, int count)
        {
            for (var i = 0; i < count; i++)
            {
                var num = _input[i];
                await channelWriter.WriteAsync(num).ConfigureAwait(false);
            }

            channelWriter.Complete();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<int> GenerateValues()
        {
            var values = new List<int>(MaxInputSize);
            var rnd = new Random();
            for (var i = 0; i < MaxInputSize; i++)
            {
                var num = rnd.Next(MinInputValue, MaxInputValue);
                values.Add(num);
            }

            return values;
        }
    }
}
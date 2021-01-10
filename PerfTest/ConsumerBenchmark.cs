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
        private const int MaxInputSize = 1000000;

        /// <summary>
        /// 
        /// </summary>
        private const int MinInputValue = 0;
        /// <summary>
        /// 
        /// </summary>
        private const int MaxInputValue = 1000000-1;

        /// <summary>
        /// Channel for data transportation
        /// </summary>
        private Channel<int> _channel;

        private List<int> _input;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputSize">Input size</param>
        /// /// <param name="sortAfter"></param>
        /// <returns></returns>
        [Benchmark]
        [Arguments(100000, false)]
        [Arguments(500000, false)]
        [Arguments(1000000, false)]
        [Arguments(100000, true)]
        [Arguments(500000, true)]
        [Arguments(1000000, true)]
        public async Task VoidSortableConsumer(int inputSize, bool sortAfter)
        {
            ISortableConsumer<int> sortableConsumer = new VoidSortableConsumer();
            var writer = Task.Run(() => WriteDataToChannelAsync(_channel.Writer, inputSize));
            var reader = Task.Run(() => ReadDataFromChannelAsync(_channel.Reader, sortableConsumer));

            await Task.WhenAll(writer, reader).ConfigureAwait(false);
            if (sortAfter)
            {
                var ordered = sortableConsumer.GetOrdered();
                Console.WriteLine(ordered.Length);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputSize">Input size</param>
        /// /// <param name="sortAfter"></param>
        /// <returns></returns>
        [Benchmark]
        [Arguments(100000, false)]
        [Arguments(500000, false)]
        [Arguments(1000000, false)]
        [Arguments(100000, true)]
        [Arguments(500000, true)]
        [Arguments(1000000, true)]
        public async Task DictionarySortableConsumer(int inputSize, bool sortAfter)
        {
            ISortableConsumer<int> sortableConsumer = new DictionarySortableConsumer(MinInputValue, MaxInputValue);
            var writer = Task.Run(() => WriteDataToChannelAsync(_channel.Writer, inputSize));
            var reader = Task.Run(() => ReadDataFromChannelAsync(_channel.Reader, sortableConsumer));

            await Task.WhenAll(writer, reader).ConfigureAwait(false);
            if (sortAfter)
            {
                var ordered = sortableConsumer.GetOrdered();
                Console.WriteLine(ordered.Length);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputSize">Input size</param>
        /// /// <param name="sortAfter"></param>
        /// <returns></returns>
        [Benchmark]
        [Arguments(100000, false)]
        [Arguments(500000, false)]
        [Arguments(1000000, false)]
        [Arguments(100000, true)]
        [Arguments(500000, true)]
        [Arguments(1000000, true)]
        public async Task ConcurrentDictionarySortableConsumer(int inputSize, bool sortAfter)
        {
            ISortableConsumer<int> sortableConsumer = new ConcurrentDictionarySortableConsumer();
            var writer = Task.Run(() => WriteDataToChannelAsync(_channel.Writer, inputSize));
            var reader = Task.Run(() => ReadDataFromChannelAsync(_channel.Reader, sortableConsumer));

            await Task.WhenAll(writer, reader).ConfigureAwait(false);
            if (sortAfter)
            {
                var ordered = sortableConsumer.GetOrdered();
                Console.WriteLine(ordered.Length);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputSize">Input size</param>
        /// /// <param name="sortAfter"></param>
        /// <returns></returns>
        [Benchmark]
        [Arguments(100000, false)]
        [Arguments(500000, false)]
        [Arguments(1000000, false)]
        [Arguments(100000, true)]
        [Arguments(500000, true)]
        [Arguments(1000000, true)]
        public async Task SortedListSortableConsumer(int inputSize, bool sortAfter)
        {
            ISortableConsumer<int> sortableConsumer = new SortedListSortableConsumer(MinInputValue, MaxInputValue);
            var writer = Task.Run(() => WriteDataToChannelAsync(_channel.Writer, inputSize));
            var reader = Task.Run(() => ReadDataFromChannelAsync(_channel.Reader, sortableConsumer));

            await Task.WhenAll(writer, reader).ConfigureAwait(false);
            if (sortAfter)
            {
                var ordered = sortableConsumer.GetOrdered();
                Console.WriteLine(ordered.Length);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputSize">Input size</param>
        /// <param name="sortAfter"></param>
        /// <returns></returns>
        /// ToDo: Optimize
        // [Benchmark]
        // [Arguments(100000, false)]
        // [Arguments(500000, false)]
        // [Arguments(1000000, false)]
        // [Arguments(100000, true)]
        // [Arguments(500000, true)]
        // [Arguments(1000000, true)]
        public async Task LinkedListSortableConsumer(int inputSize, bool sortAfter)
        {
            ISortableConsumer<int> sortableConsumer = new LinkedListSortableConsumer();
            var writer = Task.Run(() => WriteDataToChannelAsync(_channel.Writer, inputSize));
            var reader = Task.Run(() => ReadDataFromChannelAsync(_channel.Reader, sortableConsumer));

            await Task.WhenAll(writer, reader).ConfigureAwait(false);
            if (sortAfter)
            {
                var ordered = sortableConsumer.GetOrdered();
                Console.WriteLine(ordered.Length);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputSize">Input size</param>
        /// <param name="sortAfter"></param>
        /// <returns></returns>
        [Benchmark]
        [Arguments(100000, false)]
        [Arguments(500000, false)]
        [Arguments(1000000, false)]
        [Arguments(100000, true)]
        [Arguments(500000, true)]
        [Arguments(1000000, true)]
        public async Task ListSortableConsumer(int inputSize, bool sortAfter)
        {
            ISortableConsumer<int> sortableConsumer = new ListSortableConsumer();
            var writer = Task.Run(() => WriteDataToChannelAsync(_channel.Writer, inputSize));
            var reader = Task.Run(() => ReadDataFromChannelAsync(_channel.Reader, sortableConsumer));

            await Task.WhenAll(writer, reader).ConfigureAwait(false);
            if (sortAfter)
            {
                var ordered = sortableConsumer.GetOrdered();
                Console.WriteLine(ordered.Length);
            }
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
        /// <param name="sortableConsumer"></param>
        /// <returns></returns>
        private async Task ReadDataFromChannelAsync(ChannelReader<int> channelReader, ISortableConsumer<int> sortableConsumer)
        {
            while (await channelReader.WaitToReadAsync().ConfigureAwait(false))
            {
                var val = await channelReader.ReadAsync().ConfigureAwait(false);
                await sortableConsumer.ConsumeAsync(val).ConfigureAwait(false);
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
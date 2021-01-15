using System;
using System.Collections.Generic;
using System.Threading.Channels;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using JetBrains.Annotations;
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
        /// MaxNumber of elements in input
        /// Used to generate test data and limit benchmark params
        /// </summary>
        private const int MaxInputSize = 2000000;

        /// <summary>
        /// Minimum value in input
        /// </summary>
        private const int MinInputValue = 0;
        /// <summary>
        /// Maximum value in input
        /// </summary>
        private const int MaxInputValue = 2000000-1;

        /// <summary>
        /// Channel for data transportation
        /// </summary>
        private Channel<int> _channel;

        /// <summary>
        /// Input numners, simmilar for each consumer
        /// </summary>
        private List<int> _input;

        #region BenchmarkParams

        /// <summary>
        /// Use sort in consumer in benchmark run
        /// </summary>
        [ParamsSource(nameof(ValuesForUseSort))]
        [UsedImplicitly]
        public bool UseSort { get; set; }

        /// <summary>
        /// Number of elements in benchmark run
        /// </summary>
        [ParamsSource(nameof(ValuesForInputSize))]
        [UsedImplicitly]
        public int InputSize { get; set; }

        /// <summary>
        /// Number of elements in benchmark run
        /// </summary>
        [ParamsSource(nameof(ValuesForConsumer))]
        [UsedImplicitly]
        public ISortableConsumer<int> Consumer { get; set; }

        /// <summary>
        /// Possible values for param <see cref="UseSort"/>
        /// </summary>
        public IEnumerable<bool> ValuesForUseSort => new[] {true, false};

        /// <summary>
        /// Possible values for param <see cref="InputSize"/>
        /// </summary>
        public IEnumerable<int> ValuesForInputSize => new[] {100000, }; //500000, 1000000, MaxInputSize

        /// <summary>
        /// Possible values for param <see cref="Consumer"/>
        /// </summary>
        public IEnumerable<ISortableConsumer<int>> ValuesForConsumer => new ISortableConsumer<int>[]
        {
            new VoidSortableConsumer(),
            new DictionarySortableConsumer(MinInputValue, MaxInputValue),
            new ConcurrentDictionarySortableConsumer(),
            new SortedListSortableConsumer(MinInputValue, MaxInputValue),
            // ToDo new LinkedListSortableConsumer(),
            // ToDo new ListSortableConsumer() Long sort in big numbers
        };

        #endregion

        /// <summary>
        /// Benchmark
        /// </summary>
        [Benchmark]
        public async Task Benchmark()
        {
            var writer = Task.Run(() => WriteDataToChannelAsync(_channel.Writer, InputSize));
            var reader = Task.Run(() => ReadDataFromChannelAsync(_channel.Reader, Consumer));

            await Task.WhenAll(writer, reader).ConfigureAwait(false);
            if (UseSort)
            {
                var ordered = Consumer.GetOrdered();
                Console.WriteLine(ordered.Length);
            }
        }

        [GlobalSetup]
        public void GlobalSetup()
        {
            _input = GenerateValues();
        }

        [IterationSetup]
        public void IterationSetup()
        {
            _channel = Channel.CreateUnbounded<int>(new UnboundedChannelOptions
            {
                SingleReader = true, SingleWriter = true
            });
        }

        /// <summary>
        /// Reads data from channel and sends it to Consumer
        /// </summary>
        private async Task ReadDataFromChannelAsync(ChannelReader<int> channelReader, ISortableConsumer<int> sortableConsumer)
        {
            while (await channelReader.WaitToReadAsync().ConfigureAwait(false))
            {
                var val = await channelReader.ReadAsync().ConfigureAwait(false);
                await sortableConsumer.ConsumeAsync(val).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Write data to channel
        /// </summary>
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
        /// Generate input values for benchmark
        /// </summary>
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
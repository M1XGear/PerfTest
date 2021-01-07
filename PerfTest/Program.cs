using System;
using System.Threading.Tasks;
using BenchmarkDotNet.Running;

namespace PerfTest
{
    public class Program
    {
        static async Task Main()
        {
            Console.WriteLine("Start");

            var summary = BenchmarkRunner.Run<ConsumerBenchmark>();

            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}
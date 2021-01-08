using System;
using BenchmarkDotNet.Running;

namespace PerfTest
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Start");

            BenchmarkRunner.Run<ConsumerBenchmark>();

            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}
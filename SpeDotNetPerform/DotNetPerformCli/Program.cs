using DotNetPerformBenchmarks.StringBuilder;
using System;

namespace DotNetPerformCli {
    class Program {
        static void Main(string[] args) {
            var benchmark = new StringBuilder_Performance() {
                Concats = 20,
                Capacity = 201,
                Iters = 100000000
            };
            benchmark.WarmupPool();

            Console.WriteLine("Read to start performing string concatenations.");
            Console.ReadKey();

            benchmark.BenchmarkPooled();

            Console.WriteLine("Finished.");
        }
    }
}

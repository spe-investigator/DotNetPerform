using BenchmarkDotNet.Running;

namespace DotNetPerformBenchmarks {
    class Program {
        static void Main(string[] args) {
            var summary = BenchmarkRunner.Run(typeof(Program).Assembly);
        }
    }
}

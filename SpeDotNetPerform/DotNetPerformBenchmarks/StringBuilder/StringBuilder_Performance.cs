using BenchmarkDotNet.Attributes;
using Text = System.Text;

namespace DotNetPerformBenchmarks.StringBuilder {
    [RPlotExporter]
    public class StringBuilder_Performance {
        public const string stringAllocation = "0123456789";

        [Params(5, 10, 50)]
        public int ConcatenationIterations { get; set; }

        [Params(0, 10, 50)]
        public int Capacity { get; set; }

        [Params(10000, 50000, 100000)]
        public int BenchmarkIterations { get; set; }

        //[Params(10)]
        public int PoolSize { get; set; } = 10;

        [Benchmark]
        public void BenchmarkPooled() {
            using (var test = new SpeDotNetPerform.Performance.PerformanceBaseTest<Text.StringBuilder>()) {
                for (var i = 0; i < BenchmarkIterations; i++) {
                    var stringBuilder = new Text.Perf.StringBuilder(Capacity, PoolSize);
                    for (var x = 0; x < ConcatenationIterations; x++) {
                        stringBuilder.Append(stringAllocation);
                    }
                }
            }
        }

        [Benchmark(Baseline = true)]
        public void BenchmarkNonPooled() {
            for (var i = 0; i < BenchmarkIterations; i++) {
                var stringBuilder = new Text.StringBuilder(Capacity);
                for (var x = 0; x < ConcatenationIterations; x++) {
                    stringBuilder.Append(stringAllocation);
                }
            }
        }
    }
}

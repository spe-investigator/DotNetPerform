using BenchmarkDotNet.Attributes;
using Text = System.Text;
using Perf = System.Performance;
using BenchmarkDotNet.Jobs;

namespace DotNetPerformBenchmarks.StringBuilder {
    [GcForce(false)]
    [GcServer(true)]
    [GcConcurrent(false)]
    [RPlotExporter]
    [SimpleJob(RuntimeMoniker.Net472, baseline: true)]
    [SimpleJob(RuntimeMoniker.NetCoreApp30)]
    public class StringBuilder_Performance {
        public char[] stringAllocation = "0123456789".ToCharArray();

        [Params(10, 50)]
        public int Concats { get; set; }

        [Params(0, 10, 50)]
        public int Capacity { get; set; }

        [Params(50000, 100000)]
        public int Iters { get; set; }

        //[Params(10)]
        public int PoolSize { get; set; } = 10;

        public void WarmupPool() {
            // Creating first object will warm pool up.
            using (var test = new Perf.PoolBoy<Text.StringBuilder>()) {
                using (var stringBuilder = new Perf.Text.StringBuilder(Capacity, poolSize: PoolSize)) {
                    // Access the wrapper object in order to create object pool.
                    var wrapper = stringBuilder.wrapperObject;
                }
            }
        }

        //[Benchmark]
        public void BenchmarkPooled() {
            using (var test = new Perf.PoolBoy<Text.StringBuilder>()) {
                for (var i = 0; i < Iters; i++) {
                    using (var stringBuilder = new Perf.Text.StringBuilder(Capacity, poolSize: PoolSize)) {
                        for (var x = 0; x < Concats; x++) {
                            stringBuilder.Append(stringAllocation);
                        }
                    }
                }
            }
        }

        //[Benchmark(Baseline = true)]
        public void BenchmarkNonPooled() {
            for (var i = 0; i < Iters; i++) {
                var stringBuilder = new Text.StringBuilder(Capacity);
                for (var x = 0; x < Concats; x++) {
                    stringBuilder.Append(stringAllocation);
                }
            }
        }
    }
}

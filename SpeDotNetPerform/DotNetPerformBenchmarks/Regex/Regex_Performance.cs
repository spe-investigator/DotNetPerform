using BenchmarkDotNet.Attributes;
using Text = System.Text;
using Perf = System.Performance;
using BenchmarkDotNet.Jobs;

namespace DotNetPerformBenchmarks.StringBuilder {
    //[GcForce(false)]
    //[GcServer(true)]
    //[GcConcurrent(false)]
    //[RPlotExporter]
    //[SimpleJob(RuntimeMoniker.Net472, baseline: true)]
    //[SimpleJob(RuntimeMoniker.NetCoreApp30)]
    public class Regex_Performance {
        public string stringAllocation = "0123456789";

        [Params(10, 50)]
        public int Concats { get; set; }

        [Params(10000, 50000)]
        public int Iters { get; set; }

        //[Params(10)]
        public int PoolSize { get; set; } = 10;

        public void WarmupPool() {
            // Creating first object will warm pool up.
            using (var test = new Perf.PoolBoy<Text.RegularExpressions.Regex>()) {
                using (var regex = new Perf.Text.RegularExpressions.Regex("[0-9]+", poolSize: PoolSize)) {
                    // Access the wrapper object in order to create object pool.
                    var wrapper = regex.wrapperObject;
                }
            }
        }

        //[Benchmark]
        public void BenchmarkPooled() {
            using (var test = new Perf.PoolBoy<Text.StringBuilder>()) {
                for (var i = 0; i < Iters; i++) {
                    using (var regex = new Perf.Text.RegularExpressions.Regex("[0-9]+", poolSize: PoolSize)) {
                        for (var x = 0; x < Concats; x++) {
                            regex.IsMatch(stringAllocation);
                        }
                    }
                }
            }
        }

        //[Benchmark(Baseline = true)]
        public void BenchmarkNonPooled() {
            for (var i = 0; i < Iters; i++) {
                var regex = new Text.RegularExpressions.Regex("[0-9]+");
                for (var x = 0; x < Concats; x++) {
                    regex.IsMatch(stringAllocation);
                }
            }
        }
    }
}

using Text = System.Text;
using Xunit;
using FluentAssertions;
using System.Linq;
using Perf = System.Performance;

namespace DotNetPerformTests.Collections.Generic {
    [Collection("Sequential")]
    public class List_Positive {
        const bool DONOTCLEAR = true;

        [Theory]
        [InlineData(null, 100)] // No cap, no max
        [InlineData(101, 100)] // Cap, no max
        public void ByCharacteristic_PoolKey_ShouldBeSame(int? capacity, int appends) {
            var contents = "0123456789";
            using (var test = new Perf.PoolBoy<System.Collections.Generic.List<string>>()) {
                var list = capacity.HasValue ? new Perf.Collection.Generic.List<string>(capacity.Value) : new Perf.Collection.Generic.List<string>(0);
                for (var i = 0; i < appends; i++) {
                    list.Add(contents);
                }
                list.Dispose(DONOTCLEAR);
                test.ResetItemCounter();

                var list2 = capacity.HasValue ? new Perf.Collection.Generic.List<string>(capacity.Value) : new Perf.Collection.Generic.List<string>();

                list2.performanceObject.Should().BeSameAs(list.performanceObject);
                list2.ToString().Should().Be(list.ToString());
            }
        }

        [Theory]
        [InlineData(null, 100, 4)]
        [InlineData(null, 100, 20)]
        [InlineData(101, 100, 4)]
        [InlineData(101, 100, 20)]
        public void Allocate_NonPooled(int? capacity, int appends, int poolSize) {
            using (var test = new Perf.PoolBoy<System.Collections.Generic.List<string>>()) {
                Perf.Collection.Generic.List<string>? priorList = null;
                var created = 0;
                var contents = "0123456789";

                do {
                    var list = capacity.HasValue ? new Perf.Collection.Generic.List<string>(capacity.Value, poolSize: poolSize) : new Perf.Collection.Generic.List<string>(poolSize: poolSize);

                    for (var i = 0; i < appends; i++) {
                        list.Add(contents);
                    }
                    list.wrapperObject.IsPoolAllocated.Should().BeTrue();

                    if (priorList.HasValue) {
                        list.performanceObject.Should().NotBeSameAs(priorList.Value.performanceObject);
                    }

                    priorList = list;
                } while (++created < poolSize);

                var afterPoolSaturated = capacity.HasValue ? new Perf.Collection.Generic.List<string>(capacity.Value, poolSize: poolSize) : new Perf.Collection.Generic.List<string>(poolSize: poolSize);
                afterPoolSaturated.wrapperObject.IsPoolAllocated.Should().BeFalse();

                var afterPoolSaturatedAgain = capacity.HasValue ? new Perf.Collection.Generic.List<string>(capacity.Value, poolSize: poolSize) : new Perf.Collection.Generic.List<string>(poolSize: poolSize);
                afterPoolSaturatedAgain.wrapperObject.IsPoolAllocated.Should().BeFalse();
            }
        }
    }
}

using System;
using Txt = System.Text;
using Xunit;
using FluentAssertions;
using Perf = System.Performance;

namespace DotNetPerformTests.StringBuilder {
    [Collection("Sequential")]
    public class List_Negative {
        [Theory]
        [InlineData(20)]
        [InlineData(100)]
        public void PoolSizeExceeded(int poolSize) {
            using (var test = new Perf.PoolBoy<Txt.StringBuilder>()) {
                Perf.Text.StringBuilder? priorStringBuilder = null;
                var created = 0;

                do {
                    var stringBuilder = new Perf.Text.StringBuilder(poolSize: poolSize);
                    stringBuilder.Append("I want to test this out.");
                    stringBuilder.IsPoolAllocated.Should().BeTrue();

                    if (priorStringBuilder != null) {
                        stringBuilder.performanceObject.Should().NotBeSameAs(priorStringBuilder.Value.performanceObject);
                    }

                    created++;
                    priorStringBuilder = stringBuilder;
                } while (created < poolSize);

                var afterPoolSaturated = new Perf.Text.StringBuilder(poolSize: poolSize);
                afterPoolSaturated.IsPoolAllocated.Should().BeFalse();
            }
        }

        [Fact]
        public void ObjectCapacityExceeded_RemovedFromPool() {
            int poolSize = 10;
            
            using (var test = new Perf.PoolBoy<Txt.StringBuilder>()) {
                var stringBuilder = new Perf.Text.StringBuilder(poolSize: poolSize);
                stringBuilder.Append("I want to test this out.");
                stringBuilder.IsPoolAllocated.Should().BeTrue();

                stringBuilder.Dispose();
            }
        }
    }
}

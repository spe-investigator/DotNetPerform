using System;
using Text = System.Text;
using Xunit;
using FluentAssertions;

namespace DotNetPerformTests.StringBuilder {
    [Collection("Sequential")]
    public class StringBuilder_Negative {
        [Theory]
        [InlineData(20)]
        [InlineData(100)]
        public void PoolSizeExceeded(int poolSize) {
            using (var test = new SpeDotNetPerform.Performance.PerformanceBaseTest<Text.StringBuilder>()) {
                Text.Perf.StringBuilder priorStringBuilder = null;
                var created = 0;

                do {
                    var stringBuilder = new Text.Perf.StringBuilder(poolSize: poolSize);
                    stringBuilder.Append("I want to test this out.");
                    stringBuilder.IsPoolAllocated.Should().BeTrue();

                    if (priorStringBuilder != null) {
                        stringBuilder._performanceObject.Should().NotBeSameAs(priorStringBuilder._performanceObject);
                    }

                    created++;
                    priorStringBuilder = stringBuilder;
                } while (created < poolSize);

                var afterPoolSaturated = new Text.Perf.StringBuilder(poolSize: poolSize);
                afterPoolSaturated.IsPoolAllocated.Should().BeFalse();
            }
        }

        [Fact]
        public void ObjectCapacityExceeded_RemovedFromPool() {
            int poolSize = 10;
            
            using (var test = new SpeDotNetPerform.Performance.PerformanceBaseTest<Text.StringBuilder>()) {
                var stringBuilder = new Text.Perf.StringBuilder(poolSize: poolSize);
                stringBuilder.Append("I want to test this out.");
                stringBuilder.IsPoolAllocated.Should().BeTrue();

                stringBuilder.Dispose();

            }
        }
    }
}

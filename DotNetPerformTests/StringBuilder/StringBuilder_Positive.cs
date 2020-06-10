using System;
using Text = System.Text;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;

namespace DotNetPerformTests.StringBuilder {
    [Collection("Sequential")]
    public class StringBuilder_Positive {
        const bool DONOTCLEAR = true;

        [Theory]
        [InlineData(null)]
        [InlineData("Test1")]
        public void ByCharacteristic_PoolKey_ShouldBeSame(string poolKey) {
            using (var test = new SpeDotNetPerform.Performance.PerformanceBaseTest<Text.StringBuilder>()) {
                var stringBuilder = new Text.Perf.StringBuilder(poolKey);
                stringBuilder.Append("I want to test this out.");
                stringBuilder.Dispose(DONOTCLEAR);

                var stringBuilder2 = new Text.Perf.StringBuilder(poolKey);

                stringBuilder2._performanceObject.Should().BeSameAs(stringBuilder._performanceObject);
                stringBuilder2.ToString().Should().Be(stringBuilder.ToString());
            }
        }

        [Theory]
        [InlineData(4)]
        [InlineData(20)]
        public void Allocate_NonPooled(int poolSize) {
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

                var afterPoolSaturatedAgain = new Text.Perf.StringBuilder(poolSize: poolSize);
                afterPoolSaturatedAgain.IsPoolAllocated.Should().BeFalse();
            }
        }
    }
}

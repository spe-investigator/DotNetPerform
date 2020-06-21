using System;
using Text = System.Text;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;

namespace DotNetPerformTests.StringBuilder {
    [Collection("Sequential")]
    public class StringBuilder_Positive {
        const bool DONOTCLEAR = true;

        [Theory]
        [InlineData(null, null, null)] // No cap, no max
        [InlineData(null, null, "Test1")]
        [InlineData(10, null, null)] // Cap, no max
        [InlineData(10, null, "Test1")]
        [InlineData(10, 1000, null)] // Cap with max
        [InlineData(10, 1000, "Test1")]
        public void ByCharacteristic_PoolKey_ShouldBeSame(int? capacity, int? maxCapacity, string poolKey) {
            using (var test = new SpeDotNetPerform.Performance.PoolBoy<Text.StringBuilder>()) {
                var stringBuilder = new Text.Perf.StringBuilder(capacity, maxCapacity, poolKey);
                stringBuilder.Append("I want to test this out.");
                stringBuilder.Dispose(DONOTCLEAR);
                test.ResetItemCounter();

                var stringBuilder2 = new Text.Perf.StringBuilder(capacity, maxCapacity, poolKey);

                stringBuilder2.performanceObject.Should().BeSameAs(stringBuilder.performanceObject);
                stringBuilder2.ToString().Should().Be(stringBuilder.ToString());
            }
        }

        [Theory]
        [InlineData(null, null, 4)]
        [InlineData(null, null, 20)]
        [InlineData(10, null, 4)]
        [InlineData(10, null, 20)]
        //[InlineData(10, 1000, 4)]
        //[InlineData(10, 1000, 20)]
        public void Allocate_NonPooled(int? capacity, int? maxCapacity, int poolSize) {
            using (var test = new SpeDotNetPerform.Performance.PoolBoy<Text.StringBuilder>()) {
                Text.Perf.StringBuilder priorStringBuilder = null;
                var created = 0;
                var contents = string.Join(',', Enumerable.Repeat("I want to test this out", 100));

                do {
                    var stringBuilder = new Text.Perf.StringBuilder(capacity, maxCapacity, poolSize: poolSize);
                    
                    stringBuilder.Append(contents);
                    stringBuilder.IsPoolAllocated.Should().BeTrue();

                    if (priorStringBuilder != null) {
                        stringBuilder.performanceObject.Should().NotBeSameAs(priorStringBuilder.performanceObject);
                    }

                    created++;
                    priorStringBuilder = stringBuilder;
                } while (created < poolSize);

                var afterPoolSaturated = new Text.Perf.StringBuilder(capacity, maxCapacity, poolSize: poolSize);
                afterPoolSaturated.IsPoolAllocated.Should().BeFalse();

                var afterPoolSaturatedAgain = new Text.Perf.StringBuilder(capacity, maxCapacity, poolSize: poolSize);
                afterPoolSaturatedAgain.IsPoolAllocated.Should().BeFalse();
            }
        }
    }
}

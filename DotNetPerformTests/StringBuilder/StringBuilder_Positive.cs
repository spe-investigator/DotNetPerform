using System;
using Text = System.Text;
using Xunit;
using FluentAssertions;

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

                stringBuilder2.Dispose();
            }
        }

        [Fact]
        public void Allocate_NonPooled() {
            using (var test = new SpeDotNetPerform.Performance.PerformanceBaseTest<Text.StringBuilder>()) {
                var stringBuilder = new Text.Perf.StringBuilder(poolSize: 4);
                stringBuilder.Append("I want to test this out.");
                stringBuilder.IsPoolAllocated.Should().BeTrue();

                var stringBuilder2 = new Text.Perf.StringBuilder(poolSize: 4);
                stringBuilder2.IsPoolAllocated.Should().BeTrue();

                var stringBuilder3 = new Text.Perf.StringBuilder(poolSize: 4);
                stringBuilder3.IsPoolAllocated.Should().BeTrue();

                var stringBuilder4 = new Text.Perf.StringBuilder(poolSize: 4);
                stringBuilder4.IsPoolAllocated.Should().BeTrue();

                var stringBuilder5 = new Text.Perf.StringBuilder(poolSize: 4);

                stringBuilder5.IsPoolAllocated.Should().BeFalse();

                stringBuilder.Dispose();
                stringBuilder2.Dispose();
                stringBuilder3.Dispose();
                stringBuilder4.Dispose();
                stringBuilder5.Dispose();
            }
        }
    }
}

using System;
using Text = System.Text;
using Xunit;
using FluentAssertions;

namespace DotNetPerformTests {
    public class StringBuilder_Positive {
        const bool DONOTCLEAR = true;

        [Theory]
        [InlineData(null)]
        [InlineData("Test1")]
        public void ByCharacteristic_PoolKey_ShouldBeSame(string poolKey) {
            var stringBuilder = new Text.Perf.StringBuilder(poolKey);
            stringBuilder.Append("I want to test this out.");
            stringBuilder.Dispose(DONOTCLEAR);
            
            var stringBuilder2 = new Text.Perf.StringBuilder(poolKey);

            stringBuilder2._performanceObject.Should().BeSameAs(stringBuilder._performanceObject);
            stringBuilder2.ToString().Should().Be(stringBuilder.ToString());
        }

        [Fact]
        public void Allocate_NonPooled() {
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
        }
    }
}

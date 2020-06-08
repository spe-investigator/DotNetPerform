using System;
using Text = System.Text;
using Xunit;
using FluentAssertions;

namespace DotNetPerformTests {
    public class StringBuilder_Positive {
        const bool DONOTCLEAR = true;

        [Theory]
        [InlineData(PerformanceCharacteristic.ThreadStatic)]
        [InlineData(PerformanceCharacteristic.Pooled)]
        public void ByCharacteristic_ShouldBeSame(PerformanceCharacteristic characteristic) {
            var stringBuilder = new Text.Perf.StringBuilder(characteristic);
            stringBuilder.Append("Test, test, test");
            stringBuilder.Dispose(DONOTCLEAR);
            
            var stringBuilder2 = new Text.Perf.StringBuilder(characteristic);
            
            stringBuilder._performanceObject.Should().BeSameAs(stringBuilder2._performanceObject);
            stringBuilder.ToString().Should().Be(stringBuilder2.ToString());
        }

        [Theory]
        [InlineData(PerformanceCharacteristic.ThreadStatic)]
        [InlineData(PerformanceCharacteristic.Pooled)]
        public void ByCharacteristic_PoolKey_ShouldBeSame(PerformanceCharacteristic characteristic) {
            var poolKey = "Test1";
            var stringBuilder = new Text.Perf.StringBuilder(characteristic, poolKey);
            stringBuilder.Append("I want to test this out.");
            stringBuilder.Dispose(DONOTCLEAR);
            
            var stringBuilder2 = new Text.Perf.StringBuilder(characteristic, poolKey);

            stringBuilder2._performanceObject.Should().BeSameAs(stringBuilder._performanceObject);
            stringBuilder2.ToString().Should().Be(stringBuilder.ToString());
        }
    }
}

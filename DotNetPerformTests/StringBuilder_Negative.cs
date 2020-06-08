using System;
using Text = System.Text;
using Xunit;

namespace DotNetPerformTests {
    public class StringBuilder_Negative {
        [Fact]
        public void ThreadStatic() {
            var stringBuilder = new Text.Perf.StringBuilder(PerformanceCharacteristic.Pooled, poolSize: 100);
        }
    }
}

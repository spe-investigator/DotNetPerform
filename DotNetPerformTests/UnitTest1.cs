using System;
using Text = System.Text;
using Xunit;

namespace DotNetPerformTests {
    public class StringBuilder_Performance {
        [Fact]
        public void ThreadStatic() {
            var stringBuilder = new Text.Perf.StringBuilder(PerformanceCharacteristic.Pooled, 10);
        }
    }
}

using System;
using Text = System.Text;
using Xunit;
using FluentAssertions;

namespace DotNetPerformTests.StringBuilder {
    [Collection("Sequential")]
    public class StringBuilder_Negative {
        [Fact]
        public void ThreadStatic() {
            using (var test = new SpeDotNetPerform.Performance.PerformanceBaseTest<Text.StringBuilder>()) {
                var stringBuilder = new Text.Perf.StringBuilder(poolSize: 100);
                stringBuilder.Dispose();
            }
        }
    }
}

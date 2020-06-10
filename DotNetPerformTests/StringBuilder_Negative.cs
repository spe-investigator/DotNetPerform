using System;
using Text = System.Text;
using Xunit;
using FluentAssertions;

namespace DotNetPerformTests {
    [Collection("Sequential")]
    public class StringBuilder_Negative {
        [Fact]
        public void ThreadStatic() {
            var stringBuilder = new Text.Perf.StringBuilder(poolSize: 100);
            stringBuilder.Dispose();
        }
    }
}

using System;
using Text = System.Text;
using Xunit;
using FluentAssertions;

namespace DotNetPerformTests {
    public class StringBuilder_Negative {
        [Fact]
        public void ThreadStatic() {
            var stringBuilder = new Text.Perf.StringBuilder(poolSize: 100);
        }
    }
}

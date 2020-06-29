using Txt = System.Text;
using Xunit;
using FluentAssertions;
using Perf = System.Performance;

namespace DotNetPerformTests.Text.RegularExpressions
{
    [Collection("Sequential")]
    public class Regex_Negative {
        [Theory]
        [InlineData(20)]
        [InlineData(100)]
        public void PoolSizeExceeded(int poolSize) {
            using (var test = new Perf.PoolBoy<Txt.RegularExpressions.Regex>()) {
                const string numbersRegexPattern = "[0-9]+";
                Perf.Text.RegularExpressions.Regex? priorRegex = null;
                var created = 0;

                do {
                    var regexNumbers = new Perf.Text.RegularExpressions.Regex(numbersRegexPattern, poolSize: poolSize);
                    regexNumbers.IsPoolAllocated.Should().BeTrue();

                    if (priorRegex != null) {
                        regexNumbers.performanceObject.Should().NotBeSameAs(priorRegex.Value.performanceObject);
                    }

                    created++;
                    priorRegex = regexNumbers;
                } while (created < poolSize);

                var afterPoolSaturated = new Perf.Text.RegularExpressions.Regex(numbersRegexPattern, poolSize: poolSize);
                afterPoolSaturated.IsPoolAllocated.Should().BeFalse();
            }
        }
    }
}

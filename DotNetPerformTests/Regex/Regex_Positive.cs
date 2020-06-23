using System;
using Text = System.Text;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;

namespace DotNetPerformTests.StringBuilder {
    [Collection("Sequential")]
    public class Regex_Positive {
        const bool DONOTCLEAR = true;

        [Theory]
        [InlineData(null)]
        [InlineData("Test1")]
        public void SamePattern_SamePool(string poolKey) {
            using (var test = new SpeDotNetPerform.Performance.PoolBoy<Text.RegularExpressions.Regex>()) {
                const string numbersRegexPattern = "[0-9]+";
                var regexNumbers = new Text.RegularExpressions.Perf.Regex(numbersRegexPattern, poolKey);

                var regexNumbers2 = new Text.RegularExpressions.Perf.Regex(numbersRegexPattern, poolKey);

                regexNumbers.IsPoolAllocated.Should().BeTrue();
                regexNumbers2.IsPoolAllocated.Should().BeTrue();
                regexNumbers2._objectPool.Should().BeSameAs(regexNumbers._objectPool);
                regexNumbers2.performanceObject.Should().NotBeSameAs(regexNumbers.performanceObject);
            }
        }

        [Theory]
        [InlineData(4)]
        [InlineData(20)]
        public void Allocate_NonPooled(int poolSize) {
            using (var test = new SpeDotNetPerform.Performance.PoolBoy<Text.RegularExpressions.Regex>()) {
                Text.RegularExpressions.Perf.Regex? priorRegexNumbers = null;
                const string numbersRegexPattern = "[0-9]+";
                var created = 0;

                do {
                    var regexNumbers = new Text.RegularExpressions.Perf.Regex(numbersRegexPattern, poolSize: poolSize);
                    regexNumbers.IsPoolAllocated.Should().BeTrue();

                    if (priorRegexNumbers != null) {
                        regexNumbers.performanceObject.Should().NotBeSameAs(priorRegexNumbers.Value.performanceObject);
                    }

                    created++;
                    priorRegexNumbers = regexNumbers;
                } while (created < poolSize);

                var afterPoolSaturated = new Text.RegularExpressions.Perf.Regex(numbersRegexPattern, poolSize: poolSize);
                afterPoolSaturated.IsPoolAllocated.Should().BeFalse();

                var afterPoolSaturatedAgain = new Text.RegularExpressions.Perf.Regex(numbersRegexPattern, poolSize: poolSize);
                afterPoolSaturatedAgain.IsPoolAllocated.Should().BeFalse();
            }
        }
    }
}

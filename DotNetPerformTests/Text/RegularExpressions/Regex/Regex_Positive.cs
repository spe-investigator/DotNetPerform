using System;
using Txt = System.Text;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using Perf = System.Performance;

namespace DotNetPerformTests.Text.RegularExpressions {
    [Collection("Sequential")]
    public class Regex_Positive {
        const bool DONOTCLEAR = true;

        [Theory]
        [InlineData(null)]
        [InlineData("Test1")]
        public void SamePattern_SamePool(string poolKey) {
            using (var test = new Perf.PoolBoy<Txt.RegularExpressions.Regex>()) {
                const string numbersRegexPattern = "[0-9]+";
                var regexNumbers = new Perf.Text.RegularExpressions.Regex(numbersRegexPattern, poolKey);

                var regexNumbers2 = new Perf.Text.RegularExpressions.Regex(numbersRegexPattern, poolKey);

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
            using (var test = new Perf.PoolBoy<Txt.RegularExpressions.Regex>()) {
                Perf.Text.RegularExpressions.Regex? priorRegexNumbers = null;
                const string numbersRegexPattern = "[0-9]+";
                var created = 0;

                do {
                    var regexNumbers = new Perf.Text.RegularExpressions.Regex(numbersRegexPattern, poolSize: poolSize);
                    regexNumbers.IsPoolAllocated.Should().BeTrue();

                    if (priorRegexNumbers != null) {
                        regexNumbers.performanceObject.Should().NotBeSameAs(priorRegexNumbers.Value.performanceObject);
                    }

                    created++;
                    priorRegexNumbers = regexNumbers;
                } while (created < poolSize);

                var afterPoolSaturated = new Perf.Text.RegularExpressions.Regex(numbersRegexPattern, poolSize: poolSize);
                afterPoolSaturated.IsPoolAllocated.Should().BeFalse();

                var afterPoolSaturatedAgain = new Perf.Text.RegularExpressions.Regex(numbersRegexPattern, poolSize: poolSize);
                afterPoolSaturatedAgain.IsPoolAllocated.Should().BeFalse();
            }
        }
    }
}

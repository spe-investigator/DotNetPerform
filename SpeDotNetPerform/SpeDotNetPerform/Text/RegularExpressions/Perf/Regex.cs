using Microsoft.Extensions.ObjectPool;
using System;
using System.Performance;

namespace System.Text.RegularExpressions.Perf {
    /// <summary>
    /// Represents an immutable regular expression.
    /// </summary>
    public class Regex : PerformanceBase<System.Text.RegularExpressions.Regex> {
        /// <summary>
        /// Initializes a new instance of the System.Text.RegularExpressions.Regex class
        ///     for the specified regular expression.
        /// </summary>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <exception cref="System.ArgumentException">A regular expression parsing error occurred.</exception>
        /// <exception cref="System.ArgumentNullException">pattern is null.</exception>
        public Regex(string pattern, PerformanceCharacteristic characteristic = PerformanceCharacteristic.ThreadStatic, string poolKey = null, int poolSize = 10) : base(characteristic, poolSize, poolKey, new RegexPooledObjectPolicy(pattern)/*, (pooledObjectPolicy, maximumRetention) => new */) {
        }

        /// <summary>
        /// Initializes a new instance of the System.Text.RegularExpressions.Regex class
        ///     for the specified regular expression.
        /// </summary>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <param name="options">A bitwise combination of the enumeration values that modify the regular expression.</param>
        /// <exception cref="System.ArgumentException">A regular expression parsing error occurred.</exception>
        /// <exception cref="System.ArgumentNullException">pattern is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException:">options contains an invalid flag.</exception>
        public Regex(string pattern, RegexOptions options, PerformanceCharacteristic characteristic = PerformanceCharacteristic.ThreadStatic, string poolKey = null, int poolSize = 10) : base(characteristic, poolSize, poolKey, new RegexPooledObjectPolicy(pattern, options)/*, (pooledObjectPolicy, maximumRetention) => new */) {
        }

        /// <summary>
        /// Initializes a new instance of the System.Text.RegularExpressions.Regex class
        /// for the specified regular expression, with options that modify the pattern and
        /// a value that specifies how long a pattern matching method should attempt a match
        /// before it times out.
        /// </summary>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <param name="options">A bitwise combination of the enumeration values that modify the regular expression.</param>
        /// <param name="matchTimeout">A time-out interval, or System.Text.RegularExpressions.Regex.InfiniteMatchTimeout to indicate that the method should not time out.</param>
        /// <exception cref="System.ArgumentException">A regular expression parsing error occurred.</exception>
        /// <exception cref="System.ArgumentNullException">pattern is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException:">
        /// options is not a valid System.Text.RegularExpressions.RegexOptions value. -or- matchTimeout is negative, zero, or greater than approximately 24 days.
        /// </exception>
        public Regex(string pattern, RegexOptions options, TimeSpan matchTimeout, PerformanceCharacteristic characteristic = PerformanceCharacteristic.ThreadStatic, string poolKey = null, int poolSize = 10) : base(characteristic, poolSize, poolKey, new RegexPooledObjectPolicy(pattern, options, matchTimeout)/*, (pooledObjectPolicy, maximumRetention) => new */) {
        }

        //
        // Summary:
        //     Gets the time-out interval of the current instance.
        //
        // Returns:
        //     The maximum time interval that can elapse in a pattern-matching operation before
        //     a System.Text.RegularExpressions.RegexMatchTimeoutException is thrown, or System.Text.RegularExpressions.Regex.InfiniteMatchTimeout
        //     if time-outs are disabled.
        public TimeSpan MatchTimeout => _performanceObject.MatchTimeout;

        //
        // Summary:
        //     Gets the options that were passed into the System.Text.RegularExpressions.Regex
        //     constructor.
        //
        // Returns:
        //     One or more members of the System.Text.RegularExpressions.RegexOptions enumeration
        //     that represent options that were passed to the System.Text.RegularExpressions.Regex
        //     constructor
        public RegexOptions Options => _performanceObject.Options;

        //
        // Summary:
        //     Gets a value that indicates whether the regular expression searches from right
        //     to left.
        //
        // Returns:
        //     true if the regular expression searches from right to left; otherwise, false.
        public bool RightToLeft => _performanceObject.RightToLeft;

        //
        // Summary:
        //     Returns an array of capturing group names for the regular expression.
        //
        // Returns:
        //     A string array of group names.
        public string[] GetGroupNames() => _performanceObject.GetGroupNames();

        //
        // Summary:
        //     Returns an array of capturing group numbers that correspond to group names in
        //     an array.
        //
        // Returns:
        //     An integer array of group numbers.
        public int[] GetGroupNumbers() => _performanceObject.GetGroupNumbers();

        //
        // Summary:
        //     Gets the group name that corresponds to the specified group number.
        //
        // Parameters:
        //   i:
        //     The group number to convert to the corresponding group name.
        //
        // Returns:
        //     A string that contains the group name associated with the specified group number.
        //     If there is no group name that corresponds to i, the method returns System.String.Empty.
        public string GroupNameFromNumber(int i) => _performanceObject.GroupNameFromNumber(i);

        //
        // Summary:
        //     Returns the group number that corresponds to the specified group name.
        //
        // Parameters:
        //   name:
        //     The group name to convert to the corresponding group number.
        //
        // Returns:
        //     The group number that corresponds to the specified group name, or -1 if name
        //     is not a valid group name.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     name is null.
        public int GroupNumberFromName(string name) => _performanceObject.GroupNumberFromName(name);

        //
        // Summary:
        //     Indicates whether the regular expression specified in the System.Text.RegularExpressions.Regex
        //     constructor finds a match in a specified input string.
        //
        // Parameters:
        //   input:
        //     The string to search for a match.
        //
        // Returns:
        //     true if the regular expression finds a match; otherwise, false.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     input is null.
        //
        //   T:System.Text.RegularExpressions.RegexMatchTimeoutException:
        //     A time-out occurred. For more information about time-outs, see the Remarks section.
        public bool IsMatch(string input) => _performanceObject.IsMatch(input);

        //
        // Summary:
        //     Indicates whether the regular expression specified in the System.Text.RegularExpressions.Regex
        //     constructor finds a match in the specified input string, beginning at the specified
        //     starting position in the string.
        //
        // Parameters:
        //   input:
        //     The string to search for a match.
        //
        //   startat:
        //     The character position at which to start the search.
        //
        // Returns:
        //     true if the regular expression finds a match; otherwise, false.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     input is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     startat is less than zero or greater than the length of input.
        //
        //   T:System.Text.RegularExpressions.RegexMatchTimeoutException:
        //     A time-out occurred. For more information about time-outs, see the Remarks section.
        public bool IsMatch(string input, int startat) => _performanceObject.IsMatch(input, startat);

        //
        // Summary:
        //     Searches the specified input string for the first occurrence of the regular expression
        //     specified in the System.Text.RegularExpressions.Regex constructor.
        //
        // Parameters:
        //   input:
        //     The string to search for a match.
        //
        // Returns:
        //     An object that contains information about the match.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     input is null.
        //
        //   T:System.Text.RegularExpressions.RegexMatchTimeoutException:
        //     A time-out occurred. For more information about time-outs, see the Remarks section.
        public Match Match(string input) => _performanceObject.Match(input);

        //
        // Summary:
        //     Searches the input string for the first occurrence of a regular expression, beginning
        //     at the specified starting position in the string.
        //
        // Parameters:
        //   input:
        //     The string to search for a match.
        //
        //   startat:
        //     The zero-based character position at which to start the search.
        //
        // Returns:
        //     An object that contains information about the match.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     input is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     startat is less than zero or greater than the length of input.
        //
        //   T:System.Text.RegularExpressions.RegexMatchTimeoutException:
        //     A time-out occurred. For more information about time-outs, see the Remarks section.
        public Match Match(string input, int startat) => _performanceObject.Match(input, startat);

        //
        // Summary:
        //     Searches the input string for the first occurrence of a regular expression, beginning
        //     at the specified starting position and searching only the specified number of
        //     characters.
        //
        // Parameters:
        //   input:
        //     The string to search for a match.
        //
        //   beginning:
        //     The zero-based character position in the input string that defines the leftmost
        //     position to be searched.
        //
        //   length:
        //     The number of characters in the substring to include in the search.
        //
        // Returns:
        //     An object that contains information about the match.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     input is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     beginning is less than zero or greater than the length of input. -or- length
        //     is less than zero or greater than the length of input. -or- beginning+length–1
        //     identifies a position that is outside the range of input.
        //
        //   T:System.Text.RegularExpressions.RegexMatchTimeoutException:
        //     A time-out occurred. For more information about time-outs, see the Remarks section.
        public Match Match(string input, int beginning, int length) => _performanceObject.Match(input, beginning, length);

        //
        // Summary:
        //     Searches the specified input string for all occurrences of a regular expression,
        //     beginning at the specified starting position in the string.
        //
        // Parameters:
        //   input:
        //     The string to search for a match.
        //
        //   startat:
        //     The character position in the input string at which to start the search.
        //
        // Returns:
        //     A collection of the System.Text.RegularExpressions.Match objects found by the
        //     search. If no matches are found, the method returns an empty collection object.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     input is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     startat is less than zero or greater than the length of input.
        public MatchCollection Matches(string input, int startAt) => _performanceObject.Matches(input, startAt);

        //
        // Summary:
        //     Searches the specified input string for all occurrences of a regular expression.
        //
        // Parameters:
        //   input:
        //     The string to search for a match.
        //
        // Returns:
        //     A collection of the System.Text.RegularExpressions.Match objects found by the
        //     search. If no matches are found, the method returns an empty collection object.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     input is null.
        public MatchCollection Matches(string input) => _performanceObject.Matches(input);

        //
        // Summary:
        //     In a specified input substring, replaces a specified maximum number of strings
        //     that match a regular expression pattern with a specified replacement string.
        //
        // Parameters:
        //   input:
        //     The string to search for a match.
        //
        //   replacement:
        //     The replacement string.
        //
        //   count:
        //     Maximum number of times the replacement can occur.
        //
        //   startat:
        //     The character position in the input string where the search begins.
        //
        // Returns:
        //     A new string that is identical to the input string, except that the replacement
        //     string takes the place of each matched string. If the regular expression pattern
        //     is not matched in the current instance, the method returns the current instance
        //     unchanged.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     input or replacement is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     startat is less than zero or greater than the length of input.
        //
        //   T:System.Text.RegularExpressions.RegexMatchTimeoutException:
        //     A time-out occurred. For more information about time-outs, see the Remarks section.
        public string Replace(string input, string replacement, int count, int startAt) => _performanceObject.Replace(input, replacement, count, startAt);

        //
        // Summary:
        //     In a specified input string, replaces all strings that match a specified regular
        //     expression with a string returned by a System.Text.RegularExpressions.MatchEvaluator
        //     delegate.
        //
        // Parameters:
        //   input:
        //     The string to search for a match.
        //
        //   evaluator:
        //     A custom method that examines each match and returns either the original matched
        //     string or a replacement string.
        //
        // Returns:
        //     A new string that is identical to the input string, except that a replacement
        //     string takes the place of each matched string. If the regular expression pattern
        //     is not matched in the current instance, the method returns the current instance
        //     unchanged.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     input or evaluator is null.
        //
        //   T:System.Text.RegularExpressions.RegexMatchTimeoutException:
        //     A time-out occurred. For more information about time-outs, see the Remarks section.
        public string Replace(string input, MatchEvaluator evaluator) => _performanceObject.Replace(input, evaluator);

        //
        // Summary:
        //     In a specified input string, replaces a specified maximum number of strings that
        //     match a regular expression pattern with a specified replacement string.
        //
        // Parameters:
        //   input:
        //     The string to search for a match.
        //
        //   replacement:
        //     The replacement string.
        //
        //   count:
        //     The maximum number of times the replacement can occur.
        //
        // Returns:
        //     A new string that is identical to the input string, except that the replacement
        //     string takes the place of each matched string. If the regular expression pattern
        //     is not matched in the current instance, the method returns the current instance
        //     unchanged.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     input or replacement is null.
        //
        //   T:System.Text.RegularExpressions.RegexMatchTimeoutException:
        //     A time-out occurred. For more information about time-outs, see the Remarks section.
        public string Replace(string input, string replacement, int count) => _performanceObject.Replace(input, replacement, count);

        //
        // Summary:
        //     In a specified input substring, replaces a specified maximum number of strings
        //     that match a regular expression pattern with a string returned by a System.Text.RegularExpressions.MatchEvaluator
        //     delegate.
        //
        // Parameters:
        //   input:
        //     The string to search for a match.
        //
        //   evaluator:
        //     A custom method that examines each match and returns either the original matched
        //     string or a replacement string.
        //
        //   count:
        //     The maximum number of times the replacement will occur.
        //
        //   startat:
        //     The character position in the input string where the search begins.
        //
        // Returns:
        //     A new string that is identical to the input string, except that a replacement
        //     string takes the place of each matched string. If the regular expression pattern
        //     is not matched in the current instance, the method returns the current instance
        //     unchanged.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     input or evaluator is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     startat is less than zero or greater than the length of input.
        //
        //   T:System.Text.RegularExpressions.RegexMatchTimeoutException:
        //     A time-out occurred. For more information about time-outs, see the Remarks section.
        public string Replace(string input, MatchEvaluator evaluator, int count, int startAt) => _performanceObject.Replace(input, evaluator, count, startAt);

        //
        // Summary:
        //     In a specified input string, replaces a specified maximum number of strings that
        //     match a regular expression pattern with a string returned by a System.Text.RegularExpressions.MatchEvaluator
        //     delegate.
        //
        // Parameters:
        //   input:
        //     The string to search for a match.
        //
        //   evaluator:
        //     A custom method that examines each match and returns either the original matched
        //     string or a replacement string.
        //
        //   count:
        //     The maximum number of times the replacement will occur.
        //
        // Returns:
        //     A new string that is identical to the input string, except that a replacement
        //     string takes the place of each matched string. If the regular expression pattern
        //     is not matched in the current instance, the method returns the current instance
        //     unchanged.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     input or evaluator is null.
        //
        //   T:System.Text.RegularExpressions.RegexMatchTimeoutException:
        //     A time-out occurred. For more information about time-outs, see the Remarks section.
        public string Replace(string input, MatchEvaluator evaluator, int count) => _performanceObject.Replace(input, evaluator, count);

        //
        // Summary:
        //     In a specified input string, replaces all strings that match a regular expression
        //     pattern with a specified replacement string.
        //
        // Parameters:
        //   input:
        //     The string to search for a match.
        //
        //   replacement:
        //     The replacement string.
        //
        // Returns:
        //     A new string that is identical to the input string, except that the replacement
        //     string takes the place of each matched string. If the regular expression pattern
        //     is not matched in the current instance, the method returns the current instance
        //     unchanged.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     input or replacement is null.
        //
        //   T:System.Text.RegularExpressions.RegexMatchTimeoutException:
        //     A time-out occurred. For more information about time-outs, see the Remarks section.
        public string Replace(string input, string replacement) => _performanceObject.Replace(input, replacement);

        //
        // Summary:
        //     Splits an input string a specified maximum number of times into an array of substrings,
        //     at the positions defined by a regular expression specified in the System.Text.RegularExpressions.Regex
        //     constructor. The search for the regular expression pattern starts at a specified
        //     character position in the input string.
        //
        // Parameters:
        //   input:
        //     The string to be split.
        //
        //   count:
        //     The maximum number of times the split can occur.
        //
        //   startat:
        //     The character position in the input string where the search will begin.
        //
        // Returns:
        //     An array of strings.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     input is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     startat is less than zero or greater than the length of input.
        //
        //   T:System.Text.RegularExpressions.RegexMatchTimeoutException:
        //     A time-out occurred. For more information about time-outs, see the Remarks section.
        public string[] Split(string input, int count, int startAt) => _performanceObject.Split(input, count, startAt);

        //
        // Summary:
        //     Splits an input string a specified maximum number of times into an array of substrings,
        //     at the positions defined by a regular expression specified in the System.Text.RegularExpressions.Regex
        //     constructor.
        //
        // Parameters:
        //   input:
        //     The string to be split.
        //
        //   count:
        //     The maximum number of times the split can occur.
        //
        // Returns:
        //     An array of strings.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     input is null.
        //
        //   T:System.Text.RegularExpressions.RegexMatchTimeoutException:
        //     A time-out occurred. For more information about time-outs, see the Remarks section.
        public string[] Split(string input, int count) => _performanceObject.Split(input, count);

        //
        // Summary:
        //     Splits an input string into an array of substrings at the positions defined by
        //     a regular expression pattern specified in the System.Text.RegularExpressions.Regex
        //     constructor.
        //
        // Parameters:
        //   input:
        //     The string to split.
        //
        // Returns:
        //     An array of strings.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     input is null.
        //
        //   T:System.Text.RegularExpressions.RegexMatchTimeoutException:
        //     A time-out occurred. For more information about time-outs, see the Remarks section.
        public string[] Split(string input) => _performanceObject.Split(input);

        //
        // Summary:
        //     Returns the regular expression pattern that was passed into the Regex constructor.
        //
        // Returns:
        //     The pattern parameter that was passed into the Regex constructor.
        public override string ToString() => _performanceObject.ToString();


        ////
        //// Summary:
        ////     Initializes a new instance of the System.Text.RegularExpressions.Regex class.
        //protected Regex();
        ////
        //// Summary:
        ////     Initializes a new instance of the System.Text.RegularExpressions.Regex class
        ////     by using serialized data.
        ////
        //// Parameters:
        ////   info:
        ////     The object that contains a serialized pattern and System.Text.RegularExpressions.RegexOptions
        ////     information.
        ////
        ////   context:
        ////     The destination for this serialization. (This parameter is not used; specify
        ////     null.)
        ////
        //// Exceptions:
        ////   T:System.ArgumentException:
        ////     A regular expression parsing error occurred.
        ////
        ////   T:System.ArgumentNullException:
        ////     The pattern that info contains is null.
        ////
        ////   T:System.ArgumentOutOfRangeException:
        ////     info contains an invalid System.Text.RegularExpressions.RegexOptions flag.
        //protected Regex(SerializationInfo info, StreamingContext context);

        /// <summary>
        /// Not used for Pooling, only used for ThreadStatic object creation.
        /// </summary>
        internal class RegexPooledObjectPolicy : PooledObjectPolicy<System.Text.RegularExpressions.Regex> {
            public string Pattern { get; set; }
            
            public RegexOptions? Options { get; }
            
            public TimeSpan? MatchTimeout { get; }

            public RegexPooledObjectPolicy(string pattern, RegexOptions? options = null, TimeSpan? matchTimeout = null) {
                Pattern = pattern;
                Options = options;
                MatchTimeout = matchTimeout;
            }

            public override RegularExpressions.Regex Create() {
                if (MatchTimeout.HasValue) {
                    return new RegularExpressions.Regex(Pattern, Options.Value, MatchTimeout.Value);
                }
                if (Options.HasValue) {
                    if (!MatchTimeout.HasValue) {
                        throw new ArgumentNullException(nameof(MatchTimeout));
                    }
                    return new RegularExpressions.Regex(Pattern, Options.Value, MatchTimeout.Value);
                }

                return new RegularExpressions.Regex(Pattern);
            }

            public override bool Return(RegularExpressions.Regex obj) {
                // Do nothing.
                return true;
            }
        }
    }
}

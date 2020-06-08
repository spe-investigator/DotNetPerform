using SpeDotNetPerform.Performance;
using System.Performance;

namespace System.Text.Perf {
    public class StringBuilder : PerformanceBase<Text.StringBuilder> {
        private readonly int? _maximumRetainedCapacity;
        private readonly int? _capacity;

        /// <summary>
        /// Initializes a new instance of the System.Text.StringBuilder class.
        /// </summary>
        /// <param name="characteristic">The type of pooling to utilize.</param>
        /// <param name="poolKey">Pool name for Pooled characteristic. Groups objects with same pool key.</param>
        /// <param name="poolSize">Pool size for Pooled characteristic.</param>
        public StringBuilder(PerformanceCharacteristic characteristic = PerformanceCharacteristic.ThreadStatic, string poolKey = "", int poolSize = 10) : base(characteristic, poolSize, poolKey) {
        }

        /// <summary>
        /// Initializes a new instance of the System.Text.StringBuilder class using the specified capacity.
        /// </summary>
        /// <param name="capacity">The suggested starting size of this instance.</param>
        /// <param name="characteristic">The type of pooling to utilize.</param>
        /// <param name="poolKey">Pool name for Pooled characteristic. Groups objects with same pool key.</param>
        /// <param name="poolSize">Pool size for Pooled characteristic.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">capacity is less than zero.</exception>
        public StringBuilder(int capacity, PerformanceCharacteristic characteristic = PerformanceCharacteristic.ThreadStatic, string poolKey = "", int poolSize = 10) : base(characteristic, poolSize, poolKey) {
            _capacity = capacity;
        }

        /// <summary>
        /// Initializes a new instance of the System.Text.StringBuilder class that starts
        /// with a specified capacity and can grow to a specified maximum.
        /// </summary>
        /// <param name="capacity">The suggested starting size of the System.Text.StringBuilder.</param>
        /// <param name="maximumRetainedCapacity">The maximum number of characters the current string can contain.</param>
        /// <param name="characteristic">The type of pooling to utilize.</param>
        /// <param name="poolKey">Pool name for Pooled characteristic. Groups objects with same pool key.</param>
        /// <param name="poolSize">Pool size for Pooled characteristic.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">maxCapacity is less than one, capacity is less than zero, or capacity is greater than maxCapacity.</exception>
        public StringBuilder(int capacity, int maximumRetainedCapacity, PerformanceCharacteristic characteristic, string poolKey = "", int poolSize = 10) : base(characteristic, poolSize, poolKey) {
            _capacity = capacity;
            _maximumRetainedCapacity = maximumRetainedCapacity;
        }

        //
        // Summary:
        //     Gets or sets the character at the specified character position in this instance.
        //
        // Parameters:
        //   index:
        //     The position of the character.
        //
        // Returns:
        //     The Unicode character at position index.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     index is outside the bounds of this instance while setting a character.
        //
        //   T:System.IndexOutOfRangeException:
        //     index is outside the bounds of this instance while getting a character.
        public char this[int index] { get => _performanceObject[index]; set => _performanceObject[index] = value; }

        //
        // Summary:
        //     Gets or sets the maximum number of characters that can be contained in the memory
        //     allocated by the current instance.
        //
        // Returns:
        //     The maximum number of characters that can be contained in the memory allocated
        //     by the current instance. Its value can range from System.Text.StringBuilder.Length
        //     to System.Text.StringBuilder.MaxCapacity.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     The value specified for a set operation is less than the current length of this
        //     instance. -or- The value specified for a set operation is greater than the maximum
        //     capacity.
        public int? Capacity { get => _performanceObject?.Capacity; set => _performanceObject.Capacity = value.Value; }
        //
        // Summary:
        //     Gets or sets the length of the current System.Text.StringBuilder object.
        //
        // Returns:
        //     The length of this instance.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     The value specified for a set operation is less than zero or greater than System.Text.StringBuilder.MaxCapacity.
        public int Length { get => _performanceObject.Length; set => _performanceObject.Length = value; }
        //
        // Summary:
        //     Gets the maximum capacity of this instance.
        //
        // Returns:
        //     The maximum number of characters this instance can hold.
        public int? MaxCapacity { get => _performanceObject?.MaxCapacity; }

        //
        // Summary:
        //     Appends a specified number of copies of the string representation of a Unicode
        //     character to this instance.
        //
        // Parameters:
        //   value:
        //     The character to append.
        //
        //   repeatCount:
        //     The number of times to append value.
        //
        // Returns:
        //     A reference to this instance after the append operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     repeatCount is less than zero. -or- Enlarging the value of this instance would
        //     exceed System.Text.StringBuilder.MaxCapacity.
        //
        //   T:System.OutOfMemoryException:
        //     Out of memory.
        public StringBuilder Append(char value, int repeatCount) {
            _performanceObject.Append(value, repeatCount);
            return this;
        }

        //
        // Summary:
        //     Appends the string representation of a specified Boolean value to this instance.
        //
        // Parameters:
        //   value:
        //     The Boolean value to append.
        //
        // Returns:
        //     A reference to this instance after the append operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     Enlarging the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder Append(bool value) {
            _performanceObject.Append(value);
            return this;
        }

        //
        // Summary:
        //     Appends the string representation of a specified System.Char object to this instance.
        //
        // Parameters:
        //   value:
        //     The UTF-16-encoded code unit to append.
        //
        // Returns:
        //     A reference to this instance after the append operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     Enlarging the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder Append(char value) {
			_performanceObject.Append(value);

			return this;
		}
        //
        // Summary:
        //     Appends the string representation of a specified 64-bit unsigned integer to this
        //     instance.
        //
        // Parameters:
        //   value:
        //     The value to append.
        //
        // Returns:
        //     A reference to this instance after the append operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     Enlarging the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
        //[CLSCompliant(false)]
        public StringBuilder Append(ulong value) {
			_performanceObject.Append(value);

			return this;
		}
        //
        // Summary:
        //     Appends the string representation of a specified 32-bit unsigned integer to this
        //     instance.
        //
        // Parameters:
        //   value:
        //     The value to append.
        //
        // Returns:
        //     A reference to this instance after the append operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     Enlarging the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
        //[CLSCompliant(false)]
        public StringBuilder Append(uint value) {
			_performanceObject.Append(value);

			return this;
		}
        //
        // Summary:
        //     Appends the string representation of a specified 8-bit unsigned integer to this
        //     instance.
        //
        // Parameters:
        //   value:
        //     The value to append.
        //
        // Returns:
        //     A reference to this instance after the append operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     Enlarging the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder Append(byte value) {
			_performanceObject.Append(value);

			return this;
		}
        //
        // Summary:
        //     Appends a copy of a specified substring to this instance.
        //
        // Parameters:
        //   value:
        //     The string that contains the substring to append.
        //
        //   startIndex:
        //     The starting position of the substring within value.
        //
        //   count:
        //     The number of characters in value to append.
        //
        // Returns:
        //     A reference to this instance after the append operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     value is null, and startIndex and count are not zero.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     count less than zero. -or- startIndex less than zero. -or- startIndex + count
        //     is greater than the length of value. -or- Enlarging the value of this instance
        //     would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder Append(string value, int startIndex, int count) {
			_performanceObject.Append(value, startIndex, count);

			return this;
		}
        //
        // Summary:
        //     Appends a copy of the specified string to this instance.
        //
        // Parameters:
        //   value:
        //     The string to append.
        //
        // Returns:
        //     A reference to this instance after the append operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     Enlarging the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder Append(string value) {
			_performanceObject.Append(value);

			return this;
		}
        //
        // Summary:
        //     Appends the string representation of a specified single-precision floating-point
        //     number to this instance.
        //
        // Parameters:
        //   value:
        //     The value to append.
        //
        // Returns:
        //     A reference to this instance after the append operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     Enlarging the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder Append(float value) {
			_performanceObject.Append(value);

			return this;
		}
        //
        // Summary:
        //     Appends the string representation of a specified 16-bit unsigned integer to this
        //     instance.
        //
        // Parameters:
        //   value:
        //     The value to append.
        //
        // Returns:
        //     A reference to this instance after the append operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     Enlarging the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
        //[CLSCompliant(false)]
        public StringBuilder Append(ushort value) {
			_performanceObject.Append(value);

			return this;
		}
        //
        // Summary:
        //     Appends the string representation of a specified object to this instance.
        //
        // Parameters:
        //   value:
        //     The object to append.
        //
        // Returns:
        //     A reference to this instance after the append operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     Enlarging the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder Append(object value) {
			_performanceObject.Append(value);

			return this;
		}
        //
        // Summary:
        //     Appends the string representation of the Unicode characters in a specified array
        //     to this instance.
        //
        // Parameters:
        //   value:
        //     The array of characters to append.
        //
        // Returns:
        //     A reference to this instance after the append operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     Enlarging the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder Append(char[] value) {
			_performanceObject.Append(value);

			return this;
		}
        //
        // Summary:
        //     Appends the string representation of a specified subarray of Unicode characters
        //     to this instance.
        //
        // Parameters:
        //   value:
        //     A character array.
        //
        //   startIndex:
        //     The starting position in value.
        //
        //   charCount:
        //     The number of characters to append.
        //
        // Returns:
        //     A reference to this instance after the append operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     value is null, and startIndex and charCount are not zero.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     charCount is less than zero. -or- startIndex is less than zero. -or- startIndex
        //     + charCount is greater than the length of value. -or- Enlarging the value of
        //     this instance would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder Append(char[] value, int startIndex, int charCount) {
			_performanceObject.Append(value, startIndex, charCount);

			return this;
		}
        //
        // Summary:
        //     Appends the string representation of a specified 8-bit signed integer to this
        //     instance.
        //
        // Parameters:
        //   value:
        //     The value to append.
        //
        // Returns:
        //     A reference to this instance after the append operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     Enlarging the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
        //[CLSCompliant(false)]
        public StringBuilder Append(sbyte value) {
			_performanceObject.Append(value);

			return this;
		}
        //
        // Summary:
        //     Appends the string representation of a specified decimal number to this instance.
        //
        // Parameters:
        //   value:
        //     The value to append.
        //
        // Returns:
        //     A reference to this instance after the append operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     Enlarging the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder Append(decimal value) {
			_performanceObject.Append(value);

			return this;
		}
        //
        // Summary:
        //     Appends an array of Unicode characters starting at a specified address to this
        //     instance.
        //
        // Parameters:
        //   value:
        //     A pointer to an array of characters.
        //
        //   valueCount:
        //     The number of characters in the array.
        //
        // Returns:
        //     A reference to this instance after the append operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     valueCount is less than zero. -or- Enlarging the value of this instance would
        //     exceed System.Text.StringBuilder.MaxCapacity.
        //
        //   T:System.NullReferenceException:
        //     value is a null pointer.
        //[CLSCompliant(false)]
        public unsafe StringBuilder Append(char* value, int valueCount) {
			_performanceObject.Append(value, valueCount);

			return this;
		}
        //
        // Summary:
        //     Appends the string representation of a specified 16-bit signed integer to this
        //     instance.
        //
        // Parameters:
        //   value:
        //     The value to append.
        //
        // Returns:
        //     A reference to this instance after the append operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     Enlarging the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder Append(short value) {
			_performanceObject.Append(value);

			return this;
		}        //
        // Summary:
        //     Appends the string representation of a specified 32-bit signed integer to this
        //     instance.
        //
        // Parameters:
        //   value:
        //     The value to append.
        //
        // Returns:
        //     A reference to this instance after the append operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     Enlarging the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder Append(int value) {
			_performanceObject.Append(value);

			return this;
		}
        //
        // Summary:
        //     Appends the string representation of a specified 64-bit signed integer to this
        //     instance.
        //
        // Parameters:
        //   value:
        //     The value to append.
        //
        // Returns:
        //     A reference to this instance after the append operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     Enlarging the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder Append(long value) {
			_performanceObject.Append(value);

			return this;
		}
        //
        // Summary:
        //     Appends the string representation of a specified double-precision floating-point
        //     number to this instance.
        //
        // Parameters:
        //   value:
        //     The value to append.
        //
        // Returns:
        //     A reference to this instance after the append operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     Enlarging the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder Append(double value) {
			_performanceObject.Append(value);

			return this;
		}
        //
        // Summary:
        //     Appends the string returned by processing a composite format string, which contains
        //     zero or more format items, to this instance. Each format item is replaced by
        //     the string representation of a single argument using a specified format provider.
        //
        // Parameters:
        //   provider:
        //     An object that supplies culture-specific formatting information.
        //
        //   format:
        //     A composite format string.
        //
        //   arg0:
        //     The object to format.
        //
        // Returns:
        //     A reference to this instance after the append operation has completed. After
        //     the append operation, this instance contains any data that existed before the
        //     operation, suffixed by a copy of format in which any format specification is
        //     replaced by the string representation of arg0.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     format is null.
        //
        //   T:System.FormatException:
        //     format is invalid. -or- The index of a format item is less than 0 (zero), or
        //     greater than or equal to one (1).
        //
        //   T:System.ArgumentOutOfRangeException:
        //     The length of the expanded string would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0) {
			_performanceObject.AppendFormat(provider, format, arg0);

			return this;
		}
        //
        // Summary:
        //     Appends the string returned by processing a composite format string, which contains
        //     zero or more format items, to this instance. Each format item is replaced by
        //     the string representation of either of two arguments using a specified format
        //     provider.
        //
        // Parameters:
        //   provider:
        //     An object that supplies culture-specific formatting information.
        //
        //   format:
        //     A composite format string.
        //
        //   arg0:
        //     The first object to format.
        //
        //   arg1:
        //     The second object to format.
        //
        // Returns:
        //     A reference to this instance after the append operation has completed. After
        //     the append operation, this instance contains any data that existed before the
        //     operation, suffixed by a copy of format where any format specification is replaced
        //     by the string representation of the corresponding object argument.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     format is null.
        //
        //   T:System.FormatException:
        //     format is invalid. -or- The index of a format item is less than 0 (zero), or
        //     greater than or equal to 2 (two).
        //
        //   T:System.ArgumentOutOfRangeException:
        //     The length of the expanded string would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0, object arg1) {
			_performanceObject.AppendFormat(provider, format, arg0, arg1);

			return this;
		}
        //
        // Summary:
        //     Appends the string returned by processing a composite format string, which contains
        //     zero or more format items, to this instance. Each format item is replaced by
        //     the string representation of a corresponding argument in a parameter array using
        //     a specified format provider.
        //
        // Parameters:
        //   provider:
        //     An object that supplies culture-specific formatting information.
        //
        //   format:
        //     A composite format string.
        //
        //   args:
        //     An array of objects to format.
        //
        // Returns:
        //     A reference to this instance after the append operation has completed. After
        //     the append operation, this instance contains any data that existed before the
        //     operation, suffixed by a copy of format where any format specification is replaced
        //     by the string representation of the corresponding object argument.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     format is null.
        //
        //   T:System.FormatException:
        //     format is invalid. -or- The index of a format item is less than 0 (zero), or
        //     greater than or equal to the length of the args array.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     The length of the expanded string would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder AppendFormat(IFormatProvider provider, string format, params object[] args) {
			_performanceObject.AppendFormat(provider, format, args);

			return this;
		}
        //
        // Summary:
        //     Appends the string returned by processing a composite format string, which contains
        //     zero or more format items, to this instance. Each format item is replaced by
        //     the string representation of a single argument.
        //
        // Parameters:
        //   format:
        //     A composite format string.
        //
        //   arg0:
        //     An object to format.
        //
        // Returns:
        //     A reference to this instance with format appended. Each format item in format
        //     is replaced by the string representation of arg0.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     format is null.
        //
        //   T:System.FormatException:
        //     format is invalid. -or- The index of a format item is less than 0 (zero), or
        //     greater than or equal to 1.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     The length of the expanded string would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder AppendFormat(string format, object arg0) {
			_performanceObject.AppendFormat(format, arg0);

			return this;
		}
        //
        // Summary:
        //     Appends the string returned by processing a composite format string, which contains
        //     zero or more format items, to this instance. Each format item is replaced by
        //     the string representation of either of two arguments.
        //
        // Parameters:
        //   format:
        //     A composite format string.
        //
        //   arg0:
        //     The first object to format.
        //
        //   arg1:
        //     The second object to format.
        //
        // Returns:
        //     A reference to this instance with format appended. Each format item in format
        //     is replaced by the string representation of the corresponding object argument.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     format is null.
        //
        //   T:System.FormatException:
        //     format is invalid. -or- The index of a format item is less than 0 (zero), or
        //     greater than or equal to 2.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     The length of the expanded string would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder AppendFormat(string format, object arg0, object arg1) {
			_performanceObject.AppendFormat(format, arg0, arg1);

			return this;
		}
        //
        // Summary:
        //     Appends the string returned by processing a composite format string, which contains
        //     zero or more format items, to this instance. Each format item is replaced by
        //     the string representation of either of three arguments.
        //
        // Parameters:
        //   format:
        //     A composite format string.
        //
        //   arg0:
        //     The first object to format.
        //
        //   arg1:
        //     The second object to format.
        //
        //   arg2:
        //     The third object to format.
        //
        // Returns:
        //     A reference to this instance with format appended. Each format item in format
        //     is replaced by the string representation of the corresponding object argument.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     format is null.
        //
        //   T:System.FormatException:
        //     format is invalid. -or- The index of a format item is less than 0 (zero), or
        //     greater than or equal to 3.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     The length of the expanded string would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder AppendFormat(string format, object arg0, object arg1, object arg2) {
			_performanceObject.AppendFormat(format, arg0, arg1, arg2);

			return this;
		}
        //
        // Summary:
        //     Appends the string returned by processing a composite format string, which contains
        //     zero or more format items, to this instance. Each format item is replaced by
        //     the string representation of a corresponding argument in a parameter array.
        //
        // Parameters:
        //   format:
        //     A composite format string.
        //
        //   args:
        //     An array of objects to format.
        //
        // Returns:
        //     A reference to this instance with format appended. Each format item in format
        //     is replaced by the string representation of the corresponding object argument.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     format or args is null.
        //
        //   T:System.FormatException:
        //     format is invalid. -or- The index of a format item is less than 0 (zero), or
        //     greater than or equal to the length of the args array.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     The length of the expanded string would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder AppendFormat(string format, params object[] args) {
			_performanceObject.AppendFormat(format, args);

			return this;
		}
        //
        // Summary:
        //     Appends the string returned by processing a composite format string, which contains
        //     zero or more format items, to this instance. Each format item is replaced by
        //     the string representation of either of three arguments using a specified format
        //     provider.
        //
        // Parameters:
        //   provider:
        //     An object that supplies culture-specific formatting information.
        //
        //   format:
        //     A composite format string.
        //
        //   arg0:
        //     The first object to format.
        //
        //   arg1:
        //     The second object to format.
        //
        //   arg2:
        //     The third object to format.
        //
        // Returns:
        //     A reference to this instance after the append operation has completed. After
        //     the append operation, this instance contains any data that existed before the
        //     operation, suffixed by a copy of format where any format specification is replaced
        //     by the string representation of the corresponding object argument.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     format is null.
        //
        //   T:System.FormatException:
        //     format is invalid. -or- The index of a format item is less than 0 (zero), or
        //     greater than or equal to 3 (three).
        //
        //   T:System.ArgumentOutOfRangeException:
        //     The length of the expanded string would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0, object arg1, object arg2) {
			_performanceObject.AppendFormat(provider, format, arg0, arg1, arg2);

			return this;
		}
        //
        // Summary:
        //     Appends the default line terminator to the end of the current System.Text.StringBuilder
        //     object.
        //
        // Returns:
        //     A reference to this instance after the append operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     Enlarging the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder AppendLine() {
			_performanceObject.AppendLine();

			return this;
		}
        //
        // Summary:
        //     Appends a copy of the specified string followed by the default line terminator
        //     to the end of the current System.Text.StringBuilder object.
        //
        // Parameters:
        //   value:
        //     The string to append.
        //
        // Returns:
        //     A reference to this instance after the append operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     Enlarging the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder AppendLine(string value) {
			_performanceObject.AppendLine(value);

			return this;
		}

        //
        // Summary:
        //     Removes all characters from the current System.Text.StringBuilder instance.
        //
        // Returns:
        //     An object whose System.Text.StringBuilder.Length is 0 (zero).
        public StringBuilder Clear() {
			_performanceObject.Clear();

			return this;
		}

        //
        // Summary:
        //     Copies the characters from a specified segment of this instance to a specified
        //     segment of a destination System.Char array.
        //
        // Parameters:
        //   sourceIndex:
        //     The starting position in this instance where characters will be copied from.
        //     The index is zero-based.
        //
        //   destination:
        //     The array where characters will be copied.
        //
        //   destinationIndex:
        //     The starting position in destination where characters will be copied. The index
        //     is zero-based.
        //
        //   count:
        //     The number of characters to be copied.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     destination is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     sourceIndex, destinationIndex, or count, is less than zero. -or- sourceIndex
        //     is greater than the length of this instance.
        //
        //   T:System.ArgumentException:
        //     sourceIndex + count is greater than the length of this instance. -or- destinationIndex
        //     + count is greater than the length of destination.
        public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count) => _performanceObject.CopyTo(sourceIndex, destination, destinationIndex, count);

        //
        // Summary:
        //     Ensures that the capacity of this instance of System.Text.StringBuilder is at
        //     least the specified value.
        //
        // Parameters:
        //   capacity:
        //     The minimum capacity to ensure.
        //
        // Returns:
        //     The new capacity of this instance.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     capacity is less than zero. -or- Enlarging the value of this instance would exceed
        //     System.Text.StringBuilder.MaxCapacity.
        public int EnsureCapacity(int capacity) => _performanceObject.EnsureCapacity(capacity);

        //
        // Summary:
        //     Returns a value indicating whether this instance is equal to a specified object.
        //
        // Parameters:
        //   sb:
        //     An object to compare with this instance, or null.
        //
        // Returns:
        //     true if this instance and sb have equal string, System.Text.StringBuilder.Capacity,
        //     and System.Text.StringBuilder.MaxCapacity values; otherwise, false.
        public bool Equals(StringBuilder sb) => _performanceObject.Equals(sb);

        //
        // Summary:
        //     Inserts the string representation of a specified subarray of Unicode characters
        //     into this instance at the specified character position.
        //
        // Parameters:
        //   index:
        //     The position in this instance where insertion begins.
        //
        //   value:
        //     A character array.
        //
        //   startIndex:
        //     The starting index within value.
        //
        //   charCount:
        //     The number of characters to insert.
        //
        // Returns:
        //     A reference to this instance after the insert operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     value is null, and startIndex and charCount are not zero.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     index, startIndex, or charCount is less than zero. -or- index is greater than
        //     the length of this instance. -or- startIndex plus charCount is not a position
        //     within value. -or- Enlarging the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder Insert(int index, char[] value, int startIndex, int charCount) {
			_performanceObject.Insert(index, value, startIndex, charCount);

			return this;
		}
        //
        // Summary:
        //     Inserts the string representation of a Boolean value into this instance at the
        //     specified character position.
        //
        // Parameters:
        //   index:
        //     The position in this instance where insertion begins.
        //
        //   value:
        //     The value to insert.
        //
        // Returns:
        //     A reference to this instance after the insert operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     index is less than zero or greater than the length of this instance.
        //
        //   T:System.OutOfMemoryException:
        //     Enlarging the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder Insert(int index, bool value) {
			_performanceObject.Insert(index, value);

			return this;
		}
        //
        // Summary:
        //     Inserts the string representation of a specified 8-bit unsigned integer into
        //     this instance at the specified character position.
        //
        // Parameters:
        //   index:
        //     The position in this instance where insertion begins.
        //
        //   value:
        //     The value to insert.
        //
        // Returns:
        //     A reference to this instance after the insert operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     index is less than zero or greater than the length of this instance.
        //
        //   T:System.OutOfMemoryException:
        //     Enlarging the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder Insert(int index, byte value) {
			_performanceObject.Insert(index, value);

			return this;
		}
        //
        // Summary:
        //     Inserts the string representation of a 64-bit unsigned integer into this instance
        //     at the specified character position.
        //
        // Parameters:
        //   index:
        //     The position in this instance where insertion begins.
        //
        //   value:
        //     The value to insert.
        //
        // Returns:
        //     A reference to this instance after the insert operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     index is less than zero or greater than the length of this instance.
        //
        //   T:System.OutOfMemoryException:
        //     Enlarging the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
        //[CLSCompliant(false)]
        public StringBuilder Insert(int index, ulong value) {
			_performanceObject.Insert(index, value);

			return this;
		}
        //
        // Summary:
        //     Inserts the string representation of a specified array of Unicode characters
        //     into this instance at the specified character position.
        //
        // Parameters:
        //   index:
        //     The position in this instance where insertion begins.
        //
        //   value:
        //     The character array to insert.
        //
        // Returns:
        //     A reference to this instance after the insert operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     index is less than zero or greater than the length of this instance. -or- Enlarging
        //     the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder Insert(int index, char[] value) {
			_performanceObject.Insert(index, value);

			return this;
		}
        //
        // Summary:
        //     Inserts the string representation of a 16-bit unsigned integer into this instance
        //     at the specified character position.
        //
        // Parameters:
        //   index:
        //     The position in this instance where insertion begins.
        //
        //   value:
        //     The value to insert.
        //
        // Returns:
        //     A reference to this instance after the insert operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     index is less than zero or greater than the length of this instance.
        //
        //   T:System.OutOfMemoryException:
        //     Enlarging the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
        //[CLSCompliant(false)]
        public StringBuilder Insert(int index, ushort value) {
			_performanceObject.Insert(index, value);

			return this;
		}
        //
        // Summary:
        //     Inserts one or more copies of a specified string into this instance at the specified
        //     character position.
        //
        // Parameters:
        //   index:
        //     The position in this instance where insertion begins.
        //
        //   value:
        //     The string to insert.
        //
        //   count:
        //     The number of times to insert value.
        //
        // Returns:
        //     A reference to this instance after insertion has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     index is less than zero or greater than the current length of this instance.
        //     -or- count is less than zero.
        //
        //   T:System.OutOfMemoryException:
        //     The current length of this System.Text.StringBuilder object plus the length of
        //     value times count exceeds System.Text.StringBuilder.MaxCapacity.
        public StringBuilder Insert(int index, string value, int count) {
			_performanceObject.Insert(index, value, count);

			return this;
		}
        //
        // Summary:
        //     Inserts the string representation of a specified Unicode character into this
        //     instance at the specified character position.
        //
        // Parameters:
        //   index:
        //     The position in this instance where insertion begins.
        //
        //   value:
        //     The value to insert.
        //
        // Returns:
        //     A reference to this instance after the insert operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     index is less than zero or greater than the length of this instance. -or- Enlarging
        //     the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder Insert(int index, char value) {
			_performanceObject.Insert(index, value);

			return this;
		}
        //
        // Summary:
        //     Inserts the string representation of a 32-bit unsigned integer into this instance
        //     at the specified character position.
        //
        // Parameters:
        //   index:
        //     The position in this instance where insertion begins.
        //
        //   value:
        //     The value to insert.
        //
        // Returns:
        //     A reference to this instance after the insert operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     index is less than zero or greater than the length of this instance.
        //
        //   T:System.OutOfMemoryException:
        //     Enlarging the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
        //[CLSCompliant(false)]
        public StringBuilder Insert(int index, uint value) {
			_performanceObject.Insert(index, value);

			return this;
		}
        //
        // Summary:
        //     Inserts the string representation of a specified 8-bit signed integer into this
        //     instance at the specified character position.
        //
        // Parameters:
        //   index:
        //     The position in this instance where insertion begins.
        //
        //   value:
        //     The value to insert.
        //
        // Returns:
        //     A reference to this instance after the insert operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     index is less than zero or greater than the length of this instance.
        //
        //   T:System.OutOfMemoryException:
        //     Enlarging the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
        //[CLSCompliant(false)]
        public StringBuilder Insert(int index, sbyte value) {
			_performanceObject.Insert(index, value);

			return this;
		}
        //
        // Summary:
        //     Inserts the string representation of an object into this instance at the specified
        //     character position.
        //
        // Parameters:
        //   index:
        //     The position in this instance where insertion begins.
        //
        //   value:
        //     The object to insert, or null.
        //
        // Returns:
        //     A reference to this instance after the insert operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     index is less than zero or greater than the length of this instance.
        //
        //   T:System.OutOfMemoryException:
        //     Enlarging the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder Insert(int index, object value) {
			_performanceObject.Insert(index, value);

			return this;
		}
        //
        // Summary:
        //     Inserts the string representation of a 64-bit signed integer into this instance
        //     at the specified character position.
        //
        // Parameters:
        //   index:
        //     The position in this instance where insertion begins.
        //
        //   value:
        //     The value to insert.
        //
        // Returns:
        //     A reference to this instance after the insert operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     index is less than zero or greater than the length of this instance.
        //
        //   T:System.OutOfMemoryException:
        //     Enlarging the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder Insert(int index, long value) {
			_performanceObject.Insert(index, value);

			return this;
		}
        //
        // Summary:
        //     Inserts the string representation of a specified 32-bit signed integer into this
        //     instance at the specified character position.
        //
        // Parameters:
        //   index:
        //     The position in this instance where insertion begins.
        //
        //   value:
        //     The value to insert.
        //
        // Returns:
        //     A reference to this instance after the insert operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     index is less than zero or greater than the length of this instance.
        //
        //   T:System.OutOfMemoryException:
        //     Enlarging the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder Insert(int index, int value) {
			_performanceObject.Insert(index, value);

			return this;
		}
        //
        // Summary:
        //     Inserts the string representation of a specified 16-bit signed integer into this
        //     instance at the specified character position.
        //
        // Parameters:
        //   index:
        //     The position in this instance where insertion begins.
        //
        //   value:
        //     The value to insert.
        //
        // Returns:
        //     A reference to this instance after the insert operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     index is less than zero or greater than the length of this instance.
        //
        //   T:System.OutOfMemoryException:
        //     Enlarging the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder Insert(int index, short value) {
			_performanceObject.Insert(index, value);

			return this;
		}
        //
        // Summary:
        //     Inserts the string representation of a double-precision floating-point number
        //     into this instance at the specified character position.
        //
        // Parameters:
        //   index:
        //     The position in this instance where insertion begins.
        //
        //   value:
        //     The value to insert.
        //
        // Returns:
        //     A reference to this instance after the insert operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     index is less than zero or greater than the length of this instance.
        //
        //   T:System.OutOfMemoryException:
        //     Enlarging the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder Insert(int index, double value) {
			_performanceObject.Insert(index, value);

			return this;
		}
        //
        // Summary:
        //     Inserts the string representation of a decimal number into this instance at the
        //     specified character position.
        //
        // Parameters:
        //   index:
        //     The position in this instance where insertion begins.
        //
        //   value:
        //     The value to insert.
        //
        // Returns:
        //     A reference to this instance after the insert operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     index is less than zero or greater than the length of this instance.
        //
        //   T:System.OutOfMemoryException:
        //     Enlarging the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder Insert(int index, decimal value) {
			_performanceObject.Insert(index, value);

			return this;
		}
        //
        // Summary:
        //     Inserts the string representation of a single-precision floating point number
        //     into this instance at the specified character position.
        //
        // Parameters:
        //   index:
        //     The position in this instance where insertion begins.
        //
        //   value:
        //     The value to insert.
        //
        // Returns:
        //     A reference to this instance after the insert operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     index is less than zero or greater than the length of this instance.
        //
        //   T:System.OutOfMemoryException:
        //     Enlarging the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder Insert(int index, float value) {
			_performanceObject.Insert(index, value);

			return this;
		}
        //
        // Summary:
        //     Inserts a string into this instance at the specified character position.
        //
        // Parameters:
        //   index:
        //     The position in this instance where insertion begins.
        //
        //   value:
        //     The string to insert.
        //
        // Returns:
        //     A reference to this instance after the insert operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     index is less than zero or greater than the current length of this instance.
        //     -or- The current length of this System.Text.StringBuilder object plus the length
        //     of value exceeds System.Text.StringBuilder.MaxCapacity.
        public StringBuilder Insert(int index, string value) {
			_performanceObject.Insert(index, value);

			return this;
		}
        //
        // Summary:
        //     Removes the specified range of characters from this instance.
        //
        // Parameters:
        //   startIndex:
        //
        //   length:
        //     The number of characters to remove.
        //
        // Returns:
        //     A reference to this instance after the excise operation has completed.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     If startIndex or length is less than zero, or startIndex + length is greater
        //     than the length of this instance.
        public StringBuilder Remove(int startIndex, int length) {
			_performanceObject.Remove(startIndex, length);

			return this;
		}
        //
        // Summary:
        //     Replaces all occurrences of a specified character in this instance with another
        //     specified character.
        //
        // Parameters:
        //   oldChar:
        //     The character to replace.
        //
        //   newChar:
        //     The character that replaces oldChar.
        //
        // Returns:
        //     A reference to this instance with oldChar replaced by newChar.
        public StringBuilder Replace(char oldChar, char newChar) {
			_performanceObject.Replace(oldChar, newChar);

			return this;
		}
        //
        // Summary:
        //     Replaces, within a substring of this instance, all occurrences of a specified
        //     character with another specified character.
        //
        // Parameters:
        //   oldChar:
        //     The character to replace.
        //
        //   newChar:
        //     The character that replaces oldChar.
        //
        //   startIndex:
        //     The position in this instance where the substring begins.
        //
        //   count:
        //     The length of the substring.
        //
        // Returns:
        //     A reference to this instance with oldChar replaced by newChar in the range from
        //     startIndex to startIndex + count -1.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     startIndex + count is greater than the length of the value of this instance.
        //     -or- startIndex or count is less than zero.
        public StringBuilder Replace(char oldChar, char newChar, int startIndex, int count) {
			_performanceObject.Replace(oldChar, newChar, startIndex, count);

			return this;
		}
        //
        // Summary:
        //     Replaces all occurrences of a specified string in this instance with another
        //     specified string.
        //
        // Parameters:
        //   oldValue:
        //     The string to replace.
        //
        //   newValue:
        //     The string that replaces oldValue, or null.
        //
        // Returns:
        //     A reference to this instance with all instances of oldValue replaced by newValue.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     oldValue is null.
        //
        //   T:System.ArgumentException:
        //     The length of oldValue is zero.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     Enlarging the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder Replace(string oldValue, string newValue) {
			_performanceObject.Replace(oldValue, newValue);

			return this;
		}
        //
        // Summary:
        //     Replaces, within a substring of this instance, all occurrences of a specified
        //     string with another specified string.
        //
        // Parameters:
        //   oldValue:
        //     The string to replace.
        //
        //   newValue:
        //     The string that replaces oldValue, or null.
        //
        //   startIndex:
        //     The position in this instance where the substring begins.
        //
        //   count:
        //     The length of the substring.
        //
        // Returns:
        //     A reference to this instance with all instances of oldValue replaced by newValue
        //     in the range from startIndex to startIndex + count - 1.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     oldValue is null.
        //
        //   T:System.ArgumentException:
        //     The length of oldValue is zero.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     startIndex or count is less than zero. -or- startIndex plus count indicates a
        //     character position not within this instance. -or- Enlarging the value of this
        //     instance would exceed System.Text.StringBuilder.MaxCapacity.
        public StringBuilder Replace(string oldValue, string newValue, int startIndex, int count) {
			_performanceObject.Replace(oldValue, newValue, startIndex, count);

			return this;
		}
        //
        // Summary:
        //     Converts the value of this instance to a System.String.
        //
        // Returns:
        //     A string whose value is the same as this instance.
        public override string ToString() => _performanceObject.ToString();

        //
        // Summary:
        //     Converts the value of a substring of this instance to a System.String.
        //
        // Parameters:
        //   startIndex:
        //     The starting position of the substring in this instance.
        //
        //   length:
        //     The length of the substring.
        //
        // Returns:
        //     A string whose value is the same as the specified substring of this instance.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     startIndex or length is less than zero. -or- The sum of startIndex and length
        //     is greater than the length of the current instance.
        public string ToString(int startIndex, int length) => _performanceObject.ToString(startIndex, length);

        public override void Dispose() => Dispose(false);

        internal void Dispose(bool leaveContents) {
            if (!leaveContents) {
                _performanceObject.Clear();
            }
            
            base.Dispose();
        }

        protected override IPooledObjectPolicy<Text.StringBuilder> getPooledObjectPolicyFactory() {
            if (_maximumRetainedCapacity.HasValue) {
                return new StringBuilderPooledObjectPolicy(_capacity.Value, _maximumRetainedCapacity.Value);
            } else if (_capacity.HasValue) {
                return new StringBuilderPooledObjectPolicy(_capacity.Value);
            }
            
            return new StringBuilderPooledObjectPolicy();
        }

        public class StringBuilderPooledObjectPolicy : PooledObjectPolicy<Text.StringBuilder> {
            private const int InitialCapacityMinimum = 100;
            private const int MaximumRetainedCapacityMinimum = 4 * 1024;

            /// <summary>
            /// Default InitialCapacity is 100.
            /// </summary>
            public int? InitialCapacity { get; }

            /// <summary>
            /// Null value means retain no matter what size.
            /// </summary>
            public int? MaximumRetainedCapacity { get; }

            public StringBuilderPooledObjectPolicy() {
                InitialCapacity = InitialCapacityMinimum;
                MaximumRetainedCapacity = MaximumRetainedCapacity;
            }

            public StringBuilderPooledObjectPolicy(int? initialCapacity) {
                InitialCapacity = initialCapacity;
                MaximumRetainedCapacity = MaximumRetainedCapacityMinimum;
            }

            public StringBuilderPooledObjectPolicy(int? initialCapacity, int? maximumRetainedCapacity) {
                InitialCapacity = initialCapacity;
                MaximumRetainedCapacity = maximumRetainedCapacity;
            }

            public override Text.StringBuilder Create() {
                if (InitialCapacity.HasValue) {
                    return new Text.StringBuilder(InitialCapacity.Value);
                }
                
                return new Text.StringBuilder();
            }

            public override bool Equals(object obj) {
                return obj is StringBuilderPooledObjectPolicy policy &&
                       InitialCapacity == policy.InitialCapacity &&
                       ((!MaximumRetainedCapacity.HasValue && !policy.MaximumRetainedCapacity.HasValue) || MaximumRetainedCapacity == policy.MaximumRetainedCapacity);
            }

            public override int GetHashCode() {
                int hashCode = -686918596;
                hashCode = hashCode * -1521134295 + InitialCapacity.GetHashCode();
                hashCode = hashCode * -1521134295 + MaximumRetainedCapacity.GetHashCode();
                return hashCode;
            }

            public override bool ShouldReturn(Text.StringBuilder obj) {
                if (MaximumRetainedCapacity.HasValue && obj.Capacity > MaximumRetainedCapacity) {
                    // Too big. Discard this one.
                    return false;
                }

                return true;
            }
        }
    }
}

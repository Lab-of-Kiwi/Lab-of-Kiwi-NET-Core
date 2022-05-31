using System;
using System.Text;

namespace LabOfKiwi.Text
{
    /// <summary>
    /// An object that is used to construct a sequence of text elements separated by a delimiter and optionally starting
    /// with a supplied prefix and ending with a supplied suffix.
    /// </summary>
    public sealed class StringJoiner
    {
        private readonly string _prefix;
        private readonly string _delimiter;
        private readonly string _suffix;
        private StringBuilder? _value;
        private string _emptyValue;

        /// <summary>
        /// Constructs a <see cref="StringJoiner"/> instance using the provided delimiter, and optional prefix and
        /// suffix.
        /// </summary>
        /// 
        /// <param name="delimiter">The text to be used between each element added.</param>
        /// <param name="prefix">The text to be included at the beginning.</param>
        /// <param name="suffix">The text to be included at the end.</param>
        /// 
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="delimiter"/>, <paramref name="prefix"/>, or <paramref name="suffix"/> are <c>null</c>.
        /// </exception>
        public StringJoiner(string delimiter, string prefix = "", string suffix = "")
        {
            _delimiter = delimiter ?? throw new ArgumentNullException(nameof(delimiter));
            _prefix = prefix ?? throw new ArgumentNullException(nameof(prefix));
            _suffix = suffix ?? throw new ArgumentNullException(nameof(suffix));
            _emptyValue = prefix + suffix;
        }

        /// <summary>
        /// The current length of the text represented by this instance.
        /// </summary>
        public int Length => _value != null ? _value.Length + _suffix.Length : _emptyValue.Length;

        /// <summary>
        /// Adds the provided value as an element to this instance.
        /// </summary>
        /// 
        /// <param name="value">The element to add to this instance.</param>
        /// <returns>This instance for method chaining.</returns>
        public StringJoiner Add(string value)
        {
            PrepareBuilder().Append(value);
            return this;
        }

        /// <summary>
        /// Adds the text of the provided <see cref="StringJoiner"/> instance with its delimiter separating its elements
        /// as the next element of this instance. If <paramref name="other"/> is empty, nothing happens.
        /// </summary>
        /// 
        /// <param name="other">The <see cref="StringJoiner"/> whose text is added to this instance.</param>
        /// <returns>This instance for method chaining.</returns>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="other"/> is <c>null</c>.</exception>
        public StringJoiner Merge(StringJoiner other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            if (other._value != null)
            {
                int length = other._value.Length - other._prefix.Length;
                StringBuilder builder = PrepareBuilder();
                builder.Append(other._value.ToString(), other._prefix.Length, length);
            }

            return this;
        }

        /// <summary>
        /// Sets the text that this instance represents when no elements have been added.
        /// </summary>
        /// 
        /// <param name="value">The text that this instance represents when no elements have been added.</param>
        /// <returns>This instance for method chaining.</returns>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public StringJoiner SetEmptyValue(string value)
        {
            _emptyValue = value ?? throw new ArgumentNullException(nameof(value));
            return this;
        }

        /// <summary>
        /// Returns the text representation of this instance.
        /// </summary>
        /// <returns>The <see cref="string"/> representation of this instance.</returns>
        public override string ToString()
        {
            if (_value == null)
            {
                return _emptyValue;
            }
            else if (_suffix == string.Empty)
            {
                return _value.ToString();
            }
            else
            {
                int initialLength = _value.Length;
                string result = _value.Append(_suffix).ToString();
                _value.Length = initialLength;
                return result;
            }
        }

        // Internal helper method.
        private StringBuilder PrepareBuilder()
        {
            if (_value != null)
            {
                _value.Append(_delimiter);
            }
            else
            {
                _value = new StringBuilder().Append(_prefix);
            }

            return _value;
        }
    }
}

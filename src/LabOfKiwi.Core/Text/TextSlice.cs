using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LabOfKiwi.Text;

/// <summary>
/// Represents a slice of text already allocated for easy, memory-friendly, parsing.
/// </summary>
public readonly struct TextSlice : IEquatable<TextSlice>, IReadOnlyList<char>
{
    private readonly string? _text;
    private readonly int _startIndex;
    private readonly int _length;

    // Internal constructor
    private TextSlice(string text, int start, int length)
    {
        _text = text;
        _startIndex = start;
        _length = length;
    }

    #region Public Properties
    /// <summary>
    /// Gets the character at the specified zero-based index.
    /// </summary>
    /// 
    /// <param name="index">The zero-based index of the character.</param>
    /// <returns>The character at the specified index.</returns>
    /// 
    /// <exception cref="IndexOutOfRangeException">
    ///     <paramref name="index"/> is less than zero or greater than or equal to <see cref="Length"/>.
    /// </exception>
    public char this[int index]
    {
        get
        {
            if ((uint)index >= (uint)_length)
            {
                throw new IndexOutOfRangeException();
            }

            return _text![index + _startIndex];
        }
    }

    /// <summary>
    /// Returns a value that indicates whether the current <see cref="TextSlice"/> is empty.
    /// </summary>
    public bool IsEmpty => _length == 0;

    /// <summary>
    /// Returns the length of the current <see cref="TextSlice"/>.
    /// </summary>
    public int Length => _length;
    #endregion

    #region Public Methods
    /// <summary>
    /// Searches for a character that matches the conditions defined by the specified predicate, and returns the
    /// zero-based index of the first occurrence within the entire <see cref="TextSlice"/>.
    /// </summary>
    /// 
    /// <param name="match">
    ///     The <see cref="Predicate{T}"/> delegate that defines the conditions of the character to search for.
    /// </param>
    /// <returns>
    ///     The zero-based index of the first occurrence of a character that matches the conditions defined by
    ///     <paramref name="match"/>, if found; otherwise, -1.
    /// </returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="match"/> is <c>null</c>.</exception>
    public int FindIndex(Predicate<char> match)
    {
        if (match == null)
        {
            throw new ArgumentNullException(nameof(match));
        }

        int lastIndex = LastIndex;

        for (int i = _startIndex; i < lastIndex; i++)
        {
            if (match(_text![i]))
            {
                return i - _startIndex;
            }
        }

        return -1;
    }

    /// <summary>
    /// Searches for a character that matches the conditions defined by the specified predicate, and returns the
    /// zero-based index of the last occurrence within the entire <see cref="TextSlice"/>.
    /// </summary>
    /// 
    /// <param name="match">
    ///     The <see cref="Predicate{T}"/> delegate that defines the conditions of the character to search for.
    /// </param>
    /// <returns>
    ///     The zero-based index of the last occurrence of a character that matches the conditions defined by
    ///     <paramref name="match"/>, if found; otherwise, -1.
    /// </returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="match"/> is <c>null</c>.</exception>
    public int FindLastIndex(Predicate<char> match)
    {
        if (match == null)
        {
            throw new ArgumentNullException(nameof(match));
        }

        for (int i = LastIndex - 1; i >= _startIndex; i--)
        {
            if (match(_text![i]))
            {
                return i - _startIndex;
            }
        }

        return -1;
    }

    /// <summary>
    /// Searches for the specified character and returns the zero-based index of the first occurrence within the entire
    /// <see cref="TextSlice"/>.
    /// </summary>
    /// 
    /// <param name="c">The character to locate in the <see cref="TextSlice"/>.</param>
    /// <returns>
    ///     The zero-based index of the first occurrence of <paramref name="c"/> within the entire
    ///     <see cref="TextSlice"/>, if found; otherwise, -1.
    /// </returns>
    public int IndexOf(char c)
    {
        int lastIndex = LastIndex;

        for (int i = _startIndex; i < lastIndex; i++)
        {
            if (c == _text![i])
            {
                return i - _startIndex;
            }
        }

        return -1;
    }

    /// <summary>
    /// Searches for the specified sequence of characters and returns the zero-based index of the first occurrence
    /// within the entire <see cref="TextSlice"/>.
    /// </summary>
    /// 
    /// <param name="s">The sequence of characters to locate in the <see cref="TextSlice"/>.</param>
    /// <returns>
    ///     The zero-based index of the first occurrence of <paramref name="s"/> within the entire
    ///     <see cref="TextSlice"/>, if found; otherwise, -1. If <paramref name="s"/> is <c>null</c> or empty, then 0
    ///     is returned.
    /// </returns>
    public int IndexOf(string? s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return 0;
        }

        int lastIndex = LastIndex - s.Length;

        for (int i = _startIndex; i < lastIndex; i++)
        {
            bool isMatch = true;

            for (int j = 0; j < s.Length; j++)
            {
                if (s[j] != _text![i + j])
                {
                    isMatch = false;
                    break;
                }
            }

            if (isMatch)
            {
                return i - _startIndex;
            }
        }

        return -1;
    }

    /// <summary>
    /// Searches for the specified character and returns the zero-based index of the last occurrence within the entire
    /// <see cref="TextSlice"/>.
    /// </summary>
    /// 
    /// <param name="c">The character to locate in the <see cref="TextSlice"/>.</param>
    /// <returns>
    ///     The zero-based index of the last occurrence of <paramref name="c"/> within the entire
    ///     <see cref="TextSlice"/>, if found; otherwise, -1.
    /// </returns>
    public int LastIndexOf(char c)
    {
        for (int i = LastIndex - 1; i >= _startIndex; i--)
        {
            if (c == _text![i])
            {
                return i - _startIndex;
            }
        }

        return -1;
    }

    /// <summary>
    /// Searches for the specified sequence of characters and returns the zero-based index of the last occurrence
    /// within the entire <see cref="TextSlice"/>.
    /// </summary>
    /// 
    /// <param name="s">The sequence of characters to locate in the <see cref="TextSlice"/>.</param>
    /// <returns>
    ///     The zero-based index of the last occurrence of <paramref name="s"/> within the entire
    ///     <see cref="TextSlice"/>, if found; otherwise, -1. If <paramref name="s"/> is <c>null</c> or empty, then
    ///     <see cref="Length"/> is returned.
    /// </returns>
    public int LastIndexOf(string? s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return _length;
        }

        for (int i = LastIndex - s.Length - 1; i >= _startIndex; i--)
        {
            bool isMatch = true;

            for (int j = 0; j < s.Length; j++)
            {
                if (s[j] != _text![i + j])
                {
                    isMatch = false;
                    break;
                }
            }

            if (isMatch)
            {
                return i - _startIndex;
            }
        }

        return -1;
    }

    public TextSlice Slice(int start)
    {
        if ((uint)start > (uint)_length)
        {
            throw new ArgumentOutOfRangeException(nameof(start));
        }

        int length = _length - start;

        if (length == 0)
        {
            return default;
        }

        return new TextSlice(_text!, start + _startIndex, length);
    }

    public TextSlice Slice(int start, int length)
    {
        if ((uint)start > (uint)_length)
        {
            throw new ArgumentOutOfRangeException(nameof(start));
        }

        if (length < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(length));
        }

        if ((uint)length > (uint)(_length - start))
        {
            throw new ArgumentException("Invalid offset.");
        }

        if (length == 0)
        {
            return default;
        }

        return new TextSlice(_text!, start + _startIndex, length);
    }

    public TextSlice SliceAfter(char value)
    {
        int index = IndexOf(value);

        if (index == -1)
        {
            return default;
        }

        return this[(index + 1)..];
    }

    public TextSlice SliceAfter(Predicate<char> predicate)
    {
        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        int index = FindIndex(predicate);

        if (index == -1)
        {
            return default;
        }

        return this[(index + 1)..];
    }

    public TextSlice SliceAfter(string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return this;
        }

        int index = IndexOf(value);

        if (index == -1)
        {
            return default;
        }

        return this[(index + value.Length)..];
    }

    public TextSlice SliceAfterNewLine(NewlineOption newlineOption = NewlineOption.Any)
    {
        if (newlineOption == NewlineOption.Windows)
        {
            return SliceAfter("\r\n");
        }

        else if (newlineOption == NewlineOption.Linux)
        {
            return SliceAfter('\n');
        }

        else if (newlineOption == NewlineOption.Mac)
        {
            return SliceAfter('\r');
        }

        else if (newlineOption == (NewlineOption.Windows | NewlineOption.Linux))
        {
            return SliceAfter('\n');
        }

        else if (newlineOption == (NewlineOption.Windows | NewlineOption.Mac))
        {
            int index = IndexOf('\r');

            if (index == -1)
            {
                return default;
            }

            // Lets check to see if next is \n. If so increment index by one to treat it as a Windows newline found.
            if (TryGet(index + 1, out char next) && next == '\n')
            {
                index++;
            }

            return this[(index + 1)..];
        }

        else if (newlineOption == (NewlineOption.Linux | NewlineOption.Mac))
        {
            return SliceAfter(c => (c == '\r' || c == '\n'));
        }

        else if (newlineOption == NewlineOption.Any)
        {
            int index = FindIndex(c => (c == '\r' || c == '\n'));

            if (index == -1)
            {
                return default;
            }

            // If found was \r, lets check to see if next is \n. If so increment index by one to treat it as a Windows newline found.
            if (_text![index + _startIndex] == '\r' && TryGet(index + 1, out char next) && next == '\n')
            {
                index++;
            }

            return this[(index + 1)..];
        }

        return default;
    }

    public TextSlice SliceAfterWhiteSpace()
    {
        int index = Find(c => char.IsWhiteSpace(c), out int count);

        if (index == -1)
        {
            return default;
        }

        return this[(index + count)..];
    }

    public TextSlice SliceBefore(char value)
    {
        int index = IndexOf(value);

        if (index == -1)
        {
            return this;
        }

        return this[..index];
    }

    public TextSlice SliceBefore(Predicate<char> predicate)
    {
        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        int index = FindIndex(predicate);

        if (index == -1)
        {
            return this;
        }

        return this[..index];
    }

    public TextSlice SliceBefore(string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return default;
        }

        int index = IndexOf(value);

        if (index == -1)
        {
            return this;
        }

        return this[..index];
    }

    public TextSlice SliceBeforeNewLine(NewlineOption newlineOption = NewlineOption.Any)
    {
        if (newlineOption == NewlineOption.Windows)
        {
            return SliceBefore("\r\n");
        }
        else if (newlineOption == NewlineOption.Linux)
        {
            return SliceBefore('\n');
        }
        else if (newlineOption == NewlineOption.Mac)
        {
            return SliceBefore('\r');
        }
        else if (newlineOption == (NewlineOption.Windows | NewlineOption.Linux))
        {
            int index = IndexOf('\n');

            if (index == -1)
            {
                return this;
            }

            // Lets check to see if previous is \r. If so decrement index by one to treat it as a Windows newline found.
            if (TryGet(index - 1, out char prev) && prev == '\r')
            {
                index--;
            }

            return this[..index];
        }
        else if (newlineOption == (NewlineOption.Windows | NewlineOption.Mac))
        {
            return SliceBefore('\r');
        }
        else if (newlineOption == (NewlineOption.Linux | NewlineOption.Mac))
        {
            return SliceBefore(c => (c == '\r' || c == '\n'));
        }
        else if (newlineOption == NewlineOption.Any)
        {
            return SliceBefore(c => (c == '\r' || c == '\n'));
        }

        return this;
    }

    public TextSlice SliceBeforeWhiteSpace()
    {
        int index = FindIndex(c => char.IsWhiteSpace(c));

        if (index == -1)
        {
            return this;
        }

        return this[..index];
    }

    public (TextSlice Before, TextSlice After) SplitOnceAt(char value)
    {
        int index = IndexOf(value);

        if (index == -1)
        {
            return (this, default);
        }

        return (this[..index], this[(index + 1)..]);
    }

    public (TextSlice Before, TextSlice After) SplitOnceAt(Predicate<char> predicate)
    {
        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        int index = FindIndex(predicate);

        if (index == -1)
        {
            return (this, default);
        }

        return (this[..index], this[(index + 1)..]);
    }

    public (TextSlice Before, TextSlice After) SplitOnceAt(string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return (default, this);
        }

        int index = IndexOf(value);

        if (index == -1)
        {
            return (this, default);
        }

        return (this[..index], this[(index + value.Length)..]);
    }

    public bool TryGet(int index, out char value)
    {
        if ((uint)index >= (uint)_length)
        {
            value = default;
            return false;
        }

        value = _text![index + _startIndex];
        return true;
    }
    #endregion

    #region Standard Methods
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is TextSlice other && Equals(other);
    }

    public bool Equals(TextSlice other)
    {
        if (_length != other._length)
        {
            return false;
        }

        for (int i = 0; i < _length; i++)
        {
            char c1 = _text![i + _startIndex];
            char c2 = other._text![i + other._startIndex];

            if (c1 != c2)
            {
                return false;
            }
        }

        return true;
    }

    public IEnumerator<char> GetEnumerator()
    {
        for (int i = _startIndex; i < LastIndex; i++)
        {
            yield return _text![i];
        }
    }

    public override int GetHashCode()
    {
        HashCode hc = new();

        int lastIndex = LastIndex;

        for (int i = _startIndex; i < lastIndex; i++)
        {
            hc.Add(_text![i]);
        }

        return hc.ToHashCode();
    }

    public override string ToString()
    {
        return _text?.Substring(_startIndex, _length) ?? string.Empty;
    }
    #endregion

    #region Internal Properties / Methods
    // Last actual exclusive index.
    private int LastIndex => _startIndex + _length;

    // Internal method that finds the first occurrence of a character that matches the predicate, then counts how many
    // characters in a row match the same predicate.
    private int Find(Predicate<char> match, out int count)
    {
        int lastIndex = LastIndex;

        for (int i = _startIndex; i < lastIndex; i++)
        {
            if (match(_text![i]))
            {
                int cnt = 1;

                for (int j = i + 1; j < lastIndex; j++)
                {
                    if (match(_text![j]))
                    {
                        cnt++;
                    }
                    else
                    {
                        break;
                    }
                }

                count = cnt;
                return i - _startIndex;
            }
        }

        count = 0;
        return -1;
    }
    #endregion

    #region Operators
    public static implicit operator TextSlice(string? text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return default;
        }

        return new TextSlice(text, 0, text.Length);
    }

    public static implicit operator string(TextSlice slice)
    {
        return slice.ToString();
    }

    public static bool operator ==(TextSlice left, TextSlice right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(TextSlice left, TextSlice right)
    {
        return !(left == right);
    }
    #endregion

    #region Explicit Interface Implementations
    int IReadOnlyCollection<char>.Count => _length;

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    #endregion
}

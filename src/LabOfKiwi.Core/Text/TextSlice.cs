using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace LabOfKiwi.Text;

public readonly struct TextSlice : IEquatable<TextSlice>, IReadOnlyList<char>
{
    private readonly string? _text;
    private readonly int _startIndex;
    private readonly int _length;

    private TextSlice(string text, int start, int length)
    {
        _text = text;
        _startIndex = start;
        _length = length;
    }

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

    public bool IsEmpty => _length == 0;

    public int Length => _length;

    private int LastIndex => _startIndex + _length;

    int IReadOnlyCollection<char>.Count => _length;

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

    public int IndexOf(string? s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return 0;
        }

        int lastIndex = LastIndex - s.Length;

        for (int i = _startIndex; i <= lastIndex; i++)
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
                return i;
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

        return Slice(index + 1);
    }

    public TextSlice SliceAfter(Predicate<char> predicate)
    {
        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        int index = Find(predicate);

        if (index == -1)
        {
            return default;
        }

        return Slice(index + 1);
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

        return Slice(index + value.Length);
    }

    public TextSlice SliceAfterWhiteSpace()
    {
        int index = Find(c => char.IsWhiteSpace(c), out int count);

        if (index == -1)
        {
            return default;
        }

        return Slice(index + count);
    }

    public TextSlice SliceBefore(char value)
    {
        int index = IndexOf(value);

        if (index == -1)
        {
            return this;
        }

        return Slice(0, index);
    }

    public TextSlice SliceBefore(Predicate<char> predicate)
    {
        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }
        int index = Find(predicate);

        if (index == -1)
        {
            return this;
        }

        return Slice(0, index);
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

        return Slice(0, index);
    }

    public TextSlice SliceBeforeWhiteSpace()
    {
        int index = Find(c => char.IsWhiteSpace(c));

        if (index == -1)
        {
            return this;
        }

        return Slice(0, index);
    }

    public (TextSlice Before, TextSlice After) SplitOnceAt(char value)
    {
        int index = IndexOf(value);

        if (index == -1)
        {
            return (this, default);
        }

        return (Slice(0, index), Slice(index + 1));
    }

    public (TextSlice Before, TextSlice After) SplitOnceAt(Predicate<char> predicate)
    {
        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        int index = Find(predicate);

        if (index == -1)
        {
            return (this, default);
        }

        return (Slice(0, index), Slice(index + 1));
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

        return (Slice(0, index), Slice(index + value.Length));
    }

    public override string ToString()
    {
        return _text?.Substring(_startIndex, _length) ?? string.Empty;
    }

    private int Find(Predicate<char> predicate)
    {
        int lastIndex = LastIndex;

        for (int i = _startIndex; i < lastIndex; i++)
        {
            if (predicate(_text![i])) 
            {
                return i - _startIndex;
            }
        }

        return -1;
    }

    private int Find(Predicate<char> predicate, out int count)
    {
        int lastIndex = LastIndex;
        int index = -1;
        count = 0;

        for (int i = _startIndex; i < lastIndex; i++)
        {
            if (predicate(_text![i]))
            {
                if (index == -1)
                {
                    index = i - _startIndex;
                }

                count++;
            }

            // Break loop since we are done
            else if (index != -1)
            {
                break;
            }
        }

        return index;
    }

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

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

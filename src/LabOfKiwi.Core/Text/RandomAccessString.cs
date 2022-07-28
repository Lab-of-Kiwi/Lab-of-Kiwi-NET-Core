using LabOfKiwi.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LabOfKiwi.Text;

public class RandomAccessString : ITraverseableString
{
    private readonly string _str;
    private int _index;

    public RandomAccessString(string? str)
    {
        _str = str ?? string.Empty;
    }

    public char this[int index]
    {
        get
        {
            if ((uint)index >= (uint)_str.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            return _str[index];
        }
    }

    public int Index
    {
        get => _index;

        set
        {
            if ((uint)value > (uint)_str.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            _index = value;
        }
    }

    public int Length => _str.Length;

    public int Remaining
    {
        get => _str.Length - _index;

        set
        {
            if ((uint)value > _str.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            _index = _str.Length - value;
        }
    }

    public IEnumerator<char> GetEnumerator()
    {
        while (_index < _str.Length)
        {
            yield return _str[_index++];
        }
    }

    public int Move(int count)
    {
        int oldOffset = _index;
        int newOffset = oldOffset + count;

        newOffset = Math.Min(newOffset, _str.Length);
        newOffset = Math.Max(newOffset, 0);

        _index = newOffset;
        return newOffset - oldOffset;
    }

    public int MoveToStart()
    {
        int result = -_index;
        _index = 0;
        return result;
    }

    public int MoveToEnd()
    {
        int result = Remaining;
        _index = _str.Length;
        return result;
    }

    public int MoveWhileTrue(Predicate<char> predicate)
    {
        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        int startOffset = _index;

        while (_index < _str.Length)
        {
            char c = _str[_index];

            if (!predicate(c))
            {
                break;
            }
            
            _index++;
        }

        return _index - startOffset;
    }

    public bool NextEquals(char value)
    {
        if (TryPeek(out char current))
        {
            return current == value;
        }

        return false;
    }

    public bool NextEquals(string? value)
    {
        int remaining = Remaining;

        if (string.IsNullOrEmpty(value))
        {
            return remaining == 0;
        }

        if (value.Length > remaining)
        {
            return false;
        }

        string nextStr = _str.Substring(_index, value.Length);
        return value == nextStr;
    }

    public string Peek(int count)
    {
        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }

        count = Math.Min(count, Remaining);
        return _str.Substring(_index, count);
    }

    public int Peek(Span<char> array)
    {
        int count = Math.Min(Remaining, array.Length);

        for (int i = 0; i < count; i++)
        {
            array[i] = _str[_index + i];
        }

        return count;
    }

    public int Peek(char[] array, int offset, int count)
    {
        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }

        count = Math.Min(count, Remaining);
        _str.CopyTo(_index, array, offset, count);

        return count;
    }

    public string PeekAhead(int offset, int count)
    {
        if (offset < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(offset));
        }

        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }

        int index = _index + offset;
        index = Math.Min(index, _str.Length);

        count = Math.Min(count, _str.Length - index);
        return _str.Substring(index, count);
    }

    public string PeekAt(int index, int count)
    {
        if ((uint)index > (uint)_str.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }

        count = Math.Min(count, _str.Length - index);
        return _str.Substring(index, count);
    }

    public string PeekWhileTrue(Predicate<char> predicate)
    {
        char[] chars = ((IIndexedCollection<char>)this).PeekWhileTrue(predicate);

        if (chars.Length == 0)
        {
            return string.Empty;
        }

        return new string(chars);
    }

    public string Read(int count)
    {
        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }

        count = Math.Min(count, Remaining);

        if (count == 0)
        {
            return string.Empty;
        }

        string result = _str.Substring(_index, count);

        _index += count;
        return result;
    }
    
    public int Read(Span<char> array)
    {
        int count = Math.Min(array.Length, Remaining);

        for (int i = 0; i < count; i++)
        {
            array[i] = _str[_index + i];
        }

        _index += count;
        return count;
    }

    public int Read(char[] array, int offset, int count)
    {
        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }

        count = Math.Min(count, Remaining);
        _str.CopyTo(_index, array, offset, count);

        _index += count;
        return count;
    }

    public string ReadWhileTrue(Predicate<char> predicate)
    {
        char[] chars = ((ITraverseableCollection<char>)this).ReadWhileTrue(predicate);

        if (chars.Length == 0)
        {
            return string.Empty;
        }

        return new string(chars);
    }

    public bool TryPeek([MaybeNullWhen(false)] out char result)
    {
        if (Remaining > 0)
        {
            result = _str[_index];
            return true;
        }

        result = default;
        return false;
    }

    public bool TryRead([MaybeNullWhen(false)] out char result)
    {
        if (Remaining > 0)
        {
            result = _str[_index++];
            return true;
        }

        result = default;
        return false;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    char[] IIndexedCollection<char>.Peek(int count)
    {
        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }

        count = Math.Min(count, Remaining);

        if (count == 0)
        {
            return Array.Empty<char>();
        }

        char[] array = new char[count];
        _str.CopyTo(_index, array, 0, count);

        return array;
    }

    char[] IIndexedCollection<char>.PeekAhead(int offset, int count)
    {
        if (offset < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(offset));
        }

        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }

        int index = _index + offset;
        index = Math.Min(index, _str.Length);

        count = Math.Min(count, _str.Length - index);

        if (count == 0)
        {
            return Array.Empty<char>();
        }

        char[] array = new char[count];
        _str.CopyTo(index, array, 0, count);

        return array;
    }

    char[] IIndexedCollection<char>.PeekAt(int index, int count)
    {
        if ((uint)index > (uint)_str.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }

        count = Math.Min(count, _str.Length - index);

        if (count == 0)
        {
            return Array.Empty<char>();
        }

        char[] array = new char[count];
        _str.CopyTo(index, array, 0, count);

        return array;
    }

    char[] IIndexedCollection<char>.PeekWhileTrue(Predicate<char> predicate)
    {
        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        List<char> list = new();
        int offset = _index;

        while (offset < _str.Length)
        {
            char c = _str[offset];

            if (!predicate(c))
            {
                break;
            }

            list.Add(c);
            offset++;
        }

        return list.ToArray();
    }

    char[] ITraverseableCollection<char>.Read(int count)
    {
        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }

        count = Math.Min(count, Remaining);

        if (count == 0)
        {
            return Array.Empty<char>();
        }

        char[] array = new char[count];
        _str.CopyTo(_index, array, 0, count);

        _index += count;
        return array;
    }

    char[] ITraverseableCollection<char>.ReadWhileTrue(Predicate<char> predicate)
    {
        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        List<char> list = new();

        while (_index < _str.Length)
        {
            char c = _str[_index];

            if (!predicate(c))
            {
                break;
            }

            list.Add(c);
            _index++;
        }

        return list.ToArray();
    }
}

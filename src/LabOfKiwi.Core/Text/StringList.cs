using System;
using System.Diagnostics.CodeAnalysis;

namespace LabOfKiwi.Text;

public readonly partial struct StringList : IComparable, IComparable<StringList>, IEquatable<StringList>
{
    private readonly string? _value;

    internal StringList(string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            this = default;
            return;
        }

        _value = value;
    }

    private StringList(ElementList list, string? delimiter) : this(list.ToString(delimiter))
    {
    }

    private string Value => _value ?? string.Empty;

    public int CompareTo(object? obj)
    {
        if (obj is StringList other)
        {
            return CompareTo(other);
        }

        if (obj == null)
        {
            return 1;
        }

        throw new ArgumentException("Value must be of type StringList.");
    }

    public int CompareTo(StringList other)
    {
        return Value.CompareTo(other.Value);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is StringList other && Equals(other);
    }

    public bool Equals(StringList other)
    {
        return Value == other.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return Value;
    }

    public static implicit operator string(StringList value)
    {
        return value.Value;
    }

    public static explicit operator StringList(string? value)
    {
        return new StringList(value);
    }

    public static bool operator ==(StringList left, StringList right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(StringList left, StringList right)
    {
        return !(left == right);
    }

    public static bool operator <(StringList left, StringList right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator <=(StringList left, StringList right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >(StringList left, StringList right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator >=(StringList left, StringList right)
    {
        return left.CompareTo(right) >= 0;
    }

    public static StringList operator +(StringList left, StringList right)
    {
        return new StringList(left.Value + right.Value);
    }

    [Flags]
    public enum ModifyOption
    {
        RemoveEmpty = 0,
        IncludeEmptyBefore = 1,
        IncludeEmptyAfter = 2,
        IncludeEmpty = IncludeEmptyBefore | IncludeEmptyAfter
    }
}

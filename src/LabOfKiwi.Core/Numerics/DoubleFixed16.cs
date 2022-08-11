using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace LabOfKiwi.Numerics;

/// <summary>
/// Represents a signed 32bit fixed-point number, with 8bits for the integer portion.
/// </summary>
public readonly struct DoubleFixed16 : IComparable<DoubleFixed16>, IComparable, IEquatable<DoubleFixed16>
{
    private const int  BitShift = 48;
    private const long Converter = 1L << BitShift;

    public static readonly DoubleFixed16 MaxValue = new(long.MaxValue);
    public static readonly DoubleFixed16 MinValue = new(long.MinValue);

    internal readonly long _value;

    internal DoubleFixed16(long value)
    {
        _value = value;
    }

    #region Public Members
    public int CompareTo(object? obj)
    {
        if (obj == null)
        {
            return 1;
        }

        if (obj is DoubleFixed16 other)
        {
            return CompareTo(other);
        }

        throw new ArgumentException("Value must be of type DoubleFixed16.");
    }

    public int CompareTo(DoubleFixed16 other)
    {
        return _value.CompareTo(other._value);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is DoubleFixed16 other && Equals(other);
    }

    public bool Equals(DoubleFixed16 other)
    {
        return _value == other._value;
    }

    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }

    public override string ToString()
    {
        return ((double)this).ToString();
    }
    #endregion

    #region Non-Public Members
    private long IntegralPart => _value >> BitShift;

    internal bool IsNegative => BitOperations.LeadingZeroCount((ulong)_value) == 0;
    #endregion

    #region Cast to Signed Integer Types Operators
    public static explicit operator sbyte (DoubleFixed16 v) => (sbyte)v.IntegralPart;
    public static explicit operator short (DoubleFixed16 v) => (short)v.IntegralPart;
    public static explicit operator int   (DoubleFixed16 v) => (int)v.IntegralPart;
    public static explicit operator long  (DoubleFixed16 v) => v.IntegralPart;
    #endregion

    #region Cast to Unsigned Integer Types Operators
    public static explicit operator byte   (DoubleFixed16 v) => (byte)v.IntegralPart;
    public static explicit operator ushort (DoubleFixed16 v) => (ushort)v.IntegralPart;
    public static explicit operator uint   (DoubleFixed16 v) => (uint)v.IntegralPart;
    public static explicit operator ulong  (DoubleFixed16 v) => (ulong)v.IntegralPart;
    #endregion

    #region Cast to Floating-Point Types Operators
    public static explicit operator float  (DoubleFixed16 v) => v._value / (float)Converter;
    public static explicit operator double (DoubleFixed16 v) => v._value / (double)Converter;
    #endregion

    #region Cast from Signed Integer Types Operators
    public static implicit operator DoubleFixed16(sbyte v) => new(v << BitShift);
    public static implicit operator DoubleFixed16(short v) => new(v << BitShift);
    public static implicit operator DoubleFixed16(int v)   => new(v << BitShift);
    public static explicit operator DoubleFixed16(long v)  => new(v << BitShift);
    #endregion

    #region Cast from Unsigned Integer Types Operators
    public static implicit operator DoubleFixed16(byte v)   => new(v << BitShift);
    public static explicit operator DoubleFixed16(ushort v) => new(v << BitShift);
    public static explicit operator DoubleFixed16(uint v)   => new(v << BitShift);
    public static explicit operator DoubleFixed16(ulong v)  => new((long)(v << BitShift));
    #endregion

    #region Cast from Floating-Point Types Operators
    public static explicit operator DoubleFixed16(float v)  => new((long)(v * Converter));
    public static implicit operator DoubleFixed16(double v) => new((long)(v * Converter));
    #endregion

    #region Misc Casts
    public static implicit operator DoubleFixed16(UFixed7 v) => new((long)v._value << 23);
    public static implicit operator DoubleFixed16(UFixed8 v) => new((long)v._value << 24);
    #endregion

    #region Arithmetic Operators
    // Addition
    public static DoubleFixed16 operator +(DoubleFixed16 left, DoubleFixed16 right)
    {
        return new DoubleFixed16(left._value + right._value);
    }

    // Decrement
    public static DoubleFixed16 operator --(DoubleFixed16 v)
    {
        return new DoubleFixed16(v._value - Converter);
    }

    // Divide
    public static DoubleFixed16 operator /(DoubleFixed16 left, DoubleFixed16 right)
    {
        long value = (long)((BigInteger)Converter * left._value / right._value);
        return new DoubleFixed16(value);
    }

    // Increment
    public static DoubleFixed16 operator ++(DoubleFixed16 v)
    {
        return new DoubleFixed16(v._value + Converter);
    }

    // Multiply
    public static DoubleFixed16 operator *(DoubleFixed16 left, DoubleFixed16 right)
    {
        long value = (long)((BigInteger)left._value * right._value / Converter);
        return new DoubleFixed16(value);
    }

    // Subtraction
    public static DoubleFixed16 operator -(DoubleFixed16 left, DoubleFixed16 right)
    {
        return new DoubleFixed16(left._value - right._value);
    }

    // Unary Negate
    public static DoubleFixed16 operator -(DoubleFixed16 v)
    {
        return new DoubleFixed16((~v._value) + 1);
    }

    // Unary Positive
    public static DoubleFixed16 operator +(DoubleFixed16 v)
    {
        return v;
    }
    #endregion

    #region Comparison Operators
    public static bool operator ==(DoubleFixed16 left, DoubleFixed16 right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(DoubleFixed16 left, DoubleFixed16 right)
    {
        return !(left == right);
    }

    public static bool operator <(DoubleFixed16 left, DoubleFixed16 right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator <=(DoubleFixed16 left, DoubleFixed16 right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >(DoubleFixed16 left, DoubleFixed16 right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator >=(DoubleFixed16 left, DoubleFixed16 right)
    {
        return left.CompareTo(right) >= 0;
    }
    #endregion
}

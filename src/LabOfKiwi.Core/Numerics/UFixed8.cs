using System;
using System.Diagnostics.CodeAnalysis;

namespace LabOfKiwi.Numerics;

/// <summary>
/// Represents an unsigned 32bit fixed-point number, with 8bits for the integer portion.
/// </summary>
public readonly struct UFixed8 : IComparable<UFixed8>, IComparable, IEquatable<UFixed8>
{
    private const int  BitShift  = 24;
    private const uint Converter = 1U << BitShift;

    /// <summary>
    /// Maximum <see cref="UFixed8"/> value: <c>255.999999940395355224609375</c>
    /// </summary>
    public static readonly UFixed8 MaxValue = new(uint.MaxValue);

    /// <summary>
    /// Minimum <see cref="UFixed8"/> value: <c>0</c>
    /// </summary>
    public static readonly UFixed8 MinValue = new(0U);

    private readonly uint _value;

    private UFixed8(uint value)
    {
        _value = value;
    }

    #region Public Members
    public uint Bits => _value;

    public int Sign => _value != 0U ? 1 : 0;

    public int CompareTo(object? obj)
    {
        if (obj == null)
        {
            return 1;
        }

        if (obj is UFixed8 other)
        {
            return CompareTo(other);
        }

        throw new ArgumentException("Value must be of type Fixed8.");
    }

    public int CompareTo(UFixed8 other)
    {
        return _value.CompareTo(other._value);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is UFixed8 other && Equals(other);
    }

    public bool Equals(UFixed8 other)
    {
        return _value == other._value;
    }

    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }

    public override string ToString()
    {
        return ((decimal)this).ToString();
    }
    #endregion

    #region Non-Public Members
    private byte IntegralPart => (byte)(_value >> BitShift);
    #endregion

    #region Cast to Signed Integer Types Operators
    public static explicit operator sbyte (UFixed8 v) => (sbyte)v.IntegralPart;
    public static explicit operator short (UFixed8 v) => v.IntegralPart;
    public static explicit operator int   (UFixed8 v) => v.IntegralPart;
    public static explicit operator long  (UFixed8 v) => v.IntegralPart;
    #endregion

    #region Cast to Unsigned Integer Types Operators
    public static explicit operator byte   (UFixed8 v) => v.IntegralPart;
    public static explicit operator ushort (UFixed8 v) => v.IntegralPart;
    public static explicit operator uint   (UFixed8 v) => v.IntegralPart;
    public static explicit operator ulong  (UFixed8 v) => v.IntegralPart;
    #endregion

    #region Cast to Floating-Point Types Operators
    public static explicit operator float   (UFixed8 v) => v._value / (float)Converter;
    public static explicit operator double  (UFixed8 v) => v._value / (double)Converter;
    public static explicit operator decimal (UFixed8 v) => v._value / (decimal)Converter;
    #endregion

    #region Cast from Signed Integer Types Operators
    public static explicit operator UFixed8(sbyte v) => new((uint)(v << BitShift));
    public static explicit operator UFixed8(short v) => new((uint)(v << BitShift));
    public static implicit operator UFixed8(int v)   => new((uint)(v << BitShift));
    public static explicit operator UFixed8(long v)  => new((uint)(v << BitShift));
    #endregion

    #region Cast from Unsigned Integer Types Operators
    public static implicit operator UFixed8(byte v)   => new((uint)(v << BitShift));
    public static explicit operator UFixed8(ushort v) => new((uint)(v << BitShift));
    public static explicit operator UFixed8(uint v)   => new(v << BitShift);
    public static explicit operator UFixed8(ulong v)  => new((uint)(v << BitShift));
    #endregion

    #region Cast from Floating-Point Types Operators
    public static explicit operator UFixed8(float v)   => new((uint)(v * Converter));
    public static implicit operator UFixed8(double v)  => new((uint)(v * Converter));
    public static explicit operator UFixed8(decimal v) => new((uint)(v * Converter));
    #endregion

    #region Misc Casts
    public static explicit operator UFixed8(Fixed8 v)   => new(v.Bits);
    public static explicit operator UFixed8(Fixed16 v)  => new(v.Bits << 8);
    public static explicit operator UFixed8(UFixed7 v)  => new(v.Bits >> 1);
    public static explicit operator UFixed8(UFixed16 v) => new(v.Bits << 8);
    #endregion

    #region Arithmetic Operators
    // Addition
    public static UFixed8 operator +(UFixed8 left, UFixed8 right)
    {
        return new UFixed8(left._value + right._value);
    }

    // Decrement
    public static UFixed8 operator --(UFixed8 v)
    {
        return new UFixed8(v._value - Converter);
    }

    // Divide
    public static UFixed8 operator /(UFixed8 left, UFixed8 right)
    {
        uint value = (uint)((ulong)Converter * left._value / right._value);
        return new UFixed8(value);
    }

    // Increment
    public static UFixed8 operator ++(UFixed8 v)
    {
        return new UFixed8(v._value + Converter);
    }

    // Multiply
    public static UFixed8 operator *(UFixed8 left, UFixed8 right)
    {
        uint value = (uint)((ulong)left._value * right._value / Converter);
        return new UFixed8(value);
    }

    // Subtraction
    public static UFixed8 operator -(UFixed8 left, UFixed8 right)
    {
        return new UFixed8(left._value - right._value);
    }

    // Unary Negate
    public static DoubleFixed16 operator -(UFixed8 v)
    {
        long value = -v._value;
        return new DoubleFixed16(value << 24);
    }

    // Unary Positive
    public static UFixed8 operator +(UFixed8 v)
    {
        return v;
    }
    #endregion

    #region Comparison Operators
    public static bool operator ==(UFixed8 left, UFixed8 right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(UFixed8 left, UFixed8 right)
    {
        return !(left == right);
    }

    public static bool operator <(UFixed8 left, UFixed8 right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator <=(UFixed8 left, UFixed8 right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >(UFixed8 left, UFixed8 right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator >=(UFixed8 left, UFixed8 right)
    {
        return left.CompareTo(right) >= 0;
    }
    #endregion
}

using System;
using System.Diagnostics.CodeAnalysis;

namespace LabOfKiwi.Numerics;

/// <summary>
/// Represents a signed 32bit fixed-point number, with 8bits for the integer portion.
/// </summary>
public readonly struct Fixed8 : IComparable<Fixed8>, IComparable, IEquatable<Fixed8>
{
    private const int BitShift  = 24;
    private const int Converter = 1 << BitShift;

    /// <summary>
    /// Maximum <see cref="Fixed8"/> value: <c>127.999999940395355224609375</c>
    /// </summary>
    public static readonly Fixed8 MaxValue = new(int.MaxValue);

    /// <summary>
    /// Minimum <see cref="Fixed8"/> value: <c>-128</c>
    /// </summary>
    public static readonly Fixed8 MinValue = new(int.MinValue);

    private readonly int _value;

    private Fixed8(int value)
    {
        _value = value;
    }

    #region Public Members
    public uint Bits => (uint)_value;

    public int Sign => Math.Sign(_value);

    public int CompareTo(object? obj)
    {
        if (obj == null)
        {
            return 1;
        }

        if (obj is Fixed8 other)
        {
            return CompareTo(other);
        }

        throw new ArgumentException("Value must be of type Fixed8.");
    }

    public int CompareTo(Fixed8 other)
    {
        return _value.CompareTo(other._value);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Fixed8 other && Equals(other);
    }

    public bool Equals(Fixed8 other)
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
    private sbyte IntegralPart => (sbyte)(_value >> BitShift);
    #endregion

    #region Cast to Signed Integer Types Operators
    public static explicit operator sbyte (Fixed8 v) => v.IntegralPart;
    public static explicit operator short (Fixed8 v) => v.IntegralPart;
    public static explicit operator int   (Fixed8 v) => v.IntegralPart;
    public static explicit operator long  (Fixed8 v) => v.IntegralPart;
    #endregion

    #region Cast to Unsigned Integer Types Operators
    public static explicit operator byte   (Fixed8 v) => (byte)v.IntegralPart;
    public static explicit operator ushort (Fixed8 v) => (ushort)v.IntegralPart;
    public static explicit operator uint   (Fixed8 v) => (uint)v.IntegralPart;
    public static explicit operator ulong  (Fixed8 v) => (ulong)v.IntegralPart;
    #endregion

    #region Cast to Floating-Point Types Operators
    public static explicit operator float   (Fixed8 v) => v._value / (float)Converter;
    public static explicit operator double  (Fixed8 v) => v._value / (double)Converter;
    public static explicit operator decimal (Fixed8 v) => v._value / (decimal)Converter;
    #endregion

    #region Cast from Signed Integer Types Operators
    public static implicit operator Fixed8(sbyte v) => new(v << BitShift);
    public static explicit operator Fixed8(short v) => new(v << BitShift);
    public static implicit operator Fixed8(int v)   => new(v << BitShift);
    public static explicit operator Fixed8(long v)  => new((int)(v << BitShift));
    #endregion

    #region Cast from Unsigned Integer Types Operators
    public static explicit operator Fixed8(byte v)   => new(v << BitShift);
    public static explicit operator Fixed8(ushort v) => new(v << BitShift);
    public static explicit operator Fixed8(uint v)   => new((int)(v << BitShift));
    public static explicit operator Fixed8(ulong v)  => new((int)(v << BitShift));
    #endregion

    #region Cast from Floating-Point Types Operators
    public static explicit operator Fixed8(float v)   => new((int)(v * Converter));
    public static implicit operator Fixed8(double v)  => new((int)(v * Converter));
    public static explicit operator Fixed8(decimal v) => new((int)(v * Converter));
    #endregion

    #region Misc Casts
    public static explicit operator Fixed8(Fixed16 v)  => new((int)v.Bits << 8);
    public static explicit operator Fixed8(UFixed7 v)  => new((int)(v.Bits >> 1));
    public static explicit operator Fixed8(UFixed8 v)  => new((int)v.Bits);
    public static explicit operator Fixed8(UFixed16 v) => new((int)v.Bits << 8);
    #endregion

    #region Arithmetic Operators
    // Addition
    public static Fixed8 operator +(Fixed8 left, Fixed8 right)
    {
        return new Fixed8(left._value + right._value);
    }

    // Decrement
    public static Fixed8 operator --(Fixed8 v)
    {
        return new Fixed8(v._value - Converter);
    }

    // Divide
    public static Fixed8 operator /(Fixed8 left, Fixed8 right)
    {
        int value = (int)((long)Converter * left._value / right._value);
        return new Fixed8(value);
    }

    // Increment
    public static Fixed8 operator ++(Fixed8 v)
    {
        return new Fixed8(v._value + Converter);
    }

    // Multiply
    public static Fixed8 operator *(Fixed8 left, Fixed8 right)
    {
        int value = (int)((long)left._value * right._value / Converter);
        return new Fixed8(value);
    }

    // Subtraction
    public static Fixed8 operator -(Fixed8 left, Fixed8 right)
    {
        return new Fixed8(left._value - right._value);
    }

    // Unary Negate
    public static Fixed8 operator -(Fixed8 v)
    {
        return new Fixed8((~v._value) + 1);
    }

    // Unary Positive
    public static Fixed8 operator +(Fixed8 v)
    {
        return v;
    }
    #endregion

    #region Comparison Operators
    public static bool operator ==(Fixed8 left, Fixed8 right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Fixed8 left, Fixed8 right)
    {
        return !(left == right);
    }

    public static bool operator <(Fixed8 left, Fixed8 right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator <=(Fixed8 left, Fixed8 right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >(Fixed8 left, Fixed8 right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator >=(Fixed8 left, Fixed8 right)
    {
        return left.CompareTo(right) >= 0;
    }
    #endregion
}

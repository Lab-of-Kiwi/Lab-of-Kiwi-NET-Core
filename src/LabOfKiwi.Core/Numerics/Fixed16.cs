using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace LabOfKiwi.Numerics;

/// <summary>
/// Represents a signed 32bit fixed-point number, with 16bits for the integer portion.
/// </summary>
public readonly struct Fixed16 : IComparable<Fixed16>, IComparable, IEquatable<Fixed16>
{
    private const int BitShift  = 16;
    private const int Converter = 1 << BitShift;

    public static readonly Fixed16 MaxValue = new(int.MaxValue);
    public static readonly Fixed16 MinValue = new(int.MinValue);

    internal readonly int _value;

    private Fixed16(int value)
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

        if (obj is Fixed16 other)
        {
            return CompareTo(other);
        }

        throw new ArgumentException("Value must be of type Fixed16.");
    }

    public int CompareTo(Fixed16 other)
    {
        return _value.CompareTo(other._value);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Fixed16 other && Equals(other);
    }

    public bool Equals(Fixed16 other)
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
    private int IntegralPart => _value >> BitShift;

    internal bool IsNegative => BitOperations.LeadingZeroCount((uint)_value) == 0;
    #endregion

    #region Cast to Signed Integer Types Operators
    public static explicit operator sbyte (Fixed16 v) => (sbyte)v.IntegralPart;
    public static explicit operator short (Fixed16 v) => (short)v.IntegralPart;
    public static explicit operator int   (Fixed16 v) => v.IntegralPart;
    public static explicit operator long  (Fixed16 v) => v.IntegralPart;
    #endregion

    #region Cast to Unsigned Integer Types Operators
    public static explicit operator byte   (Fixed16 v) => (byte)v.IntegralPart;
    public static explicit operator ushort (Fixed16 v) => (ushort)v.IntegralPart;
    public static explicit operator uint   (Fixed16 v) => (uint)v.IntegralPart;
    public static explicit operator ulong  (Fixed16 v) => (ulong)v.IntegralPart;
    #endregion

    #region Cast to Floating-Point Types Operators
    public static explicit operator float  (Fixed16 v)  => v._value / (float)Converter;
    public static explicit operator double (Fixed16 v)  => v._value / (double)Converter;
    #endregion

    #region Cast from Signed Integer Types Operators
    public static implicit operator Fixed16(sbyte v) => new(v << BitShift);
    public static implicit operator Fixed16(short v) => new(v << BitShift);
    public static implicit operator Fixed16(int v)   => new(v << BitShift);
    public static explicit operator Fixed16(long v)  => new((int)(v << BitShift));
    #endregion

    #region Cast from Unsigned Integer Types Operators
    public static implicit operator Fixed16(byte v)   => new(v << BitShift);
    public static explicit operator Fixed16(ushort v) => new(v << BitShift);
    public static explicit operator Fixed16(uint v)   => new((int)(v << BitShift));
    public static explicit operator Fixed16(ulong v)  => new((int)(v << BitShift));
    #endregion

    #region Cast from Floating-Point Types Operators
    public static explicit operator Fixed16(float v)  => new((int)(v * Converter));
    public static implicit operator Fixed16(double v) => new((int)(v * Converter));
    #endregion

    #region Misc Casts
    public static explicit operator Fixed16(Fixed8 v)   => new(v._value >> 8);
    public static explicit operator Fixed16(UFixed7 v)  => new((int)(v._value >> 9));
    public static explicit operator Fixed16(UFixed8 v)  => new((int)(v._value >> 8));
    public static explicit operator Fixed16(UFixed16 v) => new((int)v._value);
    #endregion

    #region Arithmetic Operators
    // Addition
    public static Fixed16 operator +(Fixed16 left, Fixed16 right)
    {
        return new Fixed16(left._value + right._value);
    }

    // Decrement
    public static Fixed16 operator --(Fixed16 v)
    {
        return new Fixed16(v._value - Converter);
    }

    // Divide
    public static Fixed16 operator /(Fixed16 left, Fixed16 right)
    {
        int value = (int)((long)Converter * left._value / right._value);
        return new Fixed16(value);
    }

    // Increment
    public static Fixed16 operator ++(Fixed16 v)
    {
        return new Fixed16(v._value + Converter);
    }

    // Multiply
    public static Fixed16 operator *(Fixed16 left, Fixed16 right)
    {
        int value = (int)((long)left._value * right._value / Converter);
        return new Fixed16(value);
    }

    // Subtraction
    public static Fixed16 operator -(Fixed16 left, Fixed16 right)
    {
        return new Fixed16(left._value - right._value);
    }

    // Unary Negate
    public static Fixed16 operator -(Fixed16 v)
    {
        return new Fixed16((~v._value) + 1);
    }

    // Unary Positive
    public static Fixed16 operator +(Fixed16 v)
    {
        return v;
    }
    #endregion

    #region Comparison Operators
    public static bool operator ==(Fixed16 left, Fixed16 right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Fixed16 left, Fixed16 right)
    {
        return !(left == right);
    }

    public static bool operator <(Fixed16 left, Fixed16 right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator <=(Fixed16 left, Fixed16 right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >(Fixed16 left, Fixed16 right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator >=(Fixed16 left, Fixed16 right)
    {
        return left.CompareTo(right) >= 0;
    }
    #endregion
}

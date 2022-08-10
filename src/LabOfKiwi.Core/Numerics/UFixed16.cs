using System;
using System.Diagnostics.CodeAnalysis;

namespace LabOfKiwi.Numerics;

/// <summary>
/// Represents an unsigned 32bit fixed-point number, with 16bits for the integer portion.
/// </summary>
public readonly struct UFixed16 : IComparable<UFixed16>, IComparable, IEquatable<UFixed16>
{
    private const int  BitShift  = 16;
    private const uint Converter = 1U << BitShift;

    public static readonly UFixed16 MaxValue = new(uint.MaxValue);
    public static readonly UFixed16 MinValue = new(0U);

    internal readonly uint _value;

    private UFixed16(uint value)
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

        if (obj is UFixed16 other)
        {
            return CompareTo(other);
        }

        throw new ArgumentException("Value must be of type Fixed16.");
    }

    public int CompareTo(UFixed16 other)
    {
        return _value.CompareTo(other._value);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is UFixed16 other && Equals(other);
    }

    public bool Equals(UFixed16 other)
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
    private uint IntegralPart => _value >> BitShift;
    #endregion

    #region Cast to Signed Integer Types Operators
    public static explicit operator sbyte (UFixed16 v) => unchecked((sbyte)v.IntegralPart);
    public static explicit operator short (UFixed16 v) => unchecked((short)v.IntegralPart);
    public static explicit operator int   (UFixed16 v) => unchecked((int)v.IntegralPart);
    public static explicit operator long  (UFixed16 v) => v.IntegralPart;
    #endregion

    #region Cast to Unsigned Integer Types Operators
    public static explicit operator byte   (UFixed16 v) => unchecked((byte)v.IntegralPart);
    public static explicit operator ushort (UFixed16 v) => unchecked((ushort)v.IntegralPart);
    public static explicit operator uint   (UFixed16 v) => v.IntegralPart;
    public static explicit operator ulong  (UFixed16 v) => v.IntegralPart;
    #endregion

    #region Cast to Floating-Point Types Operators
    public static explicit operator float  (UFixed16 v)  => v._value / (float)Converter;
    public static explicit operator double (UFixed16 v)  => v._value / (double)Converter;
    #endregion

    #region Cast from Signed Integer Types Operators
    public static explicit operator UFixed16(sbyte v) => new((uint)(v << BitShift));
    public static explicit operator UFixed16(short v) => new((uint)(v << BitShift));
    public static implicit operator UFixed16(int v)   => new((uint)(v << BitShift));
    public static explicit operator UFixed16(long v)  => new((uint)(v << BitShift));
    #endregion

    #region Cast from Unsigned Integer Types Operators
    public static implicit operator UFixed16(byte v)   => new((uint)(v << BitShift));
    public static explicit operator UFixed16(ushort v) => new((uint)(v << BitShift));
    public static explicit operator UFixed16(uint v)   => new(v << BitShift);
    public static explicit operator UFixed16(ulong v)  => new((uint)(v << BitShift));
    #endregion

    #region Cast from Floating-Point Types Operators
    public static explicit operator UFixed16(float v)  => new((uint)(v * Converter));
    public static explicit operator UFixed16(double v) => new((uint)(v * Converter));
    #endregion

    #region Misc Casts
    public static explicit operator UFixed16(Fixed8 v)  => new((uint)(v._value >> 8));
    public static explicit operator UFixed16(Fixed16 v) => new((uint)v._value);
    public static explicit operator UFixed16(UFixed7 v) => new(v._value >> 9);
    public static explicit operator UFixed16(UFixed8 v) => new(v._value >> 8);
    #endregion

    #region Arithmetic Operators
    // Addition
    public static UFixed16 operator +(UFixed16 left, UFixed16 right)
    {
        return new UFixed16(left._value + right._value);
    }

    // Decrement
    public static UFixed16 operator --(UFixed16 v)
    {
        return new UFixed16(v._value - Converter);
    }

    // Divide
    public static UFixed16 operator /(UFixed16 left, UFixed16 right)
    {
        uint value = (uint)((ulong)Converter * left._value / right._value);
        return new UFixed16(value);
    }

    // Increment
    public static UFixed16 operator ++(UFixed16 v)
    {
        return new UFixed16(v._value + Converter);
    }

    // Multiply
    public static UFixed16 operator *(UFixed16 left, UFixed16 right)
    {
        uint value = (uint)((ulong)left._value * right._value / Converter);
        return new UFixed16(value);
    }

    // Subtraction
    public static UFixed16 operator -(UFixed16 left, UFixed16 right)
    {
        return new UFixed16(left._value - right._value);
    }
    #endregion

    #region Comparison Operators
    public static bool operator ==(UFixed16 left, UFixed16 right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(UFixed16 left, UFixed16 right)
    {
        return !(left == right);
    }

    public static bool operator <(UFixed16 left, UFixed16 right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator <=(UFixed16 left, UFixed16 right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >(UFixed16 left, UFixed16 right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator >=(UFixed16 left, UFixed16 right)
    {
        return left.CompareTo(right) >= 0;
    }
    #endregion
}

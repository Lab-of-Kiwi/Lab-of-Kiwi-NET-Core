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

    public static readonly UFixed8 MaxValue = new(uint.MaxValue);
    public static readonly UFixed8 MinValue = new(0U);

    internal readonly uint _value;

    private UFixed8(uint value)
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
        return ((double)this).ToString();
    }
    #endregion

    #region Non-Public Members
    private uint IntegralPart => _value >> BitShift;
    #endregion

    #region Cast to Signed Integer Types Operators
    public static explicit operator sbyte (UFixed8 v) => unchecked((sbyte)v.IntegralPart);
    public static explicit operator short (UFixed8 v) => unchecked((short)v.IntegralPart);
    public static explicit operator int   (UFixed8 v) => unchecked((int)v.IntegralPart);
    public static explicit operator long  (UFixed8 v) => v.IntegralPart;
    #endregion

    #region Cast to Unsigned Integer Types Operators
    public static explicit operator byte   (UFixed8 v) => unchecked((byte)v.IntegralPart);
    public static explicit operator ushort (UFixed8 v) => unchecked((ushort)v.IntegralPart);
    public static explicit operator uint   (UFixed8 v) => v.IntegralPart;
    public static explicit operator ulong  (UFixed8 v) => v.IntegralPart;
    #endregion

    #region Cast to Floating-Point Types Operators
    public static explicit operator float  (UFixed8 v)  => v._value / (float)Converter;
    public static explicit operator double (UFixed8 v)  => v._value / (double)Converter;
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
    public static explicit operator UFixed8(float v)  => new((uint)(v * Converter));
    public static explicit operator UFixed8(double v) => new((uint)(v * Converter));
    #endregion

    #region Misc Casts
    public static explicit operator UFixed8(Fixed8 v)   => new((uint)v._value);
    public static explicit operator UFixed8(Fixed16 v)  => new((uint)v._value << 8);
    public static explicit operator UFixed8(UFixed7 v)  => new(v._value >> 1);
    public static explicit operator UFixed8(UFixed16 v) => new(v._value << 8);
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

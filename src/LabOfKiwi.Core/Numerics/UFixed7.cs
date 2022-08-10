using System;
using System.Diagnostics.CodeAnalysis;

namespace LabOfKiwi.Numerics;

/// <summary>
/// Represents an unsigned 32bit fixed-point number, with 7bits for the integer portion.
/// </summary>
public readonly struct UFixed7 : IComparable<UFixed7>, IComparable, IEquatable<UFixed7>
{
    private const int  BitShift  = 25;
    private const uint Converter = 1U << BitShift;

    public static readonly UFixed7 MaxValue = new(uint.MaxValue);
    public static readonly UFixed7 MinValue = new(0U);

    internal readonly uint _value;

    private UFixed7(uint value)
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

        if (obj is UFixed7 other)
        {
            return CompareTo(other);
        }

        throw new ArgumentException("Value must be of type Fixed7.");
    }

    public int CompareTo(UFixed7 other)
    {
        return _value.CompareTo(other._value);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is UFixed7 other && Equals(other);
    }

    public bool Equals(UFixed7 other)
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
    public static explicit operator sbyte (UFixed7 v) => unchecked((sbyte)v.IntegralPart);
    public static explicit operator short (UFixed7 v) => unchecked((short)v.IntegralPart);
    public static explicit operator int   (UFixed7 v) => unchecked((int)v.IntegralPart);
    public static explicit operator long  (UFixed7 v) => v.IntegralPart;
    #endregion

    #region Cast to Unsigned Integer Types Operators
    public static explicit operator byte   (UFixed7 v) => unchecked((byte)v.IntegralPart);
    public static explicit operator ushort (UFixed7 v) => unchecked((ushort)v.IntegralPart);
    public static explicit operator uint   (UFixed7 v) => v.IntegralPart;
    public static explicit operator ulong  (UFixed7 v) => v.IntegralPart;
    #endregion

    #region Cast to Floating-Point Types Operators
    public static explicit operator float  (UFixed7 v)  => v._value / (float)Converter;
    public static explicit operator double (UFixed7 v)  => v._value / (double)Converter;
    #endregion

    #region Cast from Signed Integer Types Operators
    public static explicit operator UFixed7(sbyte v) => new((uint)(v << BitShift));
    public static explicit operator UFixed7(short v) => new((uint)(v << BitShift));
    public static implicit operator UFixed7(int v)   => new((uint)(v << BitShift));
    public static explicit operator UFixed7(long v)  => new((uint)(v << BitShift));
    #endregion

    #region Cast from Unsigned Integer Types Operators
    public static explicit operator UFixed7(byte v)   => new((uint)(v << BitShift));
    public static explicit operator UFixed7(ushort v) => new((uint)(v << BitShift));
    public static explicit operator UFixed7(uint v)   => new(v << BitShift);
    public static explicit operator UFixed7(ulong v)  => new((uint)(v << BitShift));
    #endregion

    #region Cast from Floating-Point Types Operators
    public static explicit operator UFixed7(float v)  => new((uint)(v * Converter));
    public static explicit operator UFixed7(double v) => new((uint)(v * Converter));
    #endregion

    #region Misc Casts
    public static explicit operator UFixed7(Fixed8 v)   => new((uint)v._value << 1);
    public static explicit operator UFixed7(Fixed16 v)  => new((uint)v._value << 9);
    public static explicit operator UFixed7(UFixed8 v)  => new(v._value << 1);
    public static explicit operator UFixed7(UFixed16 v) => new(v._value << 9);
    #endregion

    #region Arithmetic Operators
    // Addition
    public static UFixed7 operator +(UFixed7 left, UFixed7 right)
    {
        return new UFixed7(left._value + right._value);
    }

    // Decrement
    public static UFixed7 operator --(UFixed7 v)
    {
        return new UFixed7(v._value - Converter);
    }

    // Divide
    public static UFixed7 operator /(UFixed7 left, UFixed7 right)
    {
        uint value = (uint)((ulong)Converter * left._value / right._value);
        return new UFixed7(value);
    }

    // Increment
    public static UFixed7 operator ++(UFixed7 v)
    {
        return new UFixed7(v._value + Converter);
    }

    // Multiply
    public static UFixed7 operator *(UFixed7 left, UFixed7 right)
    {
        uint value = (uint)((ulong)left._value * right._value / Converter);
        return new UFixed7(value);
    }

    // Subtraction
    public static UFixed7 operator -(UFixed7 left, UFixed7 right)
    {
        return new UFixed7(left._value - right._value);
    }
    #endregion

    #region Comparison Operators
    public static bool operator ==(UFixed7 left, UFixed7 right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(UFixed7 left, UFixed7 right)
    {
        return !(left == right);
    }

    public static bool operator <(UFixed7 left, UFixed7 right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator <=(UFixed7 left, UFixed7 right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >(UFixed7 left, UFixed7 right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator >=(UFixed7 left, UFixed7 right)
    {
        return left.CompareTo(right) >= 0;
    }
    #endregion
}

using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace LabOfKiwi.Numerics;

/// <summary>
/// Represents an unsigned 64bit fixed-point number, with 16bits for the integer portion.
/// </summary>
public readonly struct UDoubleFixed16 : IComparable<UDoubleFixed16>, IComparable, IEquatable<UDoubleFixed16>
{
    private const int   BitShift  = 48;
    private const ulong Converter = 1UL << BitShift;

    public static readonly UDoubleFixed16 MaxValue = new(ulong.MaxValue);
    public static readonly UDoubleFixed16 MinValue = new(0UL);

    internal readonly ulong _value;

    private UDoubleFixed16(ulong value)
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

        if (obj is UDoubleFixed16 other)
        {
            return CompareTo(other);
        }

        throw new ArgumentException("Value must be of type Fixed8.");
    }

    public int CompareTo(UDoubleFixed16 other)
    {
        return _value.CompareTo(other._value);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is UDoubleFixed16 other && Equals(other);
    }

    public bool Equals(UDoubleFixed16 other)
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
    private ulong IntegralPart => _value >> BitShift;
    #endregion

    #region Cast to Signed Integer Types Operators
    public static explicit operator sbyte (UDoubleFixed16 v) => (sbyte)v.IntegralPart;
    public static explicit operator short (UDoubleFixed16 v) => (short)v.IntegralPart;
    public static explicit operator int   (UDoubleFixed16 v) => (int)v.IntegralPart;
    public static explicit operator long  (UDoubleFixed16 v) => (long)v.IntegralPart;
    #endregion

    #region Cast to Unsigned Integer Types Operators
    public static explicit operator byte   (UDoubleFixed16 v) => (byte)v.IntegralPart;
    public static explicit operator ushort (UDoubleFixed16 v) => (ushort)v.IntegralPart;
    public static explicit operator uint   (UDoubleFixed16 v) => (uint)v.IntegralPart;
    public static explicit operator ulong  (UDoubleFixed16 v) => v.IntegralPart;
    #endregion

    #region Cast to Floating-Point Types Operators
    public static explicit operator float  (UDoubleFixed16 v) => v._value / (float)Converter;
    public static explicit operator double (UDoubleFixed16 v) => v._value / (double)Converter;
    #endregion

    #region Cast from Signed Integer Types Operators
    public static explicit operator UDoubleFixed16(sbyte v) => new((ulong)(v << BitShift));
    public static explicit operator UDoubleFixed16(short v) => new((ulong)(v << BitShift));
    public static implicit operator UDoubleFixed16(int v)   => new((ulong)(v << BitShift));
    public static explicit operator UDoubleFixed16(long v)  => new((ulong)(v << BitShift));
    #endregion

    #region Cast from Unsigned Integer Types Operators
    public static implicit operator UDoubleFixed16(byte v)   => new((ulong)(v << BitShift));
    public static implicit operator UDoubleFixed16(ushort v) => new((ulong)(v << BitShift));
    public static explicit operator UDoubleFixed16(uint v)   => new(v << BitShift);
    public static explicit operator UDoubleFixed16(ulong v)  => new(v << BitShift);
    #endregion

    #region Cast from Floating-Point Types Operators
    public static explicit operator UDoubleFixed16(float v)  => new((ulong)(v * Converter));
    public static implicit operator UDoubleFixed16(double v) => new((ulong)(v * Converter));
    #endregion

    #region Arithmetic Operators
    // Addition
    public static UDoubleFixed16 operator +(UDoubleFixed16 left, UDoubleFixed16 right)
    {
        return new UDoubleFixed16(left._value + right._value);
    }

    // Decrement
    public static UDoubleFixed16 operator --(UDoubleFixed16 v)
    {
        return new UDoubleFixed16(v._value - Converter);
    }

    // Divide
    public static UDoubleFixed16 operator /(UDoubleFixed16 left, UDoubleFixed16 right)
    {
        ulong value = (ulong)((BigInteger)Converter * left._value / right._value);
        return new UDoubleFixed16(value);
    }

    // Increment
    public static UDoubleFixed16 operator ++(UDoubleFixed16 v)
    {
        return new UDoubleFixed16(v._value + Converter);
    }

    // Multiply
    public static UDoubleFixed16 operator *(UDoubleFixed16 left, UDoubleFixed16 right)
    {
        ulong value = (ulong)((BigInteger)left._value * right._value / Converter);
        return new UDoubleFixed16(value);
    }

    // Subtraction
    public static UDoubleFixed16 operator -(UDoubleFixed16 left, UDoubleFixed16 right)
    {
        return new UDoubleFixed16(left._value - right._value);
    }
    #endregion

    #region Comparison Operators
    public static bool operator ==(UDoubleFixed16 left, UDoubleFixed16 right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(UDoubleFixed16 left, UDoubleFixed16 right)
    {
        return !(left == right);
    }

    public static bool operator <(UDoubleFixed16 left, UDoubleFixed16 right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator <=(UDoubleFixed16 left, UDoubleFixed16 right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >(UDoubleFixed16 left, UDoubleFixed16 right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator >=(UDoubleFixed16 left, UDoubleFixed16 right)
    {
        return left.CompareTo(right) >= 0;
    }
    #endregion
}

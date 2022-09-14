using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LabOfKiwi.Numerics;

[DebuggerDisplay("{DebugDisplay()}")]
public readonly struct UInt24 : IComparable, IComparable<UInt24>, IEquatable<UInt24>
{
    /// <summary>
    /// <c>16777215</c>
    /// </summary>
    public static readonly UInt24 MaxValue = new(ushort.MaxValue, byte.MaxValue);
    public static readonly UInt24 MinValue = new(0, 0);

    public static readonly UInt24 One = new(1, 0);

    private readonly ushort _lo;
    private readonly byte _hi;

    private UInt24(ushort lo, byte hi)
    {
        _lo = lo;
        _hi = hi;
    }

    #region Public Members
    public int CompareTo(object? obj)
    {
        if (obj == null)
        {
            return 1;
        }

        if (obj is UInt24 other)
        {
            return CompareTo(other);
        }

        throw new ArgumentException("Value must be of type UInt24.");
    }

    public int CompareTo(UInt24 other)
    {
        int c = _hi.CompareTo(other._hi);

        if (c != 0)
        {
            return c;
        }

        return _lo.CompareTo(other._lo);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is UInt24 other && Equals(other);
    }

    public bool Equals(UInt24 other)
    {
        return _lo == other._lo && _hi == other._hi;
    }

    public override int GetHashCode()
    {
        return (int)this;
    }

    public int LeadingZeroCount()
    {
        return Math.Min(24, BitOperations.LeadingZeroCount((uint)this));
    }

    public int Log2()
    {
        return BitOperations.Log2((uint)this);
    }

    public int PopCount()
    {
        return Math.Min(24, BitOperations.PopCount((uint)this));
    }

    public UInt24 RotateLeft(int offset)
    {
        return (this << offset) | (this >> (24 - offset));
    }

    public UInt24 RotateRight(int offset)
    {
        return (this >> offset) | (this << (24 - offset));
    }

    public override string ToString()
    {
        return ((uint)this).ToString();
    }

    public int TrailingZeroCount()
    {
        return Math.Min(24, BitOperations.TrailingZeroCount((uint)this));
    }

    public string ToString(int toBase)
    {
        return Convert.ToString((uint)this, toBase);
    }
    #endregion

    #region Non-Public Operators
    private string DebugDisplay() => "0b" + Convert.ToString(this, 2).PadLeft(24, '0');
    #endregion

    #region Implicit Operators
    // Unsigned => UInt24
    public static implicit operator UInt24(byte v)   => new(v, 0);
    public static implicit operator UInt24(ushort v) => new(v, 0);

    // UInt24 => Unsigned
    public static implicit operator uint  (UInt24 v) => ((uint)v._hi << 16) | v._lo;
    public static implicit operator ulong (UInt24 v) => (uint)v;

    // UInt24 => Signed
    public static implicit operator int  (UInt24 v) => (int)(uint)v;
    public static implicit operator long (UInt24 v) => (uint)v;

    // UInt24 => Floating Point
    public static implicit operator float  (UInt24 v) => (uint)v;
    public static implicit operator double (UInt24 v) => (uint)v;

    // Misc.
    public static implicit operator UInt24(char v) => new(v, 0);
    #endregion

    #region Explicit Operators
    // Unsigned => UInt24
    public static explicit operator UInt24(uint v)  => new((ushort)v, (byte)(v >> 16));
    public static explicit operator UInt24(ulong v) => new((ushort)v, (byte)(v >> 16));

    // Signed => UInt24
    public static explicit operator UInt24(sbyte v) => new((byte)v, 0);
    public static explicit operator UInt24(short v) => new((ushort)v, 0);
    public static explicit operator UInt24(int v)   => new((ushort)v, (byte)(v >> 16));
    public static explicit operator UInt24(long v)  => new((ushort)v, (byte)(v >> 16));

    // Floating Point => UInt24
    public static explicit operator UInt24(float v)  => (UInt24)(uint)v;
    public static explicit operator UInt24(double v) => (UInt24)(uint)v;

    // UInt24 => Unsigned
    public static explicit operator byte   (UInt24 v) => (byte)v._lo;
    public static explicit operator ushort (UInt24 v) => v._lo;

    // UInt24 => Signed
    public static explicit operator sbyte (UInt24 v) => (sbyte)v._lo;
    public static explicit operator short (UInt24 v) => (short)v._lo;
    #endregion

    #region Comparison Operators
    // Equality
    public static bool operator ==(UInt24 left, UInt24 right)
    {
        return left.Equals(right);
    }

    // Inequality
    public static bool operator !=(UInt24 left, UInt24 right)
    {
        return !(left == right);
    }

    // Less Than
    public static bool operator <(UInt24 left, UInt24 right)
    {
        return left.CompareTo(right) < 0;
    }

    // Less Than or Equal
    public static bool operator <=(UInt24 left, UInt24 right)
    {
        return left.CompareTo(right) <= 0;
    }

    // Greater Than
    public static bool operator >(UInt24 left, UInt24 right)
    {
        return left.CompareTo(right) > 0;
    }

    // Greater Than or Equal
    public static bool operator >=(UInt24 left, UInt24 right)
    {
        return left.CompareTo(right) >= 0;
    }
    #endregion

    #region Arithmetic Operators
    // Addition
    public static UInt24 operator +(UInt24 left, UInt24 right)
    {
        return (UInt24)((uint)left + (uint)right);
    }

    // Subtraction
    public static UInt24 operator -(UInt24 left, UInt24 right)
    {
        return (UInt24)((uint)left - (uint)right);
    }

    // Multiplication
    public static UInt24 operator *(UInt24 left, UInt24 right)
    {
        return (UInt24)((uint)left * (uint)right);
    }

    // Division
    public static UInt24 operator /(UInt24 left, UInt24 right)
    {
        return (UInt24)((uint)left / (uint)right);
    }

    // Modulus
    public static UInt24 operator %(UInt24 left, UInt24 right)
    {
        return (UInt24)((uint)left % (uint)right);
    }

    // Increment
    public static UInt24 operator ++(UInt24 value)
    {
        return (UInt24)((uint)value + 1U);
    }

    // Decrement
    public static UInt24 operator --(UInt24 value)
    {
        return (UInt24)((uint)value - 1U);
    }

    // Unary Negation
    public static int operator -(UInt24 value)
    {
        return (int)-(uint)value;
    }

    // Unary Plus
    public static UInt24 operator +(UInt24 value)
    {
        return value;
    }
    #endregion

    #region Bitwise Operators
    // Bitwise AND
    public static UInt24 operator &(UInt24 left, UInt24 right)
    {
        return (UInt24)((uint)left & (uint)right);
    }

    // Bitwise OR
    public static UInt24 operator |(UInt24 left, UInt24 right)
    {
        return (UInt24)((uint)left | (uint)right);
    }

    // Bitwise XOR
    public static UInt24 operator ^(UInt24 left, UInt24 right)
    {
        return (UInt24)((uint)left ^ (uint)right);
    }

    // Left Shift
    public static UInt24 operator <<(UInt24 value, int shift)
    {
        return (UInt24)((uint)value << shift);
    }

    // Right Shift
    public static UInt24 operator >>(UInt24 value, int shift)
    {
        return (UInt24)((uint)value >> shift);
    }

    // Ones Complement
    public static UInt24 operator ~(UInt24 value)
    {
        return (UInt24)~(uint)value;
    }
    #endregion
}

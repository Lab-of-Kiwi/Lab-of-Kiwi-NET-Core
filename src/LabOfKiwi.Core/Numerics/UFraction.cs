using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LabOfKiwi.Numerics;

public readonly struct UFraction : IFraction<UFraction, long, long>
{
    #region Static Fields
    public static readonly UFraction Infinity = new(1, 0U);

    public static readonly UFraction MaxValue = new(uint.MaxValue, 1U);

    public static readonly UFraction MinValue = new(0, 1U);

    public static readonly UFraction NaN = new(0, 0U);

    public static readonly UFraction One = new(1, 1U);

    public static readonly UFraction SmallestNonZeroValue = new(1, uint.MaxValue);

    public static readonly UFraction Zero = new(0, 1U);
    #endregion

    #region Instance Fields
    private readonly uint _numerator;
    private readonly uint _denominator;
    #endregion

    #region Constructors
    private UFraction(uint numerator, uint denominator)
    {
        _numerator = numerator;
        _denominator = denominator;
    }
    #endregion

    #region Public Properties
    public long Denominator => _denominator;

    public bool IsFinite => _denominator != 0;

    public bool IsInteger => _denominator == 1;

    public bool IsNaN => _denominator == 0 && Sign == 0;

    public bool IsOne => IsFinite && _numerator == _denominator;

    public bool IsInfinity => _denominator == 0 && Sign == 1;

    public bool IsZero => _numerator == 0 && _denominator != 0;

    public long Numerator => _numerator;

    public int Sign => _numerator == 0 ? 0 : 1;
    #endregion

    #region Public Methods
    public int CompareTo(object? obj)
    {
        if (obj == null)
        {
            return 1;
        }

        if (obj is UFraction other)
        {
            return CompareTo(other);
        }

        throw new ArgumentException("Value must be of type UFraction.");
    }

    public int CompareTo(UFraction other)
    {
        if (this < other) return -1;
        if (this > other) return 1;
        if (this == other) return 0;
        if (IsNaN) return other.IsNaN ? 0 : -1;

        return 1;
    }

    public static UFraction Create(uint value)
    {
        return new UFraction(value, 1);
    }

    public static UFraction Create(uint numerator, uint denominator)
    {
        if (denominator == 0)
        {
            if (numerator == 0) return NaN;
            return Infinity;
        }

        if (numerator == 0)
        {
            return Zero;
        }

        if (numerator == denominator)
        {
            return One;
        }

        uint gcd = MathFraction.GreatestCommonDivisor(numerator, denominator);
        return new UFraction(numerator / gcd, denominator / gcd);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is UFraction other && Equals(other);
    }

    public bool Equals(UFraction other)
    {
        if (this == other)
        {
            return true;
        }

        return IsNaN && other.IsNaN;
    }

    public byte[] GetBytes()
    {
        ulong i = ((ulong)_numerator << 32) | _denominator;
        return BitConverter.GetBytes(i);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_numerator, _denominator);
    }

    public UFraction Invert()
    {
        return InternalCreate(_denominator, _numerator);
    }

    public UFraction Pow(int exponent)
    {
        if (IsNaN)
        {
            return NaN;
        }

        if (exponent == 0)
        {
            return One;
        }

        UFraction value;
        long e;

        if (exponent > 0)
        {
            value = this;
            e = exponent;
        }
        else
        {
            value = Invert();
            e = -(long)exponent;
        }

        for (long i = 0; i < e; i++)
        {
            value *= this;
        }

        return value;
    }

    public static UFraction ToUFraction(byte[] array, int startIndex)
    {
        return ToUFraction(((ReadOnlySpan<byte>)array)[startIndex..]);
    }

    public static UFraction ToUFraction(ReadOnlySpan<byte> array)
    {
        ulong value = BitConverter.ToUInt64(array);
        ulong n = (uint)(value >> 32);
        ulong d = (uint)value;
        return InternalCreate(n, d);
    }

    public override string ToString()
    {
        uint n = _numerator;
        uint d = _denominator;

        if (d == 0)
        {
            if (n == 0)
            {
                return double.NaN.ToString();
            }
            else
            {
                return double.PositiveInfinity.ToString();
            }
        }

        if (n == 0 || d == 1)
        {
            return n.ToString();
        }

        return $"{n} / {d}";
    }

    public string ToString(bool mixedNumber = false)
    {
        if (mixedNumber)
        {
            var truncated = Truncate();
            var remainder = this - (UFraction)truncated;

            if (remainder != Zero)
            {
                return $"{truncated} + {remainder}";
            }

            return truncated.ToString();
        }

        return ToString();
    }

    public long Truncate()
    {
        return _numerator / _denominator;
    }

    public bool TryWriteTo(byte[]? array, int startIndex)
    {
        return TryWriteTo(((Span<byte>)array)[startIndex..]);
    }

    public bool TryWriteTo(Span<byte> array)
    {
        if (array.Length >= 8)
        {
            byte[] bytes = GetBytes();

            for (int i = 0; i < bytes.Length; i++)
            {
                array[i] = bytes[i];
            }

            return true;
        }

        return false;
    }
    #endregion

    #region Non-Public Properties
    private ulong Den => _denominator;

    private ulong Num => _numerator;
    #endregion

    #region Non-Public Methods
    private static int InternalCompare(UFraction left, UFraction right)
    {
        Debug.Assert(!(left.IsNaN || right.IsNaN));
        
        var d1 = left._denominator;
        var d2 = right._denominator;

        if (d1 == 0) // left is inf
        {
            if (d2 == 0) // right is inf
            {
                return 0;
            }

            return 1;
        }
        
        if (d2 == 0) // right is inf
        {
            return -1;
        }

        var n1 = left._numerator;
        var n2 = right._numerator;

        return (n1 * d2).CompareTo(n2 * d1);
    }

    private static UFraction InternalCreate(ulong n, ulong d)
    {
        if (d == 0)
        {
            if (n == 0) return NaN;
            return Infinity;
        }

        if (n == 0)
        {
            return Zero;
        }

        if (n == d)
        {
            return One;
        }

        ulong gcd = MathFraction.GreatestCommonDivisor(n, d);
        n /= gcd;
        d /= gcd;

        checked
        {
            return new UFraction((uint)n, (uint)d);
        }
    }
    #endregion

    #region Explicit Interface Implementations
    BigInteger IFraction.Denominator => _denominator;

    bool IFraction.IsNegativeInfinity => false;

    bool IFraction.IsPositiveInfinity => IsInfinity;

    BigInteger IFraction.Numerator => _numerator;

    UFraction IFraction<UFraction>.Abs()
    {
        return this;
    }
    #endregion

    #region Cast Operators (To UFraction)
    // Signed Integers -------------------------------------------------------------------------------------------------
    public static explicit operator UFraction(sbyte value)
    {
        return new UFraction((uint)value, 1U);
    }

    public static explicit operator UFraction(int value)
    {
        return new UFraction((uint)value, 1U);
    }

    public static explicit operator UFraction(short value)
    {
        return new UFraction((uint)value, 1U);
    }

    public static explicit operator UFraction(long value)
    {
        return new UFraction((uint)value, 1U);
    }

    // Unsigned Integers -----------------------------------------------------------------------------------------------
    public static implicit operator UFraction(byte value)
    {
        return new UFraction(value, 1U);
    }

    public static implicit operator UFraction(ushort value)
    {
        return new UFraction(value, 1U);
    }

    public static implicit operator UFraction(uint value)
    {
        return new UFraction(value, 1U);
    }

    public static explicit operator UFraction(ulong value)
    {
        return new UFraction((uint)value, 1U);
    }

    // Floating Points -------------------------------------------------------------------------------------------------
    public static explicit operator UFraction(float value)
    {
        return (UFraction)(double)value;
    }

    public static explicit operator UFraction(double value)
    {
        if (double.IsNaN(value)) return NaN;
        if (double.IsInfinity(value)) return Infinity;

        var result = FractionUtility.ConvertToFraction(value, uint.MinValue, uint.MaxValue);
        return InternalCreate((uint)result.Numerator, (uint)result.Denominator);
    }

    public static explicit operator UFraction(decimal value)
    {
        var result = FractionUtility.ConvertToFraction(value, uint.MinValue, uint.MaxValue);
        return InternalCreate((uint)result.Numerator, (uint)result.Denominator);
    }

    // CoreLib Numbers -------------------------------------------------------------------------------------------------
    public static explicit operator UFraction(BigInteger value)
    {
        return new UFraction((uint)value, 1U);
    }

    public static explicit operator UFraction(Half value)
    {
        return (UFraction)(double)value;
    }

    // Other Fractions -------------------------------------------------------------------------------------------------
    public static explicit operator UFraction(ShortFraction value)
    {
        return InternalCreate((uint)value.Numerator, (uint)value.Denominator);
    }

    public static explicit operator UFraction(Fraction value)
    {
        return InternalCreate((uint)value.Numerator, (uint)value.Denominator);
    }

    public static explicit operator UFraction(LongFraction value)
    {
        return InternalCreate((uint)value.Numerator, (uint)value.Denominator);
    }

    public static explicit operator UFraction(BigFraction value)
    {
        return InternalCreate((uint)value.Numerator, (uint)value.Denominator);
    }
    #endregion

    #region Cast Operators (From UFraction)
    // Signed Integers -------------------------------------------------------------------------------------------------
    public static explicit operator sbyte(UFraction value)
    {
        uint num = value._numerator;
        uint den = value._denominator;

        if (num == 0 || den == 0L)
        {
            return 0;
        }

        return (sbyte)(num / den);
    }

    public static explicit operator short(UFraction value)
    {
        uint num = value._numerator;
        uint den = value._denominator;

        if (num == 0 || den == 0L)
        {
            return 0;
        }

        return (short)(num / den);
    }

    public static explicit operator int(UFraction value)
    {
        uint num = value._numerator;
        uint den = value._denominator;

        if (num == 0 || den == 0L)
        {
            return 0;
        }

        return (int)(num / den);
    }

    public static explicit operator long(UFraction value)
    {
        uint num = value._numerator;
        uint den = value._denominator;

        if (num == 0 || den == 0L)
        {
            return 0;
        }

        return num / den;
    }

    // Unsigned Integers -----------------------------------------------------------------------------------------------
    public static explicit operator byte(UFraction value)
    {
        uint num = value._numerator;
        uint den = value._denominator;

        if (num == 0 || den == 0L)
        {
            return 0;
        }

        return (byte)(num / den);
    }

    public static explicit operator ushort(UFraction value)
    {
        uint num = value._numerator;
        uint den = value._denominator;

        if (num == 0 || den == 0L)
        {
            return 0;
        }

        return (ushort)(num / den);
    }

    public static explicit operator uint(UFraction value)
    {
        uint num = value._numerator;
        uint den = value._denominator;

        if (num == 0 || den == 0L)
        {
            return 0;
        }

        return num / den;
    }

    public static explicit operator ulong(UFraction value)
    {
        uint num = value._numerator;
        uint den = value._denominator;

        if (num == 0 || den == 0L)
        {
            return 0;
        }

        return num / den;
    }

    // Floating Points -------------------------------------------------------------------------------------------------
    public static explicit operator float(UFraction value)
    {
        return (float)(double)value;
    }

    public static explicit operator double(UFraction value)
    {
        uint num = value._numerator;
        uint den = value._denominator;

        if (den == 0)
        {
            return num == 0 ? double.NaN : double.PositiveInfinity;
        }

        if (num == 0)
        {
            return 0.0;
        }

        return (double)num / den;
    }

    public static explicit operator decimal(UFraction value)
    {
        uint num = value._numerator;
        uint den = value._denominator;

        if (num == 0 || den == 0L)
        {
            return decimal.Zero;
        }

        return (decimal)num / den;
    }

    // CoreLib Numbers -------------------------------------------------------------------------------------------------
    public static explicit operator BigInteger(UFraction value)
    {
        uint num = value._numerator;
        uint den = value._denominator;

        if (num == 0 || den == 0L)
        {
            return BigInteger.Zero;
        }

        return num / den;
    }

    public static explicit operator Half(UFraction value)
    {
        return (Half)(double)value;
    }
    #endregion

    #region Comparison Operators
    public static bool operator ==(UFraction left, UFraction right)
    {
        if (left.IsNaN || right.IsNaN)
        {
            return false;
        }

        return InternalCompare(left, right) == 0;
    }

    public static bool operator !=(UFraction left, UFraction right)
    {
        return !(left == right);
    }

    public static bool operator <(UFraction left, UFraction right)
    {
        if (left.IsNaN || right.IsNaN)
        {
            return false;
        }

        return InternalCompare(left, right) < 0;
    }

    public static bool operator <=(UFraction left, UFraction right)
    {
        if (left.IsNaN || right.IsNaN)
        {
            return false;
        }

        return InternalCompare(left, right) <= 0;
    }

    public static bool operator >(UFraction left, UFraction right)
    {
        if (left.IsNaN || right.IsNaN)
        {
            return false;
        }

        return InternalCompare(left, right) > 0;
    }

    public static bool operator >=(UFraction left, UFraction right)
    {
        if (left.IsNaN || right.IsNaN)
        {
            return false;
        }

        return InternalCompare(left, right) >= 0;
    }
    #endregion

    #region Arithmetic Operators
    public static UFraction operator +(UFraction left, UFraction right)
    {
        var num = (left.Num * right.Den) + (right.Num * left.Den);
        var den = left.Den * right.Den;
        return InternalCreate(num, den);
    }

    public static UFraction operator -(UFraction left, UFraction right)
    {
        var num = (left.Num * right.Den) - (right.Num * left.Den);
        var den = left.Den * right.Den;
        return InternalCreate(num, den);
    }

    public static UFraction operator *(UFraction left, UFraction right)
    {
        var num = left.Num * right.Num;
        var den = left.Den * right.Den;
        return InternalCreate(num, den);
    }

    public static UFraction operator /(UFraction left, UFraction right)
    {
        var num = left.Num * right.Den;
        var den = left.Den * right.Num;
        return InternalCreate(num, den);
    }

    public static UFraction operator ++(UFraction value)
    {
        return value + One;
    }

    public static UFraction operator --(UFraction value)
    {
        return value - One;
    }

    public static LongFraction operator -(UFraction value)
    {
        LongFraction n = -value.Numerator;
        return n / value._denominator;
    }

    public static UFraction operator +(UFraction value)
    {
        return value;
    }
    #endregion
}

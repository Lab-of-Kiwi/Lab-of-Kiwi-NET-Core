using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Numerics;

namespace LabOfKiwi.Numerics;

public readonly struct LongFraction : IFraction<LongFraction, long, BigInteger>
{
    #region Static Fields
    public static readonly LongFraction MaxValue = new(long.MaxValue, 1UL);

    public static readonly LongFraction MinusOne = new(-1L, 1UL);

    public static readonly LongFraction MinValue = new(long.MinValue, 1UL);

    public static readonly LongFraction NaN = new(0L, 0UL);

    public static readonly LongFraction NegativeInfinity = new(1L, 0UL);

    public static readonly LongFraction One = new(1L, 1UL);

    public static readonly LongFraction PositiveInfinity = new(1L, 0UL);

    public static readonly LongFraction SmallestNegativeValue = new(-1L, ulong.MaxValue);

    public static readonly LongFraction SmallestPositiveValue = new(1L, ulong.MaxValue);

    public static readonly LongFraction Zero = new(0L, 1UL);
    #endregion

    #region Instance Fields
    private readonly long _numerator;
    private readonly ulong _denominator;
    #endregion

    #region Constructors
    private LongFraction(long numerator, ulong denominator)
    {
        _numerator = numerator;
        _denominator = denominator;
    }
    #endregion

    #region Public Properties
    public BigInteger Denominator => _denominator;

    public bool IsFinite => _denominator != 0;

    public bool IsInteger => _denominator == 1;

    public bool IsNaN => _denominator == 0 && Sign == 0;

    public bool IsNegativeInfinity => _denominator == 0 && Sign == -1;

    public bool IsOne => IsFinite && Numerator == Denominator;

    public bool IsPositiveInfinity => _denominator == 0 && Sign == 1;

    public bool IsZero => _numerator == 0 && _denominator != 0;

    public long Numerator => _numerator;

    public int Sign => Math.Sign(_numerator);
    #endregion

    #region Public Methods
    public LongFraction Abs()
    {
        return new LongFraction(Math.Abs(_numerator), _denominator);
    }

    public int CompareTo(object? obj)
    {
        if (obj == null)
        {
            return 1;
        }

        if (obj is LongFraction other)
        {
            return CompareTo(other);
        }

        throw new ArgumentException("Value must be of type LongFraction.");
    }

    public int CompareTo(LongFraction other)
    {
        if (this < other) return -1;
        if (this > other) return 1;
        if (this == other) return 0;
        if (IsNaN) return other.IsNaN ? 0 : -1;

        return 1;
    }

    public static LongFraction Create(long value)
    {
        return InternalCreate(value, 1);
    }

    public static LongFraction Create(long numerator, long denominator)
    {
        return InternalCreate(numerator, denominator);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is LongFraction other && Equals(other);
    }

    public bool Equals(LongFraction other)
    {
        if (this == other)
        {
            return true;
        }

        return IsNaN && other.IsNaN;
    }

    public byte[] GetBytes()
    {
        byte[] arr = new byte[16];
        Span<byte> span = arr;

        bool success;

        success = BitConverter.TryWriteBytes(span, _numerator);
        success &= BitConverter.TryWriteBytes(span[8..], _denominator);
        Debug.Assert(success);

        return arr;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_numerator, _denominator);
    }

    public LongFraction Invert()
    {
        return InternalCreate(_denominator, _numerator);
    }

    public LongFraction Pow(int exponent)
    {
        if (IsNaN)
        {
            return NaN;
        }

        if (exponent == 0)
        {
            return One;
        }

        LongFraction value;
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

    public static LongFraction ToLongFraction(byte[] array, int startIndex)
    {
        return ToLongFraction(((ReadOnlySpan<byte>)array)[startIndex..]);
    }

    public static LongFraction ToLongFraction(ReadOnlySpan<byte> array)
    {
        long n = BitConverter.ToInt64(array);
        ulong d = BitConverter.ToUInt64(array[8..]);
        return InternalCreate(n, d);
    }

    public override string ToString()
    {
        long n = _numerator;
        ulong d = _denominator;

        if (d == 0)
        {
            if (n == 0)
            {
                return double.NaN.ToString();
            }
            else if (n > 0)
            {
                return double.PositiveInfinity.ToString();
            }
            else
            {
                return double.NegativeInfinity.ToString();
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
            var remainder = this - (LongFraction)truncated;

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
        return (long)((BigInteger)_numerator / (BigInteger)_denominator);
    }

    public bool TryWriteTo(byte[]? array, int startIndex)
    {
        return TryWriteTo(((Span<byte>)array)[startIndex..]);
    }

    public bool TryWriteTo(Span<byte> array)
    {
        if (array.Length >= 16)
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

    #region Non-Public Methods
    private static int InternalCompare(LongFraction left, LongFraction right)
    {
        var n1 = left.Numerator;
        var d1 = left.Denominator;
        var n2 = right.Numerator;
        var d2 = right.Denominator;

        if (d1 == 0)
        {
            Debug.Assert(n1 != 0);

            if (d2 == 0)
            {
                Debug.Assert(n2 != 0);
                return n1.CompareTo(n2);
            }

            return Math.Sign(n1);
        }
        else if (d2 == 0)
        {
            Debug.Assert(n2 != 0);
            return Math.Sign(n2);
        }
        else
        {
            return (n1 * d2).CompareTo(n2 * d1);
        }
    }

    private static LongFraction InternalCreate(BigInteger n, BigInteger d)
    {
        if (d == 0)
        {
            if (n > 0) return PositiveInfinity;
            if (n < 0) return NegativeInfinity;
            return NaN;
        }

        if (n == 0)
        {
            return Zero;
        }

        if (n == d)
        {
            return One;
        }

        if (d < 0)
        {
            n = -n;
            d = -d;
        }

        bool isNegative = false;

        if (n < 0)
        {
            isNegative = true;
            n = -n;
        }

        BigInteger gcd = BigInteger.GreatestCommonDivisor(n, d);

        n /= gcd;
        d /= gcd;

        if (isNegative)
        {
            n = -n;
        }

        long num = (long)n;
        ulong den = (ulong)d;

        if (!(num == n && den == d))
        {
            throw new OverflowException();
        }

        return new LongFraction(num, den);
    }
    #endregion

    #region Explicit Interface Implementations
    BigInteger IFraction.Numerator => _numerator;
    #endregion

    #region Cast Operators (To LongFraction)
    // Signed Integers -------------------------------------------------------------------------------------------------
    public static implicit operator LongFraction(sbyte value)
    {
        return new LongFraction(value, 1UL);
    }

    public static implicit operator LongFraction(short value)
    {
        return new LongFraction(value, 1UL);
    }

    public static implicit operator LongFraction(int value)
    {
        return new LongFraction(value, 1UL);
    }

    public static implicit operator LongFraction(long value)
    {
        return new LongFraction(value, 1UL);
    }

    // Unsigned Integers -----------------------------------------------------------------------------------------------
    public static implicit operator LongFraction(byte value)
    {
        return new LongFraction(value, 1UL);
    }

    public static implicit operator LongFraction(ushort value)
    {
        return new LongFraction(value, 1UL);
    }

    public static implicit operator LongFraction(uint value)
    {
        return new LongFraction(value, 1UL);
    }

    public static explicit operator LongFraction(ulong value)
    {
        return new LongFraction((long)value, 1UL);
    }

    // Floating Points -------------------------------------------------------------------------------------------------
    public static explicit operator LongFraction(float value)
    {
        return (LongFraction)(double)value;
    }

    public static explicit operator LongFraction(double value)
    {
        if (double.IsNaN(value)) return NaN;
        if (double.IsPositiveInfinity(value)) return PositiveInfinity;
        if (double.IsNegativeInfinity(value)) return NegativeInfinity;

        var result = FractionUtility.ConvertToFraction(value, long.MinValue, long.MaxValue);
        return InternalCreate((long)result.Numerator, (long)result.Denominator);
    }

    public static explicit operator LongFraction(decimal value)
    {
        var result = FractionUtility.ConvertToFraction(value, long.MinValue, long.MaxValue);
        return InternalCreate((long)result.Numerator, (long)result.Denominator);
    }

    // CoreLib Numbers -------------------------------------------------------------------------------------------------
    public static explicit operator LongFraction(BigInteger value)
    {
        return new LongFraction((long)value, 1U);
    }

    public static explicit operator LongFraction(Half value)
    {
        return (LongFraction)(double)value;
    }

    // Other Fractions -------------------------------------------------------------------------------------------------
    public static implicit operator LongFraction(Fraction value)
    {
        return InternalCreate(value.Numerator, value.Denominator);
    }

    public static implicit operator LongFraction(ShortFraction value)
    {
        return InternalCreate(value.Numerator, value.Denominator);
    }

    public static explicit operator LongFraction(BigFraction value)
    {
        long num = (long)value.Numerator;
        ulong den = (ulong)value.Denominator;
        return InternalCreate(num, den);
    }
    #endregion

    #region Cast Operators (From LongFraction)
    // Signed Integers -------------------------------------------------------------------------------------------------
    public static explicit operator sbyte(LongFraction value)
    {
        long num = value.Numerator;
        BigInteger den = value.Denominator;

        if (num == 0 || den.IsZero)
        {
            return 0;
        }

        return (sbyte)(num / den);
    }

    public static explicit operator short(LongFraction value)
    {
        long num = value.Numerator;
        BigInteger den = value.Denominator;

        if (num == 0 || den.IsZero)
        {
            return 0;
        }

        return (short)(num / den);
    }

    public static explicit operator int(LongFraction value)
    {
        long num = value.Numerator;
        BigInteger den = value.Denominator;

        if (num == 0 || den.IsZero)
        {
            return 0;
        }

        return (int)(num / den);
    }

    public static explicit operator long(LongFraction value)
    {
        long num = value.Numerator;
        BigInteger den = value.Denominator;

        if (num == 0 || den.IsZero)
        {
            return 0;
        }

        return (long)(num / den);
    }

    // Unsigned Integers -----------------------------------------------------------------------------------------------
    public static explicit operator byte(LongFraction value)
    {
        long num = value.Numerator;
        BigInteger den = value.Denominator;

        if (num == 0 || den.IsZero)
        {
            return 0;
        }

        return (byte)(num / den);
    }

    public static explicit operator ushort(LongFraction value)
    {
        long num = value.Numerator;
        BigInteger den = value.Denominator;

        if (num == 0 || den.IsZero)
        {
            return 0;
        }

        return (ushort)(num / den);
    }

    public static explicit operator uint(LongFraction value)
    {
        long num = value.Numerator;
        BigInteger den = value.Denominator;

        if (num == 0 || den.IsZero)
        {
            return 0;
        }

        return (uint)(num / den);
    }

    public static explicit operator ulong(LongFraction value)
    {
        long num = value.Numerator;
        BigInteger den = value.Denominator;

        if (num == 0 || den.IsZero)
        {
            return 0;
        }

        return (ulong)(num / den);
    }

    // Floating Points -------------------------------------------------------------------------------------------------
    public static explicit operator float(LongFraction value)
    {
        return (float)(double)value;
    }

    public static explicit operator double(LongFraction value)
    {
        long num = value.Numerator;
        BigInteger den = value.Denominator;

        if (den.IsZero)
        {
            return Math.Sign(num) switch
            {
                1 => double.PositiveInfinity,
                -1 => double.NegativeInfinity,
                _ => double.NaN
            };
        }

        if (num == 0)
        {
            return 0.0;
        }

        return num / (double)den;
    }

    public static explicit operator decimal(LongFraction value)
    {
        long num = value.Numerator;
        BigInteger den = value.Denominator;

        if (num == 0 || den.IsZero)
        {
            return decimal.Zero;
        }

        return num / (decimal)den;
    }

    // CoreLib Numbers -------------------------------------------------------------------------------------------------
    public static explicit operator BigInteger(LongFraction value)
    {
        long num = value.Numerator;
        BigInteger den = value.Denominator;

        if (num == 0 || den.IsZero)
        {
            return BigInteger.Zero;
        }

        return num / den;
    }

    public static explicit operator Half(LongFraction value)
    {
        return (Half)(double)value;
    }
    #endregion

    #region Comparison Operators
    public static bool operator ==(LongFraction left, LongFraction right)
    {
        if (left.IsNaN || right.IsNaN)
        {
            return false;
        }

        return InternalCompare(left, right) == 0;
    }

    public static bool operator !=(LongFraction left, LongFraction right)
    {
        return !(left == right);
    }

    public static bool operator <(LongFraction left, LongFraction right)
    {
        if (left.IsNaN || right.IsNaN)
        {
            return false;
        }

        return InternalCompare(left, right) < 0;
    }

    public static bool operator <=(LongFraction left, LongFraction right)
    {
        if (left.IsNaN || right.IsNaN)
        {
            return false;
        }

        return InternalCompare(left, right) <= 0;
    }

    public static bool operator >(LongFraction left, LongFraction right)
    {
        if (left.IsNaN || right.IsNaN)
        {
            return false;
        }

        return InternalCompare(left, right) > 0;
    }

    public static bool operator >=(LongFraction left, LongFraction right)
    {
        if (left.IsNaN || right.IsNaN)
        {
            return false;
        }

        return InternalCompare(left, right) >= 0;
    }
    #endregion

    #region Arithmetic Operators
    public static LongFraction operator +(LongFraction left, LongFraction right)
    {
        var num = (left.Numerator * right.Denominator) + (right.Numerator * left.Denominator);
        var den = left.Denominator * right.Denominator;
        return InternalCreate(num, den);
    }

    public static LongFraction operator -(LongFraction left, LongFraction right)
    {
        var num = (left.Numerator * right.Denominator) - (right.Numerator * left.Denominator);
        var den = left.Denominator * right.Denominator;
        return InternalCreate(num, den);
    }

    public static LongFraction operator *(LongFraction left, LongFraction right)
    {
        var num = left.Numerator * right.Numerator;
        var den = left.Denominator * right.Denominator;
        return InternalCreate(num, den);
    }

    public static LongFraction operator /(LongFraction left, LongFraction right)
    {
        var num = left.Numerator * right.Denominator;
        var den = left.Denominator * right.Numerator;
        return InternalCreate(num, den);
    }

    public static LongFraction operator ++(LongFraction value)
    {
        return value + One;
    }

    public static LongFraction operator --(LongFraction value)
    {
        return value - One;
    }

    public static LongFraction operator -(LongFraction value)
    {
        return InternalCreate(-value.Numerator, value.Denominator);
    }

    public static LongFraction operator +(LongFraction value)
    {
        return value;
    }
    #endregion
}

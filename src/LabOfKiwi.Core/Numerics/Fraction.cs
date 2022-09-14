using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace LabOfKiwi.Numerics;

public readonly struct Fraction : IFraction<Fraction, int, long>
{
    #region Static Fields
    public static readonly Fraction MaxValue = new(int.MaxValue, 1U);

    public static readonly Fraction MinusOne = new(-1, 1U);

    public static readonly Fraction MinValue = new(int.MinValue, 1U);

    public static readonly Fraction NaN = new(0, 0U);

    public static readonly Fraction NegativeInfinity = new(1, 0U);

    public static readonly Fraction One = new(1, 1U);

    public static readonly Fraction PositiveInfinity = new(1, 0U);

    public static readonly Fraction SmallestNegativeValue = new(-1, uint.MaxValue);

    public static readonly Fraction SmallestPositiveValue = new(1, uint.MaxValue);

    public static readonly Fraction Zero = new(0, 1U);
    #endregion

    #region Instance Fields
    private readonly int _numerator;
    private readonly uint _denominator;
    #endregion

    #region Constructors
    private Fraction(int numerator, uint denominator)
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

    public bool IsNegativeInfinity => _denominator == 0 && Sign == -1;

    public bool IsOne => IsFinite && _numerator == _denominator;

    public bool IsPositiveInfinity => _denominator == 0 && Sign == 1;

    public bool IsZero => _numerator == 0 && _denominator != 0;

    public int Numerator => _numerator;

    public int Sign => Math.Sign(_numerator);
    #endregion

    #region Public Methods
    public Fraction Abs()
    {
        return new Fraction(Math.Abs(_numerator), _denominator);
    }

    public int CompareTo(object? obj)
    {
        if (obj == null)
        {
            return 1;
        }

        if (obj is Fraction other)
        {
            return CompareTo(other);
        }

        throw new ArgumentException("Value must be of type Fraction.");
    }

    public int CompareTo(Fraction other)
    {
        if (this < other) return -1;
        if (this > other) return 1;
        if (this == other) return 0;
        if (IsNaN) return other.IsNaN ? 0 : -1;

        return 1;
    }

    public static Fraction Create(int value)
    {
        return InternalCreate(value, 1);
    }

    public static Fraction Create(int numerator, int denominator)
    {
        return InternalCreate(numerator, denominator);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Fraction other && Equals(other);
    }

    public bool Equals(Fraction other)
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

    public Fraction Invert()
    {
        return InternalCreate(_denominator, _numerator);
    }

    public Fraction Pow(int exponent)
    {
        if (IsNaN)
        {
            return NaN;
        }

        if (exponent == 0)
        {
            return One;
        }

        Fraction value;
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

    public static Fraction ToFraction(byte[] array, int startIndex)
    {
        return ToFraction(((ReadOnlySpan<byte>)array)[startIndex..]);
    }

    public static Fraction ToFraction(ReadOnlySpan<byte> array)
    {
        ulong value = BitConverter.ToUInt64(array);
        long n = (int)(value >> 32);
        long d = (uint)value;
        return InternalCreate(n, d);
    }

    public override string ToString()
    {
        int n = _numerator;
        uint d = _denominator;

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
            var remainder = this - (Fraction)truncated;

            if (remainder != Zero)
            {
                return $"{truncated} + {remainder}";
            }

            return truncated.ToString();
        }

        return ToString();
    }

    public int Truncate()
    {
        return (int)(_numerator / _denominator);
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

    #region Non-Public Methods
    private static int InternalCompare(Fraction left, Fraction right)
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

    private static Fraction InternalCreate(long n, long d)
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

        long gcd = MathFraction.GreatestCommonDivisor(n, d);

        n /= gcd;
        d /= gcd;

        if (isNegative)
        {
            n = -n;
        }

        checked
        {
            return new Fraction((int)n, (uint)d);
        }
    }
    #endregion

    #region Explicit Interface Implementations
    BigInteger IFraction.Denominator => _denominator;

    BigInteger IFraction.Numerator => _numerator;
    #endregion

    #region Cast Operators (To Fraction)
    // Signed Integers -------------------------------------------------------------------------------------------------
    public static implicit operator Fraction(sbyte value)
    {
        return new Fraction(value, 1U);
    }

    public static implicit operator Fraction(int value)
    {
        return new Fraction(value, 1U);
    }

    public static implicit operator Fraction(short value)
    {
        return new Fraction(value, 1U);
    }

    public static explicit operator Fraction(long value)
    {
        return new Fraction((int)value, 1U);
    }

    // Unsigned Integers -----------------------------------------------------------------------------------------------
    public static implicit operator Fraction(byte value)
    {
        return new Fraction(value, 1U);
    }

    public static implicit operator Fraction(ushort value)
    {
        return new Fraction(value, 1U);
    }

    public static explicit operator Fraction(uint value)
    {
        return new Fraction((int)value, 1U);
    }

    public static explicit operator Fraction(ulong value)
    {
        return new Fraction((int)value, 1U);
    }

    // Floating Points -------------------------------------------------------------------------------------------------
    public static explicit operator Fraction(float value)
    {
        return (Fraction)(double)value;
    }

    public static explicit operator Fraction(double value)
    {
        if (double.IsNaN(value)) return NaN;
        if (double.IsPositiveInfinity(value)) return PositiveInfinity;
        if (double.IsNegativeInfinity(value)) return NegativeInfinity;

        var result = FractionUtility.ConvertToFraction(value, int.MinValue, int.MaxValue);
        return InternalCreate((int)result.Numerator, (int)result.Denominator);
    }

    public static explicit operator Fraction(decimal value)
    {
        var result = FractionUtility.ConvertToFraction(value, int.MinValue, int.MaxValue);
        return InternalCreate((int)result.Numerator, (int)result.Denominator);
    }

    // CoreLib Numbers -------------------------------------------------------------------------------------------------
    public static explicit operator Fraction(BigInteger value)
    {
        return new Fraction((int)value, 1U);
    }

    public static explicit operator Fraction(Half value)
    {
        return (Fraction)(double)value;
    }

    // Other Fractions -------------------------------------------------------------------------------------------------
    public static implicit operator Fraction(ShortFraction value)
    {
        return InternalCreate(value.Numerator, value.Denominator);
    }

    public static explicit operator Fraction(LongFraction value)
    {
        int num = (int)value.Numerator;
        uint den = (uint)value.Denominator;
        return InternalCreate(num, den);
    }

    public static explicit operator Fraction(BigFraction value)
    {
        int num = (int)value.Numerator;
        uint den = (uint)value.Denominator;
        return InternalCreate(num, den);
    }
    #endregion

    #region Cast Operators (From Fraction)
    // Signed Integers -------------------------------------------------------------------------------------------------
    public static explicit operator sbyte(Fraction value)
    {
        int num = value.Numerator;
        long den = value.Denominator;

        if (num == 0 || den == 0L)
        {
            return 0;
        }

        return (sbyte)(num / den);
    }

    public static explicit operator short(Fraction value)
    {
        int num = value.Numerator;
        long den = value.Denominator;

        if (num == 0 || den == 0L)
        {
            return 0;
        }

        return (short)(num / den);
    }

    public static explicit operator int(Fraction value)
    {
        int num = value.Numerator;
        long den = value.Denominator;

        if (num == 0 || den == 0L)
        {
            return 0;
        }

        return (int)(num / den);
    }

    public static explicit operator long(Fraction value)
    {
        int num = value.Numerator;
        long den = value.Denominator;

        if (num == 0 || den == 0L)
        {
            return 0;
        }

        return num / den;
    }

    // Unsigned Integers -----------------------------------------------------------------------------------------------
    public static explicit operator byte(Fraction value)
    {
        int num = value.Numerator;
        long den = value.Denominator;

        if (num == 0 || den == 0L)
        {
            return 0;
        }

        return (byte)(num / den);
    }

    public static explicit operator ushort(Fraction value)
    {
        int num = value.Numerator;
        long den = value.Denominator;

        if (num == 0 || den == 0L)
        {
            return 0;
        }

        return (ushort)(num / den);
    }

    public static explicit operator uint(Fraction value)
    {
        int num = value.Numerator;
        long den = value.Denominator;

        if (num == 0 || den == 0L)
        {
            return 0;
        }

        return (uint)(num / den);
    }

    public static explicit operator ulong(Fraction value)
    {
        int num = value.Numerator;
        long den = value.Denominator;

        if (num == 0 || den == 0L)
        {
            return 0;
        }

        return (ulong)(num / den);
    }

    // Floating Points -------------------------------------------------------------------------------------------------
    public static explicit operator float(Fraction value)
    {
        return (float)(double)value;
    }

    public static explicit operator double(Fraction value)
    {
        int num = value.Numerator;
        long den = value.Denominator;

        if (den == 0)
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

        return (double)num / den;
    }

    public static explicit operator decimal(Fraction value)
    {
        int num = value.Numerator;
        long den = value.Denominator;

        if (num == 0 || den == 0L)
        {
            return decimal.Zero;
        }

        return (decimal)num / den;
    }

    // CoreLib Numbers -------------------------------------------------------------------------------------------------
    public static explicit operator BigInteger(Fraction value)
    {
        int num = value.Numerator;
        long den = value.Denominator;

        if (num == 0 || den == 0L)
        {
            return BigInteger.Zero;
        }

        return num / den;
    }

    public static explicit operator Half(Fraction value)
    {
        return (Half)(double)value;
    }
    #endregion

    #region Comparison Operators
    public static bool operator ==(Fraction left, Fraction right)
    {
        if (left.IsNaN || right.IsNaN)
        {
            return false;
        }

        return InternalCompare(left, right) == 0;
    }

    public static bool operator !=(Fraction left, Fraction right)
    {
        return !(left == right);
    }

    public static bool operator <(Fraction left, Fraction right)
    {
        if (left.IsNaN || right.IsNaN)
        {
            return false;
        }

        return InternalCompare(left, right) < 0;
    }

    public static bool operator <=(Fraction left, Fraction right)
    {
        if (left.IsNaN || right.IsNaN)
        {
            return false;
        }

        return InternalCompare(left, right) <= 0;
    }

    public static bool operator >(Fraction left, Fraction right)
    {
        if (left.IsNaN || right.IsNaN)
        {
            return false;
        }

        return InternalCompare(left, right) > 0;
    }

    public static bool operator >=(Fraction left, Fraction right)
    {
        if (left.IsNaN || right.IsNaN)
        {
            return false;
        }

        return InternalCompare(left, right) >= 0;
    }
    #endregion

    #region Arithmetic Operators
    public static Fraction operator +(Fraction left, Fraction right)
    {
        var num = (left.Numerator * right.Denominator) + (right.Numerator * left.Denominator);
        var den = left.Denominator * right.Denominator;
        return InternalCreate(num, den);
    }

    public static Fraction operator -(Fraction left, Fraction right)
    {
        var num = (left.Numerator * right.Denominator) - (right.Numerator * left.Denominator);
        var den = left.Denominator * right.Denominator;
        return InternalCreate(num, den);
    }

    public static Fraction operator *(Fraction left, Fraction right)
    {
        var num = left.Numerator * right.Numerator;
        var den = left.Denominator * right.Denominator;
        return InternalCreate(num, den);
    }

    public static Fraction operator /(Fraction left, Fraction right)
    {
        var num = left.Numerator * right.Denominator;
        var den = left.Denominator * right.Numerator;
        return InternalCreate(num, den);
    }

    public static Fraction operator ++(Fraction value)
    {
        return value + One;
    }

    public static Fraction operator --(Fraction value)
    {
        return value - One;
    }

    public static Fraction operator -(Fraction value)
    {
        return InternalCreate(-value.Numerator, value.Denominator);
    }

    public static Fraction operator +(Fraction value)
    {
        return value;
    }
    #endregion
}

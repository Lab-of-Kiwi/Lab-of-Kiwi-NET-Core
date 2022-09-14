using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace LabOfKiwi.Numerics;

public readonly struct ShortFraction : IFraction<ShortFraction, int, int>
{
    #region Static Fields
    public static readonly ShortFraction MaxValue = new(short.MaxValue, 1);

    public static readonly ShortFraction MinusOne = new(-1, 1);

    public static readonly ShortFraction MinValue = new(short.MinValue, 1);

    public static readonly ShortFraction NaN = new(0, 0);

    public static readonly ShortFraction NegativeInfinity = new(1, 0);

    public static readonly ShortFraction One = new(1, 1);

    public static readonly ShortFraction PositiveInfinity = new(1, 0);

    public static readonly ShortFraction SmallestNegativeValue = new(-1, ushort.MaxValue);

    public static readonly ShortFraction SmallestPositiveValue = new(1, ushort.MaxValue);

    public static readonly ShortFraction Zero = new(0, 1);
    #endregion

    #region Instance Fields
    private readonly short _numerator;
    private readonly ushort _denominator;
    #endregion

    #region Constructors
    private ShortFraction(short numerator, ushort denominator)
    {
        _numerator = numerator;
        _denominator = denominator;
    }
    #endregion

    #region Public Properties
    public int Denominator => _denominator;

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
    public ShortFraction Abs()
    {
        return new ShortFraction(Math.Abs(_numerator), _denominator);
    }

    public int CompareTo(object? obj)
    {
        if (obj == null)
        {
            return 1;
        }

        if (obj is ShortFraction other)
        {
            return CompareTo(other);
        }

        throw new ArgumentException("Value must be of type ShortFraction.");
    }

    public int CompareTo(ShortFraction other)
    {
        if (this < other) return -1;
        if (this > other) return 1;
        if (this == other) return 0;
        if (IsNaN) return other.IsNaN ? 0 : -1;

        return 1;
    }

    public static ShortFraction Create(short value)
    {
        return InternalCreate(value, 1);
    }

    public static ShortFraction Create(short numerator, short denominator)
    {
        return InternalCreate(numerator, denominator);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is ShortFraction other && Equals(other);
    }

    public bool Equals(ShortFraction other)
    {
        if (this == other)
        {
            return true;
        }

        return IsNaN && other.IsNaN;
    }

    public byte[] GetBytes()
    {
        uint i = ((uint)_numerator << 16) | _denominator;
        return BitConverter.GetBytes(i);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_numerator, _denominator);
    }

    public ShortFraction Invert()
    {
        return InternalCreate(_denominator, _numerator);
    }

    public ShortFraction Pow(int exponent)
    {
        if (IsNaN)
        {
            return NaN;
        }

        if (exponent == 0)
        {
            return One;
        }

        ShortFraction value;
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

    public static ShortFraction ToShortFraction(byte[] array, int startIndex)
    {
        return ToShortFraction(((ReadOnlySpan<byte>)array)[startIndex..]);
    }

    public static ShortFraction ToShortFraction(ReadOnlySpan<byte> array)
    {
        uint value = BitConverter.ToUInt32(array);
        int n = (short)(value >> 16);
        int d = (ushort)value;
        return InternalCreate(n, d);
    }

    public override string ToString()
    {
        short n = _numerator;
        ushort d = _denominator;

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
            var remainder = this - (ShortFraction)truncated;

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
        return _numerator / _denominator;
    }

    public bool TryWriteTo(byte[]? array, int startIndex)
    {
        return TryWriteTo(((Span<byte>)array)[startIndex..]);
    }

    public bool TryWriteTo(Span<byte> array)
    {
        if (array.Length >= 4)
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
    private static int InternalCompare(ShortFraction left, ShortFraction right)
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

    private static ShortFraction InternalCreate(int n, int d)
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

        int gcd = MathFraction.GreatestCommonDivisor(n, d);

        n /= gcd;
        d /= gcd;

        if (isNegative)
        {
            n = -n;
        }

        checked
        {
            return new ShortFraction((short)n, (ushort)d);
        }
    }
    #endregion

    #region Explicit Interface Implementations
    BigInteger IFraction.Denominator => _denominator;

    BigInteger IFraction.Numerator => _numerator;
    #endregion

    #region Cast Operators (To ShortFraction)
    // Signed Integers -------------------------------------------------------------------------------------------------
    public static implicit operator ShortFraction(sbyte value)
    {
        return new ShortFraction(value, 1);
    }

    public static implicit operator ShortFraction(short value)
    {
        return new ShortFraction(value, 1);
    }

    public static explicit operator ShortFraction(int value)
    {
        return new ShortFraction((short)value, 1);
    }

    public static explicit operator ShortFraction(long value)
    {
        return new ShortFraction((short)value, 1);
    }

    // Unsigned Integers -----------------------------------------------------------------------------------------------
    public static implicit operator ShortFraction(byte value)
    {
        return new ShortFraction(value, 1);
    }

    public static explicit operator ShortFraction(ushort value)
    {
        return new ShortFraction((short)value, 1);
    }

    public static explicit operator ShortFraction(uint value)
    {
        return new ShortFraction((short)value, 1);
    }

    public static explicit operator ShortFraction(ulong value)
    {
        return new ShortFraction((short)value, 1);
    }

    // Floating Points -------------------------------------------------------------------------------------------------
    public static explicit operator ShortFraction(float value)
    {
        return (ShortFraction)(double)value;
    }

    public static explicit operator ShortFraction(double value)
    {
        if (double.IsNaN(value)) return NaN;
        if (double.IsPositiveInfinity(value)) return PositiveInfinity;
        if (double.IsNegativeInfinity(value)) return NegativeInfinity;

        var result = FractionUtility.ConvertToFraction(value, short.MinValue, short.MaxValue);
        return InternalCreate((short)result.Numerator, (short)result.Denominator);
    }

    public static explicit operator ShortFraction(decimal value)
    {
        var result = FractionUtility.ConvertToFraction(value, short.MinValue, short.MaxValue);
        return InternalCreate((short)result.Numerator, (short)result.Denominator);
    }

    // CoreLib Numbers -------------------------------------------------------------------------------------------------
    public static explicit operator ShortFraction(BigInteger value)
    {
        return new ShortFraction((short)value, 1);
    }

    public static explicit operator ShortFraction(Half value)
    {
        return (ShortFraction)(double)value;
    }

    // Other Fractions -------------------------------------------------------------------------------------------------
    public static explicit operator ShortFraction(Fraction value)
    {
        short num = (short)value.Numerator;
        ushort den = (ushort)value.Denominator;
        return InternalCreate(num, den);
    }

    public static explicit operator ShortFraction(LongFraction value)
    {
        short num = (short)value.Numerator;
        ushort den = (ushort)value.Denominator;
        return InternalCreate(num, den);
    }

    public static explicit operator ShortFraction(BigFraction value)
    {
        short num = (short)value.Numerator;
        ushort den = (ushort)value.Denominator;
        return InternalCreate(num, den);
    }
    #endregion

    #region Cast Operators (From ShortFraction)
    // Signed Integers -------------------------------------------------------------------------------------------------
    public static explicit operator sbyte(ShortFraction value)
    {
        int num = value.Numerator;
        int den = value.Denominator;

        if (num == 0 || den == 0)
        {
            return 0;
        }

        return (sbyte)(num / den);
    }

    public static explicit operator short(ShortFraction value)
    {
        int num = value.Numerator;
        int den = value.Denominator;

        if (num == 0 || den == 0)
        {
            return 0;
        }

        return (short)(num / den);
    }

    public static explicit operator int(ShortFraction value)
    {
        int num = value.Numerator;
        int den = value.Denominator;

        if (num == 0 || den == 0)
        {
            return 0;
        }

        return num / den;
    }

    public static explicit operator long(ShortFraction value)
    {
        int num = value.Numerator;
        int den = value.Denominator;

        if (num == 0 || den == 0)
        {
            return 0;
        }

        return num / den;
    }

    // Unsigned Integers -----------------------------------------------------------------------------------------------
    public static explicit operator byte(ShortFraction value)
    {
        int num = value.Numerator;
        int den = value.Denominator;

        if (num == 0 || den == 0)
        {
            return 0;
        }

        return (byte)(num / den);
    }

    public static explicit operator ushort(ShortFraction value)
    {
        int num = value.Numerator;
        int den = value.Denominator;

        if (num == 0 || den == 0)
        {
            return 0;
        }

        return (ushort)(num / den);
    }

    public static explicit operator uint(ShortFraction value)
    {
        int num = value.Numerator;
        int den = value.Denominator;

        if (num == 0 || den == 0)
        {
            return 0;
        }

        return (uint)(num / den);
    }

    public static explicit operator ulong(ShortFraction value)
    {
        int num = value.Numerator;
        int den = value.Denominator;

        if (num == 0 || den == 0)
        {
            return 0;
        }

        return (ulong)(num / den);
    }

    // Floating Points -------------------------------------------------------------------------------------------------
    public static explicit operator float(ShortFraction value) => (float)(double)value;

    public static explicit operator double(ShortFraction value)
    {
        int num = value.Numerator;
        int den = value.Denominator;

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

    public static explicit operator decimal(ShortFraction value)
    {
        int num = value.Numerator;
        int den = value.Denominator;

        if (num == 0 || den == 0)
        {
            return decimal.Zero;
        }

        return (decimal)num / den;
    }

    // CoreLib Numbers -------------------------------------------------------------------------------------------------
    public static explicit operator BigInteger(ShortFraction value)
    {
        int num = value.Numerator;
        int den = value.Denominator;

        if (num == 0 || den == 0)
        {
            return BigInteger.Zero;
        }

        return num / den;
    }

    public static explicit operator Half(ShortFraction value)
    {
        return (Half)(double)value;
    }
    #endregion

    #region Comparison Operators
    public static bool operator ==(ShortFraction left, ShortFraction right)
    {
        if (left.IsNaN || right.IsNaN)
        {
            return false;
        }

        return InternalCompare(left, right) == 0;
    }

    public static bool operator !=(ShortFraction left, ShortFraction right)
    {
        return !(left == right);
    }

    public static bool operator <(ShortFraction left, ShortFraction right)
    {
        if (left.IsNaN || right.IsNaN)
        {
            return false;
        }

        return InternalCompare(left, right) < 0;
    }

    public static bool operator <=(ShortFraction left, ShortFraction right)
    {
        if (left.IsNaN || right.IsNaN)
        {
            return false;
        }

        return InternalCompare(left, right) <= 0;
    }

    public static bool operator >(ShortFraction left, ShortFraction right)
    {
        if (left.IsNaN || right.IsNaN)
        {
            return false;
        }

        return InternalCompare(left, right) > 0;
    }

    public static bool operator >=(ShortFraction left, ShortFraction right)
    {
        if (left.IsNaN || right.IsNaN)
        {
            return false;
        }

        return InternalCompare(left, right) >= 0;
    }
    #endregion

    #region Arithmetic Operators
    public static ShortFraction operator +(ShortFraction left, ShortFraction right)
    {
        var num = (left.Numerator * right.Denominator) + (right.Numerator * left.Denominator);
        var den = left.Denominator * right.Denominator;
        return InternalCreate(num, den);
    }

    public static ShortFraction operator -(ShortFraction left, ShortFraction right)
    {
        var num = (left.Numerator * right.Denominator) - (right.Numerator * left.Denominator);
        var den = left.Denominator * right.Denominator;
        return InternalCreate(num, den);
    }

    public static ShortFraction operator *(ShortFraction left, ShortFraction right)
    {
        var num = left.Numerator * right.Numerator;
        var den = left.Denominator * right.Denominator;
        return InternalCreate(num, den);
    }

    public static ShortFraction operator /(ShortFraction left, ShortFraction right)
    {
        var num = left.Numerator * right.Denominator;
        var den = left.Denominator * right.Numerator;
        return InternalCreate(num, den);
    }

    public static ShortFraction operator ++(ShortFraction value)
    {
        return value + One;
    }

    public static ShortFraction operator --(ShortFraction value)
    {
        return value - One;
    }

    public static ShortFraction operator -(ShortFraction value)
    {
        return InternalCreate(-value.Numerator, value.Denominator);
    }

    public static ShortFraction operator +(ShortFraction value)
    {
        return value;
    }
    #endregion
}

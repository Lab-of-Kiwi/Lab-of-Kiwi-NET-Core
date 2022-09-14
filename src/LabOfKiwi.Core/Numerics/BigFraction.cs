using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Numerics;

namespace LabOfKiwi.Numerics;

public readonly struct BigFraction : IFraction<BigFraction, BigInteger, BigInteger>
{
    #region Static Fields
    public static readonly BigFraction MinusOne = new(BigInteger.MinusOne, BigInteger.One);

    public static readonly BigFraction NaN = new(BigInteger.Zero, BigInteger.Zero);

    public static readonly BigFraction NegativeInfinity = new(BigInteger.MinusOne, BigInteger.Zero);

    public static readonly BigFraction One = new(BigInteger.One, BigInteger.One);

    public static readonly BigFraction PositiveInfinity = new(BigInteger.One, BigInteger.Zero);

    public static readonly BigFraction Zero = new(BigInteger.Zero, BigInteger.One);
    #endregion

    #region Instance Fields
    private readonly BigInteger _numerator;
    private readonly BigInteger _denominator;
    #endregion

    #region Constructors
    private BigFraction(BigInteger numerator, BigInteger denominator)
    {
        _numerator = numerator;
        _denominator = denominator;
    }
    #endregion

    #region Public Properties
    public BigInteger Denominator => _denominator;

    public bool IsFinite => !_denominator.IsZero;

    public bool IsInteger => _denominator.IsOne;

    public bool IsNaN => _denominator.IsZero && Sign == 0;

    public bool IsNegativeInfinity => _denominator.IsZero && Sign == -1;

    public bool IsOne => IsFinite && Numerator == Denominator;

    public bool IsPositiveInfinity => _denominator.IsZero && Sign == 1;

    public bool IsZero => _numerator == 0 && !_denominator.IsZero;

    public BigInteger Numerator => _numerator;

    public int Sign => _numerator.Sign;
    #endregion

    #region Public Methods
    public BigFraction Abs()
    {
        return new BigFraction(BigInteger.Abs(_numerator), _denominator);
    }

    public int CompareTo(object? obj)
    {
        if (obj == null)
        {
            return 1;
        }

        if (obj is BigFraction other)
        {
            return CompareTo(other);
        }

        throw new ArgumentException("Value must be of type BigFraction.");
    }

    public int CompareTo(BigFraction other)
    {
        if (this < other) return -1;
        if (this > other) return 1;
        if (this == other) return 0;
        if (IsNaN) return other.IsNaN ? 0 : -1;

        return 1;
    }

    public static BigFraction Create(BigInteger value)
    {
        return Create(value, BigInteger.One);
    }

    public static BigFraction Create(BigInteger numerator, BigInteger denominator)
    {
        if (denominator == 0)
        {
            if (numerator > 0) return PositiveInfinity;
            if (numerator < 0) return NegativeInfinity;
            return NaN;
        }

        if (numerator == 0)
        {
            return Zero;
        }

        if (numerator == denominator)
        {
            return One;
        }

        if (denominator < 0)
        {
            numerator = -numerator;
            denominator = -denominator;
        }

        bool isNegative = false;

        if (numerator < 0)
        {
            isNegative = true;
            numerator = -numerator;
        }

        BigInteger gcd = BigInteger.GreatestCommonDivisor(numerator, denominator);

        numerator /= gcd;
        denominator /= gcd;

        if (isNegative)
        {
            numerator = -numerator;
        }

        return new BigFraction(numerator, denominator);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is BigFraction other && Equals(other);
    }

    public bool Equals(BigFraction other)
    {
        if (this == other)
        {
            return true;
        }

        return IsNaN && other.IsNaN;
    }

    public byte[] GetBytes()
    {
        using var stream = new MemoryStream();

        byte[] num = _numerator.ToByteArray();
        byte[] den = _denominator.ToByteArray();

        stream.Write(BitConverter.GetBytes(num.Length));
        stream.Write(BitConverter.GetBytes(den.Length));
        stream.Write(num);
        stream.Write(den);

        return stream.ToArray();
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_numerator, _denominator);
    }

    public BigFraction Invert()
    {
        return Create(_denominator, _numerator);
    }

    public BigFraction Pow(int exponent)
    {
        if (IsNaN)
        {
            return NaN;
        }

        if (exponent == 0)
        {
            return One;
        }

        BigFraction value;
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

    public static BigFraction ToBigFraction(byte[] array, int startIndex)
    {
        return ToBigFraction(((ReadOnlySpan<byte>)array)[startIndex..]);
    }

    public static BigFraction ToBigFraction(ReadOnlySpan<byte> array)
    {
        int numSize = BitConverter.ToInt32(array);
        array = array[4..];
        int denSize = BitConverter.ToInt32(array);
        array = array[4..];

        var n = new BigInteger(array[..numSize]);
        var d = new BigInteger(array.Slice(numSize, denSize));

        return Create(n, d);
    }

    public override string ToString()
    {
        BigInteger n = _numerator;
        BigInteger d = _denominator;

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
            var remainder = this - (BigFraction)truncated;

            if (remainder != Zero)
            {
                return $"{truncated} + {remainder}";
            }

            return truncated.ToString();
        }

        return ToString();
    }

    public BigInteger Truncate()
    {
        return _numerator / _denominator;
    }

    public bool TryWriteTo(byte[]? array, int startIndex)
    {
        return TryWriteTo(((Span<byte>)array)[startIndex..]);
    }

    public bool TryWriteTo(Span<byte> array)
    {
        byte[] bytes = GetBytes();

        if (array.Length >= bytes.Length)
        {
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
    private static int InternalCompare(BigFraction left, BigFraction right)
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

            return n1.Sign;
        }
        else if (d2 == 0)
        {
            Debug.Assert(n2 != 0);
            return n2.Sign;
        }
        else
        {
            return (n1 * d2).CompareTo(n2 * d1);
        }
    }
    #endregion

    #region Cast Operators (To BigFraction)
    // Signed Integers -------------------------------------------------------------------------------------------------
    public static implicit operator BigFraction(sbyte value)
    {
        return new BigFraction(value, BigInteger.One);
    }

    public static implicit operator BigFraction(short value)
    {
        return new BigFraction(value, BigInteger.One);
    }

    public static implicit operator BigFraction(int value)
    {
        return new BigFraction(value, BigInteger.One);
    }

    // Unsigned Integers -----------------------------------------------------------------------------------------------
    public static implicit operator BigFraction(byte value)
    {
        return new BigFraction(value, BigInteger.One);
    }

    public static implicit operator BigFraction(ushort value)
    {
        return new BigFraction(value, BigInteger.One);
    }

    public static implicit operator BigFraction(uint value)
    {
        return new BigFraction(value, BigInteger.One);
    }

    public static implicit operator BigFraction(ulong value)
    {
        return new BigFraction(value, BigInteger.One);
    }

    // Floating Points -------------------------------------------------------------------------------------------------
    public static explicit operator BigFraction(float value)
    {
        return (BigFraction)(double)value;
    }

    public static explicit operator BigFraction(double value)
    {
        if (double.IsNaN(value)) return NaN;
        if (double.IsPositiveInfinity(value)) return PositiveInfinity;
        if (double.IsNegativeInfinity(value)) return NegativeInfinity;

        var result = FractionUtility.ConvertToFraction(value);
        return Create((BigInteger)result.Numerator, (BigInteger)result.Denominator);
    }

    public static explicit operator BigFraction(decimal value)
    {
        var result = FractionUtility.ConvertToFraction(value);
        return Create((BigInteger)result.Numerator, (BigInteger)result.Denominator);
    }

    // CoreLib Numbers -------------------------------------------------------------------------------------------------
    public static implicit operator BigFraction(BigInteger value)
    {
        return new BigFraction(value, BigInteger.One);
    }

    public static explicit operator BigFraction(Half value)
    {
        return (BigFraction)(double)value;
    }

    // Other Fractions -------------------------------------------------------------------------------------------------
    public static implicit operator BigFraction(ShortFraction value)
    {
        return Create(value.Numerator, value.Denominator);
    }

    public static implicit operator BigFraction(Fraction value)
    {
        return Create(value.Numerator, value.Denominator);
    }

    public static implicit operator BigFraction(LongFraction value)
    {
        return Create(value.Numerator, value.Denominator);
    }
    #endregion

    #region Cast Operators (From BigFraction)
    // Signed Integers -------------------------------------------------------------------------------------------------
    public static explicit operator sbyte(BigFraction value)
    {
        if (value.Numerator.IsZero || value.Denominator.IsZero)
        {
            return 0;
        }

        return (sbyte)(value.Numerator / value.Denominator);
    }

    public static explicit operator short(BigFraction value)
    {
        if (value.Numerator.IsZero || value.Denominator.IsZero)
        {
            return 0;
        }

        return (short)(value.Numerator / value.Denominator);
    }

    public static explicit operator int(BigFraction value)
    {
        if (value.Numerator.IsZero || value.Denominator.IsZero)
        {
            return 0;
        }

        return (int)(value.Numerator / value.Denominator);
    }

    public static explicit operator long(BigFraction value)
    {
        if (value.Numerator.IsZero || value.Denominator.IsZero)
        {
            return 0;
        }

        return (long)(value.Numerator / value.Denominator);
    }

    // Unsigned Integers -----------------------------------------------------------------------------------------------
    public static explicit operator byte(BigFraction value)
    {
        if (value.Numerator.IsZero || value.Denominator.IsZero)
        {
            return 0;
        }

        return (byte)(value.Numerator / value.Denominator);
    }

    public static explicit operator ushort(BigFraction value)
    {
        if (value.Numerator.IsZero || value.Denominator.IsZero)
        {
            return 0;
        }

        return (ushort)(value.Numerator / value.Denominator);
    }

    public static explicit operator uint(BigFraction value)
    {
        if (value.Numerator.IsZero || value.Denominator.IsZero)
        {
            return 0;
        }

        return (uint)(value.Numerator / value.Denominator);
    }

    public static explicit operator ulong(BigFraction value)
    {
        if (value.Numerator.IsZero || value.Denominator.IsZero)
        {
            return 0;
        }

        return (ulong)(value.Numerator / value.Denominator);
    }

    // Floating Points -------------------------------------------------------------------------------------------------
    public static explicit operator float(BigFraction value)
    {
        return (float)(double)value;
    }

    public static explicit operator double(BigFraction value)
    {
        if (value.Denominator.IsZero)
        {
            return value.Numerator.Sign switch
            {
                1 => double.PositiveInfinity,
                -1 => double.NegativeInfinity,
                _ => double.NaN
            };
        }

        if (value.Numerator.IsZero)
        {
            return 0.0;
        }

        return (double)value.Numerator / (double)value.Denominator;
    }

    public static explicit operator decimal(BigFraction value)
    {
        if (value.Numerator.IsZero || value.Denominator.IsZero)
        {
            return decimal.Zero;
        }

        return (decimal)value.Numerator / (decimal)value.Denominator;
    }

    // CoreLib Numbers -------------------------------------------------------------------------------------------------
    public static explicit operator BigInteger(BigFraction value)
    {
        if (value.Numerator.IsZero || value.Denominator.IsZero)
        {
            return BigInteger.Zero;
        }

        return value.Numerator / value.Denominator;
    }

    public static explicit operator Half(BigFraction value)
    {
        return (Half)(double)value;
    }
    #endregion

    #region Comparison Operators
    public static bool operator ==(BigFraction left, BigFraction right)
    {
        if (left.IsNaN || right.IsNaN)
        {
            return false;
        }

        return InternalCompare(left, right) == 0;
    }

    public static bool operator !=(BigFraction left, BigFraction right)
    {
        return !(left == right);
    }

    public static bool operator <(BigFraction left, BigFraction right)
    {
        if (left.IsNaN || right.IsNaN)
        {
            return false;
        }

        return InternalCompare(left, right) < 0;
    }

    public static bool operator <=(BigFraction left, BigFraction right)
    {
        if (left.IsNaN || right.IsNaN)
        {
            return false;
        }

        return InternalCompare(left, right) <= 0;
    }

    public static bool operator >(BigFraction left, BigFraction right)
    {
        if (left.IsNaN || right.IsNaN)
        {
            return false;
        }

        return InternalCompare(left, right) > 0;
    }

    public static bool operator >=(BigFraction left, BigFraction right)
    {
        if (left.IsNaN || right.IsNaN)
        {
            return false;
        }

        return InternalCompare(left, right) >= 0;
    }
    #endregion

    #region Arithmetic Operators
    public static BigFraction operator +(BigFraction left, BigFraction right)
    {
        var num = (left.Numerator * right.Denominator) + (right.Numerator * left.Denominator);
        var den = left.Denominator * right.Denominator;
        return Create(num, den);
    }

    public static BigFraction operator -(BigFraction left, BigFraction right)
    {
        var num = (left.Numerator * right.Denominator) - (right.Numerator * left.Denominator);
        var den = left.Denominator * right.Denominator;
        return Create(num, den);
    }

    public static BigFraction operator *(BigFraction left, BigFraction right)
    {
        var num = left.Numerator * right.Numerator;
        var den = left.Denominator * right.Denominator;
        return Create(num, den);
    }

    public static BigFraction operator /(BigFraction left, BigFraction right)
    {
        var num = left.Numerator * right.Denominator;
        var den = left.Denominator * right.Numerator;
        return Create(num, den);
    }

    public static BigFraction operator ++(BigFraction value)
    {
        return value + One;
    }

    public static BigFraction operator --(BigFraction value)
    {
        return value - One;
    }

    public static BigFraction operator -(BigFraction value)
    {
        return Create(-value.Numerator, value.Denominator);
    }

    public static BigFraction operator +(BigFraction value)
    {
        return value;
    }
    #endregion
}

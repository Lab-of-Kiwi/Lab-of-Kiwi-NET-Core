using System;
using System.Numerics;

namespace LabOfKiwi.Numerics
{
    public readonly partial struct Fraction
    {
        #region Addition
        /// <summary>
        /// Adds the values of two specified <see cref="Fraction"/> values.
        /// </summary>
        /// 
        /// <param name="left">The first value to add.</param>
        /// <param name="right">The second value to add.</param>
        /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
        public static Fraction operator +(Fraction left, Fraction right)
        {
            return Create((left._numerator * right._denominator) + (right._numerator * left._denominator), left._denominator * right._denominator);
        }
        #endregion

        #region Decrement
        /// <summary>
        /// Decrements a <see cref="Fraction"/> value by 1.
        /// </summary>
        /// 
        /// <param name="value">The value to decrement.</param>
        /// <returns>The value of the <paramref name="value"/> parameter decremented by 1.</returns>
        public static Fraction operator --(Fraction value)
        {
            return Create(value._numerator - value._denominator, value._denominator);
        }
        #endregion

        #region Division
        /// <summary>
        /// Divides a specified <see cref="Fraction"/> value by another specified <see cref="Fraction"/> value.
        /// </summary>
        /// 
        /// <param name="dividend">The value to be divided.</param>
        /// <param name="divisor">The value to divide by.</param>
        /// <returns>The result of the division.</returns>
        public static Fraction operator /(Fraction dividend, Fraction divisor)
        {
            return Create(dividend._numerator * divisor._denominator, dividend._denominator * divisor._numerator);
        }
        #endregion

        #region Equality
        /// <summary>
        /// Returns a value that indicates whether two <see cref="Fraction"/> values are equal.
        /// </summary>
        /// 
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        ///     <c>true</c> if the <paramref name="left"/> and <paramref name="right"/> parameters have the same value;
        ///     otherwise, <c>false</c>. If either parameter is <see cref="NaN"/>, <c>false</c> is returned.
        /// </returns>
        public static bool operator ==(Fraction left, Fraction right)
        {
            return left.Equals(right);
        }
        #endregion

        #region Explicit
        /// <summary>
        /// Defines an explicit conversion of a <see cref="Fraction"/> value to a <see cref="BigInteger"/> value.
        /// </summary>
        /// 
        /// <param name="value">The value to convert to a <see cref="BigInteger"/>.</param>
        /// <returns>
        ///     A <see cref="BigInteger"/> containing the value of the <paramref name="value"/> parameter.
        /// </returns>
        /// 
        /// <exception cref="ArithmeticException">
        ///     <paramref name="value"/> is either <see cref="NaN"/>, <see cref="PositiveInfinity"/>, or
        ///     <see cref="NegativeInfinity"/>.
        /// </exception>
        public static explicit operator BigInteger(Fraction value)
        {
            if (value._denominator == BigInteger.Zero)
            {
                throw new ArithmeticException("Cannot cast NaN or infinite fraction values to integer types.");
            }

            return value._numerator / value._denominator;
        }

        /// <summary>
        /// Defines an explicit conversion of a <see cref="Fraction"/> value to a <see cref="byte"/> value.
        /// </summary>
        /// 
        /// <param name="value">The value to convert to a <see cref="byte"/>.</param>
        /// <returns>A <see cref="byte"/> containing the value of the <paramref name="value"/> parameter.</returns>
        /// 
        /// <exception cref="ArithmeticException">
        ///     <paramref name="value"/> is either <see cref="NaN"/>, <see cref="PositiveInfinity"/>, or
        ///     <see cref="NegativeInfinity"/>.
        /// </exception>
        /// <exception cref="OverflowException">
        ///     <paramref name="value"/> is less than <see cref="byte.MinValue"/> or is greater than
        ///     <see cref="byte.MaxValue"/>.
        /// </exception>
        public static explicit operator byte(Fraction value)
        {
            return (byte)(BigInteger)value;
        }

        /// <summary>
        /// Defines an explicit conversion of a <see cref="Fraction"/> value to a <see cref="decimal"/> value.
        /// </summary>
        /// 
        /// <param name="value">The value to convert to a <see cref="decimal"/>.</param>
        /// <returns>A <see cref="decimal"/> containing the value of the <paramref name="value"/> parameter.</returns>
        /// 
        /// <exception cref="ArithmeticException">
        ///     <paramref name="value"/> is either <see cref="NaN"/>, <see cref="PositiveInfinity"/>, or
        ///     <see cref="NegativeInfinity"/>.
        /// </exception>
        /// <exception cref="OverflowException">
        ///     <paramref name="value"/> is less than <see cref="decimal.MinValue"/> or is greater than
        ///     <see cref="decimal.MaxValue"/>.
        /// </exception>
        public static explicit operator decimal(Fraction value)
        {
            if (value.TryDeconstruct(out BigInteger wholeNumber, out Fraction remainder, out bool isNegative))
            {
                decimal num = (decimal)wholeNumber;
                decimal rem = (decimal)remainder._numerator / (decimal)remainder._denominator;

                decimal result = num + rem;

                if (result < num)
                {
                    throw new OverflowException();
                }

                if (isNegative)
                {
                    result =decimal.Negate(result);
                }

                return result;
            }

            throw new ArithmeticException("Cannot cast NaN or infinite fraction values to decimal.");
        }

        /// <summary>
        /// Defines an explicit conversion of a <see cref="Fraction"/> value to a <see cref="double"/> value.
        /// </summary>
        /// 
        /// <param name="value">The value to convert to a <see cref="double"/>.</param>
        /// <returns>A <see cref="double"/> containing the value of the <paramref name="value"/> parameter.</returns>
        public static explicit operator double(Fraction value)
        {
            if (value.TryDeconstruct(out BigInteger wholeNumber, out Fraction remainder, out bool isNegative))
            {
                double result = (double)wholeNumber + ((double)remainder._numerator / (double)remainder._denominator);

                if (isNegative)
                {
                    result = -result;
                }

                return result;
            }

            if (value._numerator == BigInteger.Zero)
            {
                return double.NaN;
            }

            if (value._numerator == BigInteger.One)
            {
                return double.PositiveInfinity;
            }

            return double.NegativeInfinity;
        }

        /// <summary>
        /// Defines an explicit conversion of a <see cref="Fraction"/> value to a <see cref="float"/> value.
        /// </summary>
        /// 
        /// <param name="value">The value to convert to a <see cref="float"/>.</param>
        /// <returns>A <see cref="float"/> containing the value of the <paramref name="value"/> parameter.</returns>
        public static explicit operator float(Fraction value)
        {
            if (value.TryDeconstruct(out BigInteger wholeNumber, out Fraction remainder, out bool isNegative))
            {
                double result = (double)wholeNumber + ((double)remainder._numerator / (double)remainder._denominator);

                if (isNegative)
                {
                    result = -result;
                }

                return (float)result;
            }

            if (value._numerator == BigInteger.Zero)
            {
                return float.NaN;
            }

            if (value._numerator == BigInteger.One)
            {
                return float.PositiveInfinity;
            }

            return float.NegativeInfinity;
        }

        /// <summary>
        /// Defines an explicit conversion of a <see cref="Fraction"/> value to an <see cref="int"/> value.
        /// </summary>
        /// 
        /// <param name="value">The value to convert to an <see cref="int"/>.</param>
        /// <returns>An <see cref="int"/> containing the value of the <paramref name="value"/> parameter.</returns>
        /// 
        /// <exception cref="ArithmeticException">
        ///     <paramref name="value"/> is either <see cref="NaN"/>, <see cref="PositiveInfinity"/>, or
        ///     <see cref="NegativeInfinity"/>.
        /// </exception>
        /// <exception cref="OverflowException">
        ///     <paramref name="value"/> is less than <see cref="int.MinValue"/> or is greater than
        ///     <see cref="int.MaxValue"/>.
        /// </exception>
        public static explicit operator int(Fraction value)
        {
            return (int)(BigInteger)value;
        }

        /// <summary>
        /// Defines an explicit conversion of a <see cref="Fraction"/> value to a <see cref="long"/> value.
        /// </summary>
        /// 
        /// <param name="value">The value to convert to a <see cref="long"/>.</param>
        /// <returns>A <see cref="long"/> containing the value of the <paramref name="value"/> parameter.</returns>
        /// 
        /// <exception cref="ArithmeticException">
        ///     <paramref name="value"/> is either <see cref="NaN"/>, <see cref="PositiveInfinity"/>, or
        ///     <see cref="NegativeInfinity"/>.
        /// </exception>
        /// <exception cref="OverflowException">
        ///     <paramref name="value"/> is less than <see cref="long.MinValue"/> or is greater than
        ///     <see cref="long.MaxValue"/>.
        /// </exception>
        public static explicit operator long(Fraction value)
        {
            return (long)(BigInteger)value;
        }

        /// <summary>
        /// Defines an explicit conversion of a <see cref="Fraction"/> value to a <see cref="short"/> value.
        /// </summary>
        /// 
        /// <param name="value">The value to convert to a <see cref="short"/>.</param>
        /// <returns>A <see cref="short"/> containing the value of the <paramref name="value"/> parameter.</returns>
        /// 
        /// <exception cref="ArithmeticException">
        ///     <paramref name="value"/> is either <see cref="NaN"/>, <see cref="PositiveInfinity"/>, or
        ///     <see cref="NegativeInfinity"/>.
        /// </exception>
        /// <exception cref="OverflowException">
        ///     <paramref name="value"/> is less than <see cref="short.MinValue"/> or is greater than
        ///     <see cref="short.MaxValue"/>.
        /// </exception>
        public static explicit operator short(Fraction value)
        {
            return (short)(BigInteger)value;
        }

        /// <summary>
        /// Defines an explicit conversion of a <see cref="Fraction"/> value to a <see cref="sbyte"/> value.
        /// </summary>
        /// 
        /// <param name="value">The value to convert to a <see cref="sbyte"/>.</param>
        /// <returns>A <see cref="sbyte"/> containing the value of the <paramref name="value"/> parameter.</returns>
        /// 
        /// <exception cref="ArithmeticException">
        ///     <paramref name="value"/> is either <see cref="NaN"/>, <see cref="PositiveInfinity"/>, or
        ///     <see cref="NegativeInfinity"/>.
        /// </exception>
        /// <exception cref="OverflowException">
        ///     <paramref name="value"/> is less than <see cref="sbyte.MinValue"/> or is greater than
        ///     <see cref="sbyte.MaxValue"/>.
        /// </exception>
        public static explicit operator sbyte(Fraction value)
        {
            return (sbyte)(BigInteger)value;
        }

        /// <summary>
        /// Defines an explicit conversion of a <see cref="Fraction"/> value to an <see cref="uint"/> value.
        /// </summary>
        /// 
        /// <param name="value">The value to convert to an <see cref="uint"/>.</param>
        /// <returns>An <see cref="uint"/> containing the value of the <paramref name="value"/> parameter.</returns>
        /// 
        /// <exception cref="ArithmeticException">
        ///     <paramref name="value"/> is either <see cref="NaN"/>, <see cref="PositiveInfinity"/>, or
        ///     <see cref="NegativeInfinity"/>.
        /// </exception>
        /// <exception cref="OverflowException">
        ///     <paramref name="value"/> is less than <see cref="uint.MinValue"/> or is greater than
        ///     <see cref="uint.MaxValue"/>.
        /// </exception>
        public static explicit operator uint(Fraction value)
        {
            return (uint)(BigInteger)value;
        }

        /// <summary>
        /// Defines an explicit conversion of a <see cref="Fraction"/> value to an <see cref="ulong"/> value.
        /// </summary>
        /// 
        /// <param name="value">The value to convert to an <see cref="ulong"/>.</param>
        /// <returns>An <see cref="ulong"/> containing the value of the <paramref name="value"/> parameter.</returns>
        /// 
        /// <exception cref="ArithmeticException">
        ///     <paramref name="value"/> is either <see cref="NaN"/>, <see cref="PositiveInfinity"/>, or
        ///     <see cref="NegativeInfinity"/>.
        /// </exception>
        /// <exception cref="OverflowException">
        ///     <paramref name="value"/> is less than <see cref="ulong.MinValue"/> or is greater than
        ///     <see cref="ulong.MaxValue"/>.
        /// </exception>
        public static explicit operator ulong(Fraction value)
        {
            return (ulong)(BigInteger)value;
        }

        /// <summary>
        /// Defines an explicit conversion of a <see cref="Fraction"/> value to an <see cref="ushort"/> value.
        /// </summary>
        /// 
        /// <param name="value">The value to convert to an <see cref="ushort"/>.</param>
        /// <returns>An <see cref="ushort"/> containing the value of the <paramref name="value"/> parameter.</returns>
        /// 
        /// <exception cref="ArithmeticException">
        ///     <paramref name="value"/> is either <see cref="NaN"/>, <see cref="PositiveInfinity"/>, or
        ///     <see cref="NegativeInfinity"/>.
        /// </exception>
        /// <exception cref="OverflowException">
        ///     <paramref name="value"/> is less than <see cref="ushort.MinValue"/> or is greater than
        ///     <see cref="ushort.MaxValue"/>.
        /// </exception>
        public static explicit operator ushort(Fraction value)
        {
            return (ushort)(BigInteger)value;
        }
        #endregion

        #region Greater Than
        /// <summary>
        /// Returns a value that indicates whether a <see cref="Fraction"/> value is greater than another
        /// <see cref="Fraction"/> value.
        /// </summary>
        /// 
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        ///     <c>true</c> if <paramref name="left"/> is greater than <paramref name="right"/>; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator >(Fraction left, Fraction right)
        {
            return left.CompareTo(right) > 0;
        }
        #endregion

        #region Greater Than Or Equal
        /// <summary>
        /// Returns a value that indicates whether a <see cref="Fraction"/> value is greater than or equal to another
        /// <see cref="Fraction"/> value.
        /// </summary>
        /// 
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        ///     <c>true</c> if <paramref name="left"/> is greater than or equal to <paramref name="right"/>; otherwise,
        ///     <c>false</c>.
        /// </returns>
        public static bool operator >=(Fraction left, Fraction right)
        {
            return left.CompareTo(right) > -1;
        }
        #endregion

        #region Implicit
        /// <summary>
        /// Defines an implicit conversion of a <see cref="BigInteger"/> value to a <see cref="Fraction"/> value.
        /// </summary>
        /// 
        /// <param name="value">The value to convert to a <see cref="Fraction"/>.</param>
        /// <returns>A <see cref="Fraction"/> containing the value of the <paramref name="value"/> parameter.</returns>
        public static implicit operator Fraction(BigInteger value)
        {
            return new Fraction(value, BigInteger.One);
        }

        /// <summary>
        /// Defines an implicit conversion of a <see cref="byte"/> value to a <see cref="Fraction"/> value.
        /// </summary>
        /// 
        /// <param name="value">The value to convert to a <see cref="Fraction"/>.</param>
        /// <returns>A <see cref="Fraction"/> containing the value of the <paramref name="value"/> parameter.</returns>
        public static implicit operator Fraction(byte value)
        {
            return new Fraction(value, BigInteger.One);
        }

        /// <summary>
        /// Defines an implicit conversion of a <see cref="decimal"/> value to a <see cref="Fraction"/> value.
        /// </summary>
        /// 
        /// <param name="value">The value to convert to a <see cref="Fraction"/>.</param>
        /// <returns>A <see cref="Fraction"/> containing the value of the <paramref name="value"/> parameter.</returns>
        public static implicit operator Fraction(decimal value)
        {
            string dStr = value.ToString(DoubleFixedPoint);
            return ParseDecimalString(dStr);
        }

        /// <summary>
        /// Defines an implicit conversion of a <see cref="double"/> value to a <see cref="Fraction"/> value.
        /// </summary>
        /// 
        /// <param name="value">The value to convert to a <see cref="Fraction"/>.</param>
        /// <returns>A <see cref="Fraction"/> containing the value of the <paramref name="value"/> parameter.</returns>
        public static implicit operator Fraction(double value)
        {
            if (double.IsNaN(value))
            {
                return NaN;
            }

            if (double.IsPositiveInfinity(value))
            {
                return PositiveInfinity;
            }

            if (double.IsNegativeInfinity(value))
            {
                return NegativeInfinity;
            }

            string dStr = value.ToString(DoubleFixedPoint);
            return ParseDecimalString(dStr);
        }

        /// <summary>
        /// Defines an implicit conversion of a <see cref="float"/> value to a <see cref="Fraction"/> value.
        /// </summary>
        /// 
        /// <param name="value">The value to convert to a <see cref="Fraction"/>.</param>
        /// <returns>A <see cref="Fraction"/> containing the value of the <paramref name="value"/> parameter.</returns>
        public static implicit operator Fraction(float value)
        {
            if (float.IsNaN(value))
            {
                return NaN;
            }

            if (float.IsPositiveInfinity(value))
            {
                return PositiveInfinity;
            }

            if (float.IsNegativeInfinity(value))
            {
                return NegativeInfinity;
            }

            string dStr = value.ToString(DoubleFixedPoint);
            return ParseDecimalString(dStr);
        }

        /// <summary>
        /// Defines an implicit conversion of an <see cref="int"/> value to a <see cref="Fraction"/> value.
        /// </summary>
        /// 
        /// <param name="value">The value to convert to a <see cref="Fraction"/>.</param>
        /// <returns>A <see cref="Fraction"/> containing the value of the <paramref name="value"/> parameter.</returns>
        public static implicit operator Fraction(int value)
        {
            return new Fraction(value, BigInteger.One);
        }

        /// <summary>
        /// Defines an implicit conversion of a <see cref="long"/> value to a <see cref="Fraction"/> value.
        /// </summary>
        /// 
        /// <param name="value">The value to convert to a <see cref="Fraction"/>.</param>
        /// <returns>A <see cref="Fraction"/> containing the value of the <paramref name="value"/> parameter.</returns>
        public static implicit operator Fraction(long value)
        {
            return new Fraction(value, BigInteger.One);
        }

        /// <summary>
        /// Defines an implicit conversion of a <see cref="sbyte"/> value to a <see cref="Fraction"/> value.
        /// </summary>
        /// 
        /// <param name="value">The value to convert to a <see cref="Fraction"/>.</param>
        /// <returns>A <see cref="Fraction"/> containing the value of the <paramref name="value"/> parameter.</returns>
        public static implicit operator Fraction(sbyte value)
        {
            return new Fraction(value, BigInteger.One);
        }

        /// <summary>
        /// Defines an implicit conversion of a <see cref="short"/> value to a <see cref="Fraction"/> value.
        /// </summary>
        /// 
        /// <param name="value">The value to convert to a <see cref="Fraction"/>.</param>
        /// <returns>A <see cref="Fraction"/> containing the value of the <paramref name="value"/> parameter.</returns>
        public static implicit operator Fraction(short value)
        {
            return new Fraction(value, BigInteger.One);
        }

        /// <summary>
        /// Defines an implicit conversion of an <see cref="uint"/> value to a <see cref="Fraction"/> value.
        /// </summary>
        /// 
        /// <param name="value">The value to convert to a <see cref="Fraction"/>.</param>
        /// <returns>A <see cref="Fraction"/> containing the value of the <paramref name="value"/> parameter.</returns>
        public static implicit operator Fraction(uint value)
        {
            return new Fraction(value, BigInteger.One);
        }

        /// <summary>
        /// Defines an implicit conversion of an <see cref="ulong"/> value to a <see cref="Fraction"/> value.
        /// </summary>
        /// 
        /// <param name="value">The value to convert to a <see cref="Fraction"/>.</param>
        /// <returns>A <see cref="Fraction"/> containing the value of the <paramref name="value"/> parameter.</returns>
        public static implicit operator Fraction(ulong value)
        {
            return new Fraction(value, BigInteger.One);
        }

        /// <summary>
        /// Defines an implicit conversion of an <see cref="ushort"/> value to a <see cref="Fraction"/> value.
        /// </summary>
        /// 
        /// <param name="value">The value to convert to a <see cref="Fraction"/>.</param>
        /// <returns>A <see cref="Fraction"/> containing the value of the <paramref name="value"/> parameter.</returns>
        public static implicit operator Fraction(ushort value)
        {
            return new Fraction(value, BigInteger.One);
        }
        #endregion

        #region Increment
        /// <summary>
        /// Increments a <see cref="Fraction"/> value by 1.
        /// </summary>
        /// 
        /// <param name="value">The value to increment.</param>
        /// <returns>The value of the <paramref name="value"/> parameter incremented by 1.</returns>
        public static Fraction operator ++(Fraction value)
        {
            return Create(value._numerator + value._denominator, value._denominator);
        }
        #endregion

        #region Inequality
        /// <summary>
        /// Returns a value that indicates whether two <see cref="Fraction"/> values are not equal.
        /// </summary>
        /// 
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        ///     <c>false</c> if the <paramref name="left"/> and <paramref name="right"/> parameters have the same value;
        ///     otherwise, <c>true</c>. If either parameter is <see cref="NaN"/>, <c>true</c> is returned.
        /// </returns>
        public static bool operator !=(Fraction left, Fraction right)
        {
            return !left.Equals(right);
        }
        #endregion

        #region Less Than
        /// <summary>
        /// Returns a value that indicates whether a <see cref="Fraction"/> value is less than another
        /// <see cref="Fraction"/> value.
        /// </summary>
        /// 
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        ///     <c>true</c> if <paramref name="left"/> is less than <paramref name="right"/>; othweise, <c>false</c>.
        /// </returns>
        public static bool operator <(Fraction left, Fraction right)
        {
            return left.CompareTo(right) < 0;
        }
        #endregion

        #region Less Than Or Equal
        /// <summary>
        /// Returns a value that indicates whether a <see cref="Fraction"/> value is less than or equal to another
        /// <see cref="Fraction"/> value.
        /// </summary>
        /// 
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        ///     <c>true</c> if <paramref name="left"/> is less than or equal to <paramref name="right"/>; otherwise,
        ///     <c>false</c>.
        /// </returns>
        public static bool operator <=(Fraction left, Fraction right)
        {
            return left.CompareTo(right) < 1;
        }
        #endregion

        #region Multiply
        /// <summary>
        /// Multiplies two specified <see cref="Fraction"/> values.
        /// </summary>
        /// 
        /// <param name="left">The first value to multiply.</param>
        /// <param name="right">The second value to multiply.</param>
        /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        public static Fraction operator *(Fraction left, Fraction right)
        {
            return Create(left._numerator * right._numerator, left._denominator * right._denominator);
        }
        #endregion

        #region Subtraction
        /// <summary>
        /// Subtracts a <see cref="Fraction"/> value from another <see cref="Fraction"/> value.
        /// </summary>
        /// 
        /// <param name="left">The value to subtract from (the minuend).</param>
        /// <param name="right">The value to subtract (the subtrahend).</param>
        /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
        public static Fraction operator -(Fraction left, Fraction right)
        {
            return Create((left._numerator * right._denominator) - (right._numerator * left._denominator), left._denominator * right._denominator);
        }
        #endregion

        #region Unary Negation
        /// <summary>
        /// Negates a specified <see cref="Fraction"/> value.
        /// </summary>
        /// 
        /// <param name="value">The value to negate.</param>
        /// <returns>The result of the <paramref name="value"/> parameter multiplied by negative one (-1).</returns>
        public static Fraction operator -(Fraction value)
        {
            return new Fraction(BigInteger.Negate(value._numerator), value._denominator);
        }
        #endregion

        #region Unary Plus
        /// <summary>
        /// Returns the value of the <see cref="Fraction"/> operand. (The sign of the operand is unchanged.)
        /// </summary>
        /// 
        /// <param name="value">A <see cref="Fraction"/> value.</param>
        /// <returns>The value of the <paramref name="value"/> operand.</returns>
        public static Fraction operator +(Fraction value)
        {
            return value;
        }
        #endregion
    }
}

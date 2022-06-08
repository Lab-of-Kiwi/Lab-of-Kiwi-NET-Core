using System;
using System.Diagnostics;
using System.Numerics;

namespace LabOfKiwi.Numerics
{
    /// <summary>
    /// Represents a rational fraction value.
    /// </summary>
    [Serializable]
    public readonly partial struct Fraction : IComparable, IComparable<Fraction>, IEquatable<Fraction>
    {
        /// <summary>
        /// Fraction value of -1.
        /// </summary>
        public static readonly Fraction MinusOne = new(BigInteger.MinusOne, BigInteger.One);

        /// <summary>
        /// Fraction value of NaN.
        /// </summary>
        public static readonly Fraction NaN = new(BigInteger.Zero, BigInteger.Zero);

        /// <summary>
        /// Fraction value of negative infinity.
        /// </summary>
        public static readonly Fraction NegativeInfinity = new(BigInteger.MinusOne, BigInteger.Zero);

        /// <summary>
        /// Fraction value of 1.
        /// </summary>
        public static readonly Fraction One = new(BigInteger.One, BigInteger.One);

        /// <summary>
        /// Fraction value of positive infinity.
        /// </summary>
        public static readonly Fraction PositiveInfinity = new(BigInteger.One, BigInteger.Zero);

        /// <summary>
        /// Fraction value of 0.
        /// </summary>
        public static readonly Fraction Zero = new(BigInteger.Zero, BigInteger.One);

        // Numerator
        private readonly BigInteger _numerator;

        // Denominator.
        private readonly BigInteger _denominator;

        // Static constructor
        static Fraction()
        {
            // Pre-populate powers of 10 for cast helper
            PowersOf10 = new BigInteger[340];
            PowersOf10[0] = BigInteger.One;

            for (int i = 1; i < PowersOf10.Length; i++)
            {
                PowersOf10[i] = PowersOf10[i - 1] * 10;
            }
        }

        /// <summary>
        /// Constructs the default <see cref="Fraction"/> value of 0.
        /// </summary>
        public Fraction()
        {
            _numerator = BigInteger.Zero;
            _denominator = BigInteger.One;
        }

        // Internal constructor
        private Fraction(BigInteger numerator, BigInteger denominator)
        {
            Debug.Assert(IsReduced(numerator, denominator));
            _numerator = numerator;
            _denominator = denominator;
        }

        #region Public Properties
        /// <summary>
        /// Determines if this value is a decimal type, aka. finite but not an integer.
        /// </summary>
        public bool IsDecimal => _denominator > BigInteger.One;

        /// <summary>
        /// Determines if this value is not any of the following types: <see cref="NaN"/>,
        /// <see cref="PositiveInfinity"/>, <see cref="NegativeInfinity"/>.
        /// </summary>
        public bool IsFinite => _denominator != BigInteger.Zero;

        /// <summary>
        /// Determines if this value is either <see cref="PositiveInfinity"/> or <see cref="NegativeInfinity"/>.
        /// </summary>
        public bool IsInfinity => _numerator != BigInteger.Zero && _denominator == BigInteger.Zero;

        /// <summary>
        /// Determines if this value is an integer type.
        /// </summary>
        public bool IsInteger => _denominator == BigInteger.One;

        /// <summary>
        /// Determines if this value is <see cref="NaN"/>.
        /// </summary>
        public bool IsNaN => _numerator == BigInteger.Zero && _denominator == BigInteger.Zero;

        /// <summary>
        /// Determines if this value is less than zero. This includes <see cref="NegativeInfinity"/>.
        /// </summary>
        public bool IsNegative => _numerator < BigInteger.Zero;

        /// <summary>
        /// Determines if this value is <see cref="NegativeInfinity"/>.
        /// </summary>
        public bool IsNegativeInfinity => _numerator < BigInteger.Zero && _denominator == BigInteger.Zero;

        /// <summary>
        /// Determines if this value is <see cref="PositiveInfinity"/>.
        /// </summary>
        public bool IsPositiveInfinity => _numerator > BigInteger.Zero && _denominator == BigInteger.Zero;

        /// <summary>
        /// Determines if this value is zero.
        /// </summary>
        public bool IsZero => _numerator == BigInteger.Zero && _denominator != BigInteger.Zero;

        /// <summary>
        /// Gets a number that indicates the sign (negative, positive, or zero/NaN) of this <see cref="Fraction"/>.
        /// </summary>
        public int Sign => _numerator.CompareTo(BigInteger.Zero);
        #endregion

        /// <inheritdoc/>
        public int CompareTo(Fraction other)
        {
            // This is not finite
            if (_denominator == BigInteger.Zero)
            {
                // This is NaN
                if (_numerator == BigInteger.Zero)
                {
                    return other.IsNaN ? 0 : -1;
                }

                // This is PosInf
                if (_numerator == BigInteger.One)
                {
                    return other.IsPositiveInfinity ? 0 : 1;
                }

                // This is NegInf

                // Other is not finite
                if (other._denominator == BigInteger.Zero)
                {
                    // Other is NaN
                    if (other._numerator == BigInteger.Zero)
                    {
                        return 1;
                    }

                    return other._numerator == BigInteger.MinusOne ? 0 : -1;
                }

                return -1;
            }

            // Other is not finite
            if (other._denominator == BigInteger.Zero)
            {
                // Other is NaN
                if (other._numerator == BigInteger.Zero)
                {
                    return 1;
                }

                // Other is PosInf
                if (other._numerator == BigInteger.One)
                {
                    return -1;
                }

                // Other is NegInf
                return 1;
            }

            BigInteger x = _numerator * other._denominator;
            BigInteger y = other._numerator * _denominator;
            return x.CompareTo(y);
        }

        /// <inheritdoc/>
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

            throw new ArgumentException("Value must be a Fraction.");
        }

        /// <summary>
        /// Returns a <see cref="Fraction"/> value using the provided numerator and denominator.
        /// </summary>
        /// 
        /// <param name="numerator">The numerator.</param>
        /// <param name="denominator">The denominator.</param>
        /// <returns>
        ///     A <see cref="Fraction"/> that represents (<paramref name="numerator"/> / <paramref name="denominator"/>)
        /// </returns>
        public static Fraction Create(BigInteger numerator, BigInteger denominator)
        {
            Reduce(ref numerator, ref denominator);
            return new Fraction(numerator, denominator);
        }

        /// <inheritdoc/>
        public bool Equals(Fraction other)
        {
            if (_denominator == BigInteger.Zero)
            {
                if (_numerator == BigInteger.Zero)
                {
                    return false;
                }

                return _numerator == other._numerator && _denominator == other._denominator;
            }

            if (other._denominator == BigInteger.Zero)
            {
                return false;
            }

            return _numerator * other._denominator == other._numerator * _denominator;
        }

        /// <summary>
        /// Returns a value indicating whether this instance and a specified <see cref="object"/> represent the same
        /// type and value.
        /// </summary>
        /// 
        /// <param name="obj">The object to compare with this instance.</param>
        /// <returns>
        ///     <c>true</c> if value is a <see cref="decimal"/> and equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj)
        {
            return obj is Fraction other && Equals(other);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(typeof(Fraction), _numerator, _denominator);
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation.
        /// </summary>
        /// 
        /// <returns>A string that represents the value of this instance.</returns>
        public override string ToString()
        {
            if (_denominator == BigInteger.Zero)
            {
                if (_numerator == BigInteger.Zero)
                {
                    return NaNString;
                }

                if (_numerator == BigInteger.One)
                {
                    return PositiveInfinityString;
                }

                return NegativeInfinityString;
            }

            if (_numerator == BigInteger.Zero)
            {
                return ZeroString;
            }

            if (_denominator == BigInteger.One)
            {
                return _numerator.ToString();
            }

            return $"{_numerator} / {_denominator}";
        }
    }
}

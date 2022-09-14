using System;
using System.Numerics;

namespace LabOfKiwi.Numerics;

/// <summary>
/// Represents a fraction value that is expressed as an integer divided by another integer.
/// </summary>
public interface IFraction : IComparable
{
    /// <summary>
    /// Gets the denominator of this fraction.
    /// </summary>
    BigInteger Denominator { get; }

    /// <summary>
    /// Determines if this fraction is not one of the following values: NaN, ∞, -∞
    /// </summary>
    bool IsFinite { get; }

    /// <summary>
    /// Determines if this fraction is an integer value.
    /// </summary>
    bool IsInteger { get; }

    /// <summary>
    /// Determines if this fraction is NaN.
    /// </summary>
    bool IsNaN { get; }

    /// <summary>
    /// Determines if this fraction is negative infinity (-∞).
    /// </summary>
    bool IsNegativeInfinity { get; }

    /// <summary>
    /// Determines if this fraction is 1.
    /// </summary>
    bool IsOne { get; }

    /// <summary>
    /// Determines if this fraction is positive infinity (∞).
    /// </summary>
    bool IsPositiveInfinity { get; }

    /// <summary>
    /// Determines if this fraction is 0.
    /// </summary>
    bool IsZero { get; }

    /// <summary>
    /// Gets the numerator of this fraction.
    /// </summary>
    BigInteger Numerator { get; }

    /// <summary>
    /// Gets the sign of this fraction. Value will be 1 for positive, 0 for zero, and -1 for negative.
    /// </summary>
    int Sign { get; }

    /// <summary>
    /// Returns this fraction as a byte array.
    /// </summary>
    /// 
    /// <returns>An array of bytes representing this fraction.</returns>
    byte[] GetBytes();

    /// <summary>
    /// Gets the string representation of this fraction.
    /// </summary>
    /// 
    /// <param name="mixedNumber">Flag determining if the string should be formatted as a mixed fraction.</param>
    /// <returns>The string representation of this fraction.</returns>
    string ToString(bool mixedNumber = false);

    /// <summary>
    /// Writes this fraction as bytes to the provided array starting the provided index, if the array is large enough to
    /// fit this fraction's bytes starting at the provided index.
    /// </summary>
    /// 
    /// <param name="array">The byte array to write this fraction to.</param>
    /// <param name="startIndex">The starting index in <paramref name="array"/> to start writing.</param>
    /// <returns><c>true</c> if the write was successful; otherwise, <c>false</c>.</returns>
    bool TryWriteTo(byte[]? array, int startIndex);

    /// <summary>
    /// Writes this fraction as bytes to the provided array, if the array is large enough to fit this fraction's bytes.
    /// </summary>
    /// 
    /// <param name="array">The byte array to write this fraction to.</param>
    /// <returns><c>true</c> if the write was successful; otherwise, <c>false</c>.</returns>
    bool TryWriteTo(Span<byte> array);
}

/// <summary>
/// Fraction values that can provide the same type through methods.
/// </summary>
/// 
/// <typeparam name="TSelf">The type of fraction.</typeparam>
public interface IFraction<TSelf> : IFraction, IComparable<TSelf>, IEquatable<TSelf>
    where TSelf : IFraction<TSelf>
{
    /// <summary>
    /// Returns the absolute value of this fraction.
    /// </summary>
    /// 
    /// <returns>The absolute value of this fraction.</returns>
    /// <exception cref="OverflowException">An overflow occurs.</exception>
    TSelf Abs();

    /// <summary>
    /// Returns the inverse value of this fraction.
    /// </summary>
    /// 
    /// <returns>The inverse value of this fraction.</returns>
    /// <exception cref="OverflowException">An overflow occurs.</exception>
    TSelf Invert();

    /// <summary>
    /// Returns the power of this fraction to the provided exponent.
    /// </summary>
    /// 
    /// <param name="exponent">The exponent of the power.</param>
    /// <returns>The value this fraction to the power of <paramref name="exponent"/>.</returns>
    /// <exception cref="OverflowException">An overflow occurs.</exception>
    TSelf Pow(int exponent);
}

/// <summary>
/// Fraction values that have strongly typed numerator and denominator values.
/// </summary>
/// 
/// <typeparam name="TNumerator">The type of the numerator.</typeparam>
/// <typeparam name="TDenominator">The type of the denominator.</typeparam>
public interface IFraction<TNumerator, TDenominator> : IFraction
{
    /// <summary>
    /// Gets the denominator of this fraction.
    /// </summary>
    new TDenominator Denominator { get; }

    /// <summary>
    /// Gets the numerator of this fraction.
    /// </summary>
    new TNumerator Numerator { get; }

    /// <summary>
    /// Returns the whole number portion of this fraction.
    /// </summary>
    /// 
    /// <returns>The whole number portion of this fraction.</returns>
    TNumerator Truncate();
}

/// <summary>
/// Fraction values that have both strongly type numerator and denominator values, and that can provide the same type
/// through methods.
/// </summary>
/// 
/// <typeparam name="TSelf">The type of fraction.</typeparam>
/// <typeparam name="TNumerator">The type of the numerator.</typeparam>
/// <typeparam name="TDenominator">The type of the denominator.</typeparam>
public interface IFraction<TSelf, TNumerator, TDenominator> : IFraction<TSelf>, IFraction<TNumerator, TDenominator>
    where TSelf : IFraction<TSelf>
{
}

﻿using System;
using System.Collections.Generic;

namespace LabOfKiwi.Bits;

/// <summary>
/// Represents a collection of bits.
/// </summary>
public interface IBits : IComparable, IEnumerable<bool>
{
    /// <summary>
    /// Gets a value indicating if the bit at a particular index is set to 1 or not.
    /// </summary>
    /// 
    /// <param name="index">The index of the bit to check.</param>
    /// <returns><c>true</c> if the bit at <paramref name="index"/> is 1; otherwise, <c>false</c>.</returns>
    /// 
    /// <exception cref="IndexOutOfRangeException"><paramref name="index"/> is out of range.</exception>
    bool this[int index] { get; }

    /// <summary>
    /// Gets the number of leading bits that are 0.
    /// </summary>
    int LeadingZeroCount { get;}

    /// <summary>
    /// Gets the number of bits that are 1.
    /// </summary>
    int PopCount { get; }

    /// <summary>
    /// Gets the number of bits.
    /// </summary>
    int Size { get; }

    /// <summary>
    /// Gets the number of trailing bits that are 0.
    /// </summary>
    int TrailingZeroCount { get; }

    /// <summary>
    /// Gets an array of indexes that have their bits set to the provided value.
    /// </summary>
    /// 
    /// <param name="value">The value to search for.</param>
    /// <returns>An integer array.</returns>
    int[] GetIndexesOfValue(bool value);

    /// <summary>
    /// Gets a collection of bits with the same bits as this value in reverse order.
    /// </summary>
    /// 
    /// <returns>A new collection of bits.</returns>
    IBits Reverse();

    /// <summary>
    /// Gets a collection of bits with the same bits as this value rotated to the left by the amount provided.
    /// </summary>
    /// 
    /// <param name="offset">The amount of the rotation.</param>
    /// <returns>A new collection of bits.</returns>
    IBits RotateLeft(int offset);

    /// <summary>
    /// Gets a collection of bits with the same bits as this value rotated to the right by the amount provided.
    /// </summary>
    /// 
    /// <param name="offset">The amount of the rotation.</param>
    /// <returns>A new collection of bits.</returns>
    IBits RotateRight(int offset);

    /// <summary>
    /// Gets a collection of bits with the same bits as this value but with the bit at the provided index set to the
    /// provided boolean value.
    /// </summary>
    /// 
    /// <param name="index">The index of the bit to set.</param>
    /// <param name="value">The new value of the bit, where <c>true</c> is 1 and <c>false</c> is 0.</param>
    /// <returns>A new collection of bits.</returns>
    /// 
    /// <exception cref="IndexOutOfRangeException"><paramref name="index"/> is out of range.</exception>
    IBits Set(int index, bool value);
}

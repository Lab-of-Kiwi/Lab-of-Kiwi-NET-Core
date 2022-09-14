using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace LabOfKiwi.Bits;

/// <summary>
/// Represents a 12-bit integer value.
/// </summary>
public readonly struct Bits12 : IBits<Bits12>
{
    #region Constants
    /// <summary>
    /// The number of bits: 12
    /// </summary>
    public const int Size = 12;

    // The size of the container integer in bits.
    private const int ContainerSize = 8 * sizeof(ushort);

    // The mask to extract the exact bits.
    private const uint Mask = 0xFFFU;
    #endregion

    #region Static Fields
    /// <summary>
    /// The maximum value: 0xFFF
    /// </summary>
    public static readonly Bits12 MaxValue = new(0xFFFU);

    /// <summary>
    /// The miminum value: 0x000
    /// </summary>
    public static readonly Bits12 MinValue = new(0x000U);

    /// <summary>
    /// Value representing 1.
    /// </summary>
    public static readonly Bits12 One = new(0x001U);

    /// <summary>
    /// Value representing 0.
    /// </summary>
    public static readonly Bits12 Zero = new(0x000U);
    #endregion

    #region Instance Fields
    // Internal value.
    private readonly ushort _value;
    #endregion

    #region Constructors
    // Internal constructor.
    private Bits12(uint value)
    {
        _value = (ushort)(value & Mask);
    }
    #endregion

    #region Public Properties
    /// <inheritdoc/>
    public bool this[int index]
    {
        get
        {
            if ((uint)index >= Size)
            {
                throw new IndexOutOfRangeException();
            }

            return ((Bits >> index) & 1U) != 0;
        }
    }

    /// <inheritdoc/>
    public int LeadingZeroCount => BitOperations.LeadingZeroCount(Bits) - (ContainerSize - Size);

    /// <inheritdoc/>
    public int PopCount => BitOperations.PopCount(Bits);

    /// <inheritdoc/>
    public int TrailingZeroCount => Math.Min(BitOperations.TrailingZeroCount(Bits), Size);
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public int CompareTo(object? obj)
    {
        if (obj == null)
        {
            return 1;
        }

        if (obj is Bits12 other)
        {
            return CompareTo(other);
        }

        throw new ArgumentException("Value must be of type Bits12.");
    }

    /// <inheritdoc/>
    public int CompareTo(Bits12 other)
    {
        return _value.CompareTo(other._value);
    }

    /// <inheritdoc/>
    public (Bits12 Quotient, Bits12 Remainder) DivRem(Bits12 divisor)
    {
        var (Quotient, Remainder) = Math.DivRem(Bits, divisor.Bits);
        return (new(Quotient), new(Remainder));
    }

    /// <inheritdoc/>
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Bits12 other && Equals(other);
    }

    /// <inheritdoc/>
    public bool Equals(Bits12 other)
    {
        return _value == other._value;
    }

    /// <inheritdoc/>
    public IEnumerator<bool> GetEnumerator()
    {
        for (int i = 0; i < Size; i++)
        {
            yield return ((Bits >> i) & 1U) != 0;
        }
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }

    /// <inheritdoc/>
    public int[] GetIndexesOfValue(bool value)
    {
        int[] indexes = new int[PopCount];

        int idx = 0;

        for (int i = 0; i < Size; i++)
        {
            bool thisValue = ((Bits >> i) & 1U) != 0;

            if (thisValue == value)
            {
                indexes[idx++] = i;
            }
        }

        return indexes;
    }

    /// <inheritdoc/>
    public Bits12 Reverse()
    {
        uint value = (uint)_value.ReverseBits() >> (ContainerSize - Size);
        return new Bits12(value);
    }

    /// <inheritdoc/>
    public Bits12 RotateLeft(int offset)
    {
        return (this << offset) | (this >> (Size - offset));
    }

    /// <inheritdoc/>
    public Bits12 RotateRight(int offset)
    {
        return (this >> offset) | (this << (Size - offset));
    }

    /// <inheritdoc/>
    public Bits12 Set(int index, bool value)
    {
        if ((uint)index >= Size)
        {
            throw new IndexOutOfRangeException();
        }

        uint bit = 1U << index;
        uint newValue = value ? (Bits | bit) : (Bits & ~bit);
        return new Bits12(newValue);
    }

    /// <summary>
    /// Gets the string representation of this value as a sequence of 0s and 1s.
    /// </summary>
    /// 
    /// <returns>The string representation of this value.</returns>
    public override string ToString()
    {
        return Convert.ToString(Bits, 2).PadLeft(Size, '0');
    }
    #endregion

    #region Non-Public Properties
    // Internal property for value.
    internal uint Bits => _value;
    #endregion

    #region Explicit Interface Implementations
    int IBits.Size => Size;

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    #endregion

    #region Implicit Operators
    public static implicit operator Bits12(byte value) => new(value);

    public static implicit operator Bits12(ushort value) => new(value);

    public static implicit operator Bits12(uint value) => new(value);

    public static implicit operator ushort(Bits12 value) => value._value;

    public static implicit operator uint(Bits12 value) => value.Bits;

    public static implicit operator ulong(Bits12 value) => value.Bits;

    public static implicit operator short(Bits12 value) => (short)value._value;

    public static implicit operator int(Bits12 value) => value._value;

    public static implicit operator long(Bits12 value) => value._value;

    public static implicit operator float(Bits12 value) => BitConverter.UInt32BitsToSingle(value.Bits);

    public static implicit operator double(Bits12 value) => BitConverter.UInt64BitsToDouble(value.Bits);
    #endregion

    #region Explicit Operators
    public static explicit operator Bits12(sbyte value) => new((byte)value);

    public static explicit operator Bits12(short value) => new((ushort)value);

    public static explicit operator Bits12(int value) => new((uint)value);

    public static explicit operator Bits12(long value) => new((uint)value);

    public static explicit operator Bits12(ulong value) => new((uint)value);

    public static explicit operator Bits12(float value)
    {
        uint bitValue = BitConverter.SingleToUInt32Bits(value);
        return new Bits12(bitValue);
    }

    public static explicit operator Bits12(double value)
    {
        uint bitValue = (uint)BitConverter.DoubleToUInt64Bits(value);
        return new Bits12(bitValue);
    }

    public static explicit operator byte(Bits12 value) => (byte)value._value;

    public static explicit operator sbyte(Bits12 value) => (sbyte)value._value;

    public static explicit operator Bits12(Bits24 value) => new(value.Bits);
    #endregion

    #region Comparison Operators
    public static bool operator ==(Bits12 left, Bits12 right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Bits12 left, Bits12 right)
    {
        return !(left == right);
    }

    public static bool operator <(Bits12 left, Bits12 right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator <=(Bits12 left, Bits12 right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >(Bits12 left, Bits12 right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator >=(Bits12 left, Bits12 right)
    {
        return left.CompareTo(right) >= 0;
    }
    #endregion

    #region Conditional Operators
    public static bool operator true(Bits12 value)
    {
        return value.Bits != 0U;
    }

    public static bool operator false(Bits12 value)
    {
        return value.Bits == 0U;
    }
    #endregion

    #region Arithmetic Operators
    public static Bits12 operator +(Bits12 left, Bits12 right)
    {
        return new Bits12(left.Bits + right.Bits);
    }

    public static Bits12 operator -(Bits12 left, Bits12 right)
    {
        return new Bits12(left.Bits - right.Bits);
    }

    public static Bits12 operator *(Bits12 left, Bits12 right)
    {
        return new Bits12(left.Bits * right.Bits);
    }

    public static Bits12 operator /(Bits12 left, Bits12 right)
    {
        return new Bits12(left.Bits / right.Bits);
    }

    public static Bits12 operator %(Bits12 left, Bits12 right)
    {
        return new Bits12(left.Bits % right.Bits);
    }

    public static Bits12 operator ++(Bits12 value)
    {
        return new Bits12(value.Bits + 1U);
    }

    public static Bits12 operator --(Bits12 value)
    {
        return new Bits12(value.Bits - 1U);
    }

    public static Bits12 operator -(Bits12 value)
    {
        return new Bits12(~value.Bits + 1U);
    }

    public static Bits12 operator +(Bits12 value)
    {
        return value;
    }
    #endregion

    #region Bitwise Operators
    public static Bits12 operator &(Bits12 left, Bits12 right)
    {
        return new Bits12(left.Bits & right.Bits);
    }

    public static Bits12 operator |(Bits12 left, Bits12 right)
    {
        return new Bits12(left.Bits | right.Bits);
    }

    public static Bits12 operator ^(Bits12 left, Bits12 right)
    {
        return new Bits12(left.Bits ^ right.Bits);
    }

    public static Bits12 operator <<(Bits12 value, int shift)
    {
        return new Bits12(value.Bits << shift);
    }

    public static Bits12 operator >>(Bits12 value, int shift)
    {
        return new Bits12(value.Bits >> shift);
    }

    public static Bits12 operator ~(Bits12 value)
    {
        return new Bits12(~value.Bits);
    }
    #endregion
}

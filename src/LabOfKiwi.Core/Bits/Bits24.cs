using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace LabOfKiwi.Bits;

/// <summary>
/// Represents a 24-bit integer value.
/// </summary>
public readonly struct Bits24 : IBits<Bits24>
{
    #region Constants
    /// <summary>
    /// The number of bits: 24
    /// </summary>
    public const int Size = 24;

    // The size of the container integer in bits.
    private const int ContainerSize = 8 * sizeof(uint);

    // The mask to extract the exact bits.
    private const uint Mask = 0xFFFFFFU;
    #endregion

    #region Static Fields
    /// <summary>
    /// The maximum value: 0xFFFFFF
    /// </summary>
    public static readonly Bits24 MaxValue = new(0xFFFFFFU);

    /// <summary>
    /// The miminum value: 0x000000
    /// </summary>
    public static readonly Bits24 MinValue = new(0x000000U);

    /// <summary>
    /// Value representing 1.
    /// </summary>
    public static readonly Bits24 One = new(0x000001U);

    /// <summary>
    /// Value representing 0.
    /// </summary>
    public static readonly Bits24 Zero = new(0x000000U);
    #endregion

    #region Instance Fields
    // Internal value.
    private readonly uint _value;
    #endregion

    #region Constructors
    // Internal constructor.
    private Bits24(uint value)
    {
        _value = value & Mask;
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

            return ((_value >> index) & 1U) != 0;
        }
    }

    /// <inheritdoc/>
    public int LeadingZeroCount => BitOperations.LeadingZeroCount(_value) - (ContainerSize - Size);

    /// <inheritdoc/>
    public int PopCount => BitOperations.PopCount(_value);

    /// <inheritdoc/>
    public int TrailingZeroCount => Math.Min(BitOperations.TrailingZeroCount(_value), Size);
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public int CompareTo(object? obj)
    {
        if (obj == null)
        {
            return 1;
        }

        if (obj is Bits24 other)
        {
            return CompareTo(other);
        }

        throw new ArgumentException("Value must be of type Bits24.");
    }

    /// <inheritdoc/>
    public int CompareTo(Bits24 other)
    {
        return _value.CompareTo(other._value);
    }

    /// <inheritdoc/>
    public (Bits24 Quotient, Bits24 Remainder) DivRem(Bits24 divisor)
    {
        var (Quotient, Remainder) = Math.DivRem(_value, divisor._value);
        return (new(Quotient), new(Remainder));
    }

    /// <inheritdoc/>
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Bits24 other && Equals(other);
    }

    /// <inheritdoc/>
    public bool Equals(Bits24 other)
    {
        return _value == other._value;
    }

    /// <inheritdoc/>
    public IEnumerator<bool> GetEnumerator()
    {
        for (int i = 0; i < Size; i++)
        {
            yield return ((_value >> i) & 1U) != 0;
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
    public Bits24 Reverse()
    {
        uint value = _value.ReverseBits() >> (ContainerSize - Size);
        return new Bits24(value);
    }

    /// <inheritdoc/>
    public Bits24 RotateLeft(int offset)
    {
        return (this << offset) | (this >> (Size - offset));
    }

    /// <inheritdoc/>
    public Bits24 RotateRight(int offset)
    {
        return (this >> offset) | (this << (Size - offset));
    }

    /// <inheritdoc/>
    public Bits24 Set(int index, bool value)
    {
        if ((uint)index >= Size)
        {
            throw new IndexOutOfRangeException();
        }

        uint bit = 1U << index;
        uint newValue = value ? (_value | bit) : (_value & ~bit);
        return new Bits24(newValue);
    }

    /// <summary>
    /// Gets the string representation of this value as a sequence of 0s and 1s.
    /// </summary>
    /// 
    /// <returns>The string representation of this value.</returns>
    public override string ToString()
    {
        return Convert.ToString(_value, 2).PadLeft(Size, '0');
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
    public static implicit operator Bits24(byte value) => new(value);

    public static implicit operator Bits24(ushort value) => new(value);

    public static implicit operator Bits24(uint value) => new(value);

    public static implicit operator uint(Bits24 value) => value._value;

    public static implicit operator ulong(Bits24 value) => value._value;

    public static implicit operator int(Bits24 value) => (int)value._value;

    public static implicit operator long(Bits24 value) => value._value;

    public static implicit operator float(Bits24 value) => BitConverter.UInt32BitsToSingle(value._value);

    public static implicit operator double(Bits24 value) => BitConverter.UInt64BitsToDouble(value._value);

    public static implicit operator Bits24(Bits12 value) => new(value.Bits);
    #endregion

    #region Explicit Operators
    public static explicit operator Bits24(sbyte value) => new((byte)value);

    public static explicit operator Bits24(short value) => new((ushort)value);

    public static explicit operator Bits24(int value) => new((uint)value);

    public static explicit operator Bits24(long value) => new((uint)value);

    public static explicit operator Bits24(ulong value) => new((uint)value);

    public static explicit operator Bits24(float value)
    {
        uint bitValue = BitConverter.SingleToUInt32Bits(value);
        return new Bits24(bitValue);
    }

    public static explicit operator Bits24(double value)
    {
        uint bitValue = (uint)BitConverter.DoubleToUInt64Bits(value);
        return new Bits24(bitValue);
    }

    public static explicit operator byte(Bits24 value) => (byte)value._value;

    public static explicit operator ushort(Bits24 value) => (ushort)value._value;

    public static explicit operator sbyte(Bits24 value) => (sbyte)value._value;

    public static explicit operator short(Bits24 value) => (short)value._value;
    #endregion

    #region Comparison Operators
    public static bool operator ==(Bits24 left, Bits24 right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Bits24 left, Bits24 right)
    {
        return !(left == right);
    }

    public static bool operator <(Bits24 left, Bits24 right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator <=(Bits24 left, Bits24 right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >(Bits24 left, Bits24 right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator >=(Bits24 left, Bits24 right)
    {
        return left.CompareTo(right) >= 0;
    }
    #endregion

    #region Conditional Operators
    public static bool operator true(Bits24 value)
    {
        return value._value != 0U;
    }

    public static bool operator false(Bits24 value)
    {
        return value._value == 0U;
    }
    #endregion

    #region Arithmetic Operators
    public static Bits24 operator +(Bits24 left, Bits24 right)
    {
        return new Bits24(left._value + right._value);
    }

    public static Bits24 operator -(Bits24 left, Bits24 right)
    {
        return new Bits24(left._value - right._value);
    }

    public static Bits24 operator *(Bits24 left, Bits24 right)
    {
        return new Bits24(left._value * right._value);
    }

    public static Bits24 operator /(Bits24 left, Bits24 right)
    {
        return new Bits24(left._value / right._value);
    }

    public static Bits24 operator %(Bits24 left, Bits24 right)
    {
        return new Bits24(left._value % right._value);
    }

    public static Bits24 operator ++(Bits24 value)
    {
        return new Bits24(value._value + 1U);
    }

    public static Bits24 operator --(Bits24 value)
    {
        return new Bits24(value._value - 1U);
    }

    public static Bits24 operator -(Bits24 value)
    {
        return new Bits24(~value._value + 1U);
    }

    public static Bits24 operator +(Bits24 value)
    {
        return value;
    }
    #endregion

    #region Bitwise Operators
    public static Bits24 operator &(Bits24 left, Bits24 right)
    {
        return new Bits24(left._value & right._value);
    }

    public static Bits24 operator |(Bits24 left, Bits24 right)
    {
        return new Bits24(left._value | right._value);
    }

    public static Bits24 operator ^(Bits24 left, Bits24 right)
    {
        return new Bits24(left._value ^ right._value);
    }

    public static Bits24 operator <<(Bits24 value, int shift)
    {
        return new Bits24(value._value << shift);
    }

    public static Bits24 operator >>(Bits24 value, int shift)
    {
        return new Bits24(value._value >> shift);
    }

    public static Bits24 operator ~(Bits24 value)
    {
        return new Bits24(~value._value);
    }
    #endregion
}

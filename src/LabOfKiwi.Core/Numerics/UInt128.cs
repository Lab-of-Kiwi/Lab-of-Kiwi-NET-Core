using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LabOfKiwi.Numerics;

public readonly struct UInt128
{
    /// <summary>
    /// <c>340,282,366,920,938,463,463,374,607,431,768,211,455</c>
    /// </summary>
    public static readonly UInt128 MaxValue = new(ulong.MaxValue, ulong.MaxValue);
    public static readonly UInt128 MinValue = new(0UL, 0UL);

    private readonly ulong _lo;
    private readonly ulong _hi;
    
    private UInt128(ulong lo, ulong hi)
    {
        _lo = lo;
        _hi = hi;
    }

    private BigInteger BigIntegerValue => new(ToByteArray(), isUnsigned: true, isBigEndian: !BitConverter.IsLittleEndian);

    private byte[] ToByteArray()
    {
        byte[] arr = new byte[2 * sizeof(ulong)];

        using (var ms = new MemoryStream(arr))
        {
            if (BitConverter.IsLittleEndian)
            {
                ms.Write(BitConverter.GetBytes(_lo));
                ms.Write(BitConverter.GetBytes(_hi));
            }
            else
            {
                ms.Write(BitConverter.GetBytes(_hi));
                ms.Write(BitConverter.GetBytes(_lo));
            }
        }

        return arr;
    }

    public override string ToString()
    {
        if (_hi == 0UL)
        {
            return _lo.ToString();
        }

        return BigIntegerValue.ToString();
    }

    public string ToString(int toBase)
    {
        if (toBase == 2)
        {
            string lo = Convert.ToString((long)_lo, 1).PadLeft(64, '0');
            string hi = Convert.ToString((long)_hi, 1);
            return (hi + lo).TrimStart('0');
        }

        if (toBase == 10)
        {
            return ToString();
        }

        if (toBase == 16)
        {
            string lo = Convert.ToString((long)_lo, 16).PadLeft(16, '0');
            string hi = Convert.ToString((long)_hi, 16);
            return (hi + lo).TrimStart('0');
        }

        throw new ArgumentException("Unsupported base: " + toBase);
    }

    #region Implicit Operators
    public static implicit operator UInt128 (byte v)   => new(v, 0UL);
    public static implicit operator UInt128 (ushort v) => new(v, 0UL);
    public static implicit operator UInt128 (uint v)   => new(v, 0UL);
    public static implicit operator UInt128 (ulong v)  => new(v, 0UL);

    public static implicit operator BigInteger (UInt128 v) => v.BigIntegerValue;
    #endregion

    #region Explicit Operators
    public static explicit operator sbyte (UInt128 v) => (sbyte)v._lo;
    public static explicit operator short (UInt128 v) => (short)v._lo;
    public static explicit operator int   (UInt128 v) => (int)v._lo;
    public static explicit operator long  (UInt128 v) => (long)v._lo;

    public static explicit operator byte   (UInt128 v) => (byte)v._lo;
    public static explicit operator ushort (UInt128 v) => (ushort)v._lo;
    public static explicit operator uint   (UInt128 v) => (uint)v._lo;
    public static explicit operator ulong  (UInt128 v) => v._lo;

    public static explicit operator double (UInt128 v) => (double)v.BigIntegerValue;
    public static explicit operator float  (UInt128 v) => (float)v.BigIntegerValue;

    public static explicit operator UInt128(sbyte v)
    {
        ulong hi = v < 0 ? ulong.MaxValue : 0UL;
        ulong lo = (ulong)v;
        return new UInt128(lo, hi);
    }

    public static explicit operator UInt128(short v)
    {
        ulong hi = v < 0 ? ulong.MaxValue : 0UL;
        ulong lo = (ulong)v;
        return new UInt128(lo, hi);
    }

    public static explicit operator UInt128(int v)
    {
        ulong hi = v < 0 ? ulong.MaxValue : 0UL;
        ulong lo = (ulong)v;
        return new UInt128(lo, hi);
    }

    public static explicit operator UInt128(long v)
    {
        ulong hi = v < 0 ? ulong.MaxValue : 0UL;
        ulong lo = (ulong)v;
        return new UInt128(lo, hi);
    }

    public static explicit operator UInt128(BigInteger v)
    {
        byte[] arr = v.ToByteArray(isUnsigned: true, isBigEndian: !BitConverter.IsLittleEndian);

        // Only lo bits are needed
        if (arr.Length < 65)
        {
            if (arr.Length != 64)
            {
                if (BitConverter.IsLittleEndian)
                {
                    arr = arr.PadRight(64, (byte)0);
                }
                else
                {
                    arr = arr.PadLeft(64, (byte)0);
                }
            }

            return new UInt128(BitConverter.ToUInt64(arr), 0UL);
        }

        if (arr.Length < 128)
        {
            if (BitConverter.IsLittleEndian)
            {
                arr = arr.PadRight(128, (byte)0);
            }
            else
            {
                arr = arr.PadLeft(128, (byte)0);
            }
        }

        ulong lo, hi;

        if (BitConverter.IsLittleEndian)
        {
            lo = BitConverter.ToUInt64(arr);
            hi = BitConverter.ToUInt64(arr, 64);
        }
        else
        {
            hi = BitConverter.ToUInt64(arr);
            lo = BitConverter.ToUInt64(arr, 64);
        }

        return new UInt128(lo, hi);
    }
    #endregion

    #region Arithmetic Operators
    public static UInt128 operator +(UInt128 left, UInt128 right)
    {
        return (UInt128)(left.BigIntegerValue + right.BigIntegerValue);
    }

    public static UInt128 operator -(UInt128 left, UInt128 right)
    {
        return (UInt128)(left.BigIntegerValue - right.BigIntegerValue);
    }
    #endregion
}

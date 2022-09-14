using System;
using System.Numerics;

namespace LabOfKiwi;

/// <summary>
/// Extension methods for numeric built-in types.
/// </summary>
public static class NumberExtensions
{
    // Array of all byte values with the bits reversed.
    private static readonly byte[] ReversedBytes = new byte[]
    {
        0x00, 0x80, 0x40, 0xC0, 0x20, 0xA0, 0x60, 0xE0,
        0x10, 0x90, 0x50, 0xD0, 0x30, 0xB0, 0x70, 0xF0,
        0x08, 0x88, 0x48, 0xC8, 0x28, 0xA8, 0x68, 0xE8,
        0x18, 0x98, 0x58, 0xD8, 0x38, 0xB8, 0x78, 0xF8,
        0x04, 0x84, 0x44, 0xC4, 0x24, 0xA4, 0x64, 0xE4,
        0x14, 0x94, 0x54, 0xD4, 0x34, 0xB4, 0x74, 0xF4,
        0x0C, 0x8C, 0x4C, 0xCC, 0x2C, 0xAC, 0x6C, 0xEC,
        0x1C, 0x9C, 0x5C, 0xDC, 0x3C, 0xBC, 0x7C, 0xFC,
        0x02, 0x82, 0x42, 0xC2, 0x22, 0xA2, 0x62, 0xE2,
        0x12, 0x92, 0x52, 0xD2, 0x32, 0xB2, 0x72, 0xF2,
        0x0A, 0x8A, 0x4A, 0xCA, 0x2A, 0xAA, 0x6A, 0xEA,
        0x1A, 0x9A, 0x5A, 0xDA, 0x3A, 0xBA, 0x7A, 0xFA,
        0x06, 0x86, 0x46, 0xC6, 0x26, 0xA6, 0x66, 0xE6,
        0x16, 0x96, 0x56, 0xD6, 0x36, 0xB6, 0x76, 0xF6,
        0x0E, 0x8E, 0x4E, 0xCE, 0x2E, 0xAE, 0x6E, 0xEE,
        0x1E, 0x9E, 0x5E, 0xDE, 0x3E, 0xBE, 0x7E, 0xFE,
        0x01, 0x81, 0x41, 0xC1, 0x21, 0xA1, 0x61, 0xE1,
        0x11, 0x91, 0x51, 0xD1, 0x31, 0xB1, 0x71, 0xF1,
        0x09, 0x89, 0x49, 0xC9, 0x29, 0xA9, 0x69, 0xE9,
        0x19, 0x99, 0x59, 0xD9, 0x39, 0xB9, 0x79, 0xF9,
        0x05, 0x85, 0x45, 0xC5, 0x25, 0xA5, 0x65, 0xE5,
        0x15, 0x95, 0x55, 0xD5, 0x35, 0xB5, 0x75, 0xF5,
        0x0D, 0x8D, 0x4D, 0xCD, 0x2D, 0xAD, 0x6D, 0xED,
        0x1D, 0x9D, 0x5D, 0xDD, 0x3D, 0xBD, 0x7D, 0xFD,
        0x03, 0x83, 0x43, 0xC3, 0x23, 0xA3, 0x63, 0xE3,
        0x13, 0x93, 0x53, 0xD3, 0x33, 0xB3, 0x73, 0xF3,
        0x0B, 0x8B, 0x4B, 0xCB, 0x2B, 0xAB, 0x6B, 0xEB,
        0x1B, 0x9B, 0x5B, 0xDB, 0x3B, 0xBB, 0x7B, 0xFB,
        0x07, 0x87, 0x47, 0xC7, 0x27, 0xA7, 0x67, 0xE7,
        0x17, 0x97, 0x57, 0xD7, 0x37, 0xB7, 0x77, 0xF7,
        0x0F, 0x8F, 0x4F, 0xCF, 0x2F, 0xAF, 0x6F, 0xEF,
        0x1F, 0x9F, 0x5F, 0xDF, 0x3F, 0xBF, 0x7F, 0xFF
    };

    #region Int8
    /// <summary>
    /// Determines if the provided value is between two values, inclusively.
    /// </summary>
    /// 
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value in the range.</param>
    /// <param name="max">The maximum value in the range.</param>
    /// <returns>
    ///     <c>true</c> if <paramref name="value"/> is between <paramref name="min"/> and <paramref name="max"/>, inclusively.
    /// </returns>
    public static bool IsBetween(this sbyte value, int min, int max)
    {
        return min <= value && value <= max;
    }

    /// <summary>
    /// Determines if the provided value is between two values, inclusively.
    /// </summary>
    /// 
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value in the range.</param>
    /// <param name="max">The maximum value in the range.</param>
    /// <returns>
    ///     <c>true</c> if <paramref name="value"/> is between <paramref name="min"/> and <paramref name="max"/>, inclusively.
    /// </returns>
    public static bool IsBetween(this sbyte value, long min, long max)
    {
        return min <= value && value <= max;
    }

    /// <summary>
    /// Determines if the provided value is a power of 2.
    /// </summary>
    /// 
    /// <param name="value">The value to test.</param>
    /// <returns><c>true</c> if <paramref name="value"/> is a power 2; otherwise, <c>false</c>.</returns>
    public static bool IsPow2(this sbyte value)
    {
        return BitOperations.IsPow2(value);
    }

    /// <summary>
    /// Gets a value of the provided value with its bits reversed.
    /// </summary>
    /// 
    /// <param name="value">The value to reverse its bits.</param>
    /// <returns>A new value identical to <paramref name="value"/> but with its bits reversed.</returns>
    public static sbyte ReverseBits(this sbyte value)
    {
        return (sbyte)ReversedBytes[(byte)value];
    }

    /// <summary>
    /// Gets a string of the provided numeric value with its ordinal suffix.
    /// </summary>
    /// 
    /// <param name="value">The numeric value.</param>
    /// <returns>An ordinal string value.</returns>
    public static string ToOrdinalString(this sbyte value)
    {
        return ToOrdinalString((long)value);
    }
    #endregion

    #region Int16
    /// <summary>
    /// Determines if the provided value is between two values, inclusively.
    /// </summary>
    /// 
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value in the range.</param>
    /// <param name="max">The maximum value in the range.</param>
    /// <returns>
    ///     <c>true</c> if <paramref name="value"/> is between <paramref name="min"/> and <paramref name="max"/>, inclusively.
    /// </returns>
    public static bool IsBetween(this short value, int min, int max)
    {
        return min <= value && value <= max;
    }

    /// <summary>
    /// Determines if the provided value is between two values, inclusively.
    /// </summary>
    /// 
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value in the range.</param>
    /// <param name="max">The maximum value in the range.</param>
    /// <returns>
    ///     <c>true</c> if <paramref name="value"/> is between <paramref name="min"/> and <paramref name="max"/>, inclusively.
    /// </returns>
    public static bool IsBetween(this short value, long min, long max)
    {
        return min <= value && value <= max;
    }

    /// <summary>
    /// Determines if the provided value is a power of 2.
    /// </summary>
    /// 
    /// <param name="value">The value to test.</param>
    /// <returns><c>true</c> if <paramref name="value"/> is a power 2; otherwise, <c>false</c>.</returns>
    public static bool IsPow2(this short value)
    {
        return BitOperations.IsPow2(value);
    }

    /// <summary>
    /// Gets a value of the provided value with its bits reversed.
    /// </summary>
    /// 
    /// <param name="value">The value to reverse its bits.</param>
    /// <returns>A new value identical to <paramref name="value"/> but with its bits reversed.</returns>
    public static short ReverseBits(this short value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        ReverseBits(bytes);
        return BitConverter.ToInt16(bytes);
    }

    /// <summary>
    /// Gets a string of the provided numeric value with its ordinal suffix.
    /// </summary>
    /// 
    /// <param name="value">The numeric value.</param>
    /// <returns>An ordinal string value.</returns>
    public static string ToOrdinalString(this short value)
    {
        return ToOrdinalString((long)value);
    }
    #endregion

    #region Int32
    /// <summary>
    /// Determines if the provided value is between two values, inclusively.
    /// </summary>
    /// 
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value in the range.</param>
    /// <param name="max">The maximum value in the range.</param>
    /// <returns>
    ///     <c>true</c> if <paramref name="value"/> is between <paramref name="min"/> and <paramref name="max"/>, inclusively.
    /// </returns>
    public static bool IsBetween(this int value, int min, int max)
    {
        return min <= value && value <= max;
    }

    /// <summary>
    /// Determines if the provided value is between two values, inclusively.
    /// </summary>
    /// 
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value in the range.</param>
    /// <param name="max">The maximum value in the range.</param>
    /// <returns>
    ///     <c>true</c> if <paramref name="value"/> is between <paramref name="min"/> and <paramref name="max"/>, inclusively.
    /// </returns>
    public static bool IsBetween(this int value, long min, long max)
    {
        return min <= value && value <= max;
    }

    /// <summary>
    /// Determines if the provided value is a power of 2.
    /// </summary>
    /// 
    /// <param name="value">The value to test.</param>
    /// <returns><c>true</c> if <paramref name="value"/> is a power 2; otherwise, <c>false</c>.</returns>
    public static bool IsPow2(this int value)
    {
        return BitOperations.IsPow2(value);
    }

    /// <summary>
    /// Gets a value of the provided value with its bits reversed.
    /// </summary>
    /// 
    /// <param name="value">The value to reverse its bits.</param>
    /// <returns>A new value identical to <paramref name="value"/> but with its bits reversed.</returns>
    public static int ReverseBits(this int value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        ReverseBits(bytes);
        return BitConverter.ToInt32(bytes);
    }

    /// <summary>
    /// Gets a string of the provided numeric value with its ordinal suffix.
    /// </summary>
    /// 
    /// <param name="value">The numeric value.</param>
    /// <returns>An ordinal string value.</returns>
    public static string ToOrdinalString(this int value)
    {
        return ToOrdinalString((long)value);
    }
    #endregion

    #region Int64
    /// <summary>
    /// Determines if the provided value is between two values, inclusively.
    /// </summary>
    /// 
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value in the range.</param>
    /// <param name="max">The maximum value in the range.</param>
    /// <returns>
    ///     <c>true</c> if <paramref name="value"/> is between <paramref name="min"/> and <paramref name="max"/>, inclusively.
    /// </returns>
    public static bool IsBetween(this long value, long min, long max)
    {
        return min <= value && value <= max;
    }

    /// <summary>
    /// Determines if the provided value is a power of 2.
    /// </summary>
    /// 
    /// <param name="value">The value to test.</param>
    /// <returns><c>true</c> if <paramref name="value"/> is a power 2; otherwise, <c>false</c>.</returns>
    public static bool IsPow2(this long value)
    {
        return BitOperations.IsPow2(value);
    }

    /// <summary>
    /// Gets a value of the provided value with its bits reversed.
    /// </summary>
    /// 
    /// <param name="value">The value to reverse its bits.</param>
    /// <returns>A new value identical to <paramref name="value"/> but with its bits reversed.</returns>
    public static long ReverseBits(this long value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        ReverseBits(bytes);
        return BitConverter.ToInt64(bytes);
    }

    /// <summary>
    /// Gets a string of the provided numeric value with its ordinal suffix.
    /// </summary>
    /// 
    /// <param name="value">The numeric value.</param>
    /// <returns>An ordinal string value.</returns>
    public static string ToOrdinalString(this long value)
    {
        string suffix;

        if (value == long.MinValue)
        {
            suffix = "th";
        }
        else
        {
            value = Math.Abs(value);
            long last2Digits = value % 100;

            if (last2Digits == 11 || last2Digits == 12)
            {
                suffix = "th";
            }
            else
            {
                long lastDigit = value % 10;

                suffix = lastDigit switch
                {
                    1 => "st",
                    2 => "nd",
                    3 => "rd",
                    _ => "th"
                };
            }
        }

        return $"{value}{suffix}";
    }
    #endregion

    #region UInt8
    /// <summary>
    /// Determines if the provided value is between two values, inclusively.
    /// </summary>
    /// 
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value in the range.</param>
    /// <param name="max">The maximum value in the range.</param>
    /// <returns>
    ///     <c>true</c> if <paramref name="value"/> is between <paramref name="min"/> and <paramref name="max"/>, inclusively.
    /// </returns>
    public static bool IsBetween(this byte value, int min, int max)
    {
        return min <= value && value <= max;
    }

    /// <summary>
    /// Determines if the provided value is between two values, inclusively.
    /// </summary>
    /// 
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value in the range.</param>
    /// <param name="max">The maximum value in the range.</param>
    /// <returns>
    ///     <c>true</c> if <paramref name="value"/> is between <paramref name="min"/> and <paramref name="max"/>, inclusively.
    /// </returns>
    public static bool IsBetween(this byte value, long min, long max)
    {
        return min <= value && value <= max;
    }

    /// <summary>
    /// Determines if the provided value is a power of 2.
    /// </summary>
    /// 
    /// <param name="value">The value to test.</param>
    /// <returns><c>true</c> if <paramref name="value"/> is a power 2; otherwise, <c>false</c>.</returns>
    public static bool IsPow2(this byte value)
    {
        return BitOperations.IsPow2(value);
    }

    /// <summary>
    /// Gets a value of the provided value with its bits reversed.
    /// </summary>
    /// 
    /// <param name="value">The value to reverse its bits.</param>
    /// <returns>A new value identical to <paramref name="value"/> but with its bits reversed.</returns>
    public static byte ReverseBits(this byte value)
    {
        return ReversedBytes[value];
    }
    #endregion

    #region UInt16
    /// <summary>
    /// Determines if the provided value is between two values, inclusively.
    /// </summary>
    /// 
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value in the range.</param>
    /// <param name="max">The maximum value in the range.</param>
    /// <returns>
    ///     <c>true</c> if <paramref name="value"/> is between <paramref name="min"/> and <paramref name="max"/>, inclusively.
    /// </returns>
    public static bool IsBetween(this ushort value, int min, int max)
    {
        return min <= value && value <= max;
    }

    /// <summary>
    /// Determines if the provided value is between two values, inclusively.
    /// </summary>
    /// 
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value in the range.</param>
    /// <param name="max">The maximum value in the range.</param>
    /// <returns>
    ///     <c>true</c> if <paramref name="value"/> is between <paramref name="min"/> and <paramref name="max"/>, inclusively.
    /// </returns>
    public static bool IsBetween(this ushort value, long min, long max)
    {
        return min <= value && value <= max;
    }

    /// <summary>
    /// Determines if the provided value is a power of 2.
    /// </summary>
    /// 
    /// <param name="value">The value to test.</param>
    /// <returns><c>true</c> if <paramref name="value"/> is a power 2; otherwise, <c>false</c>.</returns>
    public static bool IsPow2(this ushort value)
    {
        return BitOperations.IsPow2(value);
    }

    /// <summary>
    /// Gets a value of the provided value with its bits reversed.
    /// </summary>
    /// 
    /// <param name="value">The value to reverse its bits.</param>
    /// <returns>A new value identical to <paramref name="value"/> but with its bits reversed.</returns>
    public static ushort ReverseBits(this ushort value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        ReverseBits(bytes);
        return BitConverter.ToUInt16(bytes);
    }
    #endregion

    #region UInt32
    /// <summary>
    /// Determines if the provided value is between two values, inclusively.
    /// </summary>
    /// 
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value in the range.</param>
    /// <param name="max">The maximum value in the range.</param>
    /// <returns>
    ///     <c>true</c> if <paramref name="value"/> is between <paramref name="min"/> and <paramref name="max"/>, inclusively.
    /// </returns>
    public static bool IsBetween(this uint value, uint min, uint max)
    {
        return min <= value && value <= max;
    }

    /// <summary>
    /// Determines if the provided value is between two values, inclusively.
    /// </summary>
    /// 
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value in the range.</param>
    /// <param name="max">The maximum value in the range.</param>
    /// <returns>
    ///     <c>true</c> if <paramref name="value"/> is between <paramref name="min"/> and <paramref name="max"/>, inclusively.
    /// </returns>
    public static bool IsBetween(this uint value, long min, long max)
    {
        return min <= value && value <= max;
    }

    /// <summary>
    /// Determines if the provided value is between two values, inclusively.
    /// </summary>
    /// 
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value in the range.</param>
    /// <param name="max">The maximum value in the range.</param>
    /// <returns>
    ///     <c>true</c> if <paramref name="value"/> is between <paramref name="min"/> and <paramref name="max"/>, inclusively.
    /// </returns>
    public static bool IsBetween(this uint value, ulong min, ulong max)
    {
        return min <= value && value <= max;
    }

    /// <summary>
    /// Determines if the provided value is a power of 2.
    /// </summary>
    /// 
    /// <param name="value">The value to test.</param>
    /// <returns><c>true</c> if <paramref name="value"/> is a power 2; otherwise, <c>false</c>.</returns>
    public static bool IsPow2(this uint value)
    {
        return BitOperations.IsPow2(value);
    }

    /// <summary>
    /// Gets a value of the provided value with its bits reversed.
    /// </summary>
    /// 
    /// <param name="value">The value to reverse its bits.</param>
    /// <returns>A new value identical to <paramref name="value"/> but with its bits reversed.</returns>
    public static uint ReverseBits(this uint value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        ReverseBits(bytes);
        return BitConverter.ToUInt32(bytes);
    }
    #endregion

    #region UInt64
    /// <summary>
    /// Determines if the provided value is between two values, inclusively.
    /// </summary>
    /// 
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value in the range.</param>
    /// <param name="max">The maximum value in the range.</param>
    /// <returns>
    ///     <c>true</c> if <paramref name="value"/> is between <paramref name="min"/> and <paramref name="max"/>, inclusively.
    /// </returns>
    public static bool IsBetween(this ulong value, ulong min, ulong max)
    {
        return min <= value && value <= max;
    }

    /// <summary>
    /// Determines if the provided value is a power of 2.
    /// </summary>
    /// 
    /// <param name="value">The value to test.</param>
    /// <returns><c>true</c> if <paramref name="value"/> is a power 2; otherwise, <c>false</c>.</returns>
    public static bool IsPow2(this ulong value)
    {
        return BitOperations.IsPow2(value);
    }

    /// <summary>
    /// Gets a value of the provided value with its bits reversed.
    /// </summary>
    /// 
    /// <param name="value">The value to reverse its bits.</param>
    /// <returns>A new value identical to <paramref name="value"/> but with its bits reversed.</returns>
    public static ulong ReverseBits(this ulong value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        ReverseBits(bytes);
        return BitConverter.ToUInt64(bytes);
    }
    #endregion

    #region Single
    /// <summary>
    /// Determines if the provided value is between two values, inclusively.
    /// </summary>
    /// 
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value in the range.</param>
    /// <param name="max">The maximum value in the range.</param>
    /// <returns>
    ///     <c>true</c> if <paramref name="value"/> is between <paramref name="min"/> and <paramref name="max"/>, inclusively.
    /// </returns>
    public static bool IsBetween(this float value, float min, float max)
    {
        return min <= value && value <= max;
    }

    /// <summary>
    /// Determines if the provided value is between two values, inclusively.
    /// </summary>
    /// 
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value in the range.</param>
    /// <param name="max">The maximum value in the range.</param>
    /// <returns>
    ///     <c>true</c> if <paramref name="value"/> is between <paramref name="min"/> and <paramref name="max"/>, inclusively.
    /// </returns>
    public static bool IsBetween(this float value, double min, double max)
    {
        return min <= value && value <= max;
    }

    /// <summary>
    /// Gets a value of the provided value with its bits reversed.
    /// </summary>
    /// 
    /// <param name="value">The value to reverse its bits.</param>
    /// <returns>A new value identical to <paramref name="value"/> but with its bits reversed.</returns>
    public static float ReverseBits(this float value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        ReverseBits(bytes);
        return BitConverter.ToSingle(bytes);
    }
    #endregion

    #region Double
    /// <summary>
    /// Determines if the provided value is between two values, inclusively.
    /// </summary>
    /// 
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value in the range.</param>
    /// <param name="max">The maximum value in the range.</param>
    /// <returns>
    ///     <c>true</c> if <paramref name="value"/> is between <paramref name="min"/> and <paramref name="max"/>, inclusively.
    /// </returns>
    public static bool IsBetween(this double value, double min, double max)
    {
        return min <= value && value <= max;
    }

    /// <summary>
    /// Gets a value of the provided value with its bits reversed.
    /// </summary>
    /// 
    /// <param name="value">The value to reverse its bits.</param>
    /// <returns>A new value identical to <paramref name="value"/> but with its bits reversed.</returns>
    public static double ReverseBits(this double value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        ReverseBits(bytes);
        return BitConverter.ToDouble(bytes);
    }
    #endregion

    // Internal method to reverse the bits of an entire region of memory.
    private static void ReverseBits(Span<byte> bytes)
    {
        for (int i = 0; i < bytes.Length; i++)
        {
            bytes[i] = ReversedBytes[bytes[i]];
        }

        bytes.Reverse();
    }
}

using System;
using System.Diagnostics;
using System.Text;

namespace LabOfKiwi;

public static partial class ArrayExtensions
{
    /// <summary>
    /// Returns a binary string representation of the provided byte array, formatted based on the remaining method
    /// parameters.
    /// </summary>
    /// 
    /// <param name="array">The byte array to read as a binary string.</param>
    /// <param name="delimiter">A sequence of text placed between each grouping.</param>
    /// <param name="groupByNibbles">
    ///     If set to <c>true</c>, grouping of bits is per nibble; otherwise, grouping is per byte.
    /// </param>
    /// <returns>A binary string representation of <paramref name="array"/>.</returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="array"/> is <c>null</c>.</exception>
    /// <exception cref="OverflowException">The resulting string would be too large.</exception>
    public static string ToBinaryString(this byte[] array, string? delimiter = null, bool groupByNibbles = false)
    {
        if (array == null)
        {
            throw new ArgumentNullException(nameof(array));
        }

        if (array.Length == 0)
        {
            return string.Empty;
        }

        int resultLength;

        checked
        {
            resultLength = array.Length * 8;

            if (groupByNibbles)
            {
                resultLength += (2 * array.Length - 1) * (delimiter?.Length ?? 0);
            }
            else
            {
                resultLength += (array.Length - 1) * (delimiter?.Length ?? 0);
            }
        }

        StringBuilder sb = new StringBuilder(resultLength);
        string bin;

        if (string.IsNullOrEmpty(delimiter))
        {
            for (int i = 0; i < array.Length; i++)
            {
                bin = Convert.ToString(array[i], 2).PadLeft(8, '0');
                sb.Append(bin);
            }    
        }
        else if (groupByNibbles)
        {
            bin = Convert.ToString(array[0], 2).PadLeft(8, '0');
            sb.Append(bin[0..4] + delimiter + bin[4..]);

            for (int i = 1; i < array.Length; i++)
            {
                sb.Append(delimiter);
                bin = Convert.ToString(array[i], 2).PadLeft(8, '0');
                sb.Append(bin[0..4] + delimiter + bin[4..]);
            }
        }
        else
        {
            bin = Convert.ToString(array[0], 2).PadLeft(8, '0');
            sb.Append(bin);

            for (int i = 1; i < array.Length; i++)
            {
                sb.Append(delimiter);
                bin = Convert.ToString(array[i], 2).PadLeft(8, '0');
                sb.Append(bin);
            }
        }

        Debug.Assert(sb.Length == resultLength);
        return sb.ToString();
    }

    /// <summary>
    /// Returns a hexadecimal string representation of the provided byte array, formatted based on the remaining
    /// method parameters.
    /// </summary>
    /// 
    /// <param name="array">The byte array to read as a hexadecimal string.</param>
    /// <param name="uppercase">If set to <c>true</c>, resulting string is uppercase; otherwise, lowercase.</param>
    /// <param name="delimiter">A sequence of text placed between each byte.</param>
    /// <returns>A hexadecimal string representation of <paramref name="array"/>.</returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="array"/> is <c>null</c>.</exception>
    /// <exception cref="OverflowException">The resulting string would be too large.</exception>
    public static string ToHexString(this byte[] array, bool uppercase = false, string? delimiter = null)
    {
        if (array == null)
        {
            throw new ArgumentNullException(nameof(array));
        }

        if (array.Length == 0)
        {
            return string.Empty;
        }

        int resultLength;

        checked
        {
            resultLength = array.Length * 2;
            resultLength += (array.Length - 1) * (delimiter?.Length ?? 0);
        }

        StringBuilder sb = new StringBuilder(resultLength);
        string hex;

        if (uppercase)
        {
            if (string.IsNullOrEmpty(delimiter))
            {
                for (int i = 0; i < array.Length; i++)
                {
                    hex = Convert.ToString(array[i], 16).ToUpperInvariant();

                    if (hex.Length == 2)
                    {
                        sb.Append(hex);
                    }
                    else
                    {
                        sb.Append('0').Append(hex[0]);
                    }
                }
            }
            else
            {
                hex = Convert.ToString(array[0], 16).ToUpperInvariant();

                if (hex.Length == 2)
                {
                    sb.Append(hex);
                }
                else
                {
                    sb.Append('0').Append(hex[0]);
                }

                for (int i = 1; i < array.Length; i++)
                {
                    sb.Append(delimiter);

                    hex = Convert.ToString(array[i], 16).ToUpperInvariant();

                    if (hex.Length == 2)
                    {
                        sb.Append(hex);
                    }
                    else
                    {
                        sb.Append('0').Append(hex[0]);
                    }
                }
            }
        }
        else
        {
            if (string.IsNullOrEmpty(delimiter))
            {
                for (int i = 0; i < array.Length; i++)
                {
                    hex = Convert.ToString(array[i], 16);

                    if (hex.Length == 2)
                    {
                        sb.Append(hex);
                    }
                    else
                    {
                        sb.Append('0').Append(hex[0]);
                    }
                }
            }
            else
            {
                hex = Convert.ToString(array[0], 16);

                if (hex.Length == 2)
                {
                    sb.Append(hex);
                }
                else
                {
                    sb.Append('0').Append(hex[0]);
                }

                for (int i = 1; i < array.Length; i++)
                {
                    sb.Append(delimiter);

                    hex = Convert.ToString(array[i], 16);

                    if (hex.Length == 2)
                    {
                        sb.Append(hex);
                    }
                    else
                    {
                        sb.Append('0').Append(hex[0]);
                    }
                }
            }
        }

        Debug.Assert(sb.Length == resultLength);
        return sb.ToString();
    }
}

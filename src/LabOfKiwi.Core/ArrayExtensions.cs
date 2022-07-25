using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace LabOfKiwi;

/// <summary>
/// Extension methods for arrays.
/// </summary>
public static partial class ArrayExtensions
{
    public static T[] Add<T>(this T[] array, T item)
    {
        int index = array.Length;
        Array.Resize(ref array, index + 1);
        array[index] = item;
        return array;
    }

    public static bool Contains<T>(this T[] array, T item)
    {
        return ((IList<T>)array).Contains(item);
    }

    public static bool Contains(Array array, object? item)
    {
        return ((IList)array).Contains(item);
    }

    /// <summary>
    /// Returns a new array that center-aligns the elements in the provided array by padding them on the left and right
    /// with a specified value, for a specified total length.
    /// </summary>
    /// 
    /// <typeparam name="T">The type of elements in <paramref name="array"/>.</typeparam>
    /// <param name="array">The array to be padded.</param>
    /// <param name="totalWidth">
    ///     The number of elements in the resulting array, equal to the number of original elements plus any additional
    ///     padding elements.
    /// </param>
    /// <param name="paddingValue">A padding value.</param>
    /// <param name="preferLeftPadding">
    ///     Flag to determine which side to prefer padding if <paramref name="array"/> is unable to be centered exactly.
    ///     Default is left-aligned by padding on the right.
    /// </param>
    /// <returns>
    ///     A new array equivalent to <paramref name="array"/>, but center-aligned and padded on the left and right with
    ///     as many <paramref name="paddingValue"/> elements as needed to create a length of
    ///     <paramref name="totalWidth"/>. However, if <paramref name="totalWidth"/> is less than the length of
    ///     <paramref name="array"/>, the method returns a reference to <paramref name="array"/>. If
    ///     <paramref name="totalWidth"/> is equal to the length of <paramref name="array"/>, the method returns a copy
    ///     of <paramref name="array"/>.
    /// </returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="array"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="totalWidth"/> is less than zero.</exception>
    public static T[] PadCenter<T>(this T[] array, int totalWidth, T paddingValue, bool preferLeftPadding = false)
    {
        if (array == null)
        {
            throw new ArgumentNullException(nameof(array));
        }

        if (totalWidth < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(totalWidth));
        }

        int toPad = totalWidth - array.Length;

        T[] result;

        if (toPad < 0)
        {
            result = array;
        }
        else if (toPad == 0)
        {
            result = new T[array.Length];
            Array.Copy(array, result, array.Length);
        }
        else
        {
            result = new T[totalWidth];

            int smallPad = toPad / 2;
            int largePad = toPad - smallPad;

            int leftPad, rightPad;

            if (preferLeftPadding)
            {
                leftPad = largePad;
                rightPad = smallPad;
            }
            else
            {
                leftPad = smallPad;
                rightPad = largePad;
            }

            Debug.Assert(leftPad + rightPad + array.Length == totalWidth);

            Array.Fill(result, paddingValue, 0, leftPad);
            Array.Copy(array, 0, result, leftPad, array.Length);
            Array.Fill(result, paddingValue, leftPad + array.Length, rightPad);
        }

        return result;
    }

    /// <summary>
    /// Returns a new array that right-aligns the elements in the provided array by padding them on the left with a
    /// specified value, for a specified total length.
    /// </summary>
    /// 
    /// <typeparam name="T">The type of elements in <paramref name="array"/>.</typeparam>
    /// <param name="array">The array to be padded.</param>
    /// <param name="totalWidth">
    ///     The number of elements in the resulting array, equal to the number of original elements plus any additional
    ///     padding elements.
    /// </param>
    /// <param name="paddingValue">A padding value.</param>
    /// <returns>
    ///     A new array equivalent to <paramref name="array"/>, but right-aligned and padded on the left with as many
    ///     <paramref name="paddingValue"/> elements as needed to create a length of <paramref name="totalWidth"/>.
    ///     However, if <paramref name="totalWidth"/> is less than the length of <paramref name="array"/>, the method
    ///     returns a reference to <paramref name="array"/>. If <paramref name="totalWidth"/> is equal to the length of
    ///     <paramref name="array"/>, the method returns a copy of <paramref name="array"/>.
    /// </returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="array"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="totalWidth"/> is less than zero.</exception>
    public static T[] PadLeft<T>(this T[] array, int totalWidth, T paddingValue)
    {
        if (array == null)
        {
            throw new ArgumentNullException(nameof(array));
        }

        if (totalWidth < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(totalWidth));
        }

        int toPad = totalWidth - array.Length;

        T[] result;

        if (toPad < 0)
        {
            result = array;
        }
        else if (toPad == 0)
        {
            result = new T[array.Length];
            Array.Copy(array, result, array.Length);
        }
        else
        {
            result = new T[totalWidth];
            Array.Fill(result, paddingValue, 0, toPad);
            Array.Copy(array, 0, result, toPad, array.Length);
        }

        return result;
    }

    /// <summary>
    /// Returns a new array that left-aligns the elements in the provided array by padding them on the right with a
    /// specified value, for a specified total length.
    /// </summary>
    /// 
    /// <typeparam name="T">The type of elements in <paramref name="array"/>.</typeparam>
    /// <param name="array">The array to be padded.</param>
    /// <param name="totalWidth">
    ///     The number of elements in the resulting array, equal to the number of original elements plus any additional
    ///     padding elements.
    /// </param>
    /// <param name="paddingValue">A padding value.</param>
    /// <returns>
    ///     A new array equivalent to <paramref name="array"/>, but left-aligned and padded on the right with as many
    ///     <paramref name="paddingValue"/> elements as needed to create a length of <paramref name="totalWidth"/>.
    ///     However, if <paramref name="totalWidth"/> is less than the length of <paramref name="array"/>, the method
    ///     returns a reference to <paramref name="array"/>. If <paramref name="totalWidth"/> is equal to the length of
    ///     <paramref name="array"/>, the method returns a copy of <paramref name="array"/>.
    /// </returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="array"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="totalWidth"/> is less than zero.</exception>
    public static T[] PadRight<T>(this T[] array, int totalWidth, T paddingValue)
    {
        if (array == null)
        {
            throw new ArgumentNullException(nameof(array));
        }

        if (totalWidth < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(totalWidth));
        }

        int toPad = totalWidth - array.Length;

        T[] result;

        if (toPad < 0)
        {
            result = array;
        }
        else if (toPad == 0)
        {
            result = new T[array.Length];
            Array.Copy(array, result, array.Length);
        }
        else
        {
            result = new T[totalWidth];
            Array.Copy(array, 0, result, 0, array.Length);
            Array.Fill(result, paddingValue, array.Length, toPad);
        }

        return result;
    }

    /// <summary>
    /// Determines if two array instances are structurally identical.
    /// </summary>
    /// 
    /// <typeparam name="T">The type of elements in the arrays.</typeparam>
    /// <param name="array">The first array.</param>
    /// <param name="other">The second array.</param>
    /// <returns>
    ///     <c>true</c> if both arrays are the same reference or if their sizes are identical and contains the same
    ///     values; otherwise, <c>false</c>.
    /// </returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="array"/> is <c>null</c>.</exception>
    public static bool StructurallyEquals<T>(this T[] array, T[]? other) where T : struct
    {
        if (array == null)
        {
            throw new ArgumentNullException(nameof(array));
        }

        if (ReferenceEquals(array, other))
        {
            return true;
        }

        if (other == null)
        {
            return array.Length == 0;
        }

        if (array.Length != other.Length)
        {
            return false;
        }

        for (int i = 0; i < array.Length; i++)
        {
            T e1 = array[i];
            T e2 = other[i];

            if (!e1.Equals(e2))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Determines if two array instances are structurally identical.
    /// </summary>
    /// 
    /// <typeparam name="T">The type of elements in the arrays.</typeparam>
    /// <param name="array">The first array.</param>
    /// <param name="other">The second array.</param>
    /// <param name="forceReferenceMatch">Flag that requires elements to be identical references.</param>
    /// <returns>
    ///     <c>true</c> if both arrays are the same reference or if their sizes are identical and contains the same
    ///     values; otherwise, <c>false</c>.
    /// </returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="array"/> is <c>null</c>.</exception>
    public static bool StructurallyEquals<T>(this T[] array, T[]? other, bool forceReferenceMatch = false) where T : class
    {
        if (array == null)
        {
            throw new ArgumentNullException(nameof(array));
        }

        if (ReferenceEquals(array, other))
        {
            return true;
        }

        if (other == null)
        {
            return array.Length == 0;
        }

        if (array.Length != other.Length)
        {
            return false;
        }

        if (forceReferenceMatch)
        {
            for (int i = 0; i < array.Length; i++)
            {
                T e1 = array[i];
                T e2 = other[i];

                if (!ReferenceEquals(e1, e2))
                {
                    return false;
                }
            }
        }
        else
        {
            for (int i = 0; i < array.Length; i++)
            {
                T e1 = array[i];
                T e2 = other[i];

                if (!ReferenceEquals(e1, e2))
                {
                    if (e1 == null || e2 == null)
                    {
                        return false;
                    }

                    if (!e1.Equals(e2))
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }
}

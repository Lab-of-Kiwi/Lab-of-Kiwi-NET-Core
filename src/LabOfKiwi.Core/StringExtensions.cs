using LabOfKiwi.Text;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace LabOfKiwi;

/// <summary>
/// Extension methods for <see cref="string"/> objects.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Searches for a <see cref="char"/> that matches the conditions defined by the specified predicate, and returns
    /// the zero-based index of the first occurrence within the entire <see cref="string"/>.
    /// </summary>
    /// 
    /// <param name="value">The <see cref="string"/> to search.</param>
    /// <param name="match">
    ///     The <see cref="Predicate{T}"/> delegate that defines the conditions of the <see cref="char"/> to search for.
    /// </param>
    /// <returns>
    ///     The zero-based index of the first occurrence of a <see cref="char"/> that matches the conditions defined by
    ///     <paramref name="match"/>, if found; otherwise, -1.
    /// </returns>
    /// 
    /// <exception cref="ArgumentNullException">
    ///     <paramref name="value"/> is <c>null</c>.
    ///     -or-
    ///     <paramref name="match"/> is <c>null</c>.
    /// </exception>
    public static int FindIndex(this string value, Predicate<char> match)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        return FindIndex(value, 0, value.Length, match);
    }

    /// <summary>
    /// Searches for a <see cref="char"/> that matches the conditions defined by the specified predicate, and returns
    /// the zero-based index of the first occurrence within the range of <see cref="char"/> elements in the
    /// <see cref="string"/> that extends from the specified index to the last <see cref="char"/>.
    /// </summary>
    /// 
    /// <param name="value">The <see cref="string"/> to search.</param>
    /// <param name="startIndex">The zero-based starting index of the search.</param>
    /// <param name="match">
    ///     The <see cref="Predicate{T}"/> delegate that defines the conditions of the <see cref="char"/> to search for.
    /// </param>
    /// <returns>
    ///     The zero-based index of the first occurrence of a <see cref="char"/> that matches the conditions defined by
    ///     <paramref name="match"/>, if found; otherwise, -1.
    /// </returns>
    /// 
    /// <exception cref="ArgumentNullException">
    ///     <paramref name="value"/> is <c>null</c>.
    ///     -or-
    ///     <paramref name="match"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     <paramref name="startIndex"/> is outside the range of valid indexes for the <see cref="string"/>.
    /// </exception>
    public static int FindIndex(this string value, int startIndex, Predicate<char> match)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        return FindIndex(value, startIndex, value.Length - startIndex, match);
    }

    /// <summary>
    /// Searches for a <see cref="char"/> that matches the conditions defined by the specified predicate, and returns
    /// the zero-based index of the first occurrence within the range of <see cref="char"/> elements in the
    /// <see cref="string"/> that starts at the specified index and contains the specified number of <see cref="char"/>
    /// elements.
    /// </summary>
    /// 
    /// <param name="value">The <see cref="string"/> to search.</param>
    /// <param name="startIndex">The zero-based starting index of the search.</param>
    /// <param name="count">The number of <see cref="char"/> elements in the section to search.</param>
    /// <param name="match">
    ///     The <see cref="Predicate{T}"/> delegate that defines the conditions of the <see cref="char"/> to search for.
    /// </param>
    /// <returns>
    ///     The zero-based index of the first occurrence of a <see cref="char"/> that matches the conditions defined by
    ///     <paramref name="match"/>, if found; otherwise, -1.
    /// </returns>
    /// 
    /// <exception cref="ArgumentNullException">
    ///     <paramref name="value"/> is <c>null</c>.
    ///     -or-
    ///     <paramref name="match"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     <paramref name="startIndex"/> is outside the range of valid indexes for the <see cref="string"/>.
    ///     -or-
    ///     <paramref name="count"/> is less than 0.
    ///     -or-
    ///     <paramref name="startIndex"/> and <paramref name="count"/> do not specify a valid section in the
    ///     <see cref="string"/>.
    /// </exception>
    public static int FindIndex(this string value, int startIndex, int count, Predicate<char> match)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if ((uint)startIndex > (uint)value.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(startIndex));
        }

        if (count < 0 || startIndex > value.Length - count)
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }

        if (match == null)
        {
            throw new ArgumentNullException(nameof(match));
        }

        int endIndex = startIndex + count;

        for (int i = startIndex; i < endIndex; i++)
        {
            if (match(value[i]))
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// Searches for a <see cref="char"/> that matches the conditions defined by the specified predicate, and returns
    /// the zero-based index of the last occurrence within the entire <see cref="string"/>.
    /// </summary>
    /// 
    /// <param name="value">The <see cref="string"/> to search.</param>
    /// <param name="match">
    ///     The <see cref="Predicate{T}"/> delegate that defines the conditions of the <see cref="char"/> to search for.
    /// </param>
    /// <returns>
    ///     The zero-based index of the last occurrence of a <see cref="char"/> that matches the conditions defined by
    ///     <paramref name="match"/>, if found; otherwise, -1.
    /// </returns>
    /// 
    /// <exception cref="ArgumentNullException">
    ///     <paramref name="value"/> is <c>null</c>.
    ///     -or-
    ///     <paramref name="match"/> is <c>null</c>.
    /// </exception>
    public static int FindLastIndex(this string value, Predicate<char> match)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        return FindLastIndex(value, value.Length - 1, value.Length, match);
    }

    /// <summary>
    /// Searches for a <see cref="char"/> that matches the conditions defined by the specified predicate, and returns
    /// the zero-based index of the last occurrence within the range of <see cref="char"/> elements in the
    /// <see cref="string"/> that extends from the first <see cref="char"/> to the specified index.
    /// </summary>
    /// 
    /// <param name="value">The <see cref="string"/> to search.</param>
    /// <param name="startIndex">The zero-based starting index of the backward search.</param>
    /// <param name="match">
    ///     The <see cref="Predicate{T}"/> delegate that defines the conditions of the <see cref="char"/> to search for.
    /// </param>
    /// <returns>
    ///     The zero-based index of the last occurrence of a <see cref="char"/> that matches the conditions defined by
    ///     <paramref name="match"/>, if found; otherwise, -1.
    /// </returns>
    /// 
    /// <exception cref="ArgumentNullException">
    ///     <paramref name="value"/> is <c>null</c>.
    ///     -or-
    ///     <paramref name="match"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     <paramref name="startIndex"/> is outside the range of valid indexes for the <see cref="string"/>.
    /// </exception>
    public static int FindLastIndex(this string value, int startIndex, Predicate<char> match)
    {
        return FindLastIndex(value, startIndex, startIndex + 1, match);
    }

    /// <summary>
    /// Searches for a <see cref="char"/> that matches the conditions defined by the specified predicate, and returns
    /// the zero-based index of the last occurrence within the range of <see cref="char"/> elements in the
    /// <see cref="string"/> that contains the specified number of <see cref="char"/> elements and ends at the specified
    /// index.
    /// </summary>
    /// 
    /// <param name="value">The <see cref="string"/> to search.</param>
    /// <param name="startIndex">The zero-based starting index of the backward search.</param>
    /// <param name="count">The number of <see cref="char"/> elements in the section to search.</param>
    /// <param name="match">
    ///     The <see cref="Predicate{T}"/> delegate that defines the conditions of the <see cref="char"/> to search for.
    /// </param>
    /// <returns>
    ///     The zero-based index of the last occurrence of a <see cref="char"/> that matches the conditions defined by
    ///     <paramref name="match"/>, if found; otherwise, -1.
    /// </returns>
    /// 
    /// <exception cref="ArgumentNullException">
    ///     <paramref name="value"/> is <c>null</c>.
    ///     -or-
    ///     <paramref name="match"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     <paramref name="startIndex"/> is outside the range of valid indexes for the <see cref="string"/>.
    ///     -or-
    ///     <paramref name="count"/> is less than 0.
    ///     -or-
    ///     <paramref name="startIndex"/> and <paramref name="count"/> do not specify a valid section in the
    ///     <see cref="string"/>.
    /// </exception>
    public static int FindLastIndex(this string value, int startIndex, int count, Predicate<char> match)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (match == null)
        {
            throw new ArgumentNullException(nameof(match));
        }

        if (value.Length == 0)
        {
            // Special case for 0 length string
            if (startIndex != -1)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }
        }
        else
        {
            if ((uint)startIndex >= (uint)value.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }
        }

        if (count < 0 || startIndex - count + 1 < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }

        int endIndex = startIndex - count;

        for (int i = startIndex; i > endIndex; i--)
        {
            if (match(value[i]))
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// Returns a new string that center-aligns the characters in the provided string by padding them on the left and
    /// right with a specified Unicode character, for a specified total length.
    /// </summary>
    /// 
    /// <param name="value">The string to be padded.</param>
    /// <param name="totalWidth">
    ///     The number of characters in the resulting string, equal to the number of original characters plus any
    ///     additional padding characters.
    /// </param>
    /// <param name="paddingChar">A Unicode padding character.</param>
    /// <param name="preferLeftPadding">
    ///     Flag to determine which side to prefer padding if <paramref name="value"/> is unable to be centered exactly.
    ///     Default is left-aligned by padding on the right.
    /// </param>
    /// <returns>
    ///     A new string equivalent to <paramref name="value"/>, but center-aligned and padded on the left and right
    ///     with as many <paramref name="paddingChar"/> characters as needed to create a length of
    ///     <paramref name="totalWidth"/>. However, if <paramref name="totalWidth"/> is less than the length of
    ///     <paramref name="value"/>, the method returns a reference to <paramref name="value"/>. If
    ///     <paramref name="totalWidth"/> is equal to the length of <paramref name="value"/>, the method returns a new
    ///     string that is identical to <paramref name="value"/>.
    /// </returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="totalWidth"/> is less than zero.</exception>
    public static string PadCenter(this string value, int totalWidth, char paddingChar = ' ',
        bool preferLeftPadding = false)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (totalWidth < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(totalWidth));
        }

        if (value.Length > totalWidth)
        {
            return value;
        }

        char[] chars = value.ToCharArray();
        char[] result = chars.PadCenter(totalWidth, paddingChar, preferLeftPadding);

        Debug.Assert(!ReferenceEquals(chars, result));

        return new string(result);
    }

    /// <summary>
    /// Returns a string that has the same characters as the input string, but with order reversed.
    /// </summary>
    /// 
    /// <param name="value">The value to be reversed.</param>
    /// <returns>A new string with <paramref name="value"/> characters in reverse order.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
    public static string Reverse(this string value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (value.Length < 2)
        {
            return value;
        }

        char[] arr = value.ToCharArray();
        Array.Reverse(arr);

        return new string(arr);
    }

    /// <summary>
    /// Returns a string that is snake-case, i.e. words are separated with underscores, using the provided string.
    /// </summary>
    /// 
    /// <param name="value">The value to be converted to snake-case.</param>
    /// <returns>A new string in snake-case.</returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
    public static string ToSnakeCase(this string value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (value.Length == 0)
        {
            return value;
        }

        StringBuilder builder = new(value.Length + Math.Min(2, value.Length / 5));
        UnicodeCategory? previousCategory = null;

        for (var currentIndex = 0; currentIndex < value.Length; currentIndex++)
        {
            var currentChar = value[currentIndex];

            if (currentChar == '_')
            {
                builder.Append('_');
                previousCategory = null;
                continue;
            }

            var currentCategory = char.GetUnicodeCategory(currentChar);

            switch (currentCategory)
            {
                case UnicodeCategory.UppercaseLetter:
                case UnicodeCategory.TitlecaseLetter:
                    if (previousCategory == UnicodeCategory.SpaceSeparator ||
                        previousCategory == UnicodeCategory.LowercaseLetter ||
                        previousCategory != UnicodeCategory.DecimalDigitNumber &&
                        previousCategory != null &&
                        currentIndex > 0 &&
                        currentIndex + 1 < value.Length &&
                        char.IsLower(value[currentIndex + 1]))
                    {
                        builder.Append('_');
                    }

                    currentChar = char.ToLower(currentChar);
                    break;

                case UnicodeCategory.LowercaseLetter:
                case UnicodeCategory.DecimalDigitNumber:
                    if (previousCategory == UnicodeCategory.SpaceSeparator)
                    {
                        builder.Append('_');
                    }
                    break;

                default:
                    if (previousCategory != null)
                    {
                        previousCategory = UnicodeCategory.SpaceSeparator;
                    }
                    continue;
            }

            builder.Append(currentChar);
            previousCategory = currentCategory;
        }

        return builder.ToString();
    }

    public static StringList ToStringList(this string? value)
    {
        return new StringList(value);
    }
}

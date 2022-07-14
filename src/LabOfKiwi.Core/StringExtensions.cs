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
}

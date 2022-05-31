using System;
using System.Diagnostics;

namespace LabOfKiwi
{
    /// <summary>
    /// Extension methods for <see cref="string"/> objects.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Returns a new string that center-aligns the characters in the provided string by padding them on the left
        /// and right with a specified Unicode character, for a specified total length.
        /// </summary>
        /// 
        /// <param name="value">The string to be padded.</param>
        /// <param name="totalWidth">
        ///     The number of characters in the resulting string, equal to the number of original characters plus any
        ///     additional padding characters.
        /// </param>
        /// <param name="paddingChar">A Unicode padding character.</param>
        /// <param name="preferLeftPadding">
        ///     Flag to determine which side to prefer padding if <paramref name="value"/> is unable to be centered
        ///     exactly. Default is left-aligned by padding on the right.
        /// </param>
        /// <returns>
        ///     A new string equivalent to <paramref name="value"/>, but center-aligned and padded on the left and right
        ///     with as many <paramref name="paddingChar"/> characters as needed to create a length of
        ///     <paramref name="totalWidth"/>. However, if <paramref name="totalWidth"/> is less than the length of
        ///     <paramref name="value"/>, the method returns a reference to <paramref name="value"/>. If
        ///     <paramref name="totalWidth"/> is equal to the length of <paramref name="value"/>, the method returns a
        ///     new string that is identical to <paramref name="value"/>.
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
    }
}

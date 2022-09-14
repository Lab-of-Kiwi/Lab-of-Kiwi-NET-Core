namespace LabOfKiwi;

/// <summary>
/// Extension methods for <see cref="char"/> values.
/// </summary>
public static class CharExtensions
{
    /// <summary>
    /// Determines if the provided value is between two values, inclusively.
    /// </summary>
    /// 
    /// <param name="value">The value to test.</param>
    /// <param name="min">The minimum value in the range.</param>
    /// <param name="max">The maximum value in the range.</param>
    /// <returns>
    ///     <c>true</c> if <paramref name="value"/> is between <paramref name="min"/> and <paramref name="max"/>,
    ///     inclusively.
    /// </returns>
    public static bool IsBetween(this char value, char min, char max)
    {
        return min <= value && value <= max;
    }

    /// <summary>
    /// Gets a value of the provided value with its bits reversed.
    /// </summary>
    /// 
    /// <param name="value">The value to reverse its bits.</param>
    /// <returns>A new value identical to <paramref name="value"/> but with its bits reversed.</returns>
    public static char ReverseBits(this char value)
    {
        return (char)((ushort)value).ReverseBits();
    }
}

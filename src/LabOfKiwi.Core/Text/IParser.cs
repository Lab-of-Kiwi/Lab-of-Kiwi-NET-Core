using System.Diagnostics.CodeAnalysis;

namespace LabOfKiwi.Text;

/// <summary>
/// Provides methods to parse values of various types.
/// </summary>
/// 
/// <typeparam name="T">The type of element to be parsed.</typeparam>
public interface IParser<T> where T : notnull
{
    /// <summary>
    /// Determines if the provided value of type <typeparamref name="T"/> is valid.
    /// </summary>
    /// 
    /// <param name="value">The value to check for validity.</param>
    /// <returns><c>true</c> if <paramref name="value"/> is valid; otherwise, <c>false</c>.</returns>
    bool IsValid(T value);

    /// <summary>
    /// Parses the provided string into an object of type <typeparamref name="T"/>.
    /// </summary>
    /// 
    /// <param name="input">The string to be parsed.</param>
    /// <param name="output">
    ///     The parsed value, if parsing was successful; otherwise, the default value of
    ///     <typeparamref name="T"/>.
    /// </param>
    /// <returns><c>true</c> if <paramref name="input"/> was successfuly parsed; otherwise, <c>false</c>.</returns>
    bool TryParse(string input, [MaybeNullWhen(false)] out T output);

    /// <summary>
    /// Gets the string representation of an object of type <typeparamref name="T"/>.
    /// </summary>
    /// 
    /// <param name="input">The value of type <typeparamref name="T"/> to get the string representation.</param>
    /// <param name="output">
    ///     The string representation of <paramref name="input"/>, if successful; otherwise, <c>null</c>.
    /// </param>
    /// <returns>
    ///     <c>true</c> if <paramref name="input"/> was able to be converted to a string representation; otherwise,
    ///     <c>false</c>.
    /// </returns>
    bool TryToString(T input, [MaybeNullWhen(false)] out string output);
}

/// <summary>
/// Wrapper for parsers of the same type but require different validation.
/// </summary>
/// 
/// <typeparam name="T">The type of element to be parsed.</typeparam>
/// <typeparam name="TParent">The parent parser.</typeparam>
public interface IParser<T, TParent> : IParser<T>
    where T : notnull
    where TParent : IParser<T>, new()
{
    private static readonly TParent Parent = new();

    bool IParser<T>.TryParse(string input, [MaybeNullWhen(false)] out T output)
    {
        return Parent.TryParse(input, out output);
    }

    bool IParser<T>.TryToString(T input, [MaybeNullWhen(false)] out string output)
    {
        return Parent.TryToString(input, out output);
    }
}

using System;

namespace LabOfKiwi.Cmd;

/// <summary>
/// Helper class for temporarily change the Console output colors.
/// </summary>
public sealed class ConsoleColorChanger : IDisposable
{
    #region Fields
    private readonly ConsoleColor _originalForegroundColor;
    private readonly ConsoleColor _originalBackgroundColor;
    private bool _disposed;
    #endregion

    private ConsoleColorChanger(ConsoleColor foregroundColor, ConsoleColor backgroundColor)
    {
        _originalForegroundColor = Console.ForegroundColor;
        _originalBackgroundColor = Console.BackgroundColor;

        Console.ForegroundColor = foregroundColor;
        Console.BackgroundColor = backgroundColor;
    }

    /// <summary>
    /// Temporarily changes the <see cref="Console.BackgroundColor"/> until the returned object is disposed.
    /// </summary>
    /// <param name="color">The new background color.</param>
    /// <returns>An object that once disposed, the console's background color will be restored to its previous value.</returns>
    public static IDisposable ChangeBackgroundColor(ConsoleColor color)
        => new ConsoleColorChanger(foregroundColor: Console.ForegroundColor, backgroundColor: color);

    /// <summary>
    /// Temporarily changes the <see cref="Console.ForegroundColor"/> and <see cref="Console.BackgroundColor"/> until the returned object is disposed.
    /// </summary>
    /// <param name="foregroundColor">The new foreground color.</param>
    /// <param name="backgroundColor">The new background color.</param>
    /// <returns>An object that once disposed, the console's foreground and background colors will be restored to their previous values.</returns>
    public static IDisposable ChangeColors(ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        => new ConsoleColorChanger(foregroundColor: foregroundColor, backgroundColor: backgroundColor);

    /// <summary>
    /// Temporarily changes the <see cref="Console.ForegroundColor"/> until the returned object is disposed.
    /// </summary>
    /// <param name="color">The new foreground color.</param>
    /// <returns>An object that once disposed, the console's foreground color will be restored to its previous value.</returns>
    public static IDisposable ChangeForegroundColor(ConsoleColor color)
        => new ConsoleColorChanger(foregroundColor: color, backgroundColor: Console.BackgroundColor);

    /// <inheritdoc/>
    void IDisposable.Dispose()
    {
        if (!_disposed)
        {
            try
            {
                Console.ForegroundColor = _originalForegroundColor;
                Console.BackgroundColor = _originalBackgroundColor;
            }
            finally
            {
                _disposed = true;
            }
        }
    }
}

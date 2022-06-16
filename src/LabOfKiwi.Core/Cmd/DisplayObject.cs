using System;

namespace LabOfKiwi.Cmd;

public abstract class DisplayObject : IRunnable
{
    protected DisplayObject()
    {
    }

    public abstract void Run();

    #region Helper Methods
    protected static string AutoPrompt(string text, int maxSize)
    {
        return AutoPrompt(text, s => s.Length < maxSize);
    }

    protected static string AutoPrompt(string text, Predicate<string> predicate)
    {
        Console.Write(text + ": ");
        string result = string.Empty;
        ConsoleKey key;

        do
        {
            var keyInfo = Console.ReadKey(intercept: true);
            key = keyInfo.Key;

            if (key == ConsoleKey.Backspace && result.Length > 0)
            {
                Console.Write("\b \b");
                result = result[0..^1];
            }
            else if (!char.IsControl(keyInfo.KeyChar))
            {
                Console.Write(keyInfo.KeyChar);
                result += keyInfo.KeyChar;
            }
        }
        while (key != ConsoleKey.Enter && predicate(result));

        Console.WriteLine();

        return result;
    }

    /// <summary>
    /// Method that blocks the program thread until user presses enter.
    /// </summary>
    /// <param name="text">The text to display before blocking.</param>
    protected static void Block(string text)
    {
        Console.WriteLine(text);
        Console.Write("Press enter to continue...");
        Console.ReadLine();
    }

    /// <summary>
    /// Temporarily changes the <see cref="Console.BackgroundColor"/> until the returned object is disposed.
    /// </summary>
    /// <param name="color">The new background color.</param>
    /// <returns>An object that once disposed, the console's background color will be restored to its previous value.</returns>
    protected static IDisposable ChangeBackgroundColor(ConsoleColor color)
    {
        return ConsoleColorChanger.ChangeBackgroundColor(color);
    }

    /// <summary>
    /// Temporarily changes the <see cref="Console.ForegroundColor"/> and <see cref="Console.BackgroundColor"/> until the returned object is disposed.
    /// </summary>
    /// <param name="foregroundColor">The new foreground color.</param>
    /// <param name="backgroundColor">The new background color.</param>
    /// <returns>An object that once disposed, the console's foreground and background colors will be restored to their previous values.</returns>
    protected static IDisposable ChangeColors(ConsoleColor foregroundColor, ConsoleColor backgroundColor)
    {
        return ConsoleColorChanger.ChangeColors(foregroundColor: foregroundColor, backgroundColor: backgroundColor);
    }

    /// <summary>
    /// Temporarily changes the <see cref="Console.ForegroundColor"/> until the returned object is disposed.
    /// </summary>
    /// <param name="color">The new foreground color.</param>
    /// <returns>An object that once disposed, the console's foreground color will be restored to its previous value.</returns>
    protected static IDisposable ChangeForegroundColor(ConsoleColor color)
    {
        return ConsoleColorChanger.ChangeForegroundColor(color);
    }

    /// <summary>
    /// Prompts the user for text.
    /// </summary>
    /// <param name="text">The text to display before reading what the user inputs.</param>
    /// <returns>The text the user had entered.</returns>
    protected static string Prompt(string text)
    {
        Console.Write(text + ": ");
        return Console.ReadLine() ?? string.Empty;
    }
    #endregion
}

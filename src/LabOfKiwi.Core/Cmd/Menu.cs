using System;
using System.Collections.Generic;
using System.Linq;

namespace LabOfKiwi.Cmd;

public abstract class Menu : DisplayObject
{
    protected Menu()
    {
    }

    public sealed override void Run()
    {
        var options = GetOptions().ToArray();

        for (int i = 0; i < options.Length; i++)
        {
            var (Title, _) = options[i];
            RenderOption(i + 1, Title, options.Length);
        }

        RenderBetweenOptionsAndSelection();

        while (true)
        {
            string userValue = GetUserSelection();

            if (int.TryParse(userValue, out int result))
            {
                result--;

                if (0 <= result && result < options.Length)
                {
                    var (_, Callback) = options[result];
                    Callback(result);
                    break;
                }
            }

            if (!OnInvalidSelection(userValue))
            {
                break;
            }
        }
    }

    #region Internal Virtual Methods
    protected abstract IEnumerable<(string Title, Action<int> Callback)> GetOptions();

    protected virtual string GetUserSelection()
    {
        return Prompt("Enter selection");
    }

    protected virtual bool OnInvalidSelection(string userValue)
    {
        using (ChangeForegroundColor(ConsoleColor.Red))
        {
            Block("Invalid selection: " + userValue);
        }

        return true;
    }

    protected virtual void RenderOption(int index, string title, int totalOptions)
    {
        int maxIndexLength = totalOptions.ToString().Length;
        string indexStr = index.ToString().PadLeft(maxIndexLength);
        Console.WriteLine($"{indexStr}. {title}");
    }

    protected virtual void RenderBetweenOptionsAndSelection()
    {
        Console.WriteLine();
    }
    #endregion
}

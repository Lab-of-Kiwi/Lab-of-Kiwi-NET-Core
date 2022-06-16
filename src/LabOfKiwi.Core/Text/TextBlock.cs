using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LabOfKiwi.Text;

public sealed class TextBlock
{
    private readonly List<string> _lines;
    private readonly string _tab;

    public TextBlock(int maxWidth, int tabWidth = 4)
    {
        ThrowHelper.RangeCheck(nameof(maxWidth), maxWidth, minValue: 1);
        ThrowHelper.RangeCheck(nameof(tabWidth), tabWidth, minValue: 1);
        MaxWidth = maxWidth;
        _tab = new string(' ', tabWidth);
        _lines = new List<string>();
    }

    public int MaxWidth { get; }

    public int TabWidth => _tab.Length;

    public TextBlock Add(string? text)
    {
        if (!string.IsNullOrEmpty(text))
        {
            return Add(ExtractWordsAndLines(text));
        }

        return this;
    }

    private TextBlock Add(List<string> words)
    {
        if (MaxWidth == 1)
        {
            foreach (var word in words)
            {
                if (word == "\n")
                {
                    _lines.Add(string.Empty);
                    continue;
                }

                if (_lines.Count > 0)
                {
                    _lines.Add(string.Empty);
                }

                foreach (var c in word)
                {
                    _lines.Add(c.ToString());
                }
            }

            return this;
        }

        for (int i = 0; i < words.Count; i++)
        {
            string word = words[i];

            while (word.Length > 0)
            {
                string? last = _lines.LastOrDefault();
                bool isAppended;

                // No lines means no lines to append
                if (last == null)
                {
                    isAppended = false;
                }

                // Last is new line, so not appending
                else if (last == "\n")
                {
                    isAppended = false;
                }

                // Can fit, so appending is allowed
                else if (last.Length + word.Length + 1 <= MaxWidth)
                {
                    isAppended = true;
                }

                // Cannot fit by appending but word can fit on next line
                else if (word.Length <= MaxWidth)
                {
                    isAppended = false;
                }

                // Enough room to add space, at least one char from word, and a dash
                else if (word.Length + 3 <= MaxWidth)
                {
                    isAppended = true;
                }

                // No space to add bits of the word, so start on new line
                else
                {
                    isAppended = false;
                }

                if (isAppended)
                {
                    _lines[^1] = AddToLine(last!, ref word);
                }
                else
                {
                    _lines.Add(AddToLine(string.Empty, ref word));
                }
            }
        }

        return this;
    }

    private string AddToLine(string line, ref string word)
    {
        if (line.Length > 0)
        {
            word = " " + word;
        }

        if (line.Length + word.Length <= MaxWidth)
        {
            line += word;
            word = string.Empty;
        }
        else
        {
            int allowedLength = MaxWidth - line.Length - 1; // (subtract one for the dash)
            line += word[0..allowedLength] + "-";
            word = word[allowedLength..];
        }

        Debug.Assert(line.Length <= MaxWidth);
        return line;
    }

    private List<string> ExtractWordsAndLines(string text)
    {
        List<string> words = new();

        text = text.Replace("\r\n", "\n");
        text = text.Replace("\t", _tab);

        ReadOnlySpan<char> chars = text.ToCharArray();

        while (chars.Length > 0)
        {
            for (int i = 0; i < chars.Length; i++)
            {
                char c = chars[i];

                if (c == '\n')
                {
                    words.Add(new string(chars[0..i]));
                    words.Add("\n");
                    chars = chars[(i + 1)..];
                    break;
                }

                if (char.IsWhiteSpace(c) || char.IsControl(c))
                {
                    if (i > 0)
                    {
                        words.Add(new(chars[0..i]));
                    }

                    chars = chars[(i + 1)..];
                    break;
                }

                if (i == chars.Length - 1)
                {
                    words.Add(new string (chars));
                    chars = chars[..0];
                    break;
                }
            }
        }

        return words;
    }
}

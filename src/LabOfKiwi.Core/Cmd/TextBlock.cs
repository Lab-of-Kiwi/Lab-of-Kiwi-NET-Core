using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabOfKiwi.Cmd;

public sealed class TextBlock : ViewElement
{
    private readonly List<string> _words;
    private int _tabSize = 4;

    public TextBlock()
    {
        _words = new List<string>();
    }

    public int TabSize
    {
        get => _tabSize;

        set => _tabSize = ThrowHelper.RangeCheck(nameof(value), value, minValue: 1);
    }

    public TextBlock Add(string? value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            _words.AddRange(value.ToWords());
        }

        return this;
    }

    protected override List<string> InternalContentRows(int allocatedWidth, int? allocatedHeight, TextAlignOption textAlignment)
    {
        List<string> rows = new();

        string emptyRow = new(' ', allocatedWidth);

        for (int i = 0; i < _words.Count; i++)
        {
            string word = _words[i];

            if (word == "\n")
            {
                rows.Add(emptyRow);
                continue;
            }

            if (word == "\t")
            {
                for (int j = 0; j < TabSize; j++)
                {
                    if (rows.Count == 0 || rows[^1].Length == allocatedWidth)
                    {
                        rows.Add(" ");
                    }
                    else
                    {
                        rows[^1] += ' ';
                    }
                }

                continue;
            }

            if (allocatedWidth == 1)
            {
                if (rows.Count > 0 && rows[^1] != " ")
                {
                    rows.Add(" ");
                }

                foreach (char c in word)
                {
                    rows.Add(c.ToString());
                }

                continue;
            }

            while (word.Length > 0)
            {
                if (rows.Count == 0)
                {
                    if (word.Length <= allocatedWidth)
                    {
                        rows.Add(word);
                        break;
                    }

                    rows.Add(word[0..(allocatedWidth - 1)] + "-");
                    word = word[(allocatedWidth - 1)..];
                    continue;
                }

                string last = rows[^1];
                bool needSpace = last[^1] != ' ';
                int remainingAllowedLength = allocatedWidth - last.Length;

                if (needSpace)
                {
                    remainingAllowedLength--;
                }

                // Word can fit on last line
                if (word.Length <= remainingAllowedLength)
                {
                    if (needSpace)
                    {
                        rows[^1] += " " + word;
                    }
                    else
                    {
                        rows[^1] += word;
                    }

                    break;
                }
                // Word cannot fit on last line, but can on next
                else if (word.Length <= allocatedWidth)
                {
                    rows.Add(word);
                    break;
                }
                // Partial word cannot fit, so start on next
                else if (remainingAllowedLength < 2)
                {
                    rows.Add(word[0..(allocatedWidth - 1)] + "-");
                    word = word[(allocatedWidth - 1)..];
                }
                // Partial word can fit
                else
                {
                    if (needSpace)
                    {
                        rows[^1] += " " + word[0..(remainingAllowedLength - 1)] + "-";
                    }
                    else
                    {
                        rows[^1] += word[0..(remainingAllowedLength - 1)] + "-"; ;
                    }

                    word = word[(remainingAllowedLength - 1)..];
                }
            }
        }

        CleanWidth(rows, allocatedWidth, textAlignment);
        return rows;
    }
}

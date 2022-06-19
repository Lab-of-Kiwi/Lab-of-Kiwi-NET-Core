using System;
using System.Text;

namespace LabOfKiwi.Html;

public enum HtmlFormatOptions
{
    None,
    Spaces2,
    Spaces4,
    Tabs
}

internal static class HtmlFormatOptionsExtensions
{
    private const string Spaces2 = "  ";
    private const string Spaces4 = "    ";
    private const string Tabs    = "\t";

    public static string GetTab(this HtmlFormatOptions value, int count)
    {
        if (value == HtmlFormatOptions.None || count == 0)
        {
            return string.Empty;
        }

        string tab = value switch
        {
            HtmlFormatOptions.Spaces2 => Spaces2,
            HtmlFormatOptions.Spaces4 => Spaces4,
            HtmlFormatOptions.Tabs => Tabs,
            HtmlFormatOptions.None => throw new InvalidOperationException(),
            _ => throw new NotImplementedException()
        };

        if (count == 1)
        {
            return tab;
        }

        StringBuilder sb = new(count * tab.Length);

        for (int i = 0; i < count; i++)
        {
            sb.Append(tab);
        }

        return sb.ToString();
    }

    public static string GetNewLine(this HtmlFormatOptions value)
    {
        return value == HtmlFormatOptions.None ? string.Empty : "\n";
    }
}
using System;

namespace LabOfKiwi.Web.Html;

public static class HtmlHelper
{
    private static readonly char[] ASCIIWhitespace = new char[]
    {
        '\u0009', '\u000A', '\u000C', '\u000D', '\u0020'
    };

    private static readonly string[] ReservedCustomElementNames = new string[]
    {
        "annotation-xml", "color-profile",    "font-face",      "font-face-src",
        "font-face-uri",  "font-face-format", "font-face-name", "missing-glyph"
    };

    public static bool IsASCIIWhitespace(char c)
        => Array.IndexOf(ASCIIWhitespace, c) >= 0;

    public static bool HasASCIIWhitespace(string? str)
    {
        if (str != null)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (IsASCIIWhitespace(str[i]))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public static bool IsValidPotentialCustomElementName(string? str, bool requireDash = true)
    {
        if (str == null) return false;
        if (requireDash && str.Length < 2) return false;
        if (!requireDash && str.Length < 1) return false;

        char first = str[0];

        if (first < 'a' || first > 'z') return false;

        if (requireDash && !str.Contains('-')) return false;

        if (Array.IndexOf(ReservedCustomElementNames, str) >= 0) return false;

        for (int i = 1; i < str.Length; i++)
        {
            if (!IsPCENCharacter(str[i])) return false;
        }

        return true;
    }

    private static bool IsPCENCharacter(char c)
    {
        switch (c)
        {
            case '-':       return true;
            case '.':       return true;
            case '_':       return true;
            case '\u00B7' : return true;
        }

        if ('0' <= c && c <= '9') return true;
        if ('a' <= c && c <= 'z') return true;

        if ('\u00C0' <= c && c <= '\u00D6') return true;
        if ('\u00D8' <= c && c <= '\u00F6') return true;
        if ('\u00F8' <= c && c <= '\u037D') return true;
        if ('\u037F' <= c && c <= '\u1FFF') return true;
        if ('\u200C' <= c && c <= '\u200D') return true;
        if ('\u203F' <= c && c <= '\u2040') return true;
        if ('\u2070' <= c && c <= '\u218F') return true;
        if ('\u2C00' <= c && c <= '\u2FEF') return true;
        if ('\u3001' <= c && c <= '\uD7FF') return true;
        if ('\uF900' <= c && c <= '\uFDCF') return true;

        return false;
    }

    internal static void ThrowInvalidAttributeValueException(string attrName, object? value)
    {
        throw new ArgumentException(string.Format("Invalid value for attribute '{0}': {1}", attrName, value));
    }

    internal static void ThrowInvalidAttributeStateException(string attrName, string reason)
    {
        throw new InvalidOperationException(string.Format("Invalid state for attribute '{0}': {1}", attrName, reason));
    }
}

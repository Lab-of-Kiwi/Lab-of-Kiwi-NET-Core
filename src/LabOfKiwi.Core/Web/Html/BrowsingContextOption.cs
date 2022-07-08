using System;
using System.Diagnostics.CodeAnalysis;

namespace LabOfKiwi.Web.Html;

public readonly struct BrowsingContextOption : IEquatable<BrowsingContextOption>
{
    private static readonly string[] Reserved = new string[]
    {
        "_blank", "_self", "_parent", "_top"
    };

    public static readonly BrowsingContextOption Blank = new(Reserved[0]);
    public static readonly BrowsingContextOption Self = new(Reserved[1]);
    public static readonly BrowsingContextOption Parent = new(Reserved[2]);
    public static readonly BrowsingContextOption Top = new(Reserved[3]);

    private readonly string? _value;

    internal BrowsingContextOption(string value)
    {
        _value = value;
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is BrowsingContextOption other && Equals(other);

    public bool Equals(BrowsingContextOption other)
        => Equals(_value, other._value);

    public override int GetHashCode()
        =>  _value?.GetHashCode() ?? 0;

    public static BrowsingContextOption Create(string text)
    {
        if (text == null)
        {
            throw new ArgumentNullException(nameof(text));
        }

        if (text.Length == 0)
        {
            throw new ArgumentException("Browsing context must have at least one character.");
        }

        if (text[0] == '_' && !Reserved.Contains(text))
        {
            throw new ArgumentException("Browsing context cannot begin with underscore unless is one of the reserved names.");
        }

        return new BrowsingContextOption(text);
    }

    public override string ToString() => _value ?? Reserved[1];

    public static bool operator ==(BrowsingContextOption left, BrowsingContextOption right)
        => left.Equals(right);

    public static bool operator !=(BrowsingContextOption left, BrowsingContextOption right)
        => !(left == right);

    internal static bool IsValid(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return false;
        }

        if (value[0] == '_' && !Reserved.Contains(value))
        {
            return false;
        }

        return true;
    }
}

using System.Diagnostics;

namespace LabOfKiwi.Web.Html;

public sealed class Text : LeafNode
{
    private string _text;

    internal Text(string text)
    {
        Debug.Assert(!string.IsNullOrEmpty(text));
        _text = text;
    }

    internal void Append(string? text)
    {
        Debug.Assert(!string.IsNullOrEmpty(text));
        _text += text;
    }

    internal void Prepend(string? text)
    {
        Debug.Assert(!string.IsNullOrEmpty(text));
        _text = text + _text;
    }

    public override string ToString() => _text;
}

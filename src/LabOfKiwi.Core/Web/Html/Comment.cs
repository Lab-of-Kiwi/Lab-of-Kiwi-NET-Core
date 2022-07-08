namespace LabOfKiwi.Web.Html;

public sealed class Comment : LeafNode
{
    private readonly string _text;

    public Comment(string? text)
    {
        _text = text ?? string.Empty;
    }

    public override string ToString() => $"<!--{_text}-->";
}

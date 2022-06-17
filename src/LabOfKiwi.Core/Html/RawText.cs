namespace LabOfKiwi.Html;

public sealed class RawText : Element
{
    private string _text;

    public RawText(string? text)
    {
        _text = text ?? string.Empty;
    }

    public RawText Append(string? text)
    {
        if (!string.IsNullOrEmpty(text))
        {
            _text += text;
        }

        return this;
    }

    public override string ToString()
    {
        return _text;
    }
}

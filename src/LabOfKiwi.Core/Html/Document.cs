using System.Text;

namespace LabOfKiwi.Html;

public class Document
{
    public DocType DocType { get; set; } = DocType.Html5;

    public Tags.Html Html { get; } = new Tags.Html();

    public string ToFormattedString(HtmlFormatOptions formatOptions = HtmlFormatOptions.None)
    {
        StringBuilder sb = new();
        sb.Append(DocType.ToTagString());
        sb.Append(formatOptions.GetNewLine());
        sb.Append(Html.InternalToFormattedString(0, formatOptions));
        return sb.ToString();
    }

    public sealed override string ToString()
    {
        return ToFormattedString(HtmlFormatOptions.None);
    }
}

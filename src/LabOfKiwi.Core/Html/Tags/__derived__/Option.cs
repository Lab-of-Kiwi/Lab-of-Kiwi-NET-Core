using LabOfKiwi.Html.Tags.Attributes;
using System.Text;
using System.Web;

namespace LabOfKiwi.Html.Tags;

public class Option : Tag, ISelectChild
{
    public bool IsDisabled
    {
        get => Attributes.Has("disabled");
        set => Attributes.Set("disabled", value);
    }

    public string? InnerText { get; set; }

    public string? Label
    {
        get => new TextAttribute("label", Attributes, requiresEncoding: true).Value;
        set => new TextAttribute("label", Attributes, requiresEncoding: true).Value = value;
    }

    public bool IsSelected
    {
        get => Attributes.Has("selected");
        set => Attributes.Set("selected", value);
    }

    public string? Value
    {
        get => new TextAttribute("value", Attributes, requiresEncoding: true).Value;
        set => new TextAttribute("value", Attributes, requiresEncoding: true).Value = value;
    }

    protected sealed override string TagName => "option";

    internal sealed override void CompleteToString(StringBuilder sb, int tabCount, HtmlFormatOptions formatOptions)
    {
        if (InnerText == null)
        {
            sb.Append(" />");
        }
        else
        {
            string encoded = HttpUtility.HtmlEncode(InnerText);
            sb.Append($">{encoded}</{TagName}>");
        }
    }
}

using LabOfKiwi.Web.Html.Attributes;
using System.Text;

namespace LabOfKiwi.Web.Html.Tags;

public class META : Element
{
    public Encoding? Charset
    {
        get => RawAttributes.GetObject<EncodingParser, Encoding>("charset");
        set => RawAttributes.SetObject<EncodingParser, Encoding>("charset", value);
    }

    public string? Content
    {
        get => RawAttributes["content"];
        set => RawAttributes["content"] = value;
    }

    public PragmaDirective? HttpEquiv
    {
        get => RawAttributes.GetStruct<PragmaDirectiveParser, PragmaDirective>("http-equiv");
        set => RawAttributes.SetStruct<PragmaDirectiveParser, PragmaDirective>("http-equiv", value);
    }

    // TODO
    public string? Media
    {
        get => RawAttributes["media"];
        set => RawAttributes["media"] = value;
    }

    public string? Name
    {
        get => RawAttributes["name"];
        set => RawAttributes["name"] = value;
    }

    public sealed override string TagName => "meta";
}

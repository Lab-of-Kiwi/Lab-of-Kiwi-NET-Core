using LabOfKiwi.Web.Html.Attributes;

namespace LabOfKiwi.Web.Html.Tags;

public class LABEL : Element
{
    public string? For
    {
        get => RawAttributes.GetObject<TokenParser, string>("for");
        set => RawAttributes.SetObject<TokenParser, string>("for", value);
    }

    public sealed override string TagName => "label";
}

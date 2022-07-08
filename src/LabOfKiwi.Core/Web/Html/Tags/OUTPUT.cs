using LabOfKiwi.Web.Html.Attributes;
using System.Collections.Generic;

namespace LabOfKiwi.Web.Html.Tags;

public class OUTPUT : Element
{
    public IList<string> For => RawAttributes.GetList<TokenParser, string>("for");

    public string? Form
    {
        get => RawAttributes.GetObject<TokenParser, string>("form");
        set => RawAttributes.SetObject<TokenParser, string>("form", value);
    }

    public string? Name
    {
        get => RawAttributes["name"];
        set => RawAttributes["name"] = value;
    }

    public sealed override string TagName => "output";
}

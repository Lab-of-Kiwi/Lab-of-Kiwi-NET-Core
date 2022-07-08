using LabOfKiwi.Web.Html.Attributes;
using System.Collections.Generic;

namespace LabOfKiwi.Web.Html.Tags;

public class STYLE : Element
{
    public IList<string> Blocking => RawAttributes.GetList<TokenParser, string>("blocking");

    // TODO
    public string? Media
    {
        get => RawAttributes["media"];
        set => RawAttributes["media"] = value;
    }

    public sealed override string TagName => "style";
}

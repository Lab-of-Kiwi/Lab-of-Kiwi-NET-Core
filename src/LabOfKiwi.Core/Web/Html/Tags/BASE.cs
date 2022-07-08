using LabOfKiwi.Web.Html.Attributes;
using System;

namespace LabOfKiwi.Web.Html.Tags;

public class BASE : Element
{
    public Uri? Href
    {
        get => RawAttributes.GetObject<Attributes.UriParser, Uri>("href");
        set => RawAttributes.SetObject<Attributes.UriParser, Uri>("href", value);
    }

    public sealed override string TagName => "base";

    public BrowsingContextOption? Target
    {
        get => RawAttributes.GetStruct<BrowsingContextParser, BrowsingContextOption>("target");
        set => RawAttributes.SetStruct<BrowsingContextParser, BrowsingContextOption>("target", value);
    }
}

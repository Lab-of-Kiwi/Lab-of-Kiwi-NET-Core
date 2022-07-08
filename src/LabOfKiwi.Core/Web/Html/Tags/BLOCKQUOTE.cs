using System;

namespace LabOfKiwi.Web.Html.Tags;

public class BLOCKQUOTE : Element
{
    public Uri? Cite
    {
        get => RawAttributes.GetObject<Attributes.UriParser, Uri>("cite");
        set => RawAttributes.SetObject<Attributes.UriParser, Uri>("cite", value);
    }

    public sealed override string TagName => "blockquote";
}

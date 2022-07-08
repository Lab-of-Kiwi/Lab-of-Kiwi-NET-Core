using System;

namespace LabOfKiwi.Web.Html.Tags;

public class INS : Element
{
    public Uri? Cite
    {
        get => RawAttributes.GetObject<Attributes.UriParser, Uri>("cite");
        set => RawAttributes.SetObject<Attributes.UriParser, Uri>("cite", value);
    }

    // TODO
    public string? Datetime
    {
        get => RawAttributes["datetime"];
        set => RawAttributes["datatime"] = value;
    }

    public sealed override string TagName => "ins";
}

using LabOfKiwi.Web.Html.Attributes;
using System;

namespace LabOfKiwi.Web.Html.Tags;

public class EMBED : Element
{
    public long? Height
    {
        get => RawAttributes.GetLong("height", min: 0L);
        set => RawAttributes.SetLong("height", value, min: 0L);
    }

    public Uri? Source
    {
        get => RawAttributes.GetObject<Attributes.UriParser, Uri>("src");
        set => RawAttributes.SetObject<Attributes.UriParser, Uri>("src", value);
    }

    public sealed override string TagName => "embed";

    // TODO
    public string? Type
    {
        get => RawAttributes["type"];
        set => RawAttributes["type"] = value;
    }

    public long? Width
    {
        get => RawAttributes.GetLong("width", min: 0L);
        set => RawAttributes.SetLong("width", value, min: 0L);
    }
}

using LabOfKiwi.Web.Html.Attributes;
using System;

namespace LabOfKiwi.Web.Html.Tags;

public class OBJECT : Element
{
    public Uri? Data
    {
        get => RawAttributes.GetObject<Attributes.UriParser, Uri>("data");
        set => RawAttributes.SetObject<Attributes.UriParser, Uri>("data", value);
    }

    public string? Form
    {
        get => RawAttributes.GetObject<TokenParser, string>("form");
        set => RawAttributes.SetObject<TokenParser, string>("form", value);
    }

    public long? Height
    {
        get => RawAttributes.GetLong("height", min: 0L);
        set => RawAttributes.SetLong("height", value, min: 0L);
    }

    // TODO
    public string? Name
    {
        get => RawAttributes["name"];
        set => RawAttributes["name"] = value;
    }

    public sealed override string TagName => "object";

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

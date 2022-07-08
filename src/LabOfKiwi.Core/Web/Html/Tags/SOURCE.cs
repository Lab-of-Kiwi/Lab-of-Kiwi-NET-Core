using LabOfKiwi.Web.Html.Attributes;
using System;
using System.Collections.Generic;

namespace LabOfKiwi.Web.Html.Tags;

public class SOURCE : Element
{
    public long? Height
    {
        get => RawAttributes.GetLong("height", min: 0L);
        set => RawAttributes.SetLong("height", value, min: 0L);
    }

    // TODO
    public string? Media
    {
        get => RawAttributes["media"];
        set => RawAttributes["media"] = value;
    }

    // TODO
    public string? Sizes
    {
        get => RawAttributes["sizes"];
        set => RawAttributes["sizes"] = value;
    }

    public Uri? Source
    {
        get => RawAttributes.GetObject<Attributes.UriParser, Uri>("src");
        set => RawAttributes.SetObject<Attributes.UriParser, Uri>("src", value);
    }

    // TODO
    public IList<string> SourceSet => RawAttributes.GetList<StringParser, string>("srcset", ",", true);

    public sealed override string TagName => "source";

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

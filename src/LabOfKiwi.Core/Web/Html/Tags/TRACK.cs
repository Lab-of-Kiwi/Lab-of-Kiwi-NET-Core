using LabOfKiwi.Web.Html.Attributes;
using System;
using System.Globalization;

namespace LabOfKiwi.Web.Html.Tags;

public class TRACK : Element
{
    public bool IsDefault
    {
        get => RawAttributes.GetBoolean("default");
        set => RawAttributes.SetBoolean("default", value);
    }

    public TrackKind? Kind
    {
        get => RawAttributes.GetEnum<TrackKind>("kind");
        set => RawAttributes.SetEnum("kind", value);
    }

    public string? Label
    {
        get => RawAttributes["label"];
        set => RawAttributes["label"] = value;
    }

    public Uri? Source
    {
        get => RawAttributes.GetObject<Attributes.UriParser, Uri>("src");
        set => RawAttributes.SetObject<Attributes.UriParser, Uri>("src", value);
    }

    public CultureInfo? SourceLanguage
    {
        get => RawAttributes.GetObject<CultureInfoParser, CultureInfo>("srclang");
        set => RawAttributes.SetObject<CultureInfoParser, CultureInfo>("srclang", value);
    }

    public sealed override string TagName => "track";
}

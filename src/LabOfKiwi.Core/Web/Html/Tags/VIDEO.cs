using LabOfKiwi.Web.Html.Attributes;
using System;

namespace LabOfKiwi.Web.Html.Tags;

public class VIDEO : Element
{
    public CrossoriginOption? Crossorigin
    {
        get => RawAttributes.GetStruct<CrossoriginParser, CrossoriginOption>("crossorigin");
        set => RawAttributes.SetStruct<CrossoriginParser, CrossoriginOption>("crossorigin", value);
    }

    public long? Height
    {
        get => RawAttributes.GetLong("height", min: 0L);
        set => RawAttributes.SetLong("height", value, min: 0L);
    }

    public bool IsAutoPlay
    {
        get => RawAttributes.GetBoolean("autoplay");
        set => RawAttributes.SetBoolean("autoplay", value);
    }

    public bool IsInline
    {
        get => RawAttributes.GetBoolean("playsinline");
        set => RawAttributes.SetBoolean("playsinline", value);
    }

    public bool IsLoop
    {
        get => RawAttributes.GetBoolean("loop");
        set => RawAttributes.SetBoolean("loop", value);
    }

    public bool IsMuted
    {
        get => RawAttributes.GetBoolean("muted");
        set => RawAttributes.SetBoolean("muted", value);
    }

    public Uri? Poster
    {
        get => RawAttributes.GetObject<Attributes.UriParser, Uri>("poster");
        set => RawAttributes.SetObject<Attributes.UriParser, Uri>("poster", value);
    }

    public PreloadOption? Preload
    {
        get => RawAttributes.GetEnum<PreloadOption>("preload");
        set => RawAttributes.SetEnum("preload", value);
    }

    public bool ShowControls
    {
        get => RawAttributes.GetBoolean("controls");
        set => RawAttributes.SetBoolean("controls", value);
    }

    public Uri? Source
    {
        get => RawAttributes.GetObject<Attributes.UriParser, Uri>("src");
        set => RawAttributes.SetObject<Attributes.UriParser, Uri>("src", value);
    }

    public sealed override string TagName => "video";

    public long? Width
    {
        get => RawAttributes.GetLong("width", min: 0L);
        set => RawAttributes.SetLong("width", value, min: 0L);
    }
}

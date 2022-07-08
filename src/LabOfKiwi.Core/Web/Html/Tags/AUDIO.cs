using LabOfKiwi.Web.Html.Attributes;
using System;

namespace LabOfKiwi.Web.Html.Tags;

public class AUDIO : Element
{
    public CrossoriginOption? Crossorigin
    {
        get => RawAttributes.GetStruct<CrossoriginParser, CrossoriginOption>("crossorigin");
        set => RawAttributes.SetStruct<CrossoriginParser, CrossoriginOption>("crossorigin", value);
    }

    public bool IsAutoPlay
    {
        get => RawAttributes.GetBoolean("autoplay");
        set => RawAttributes.SetBoolean("autoplay", value);
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

    public sealed override string TagName => "audio";
}

using LabOfKiwi.Web.Html.Attributes;
using System;
using System.Collections.Generic;

namespace LabOfKiwi.Web.Html.Tags;

public class IMG : Element
{
    public string? Alt
    {
        get => RawAttributes["alt"];
        set => RawAttributes["alt"] = value;
    }

    public CrossoriginOption? Crossorigin
    {
        get => RawAttributes.GetStruct<CrossoriginParser, CrossoriginOption>("crossorigin");
        set => RawAttributes.SetStruct<CrossoriginParser, CrossoriginOption>("crossorigin", value);
    }

    public DecodingOption? Decoding
    {
        get => RawAttributes.GetEnum<DecodingOption>("decoding");
        set => RawAttributes.SetEnum("decoding", value);
    }

    public long? Height
    {
        get => RawAttributes.GetLong("height", min: 0L);
        set => RawAttributes.SetLong("height", value, min: 0L);
    }

    public bool IsMap
    {
        get => RawAttributes.GetBoolean("ismap");
        set => RawAttributes.SetBoolean("ismap", value);
    }

    public LoadingOption? Loading
    {
        get => RawAttributes.GetEnum<LoadingOption>("loading");
        set => RawAttributes.SetEnum("loading", value);
    }

    public ReferrerPolicyOption? ReferrerPolicy
    {
        get => RawAttributes.GetStruct<ReferrerPolicyParser, ReferrerPolicyOption>("referrerpolicy");
        set => RawAttributes.SetStruct<ReferrerPolicyParser, ReferrerPolicyOption>("referrerpolicy", value);
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

    public sealed override string TagName => "img";

    // TODO
    public string? UseMap
    {
        get => RawAttributes["usemap"];
        set => RawAttributes["usemap"] = value;
    }

    public long? Width
    {
        get => RawAttributes.GetLong("width", min: 0L);
        set => RawAttributes.SetLong("width", value, min: 0L);
    }
}

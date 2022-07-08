using LabOfKiwi.Web.Html.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace LabOfKiwi.Web.Html.Tags;

public class LINK : Element
{
    // TODO
    public string? As
    {
        get => RawAttributes["as"];
        set => RawAttributes["as"] = value;
    }

    // TODO
    public string? Color
    {
        get => RawAttributes["color"];
        set => RawAttributes["color"] = value;
    }

    public CrossoriginOption? Crossorigin
    {
        get => RawAttributes.GetStruct<CrossoriginParser, CrossoriginOption>("crossorigin");
        set => RawAttributes.SetStruct<CrossoriginParser, CrossoriginOption>("crossorigin", value);
    }

    public IList<string> Blocking => RawAttributes.GetList<TokenParser, string>("blocking");

    public Uri? Href
    {
        get => RawAttributes.GetObject<Attributes.UriParser, Uri>("href");
        set => RawAttributes.SetObject<Attributes.UriParser, Uri>("href", value);
    }

    public CultureInfo? HrefLang
    {
        get => RawAttributes.GetObject<CultureInfoParser, CultureInfo>("hreflang");
        set => RawAttributes.SetObject<CultureInfoParser, CultureInfo>("hreflang", value);
    }

    // TODO
    public string? ImageSizes
    {
        get => RawAttributes["imagesizes"];
        set => RawAttributes["imagesizes"] = value;
    }

    // TODO
    public IList<string> ImageSrcSet => RawAttributes.GetList<StringParser, string>("imagesrcset", ",", true);

    public string? Integrity
    {
        get => RawAttributes["integrity"];
        set => RawAttributes["integrity"] = value;
    }

    public bool IsDisabled
    {
        get => RawAttributes.GetBoolean("disabled");
        set => RawAttributes.SetBoolean("disabled", value);
    }

    // TODO
    public string? Media
    {
        get => RawAttributes["media"];
        set => RawAttributes["media"] = value;
    }

    public ReferrerPolicyOption? ReferrerPolicy
    {
        get => RawAttributes.GetStruct<ReferrerPolicyParser, ReferrerPolicyOption>("referrerpolicy");
        set => RawAttributes.SetStruct<ReferrerPolicyParser, ReferrerPolicyOption>("referrerpolicy", value);
    }

    public IList<string> Relationships => RawAttributes.GetList<TokenParser, string>("rel");

    // TODO
    public IList<string> Sizes => RawAttributes.GetList<TokenParser, string>("sizes");

    public sealed override string TagName => "link";

    // TODO
    public string? Type
    {
        get => RawAttributes["type"];
        set => RawAttributes["type"] = value;
    }
}

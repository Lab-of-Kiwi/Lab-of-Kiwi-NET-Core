using LabOfKiwi.Web.Html.Attributes;
using System;
using System.Collections.Generic;

namespace LabOfKiwi.Web.Html.Tags;

public class AREA : Element
{
    public string? Alt
    {
        get => RawAttributes["alt"];
        set => RawAttributes["alt"] = value;
    }

    public IList<float> Coordinates => RawAttributes.GetList<NormalFloatParser, float>("coords", delimiter: ",", allowDuplicates: true);

    public string? Download
    {
        get => RawAttributes["download"];
        set => RawAttributes["download"] = value;
    }

    public Uri? Href
    {
        get => RawAttributes.GetObject<Attributes.UriParser, Uri>("href");
        set => RawAttributes.SetObject<Attributes.UriParser, Uri>("href", value);
    }

    public IList<Uri> Ping => RawAttributes.GetList<Attributes.UriParser, Uri>("ping", ",", true);

    public ReferrerPolicyOption? ReferrerPolicy
    {
        get => RawAttributes.GetStruct<ReferrerPolicyParser, ReferrerPolicyOption>("referrerpolicy");
        set => RawAttributes.SetStruct<ReferrerPolicyParser, ReferrerPolicyOption>("referrerpolicy", value);
    }

    public IList<string> Relationships => RawAttributes.GetList<TokenParser, string>("rel");

    public ShapeOption? Shape
    {
        get => RawAttributes.GetEnum<ShapeOption>("shape");
        set => RawAttributes.SetEnum("shape", value);
    }

    public sealed override string TagName => "area";

    public BrowsingContextOption? Target
    {
        get => RawAttributes.GetStruct<BrowsingContextParser, BrowsingContextOption>("target");
        set => RawAttributes.SetStruct<BrowsingContextParser, BrowsingContextOption>("target", value);
    }

    public enum ShapeOption
    {
        Default,
        Circle,
        Poly,
        Rect
    }
}

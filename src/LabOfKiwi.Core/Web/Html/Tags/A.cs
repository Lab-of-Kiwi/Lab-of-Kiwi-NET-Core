using LabOfKiwi.Web.Html.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace LabOfKiwi.Web.Html.Tags;

public class A : Element
{
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

    public CultureInfo? HrefLang
    {
        get => RawAttributes.GetObject<CultureInfoParser, CultureInfo>("hreflang");
        set => RawAttributes.SetObject<CultureInfoParser, CultureInfo>("hreflang", value);
    }

    public IList<Uri> Ping => RawAttributes.GetList<Attributes.UriParser, Uri>("ping", ",", true);

    public ReferrerPolicyOption? ReferrerPolicy
    {
        get => RawAttributes.GetStruct<ReferrerPolicyParser, ReferrerPolicyOption>("referrerpolicy");
        set => RawAttributes.SetStruct<ReferrerPolicyParser, ReferrerPolicyOption>("referrerpolicy", value);
    }

    public IList<string> Relationships => RawAttributes.GetList<TokenParser, string>("rel");

    public sealed override string TagName => "a";

    public BrowsingContextOption? Target
    {
        get => RawAttributes.GetStruct<BrowsingContextParser, BrowsingContextOption>("target");
        set => RawAttributes.SetStruct<BrowsingContextParser, BrowsingContextOption>("target", value);
    }

    // TODO
    public string? Type
    {
        get => RawAttributes["type"];
        set => RawAttributes["type"] = value;
    }
}

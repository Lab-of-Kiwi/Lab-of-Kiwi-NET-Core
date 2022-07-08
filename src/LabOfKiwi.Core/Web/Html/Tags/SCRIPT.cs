using LabOfKiwi.Web.Html.Attributes;
using System;
using System.Collections.Generic;

namespace LabOfKiwi.Web.Html.Tags;

public class SCRIPT : Element
{
    public bool AllowModule
    {
        get => !RawAttributes.GetBoolean("nomodule");
        set => RawAttributes.SetBoolean("nomodule", !value);
    }

    public bool IsAsync
    {
        get => RawAttributes.GetBoolean("async");
        set => RawAttributes.SetBoolean("async", value);
    }

    public bool IsDeferred
    {
        get => RawAttributes.GetBoolean("defer");
        set => RawAttributes.SetBoolean("defer", value);
    }

    public IList<string> Blocking => RawAttributes.GetList<TokenParser, string>("blocking");

    public CrossoriginOption? Crossorigin
    {
        get => RawAttributes.GetStruct<CrossoriginParser, CrossoriginOption>("crossorigin");
        set => RawAttributes.SetStruct<CrossoriginParser, CrossoriginOption>("crossorigin", value);
    }

    public string? Integrity
    {
        get => RawAttributes["integrity"];
        set => RawAttributes["integrity"] = value;
    }

    public ReferrerPolicyOption? ReferrerPolicy
    {
        get => RawAttributes.GetStruct<ReferrerPolicyParser, ReferrerPolicyOption>("referrerpolicy");
        set => RawAttributes.SetStruct<ReferrerPolicyParser, ReferrerPolicyOption>("referrerpolicy", value);
    }

    public Uri? Source
    {
        get => RawAttributes.GetObject<Attributes.UriParser, Uri>("src");
        set => RawAttributes.SetObject<Attributes.UriParser, Uri>("src", value);
    }

    public sealed override string TagName => "script";

    // TODO
    public string? Type
    {
        get => RawAttributes["type"];
        set => RawAttributes["type"] = value;
    }
}

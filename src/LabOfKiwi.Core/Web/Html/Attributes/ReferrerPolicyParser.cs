using System;

namespace LabOfKiwi.Web.Html.Attributes;

public class ReferrerPolicyParser : Parser<ReferrerPolicyOption>
{
    protected override ReferrerPolicyOption InternalParse(string rawValue) => rawValue switch
    {
        "no-referrer" => ReferrerPolicyOption.NoReferrer,
        "no-referrer-when-downgrade" => ReferrerPolicyOption.NoReferrerWhenDowngrade,
        "same-origin" => ReferrerPolicyOption.SameOrigin,
        "origin" => ReferrerPolicyOption.Origin,
        "strict-origin" => ReferrerPolicyOption.StrictOrigin,
        "origin-when-cross-origin" => ReferrerPolicyOption.OriginWhenCrossOrigin,
        "strict-origin-when-cross-origin" => ReferrerPolicyOption.StricOriginWhenCrossOrigin,
        "unsafe-url" => ReferrerPolicyOption.UnsafeUrl,
        _ => base.InternalParse(rawValue)
    };

    protected override string InternalToString(ReferrerPolicyOption value) => value switch
    {
        ReferrerPolicyOption.NoReferrer => "no-referrer",
        ReferrerPolicyOption.NoReferrerWhenDowngrade => "no-referrer-when-downgrade",
        ReferrerPolicyOption.SameOrigin => "same-origin",
        ReferrerPolicyOption.Origin => "origin",
        ReferrerPolicyOption.StrictOrigin => "strict-origin",
        ReferrerPolicyOption.OriginWhenCrossOrigin => "origin-when-cross-origin",
        ReferrerPolicyOption.StricOriginWhenCrossOrigin => "strict-origin-when-cross-origin",
        ReferrerPolicyOption.UnsafeUrl => "unsafe-url",
        _ => throw new NotImplementedException()
    };
}

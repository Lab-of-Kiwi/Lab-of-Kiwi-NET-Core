using System;

namespace LabOfKiwi.Web.Html.Attributes;

public class PragmaDirectiveParser : Parser<PragmaDirective>
{
    protected override PragmaDirective InternalParse(string rawValue) => rawValue switch
    {
        "content-type" => PragmaDirective.ContentType,
        "default-style" => PragmaDirective.DefaultStyle,
        "refresh" => PragmaDirective.Refresh,
        "x-ua-compatible" => PragmaDirective.XUACompatible,
        "content-security-policy" => PragmaDirective.ContentSecurityPolicy,
        _ => base.InternalParse(rawValue)
    };

    protected override string InternalToString(PragmaDirective value) => value switch
    {
        PragmaDirective.ContentType => "content-type",
        PragmaDirective.DefaultStyle => "default-style",
        PragmaDirective.Refresh => "refresh",
        PragmaDirective.XUACompatible => "x-ua-compatible",
        PragmaDirective.ContentSecurityPolicy => "content-security-policy",
        _ => throw new NotImplementedException()
    };
}

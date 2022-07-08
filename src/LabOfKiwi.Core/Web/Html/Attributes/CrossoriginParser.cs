using System;

namespace LabOfKiwi.Web.Html.Attributes;

public class CrossoriginParser : Parser<CrossoriginOption>
{
    protected override CrossoriginOption InternalParse(string rawValue) => rawValue switch
    {
        "anonymous" => CrossoriginOption.Anonymous,
        "use-credentials" => CrossoriginOption.UseCredentials,
        _ => base.InternalParse(rawValue)
    };

    protected override string InternalToString(CrossoriginOption value) => value switch
    {
        CrossoriginOption.Anonymous => "anonymous",
        CrossoriginOption.UseCredentials => "use-credentials",
        _ => throw new NotImplementedException()
    };
}

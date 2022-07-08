using System;

namespace LabOfKiwi.Web.Html.Attributes;

public class EncodingTypeParser : Parser<EncodingTypeOption>
{
    protected override EncodingTypeOption InternalParse(string rawValue) => rawValue switch
    {
        "application/x-www-form-urlencoded" => EncodingTypeOption.UrlEncoded,
        "multipart/form-data" => EncodingTypeOption.MultipartFormData,
        "text/plain" => EncodingTypeOption.PlainText,
        _ => base.InternalParse(rawValue)
    };

    protected override string InternalToString(EncodingTypeOption value) => value switch
    {
        EncodingTypeOption.UrlEncoded => "application/x-www-form-urlencoded",
        EncodingTypeOption.MultipartFormData => "multipart/form-data",
        EncodingTypeOption.PlainText => "text/plain",
        _ => throw new NotImplementedException()
    };
}

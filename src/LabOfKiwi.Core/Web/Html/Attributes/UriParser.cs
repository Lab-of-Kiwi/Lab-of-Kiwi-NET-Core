using System;

namespace LabOfKiwi.Web.Html.Attributes;

public class UriParser : Parser<Uri>
{
    protected sealed override Uri InternalParse(string rawValue)
    {
        if (Uri.TryCreate(rawValue, UriKind.RelativeOrAbsolute, out Uri? result))
        {
            return result;
        }

        return base.InternalParse(rawValue);
    }
    protected sealed override string InternalToString(Uri value)
        => value.ToString();
}

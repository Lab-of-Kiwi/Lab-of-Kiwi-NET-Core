using System.Globalization;

namespace LabOfKiwi.Web.Html.Attributes;

public class CultureInfoParser : Parser<CultureInfo>
{
    protected override CultureInfo InternalParse(string rawValue)
    {
        try
        {
            return new CultureInfo(rawValue);
        }
        catch
        {
            return base.InternalParse(rawValue);
        }
    }

    protected override string InternalToString(CultureInfo value)
        => value.TwoLetterISOLanguageName;
}

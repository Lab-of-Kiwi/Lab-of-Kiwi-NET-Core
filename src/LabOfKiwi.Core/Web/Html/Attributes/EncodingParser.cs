using System.Text;

namespace LabOfKiwi.Web.Html.Attributes;

public class EncodingParser : Parser<Encoding>
{
    protected sealed override Encoding InternalParse(string rawValue)
    {
        try
        {
            return Encoding.GetEncoding(rawValue);
        }
        catch
        {
            return base.InternalParse(rawValue);
        }
    }

    protected sealed override string InternalToString(Encoding value)
    {
        return value.WebName.ToLowerInvariant();
    }
}

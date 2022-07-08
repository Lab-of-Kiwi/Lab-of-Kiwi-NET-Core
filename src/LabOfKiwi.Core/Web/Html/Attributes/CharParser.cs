using System.Diagnostics;

namespace LabOfKiwi.Web.Html.Attributes;

public class CharParser : Parser<char>
{
    protected sealed override char InternalParse(string rawValue)
    {
        if (rawValue.Length == 1)
        {
            return rawValue[0];
        }

        return base.InternalParse(rawValue);
    }

    protected sealed override string InternalToString(char value)
        => value.ToString();
}

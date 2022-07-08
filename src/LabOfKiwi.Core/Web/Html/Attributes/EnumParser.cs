using System;
using System.Diagnostics;

namespace LabOfKiwi.Web.Html.Attributes;

public class EnumParser<T> : Parser<T> where T : struct, Enum
{
    protected override T InternalParse(string rawValue)
    {
        try
        {
            return Enum.Parse<T>(rawValue, true);
        }
        catch
        {
            return base.InternalParse(rawValue);
        }
    }

    protected override string InternalToString(T value)
    {
        string? rawValue = Enum.GetName(value);
        Debug.Assert(rawValue != null);
        return rawValue;
    }
}

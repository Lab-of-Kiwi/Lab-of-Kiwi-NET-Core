using System.Diagnostics;
using System.Globalization;

namespace LabOfKiwi.Html.Tags.Attributes;

internal class LanguageAttribute : Attribute<CultureInfo>
{
    public LanguageAttribute(string name, AttributeCollection attributes) : base(name, attributes, false)
    {
    }

    protected sealed override string ConvertToRawValue(CultureInfo value)
    {
        Debug.Assert(value != null);
        return value.TwoLetterISOLanguageName;
    }

    protected sealed override CultureInfo ConvertToUserValue(string rawValue)
    {
        Debug.Assert(rawValue != null);
        return CultureInfo.GetCultureInfo(rawValue);
    }
}

using System.Diagnostics;

namespace LabOfKiwi.Html.Tags.Attributes;

internal class TextAttribute : Attribute<string>
{
    public TextAttribute(string name, AttributeCollection attributes, bool requiresEncoding = false) : base(name, attributes, requiresEncoding)
    {
    }

    protected sealed override string ConvertToRawValue(string value)
    {
        Debug.Assert(value != null);
        return value;
    }

    protected sealed override string ConvertToUserValue(string rawValue)
    {
        Debug.Assert(rawValue != null);
        return rawValue;
    }
}

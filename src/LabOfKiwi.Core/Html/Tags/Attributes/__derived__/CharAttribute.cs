using System.Diagnostics;

namespace LabOfKiwi.Html.Tags.Attributes;

internal class CharAttribute : Attribute<char?>
{
    public CharAttribute(string name, AttributeCollection attributes, bool requiresEncoding = false) : base(name, attributes, requiresEncoding)
    {
    }

    protected sealed override string ConvertToRawValue(char? value)
    {
        Debug.Assert(value.HasValue);
        return value.Value.ToString();
    }

    protected sealed override char? ConvertToUserValue(string rawValue)
    {
        Debug.Assert(rawValue.Length == 1);
        return rawValue[0];
    }
}

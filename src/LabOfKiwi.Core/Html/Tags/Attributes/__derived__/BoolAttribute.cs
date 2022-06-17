using System;
using System.Diagnostics;

namespace LabOfKiwi.Html.Tags.Attributes;

internal class BoolAttribute : Attribute<bool?>
{
    public BoolAttribute(string name, AttributeCollection attributes, BoolTextType type) : base(name, attributes, false)
    {
        (TrueString, FalseString) = type.GetStringValues();
    }

    private string TrueString { get; }

    private string FalseString { get; }

    protected sealed override string ConvertToRawValue(bool? value)
    {
        Debug.Assert(value.HasValue);
        return value.Value ? TrueString : FalseString;
    }

    protected sealed override bool? ConvertToUserValue(string rawValue)
    {
        if (rawValue == TrueString)
        {
            return true;
        }

        if (rawValue == FalseString)
        {
            return false;
        }

        throw new InvalidOperationException("Invalid value returned: " + rawValue);
    }
}

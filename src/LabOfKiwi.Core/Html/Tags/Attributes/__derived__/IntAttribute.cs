using System.Diagnostics;

namespace LabOfKiwi.Html.Tags.Attributes;

internal class IntAttribute : Attribute<int?>
{
    public IntAttribute(string name, AttributeCollection attributes, int minValue = int.MinValue, int maxValue = int.MaxValue) : base(name, attributes, false)
    {
        Debug.Assert(minValue <= maxValue);
        MinValue = minValue;
        MaxValue = maxValue;
    }

    public int MinValue { get; }

    public int MaxValue { get; }

    protected sealed override string ConvertToRawValue(int? value)
    {
        Debug.Assert(value.HasValue);
        return value.Value.ToString();
    }

    protected sealed override int? ConvertToUserValue(string rawValue)
    {
        Debug.Assert(rawValue != null);
        bool success = int.TryParse(rawValue, out int result);
        Debug.Assert(success);
        Debug.Assert(IsValid(result));
        return result;
    }

    protected override bool IsValid(int? value)
    {
        Debug.Assert(value.HasValue);
        int iValue = value.Value;
        return MinValue <= iValue && iValue <= MaxValue;
    }
}

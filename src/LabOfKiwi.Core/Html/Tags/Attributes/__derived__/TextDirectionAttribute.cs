using System;
using System.Diagnostics;

namespace LabOfKiwi.Html.Tags.Attributes;

internal class TextDirectionAttribute : Attribute<TextDirectionOption?>
{
    public TextDirectionAttribute(string name, AttributeCollection attributes) : base(name, attributes, false)
    {
    }

    protected sealed override string ConvertToRawValue(TextDirectionOption? value)
    {
        Debug.Assert(value.HasValue);

        return value.Value switch
        {
            TextDirectionOption.LeftToRight => "ltr",
            TextDirectionOption.RightToLeft => "rtl",
            TextDirectionOption.Auto        => "auto",
            _                               => throw new InvalidOperationException()
        };
    }

    protected sealed override TextDirectionOption? ConvertToUserValue(string rawValue)
    {
        Debug.Assert(rawValue != null);

        return rawValue switch
        {
            "ltr"  => TextDirectionOption.LeftToRight,
            "rtl"  => TextDirectionOption.RightToLeft,
            "auto" => TextDirectionOption.Auto,
            _      => throw new InvalidOperationException()
        };
    }
}

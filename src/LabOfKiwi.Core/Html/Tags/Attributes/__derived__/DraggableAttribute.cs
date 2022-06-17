using System;
using System.Diagnostics;

namespace LabOfKiwi.Html.Tags.Attributes;

internal class DraggableAttribute : Attribute<DraggableOption?>
{
    public DraggableAttribute(string name, AttributeCollection attributes) : base(name, attributes, false)
    {
    }

    protected sealed override string ConvertToRawValue(DraggableOption? value)
    {
        Debug.Assert(value.HasValue);

        return value switch
        {
            DraggableOption.Auto  => "auto",
            DraggableOption.True  => "true",
            DraggableOption.False => "false",
            _                     => throw new InvalidOperationException(),
        };
    }

    protected sealed override DraggableOption? ConvertToUserValue(string rawValue)
    {
        Debug.Assert(rawValue != null);

        return rawValue switch
        {
            "auto"  => DraggableOption.Auto,
            "true"  => DraggableOption.True,
            "false" => DraggableOption.False,
            _       => throw new InvalidOperationException(),
        };
    }
}

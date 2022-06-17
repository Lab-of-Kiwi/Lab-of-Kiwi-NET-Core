namespace LabOfKiwi.Html.Tags.Attributes;

public class ToggleableAttribute : Attribute
{
    internal ToggleableAttribute(string name, AttributeCollection attributes) : base(name, attributes)
    {
    }

    public bool Value
    {
        get => Exists();

        set => SetRawValue(value);
    }
}

namespace LabOfKiwi.Html.Tags.Attributes;

internal abstract class Attribute
{
    protected Attribute(string name, AttributeCollection attributes)
    {
        Attributes = attributes;
        Name = name;
    }

    private AttributeCollection Attributes { get; }

    public string Name { get; }

    public void Delete()
    {
        SetRawValue(false);
    }

    protected string? GetRawValue() => Attributes.Get(Name);

    protected void SetRawValue(string value)
    {
        Attributes.Set(Name, value);
    }

    protected void SetRawValue(bool value)
    {
        Attributes.Set(Name, value);
    }

    protected bool Exists()
    {
        return Attributes.Has(Name);
    }
}

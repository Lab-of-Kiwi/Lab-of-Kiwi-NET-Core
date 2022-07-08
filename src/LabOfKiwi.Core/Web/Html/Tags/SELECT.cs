using LabOfKiwi.Web.Html.Attributes;

namespace LabOfKiwi.Web.Html.Tags;

public class SELECT : Element
{
    public bool AllowMultiple
    {
        get => RawAttributes.GetBoolean("multiple");
        set => RawAttributes.SetBoolean("multiple", value);
    }

    // TODO
    public string? AutoComplete
    {
        get => RawAttributes["autocomplete"];
        set => RawAttributes["autocomplete"] = value;
    }

    public string? Form
    {
        get => RawAttributes.GetObject<TokenParser, string>("form");
        set => RawAttributes.SetObject<TokenParser, string>("form", value);
    }

    public bool IsDisabled
    {
        get => RawAttributes.GetBoolean("disabled");
        set => RawAttributes.SetBoolean("disabled", value);
    }

    public bool IsRequired
    {
        get => RawAttributes.GetBoolean("required");
        set => RawAttributes.SetBoolean("required", value);
    }

    public string? Name
    {
        get => RawAttributes["name"];
        set => RawAttributes["name"] = value;
    }

    public long? Size
    {
        get => RawAttributes.GetLong("size", min: 1);
        set => RawAttributes.SetLong("size", value, min: 1);
    }

    public sealed override string TagName => "select";
}

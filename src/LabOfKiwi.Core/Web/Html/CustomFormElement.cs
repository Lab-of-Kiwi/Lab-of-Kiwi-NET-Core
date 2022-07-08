using LabOfKiwi.Web.Html.Attributes;

namespace LabOfKiwi.Web.Html;

public abstract class CustomFormElement : CustomElement
{
    protected CustomFormElement(string tagName) : base(tagName)
    {
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

    public bool IsReadOnly
    {
        get => RawAttributes.GetBoolean("readonly");
        set => RawAttributes.SetBoolean("readonly", value);
    }

    public string? Name
    {
        get => RawAttributes["name"];
        set => RawAttributes["name"] = value;
    }
}

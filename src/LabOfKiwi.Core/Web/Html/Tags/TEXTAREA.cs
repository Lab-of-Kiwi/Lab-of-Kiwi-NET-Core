using LabOfKiwi.Web.Html.Attributes;

namespace LabOfKiwi.Web.Html.Tags;

public class TEXTAREA : Element
{
    // TODO
    public string? AutoComplete
    {
        get => RawAttributes["autocomplete"];
        set => RawAttributes["autocomplete"] = value;
    }

    public long? Cols
    {
        get => RawAttributes.GetLong("cols", min: 1L);
        set => RawAttributes.SetLong("cols", value, min: 1L);
    }

    public string? DirName
    {
        get => RawAttributes["dirname"];
        set => RawAttributes["dirname"] = value;
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

    public bool IsRequired
    {
        get => RawAttributes.GetBoolean("required");
        set => RawAttributes.SetBoolean("required", value);
    }

    public long? MaxLength
    {
        get => RawAttributes.GetLong("maxlength", min: 0L);
        set => RawAttributes.SetLong("maxlength", value, min: 0L);
    }

    public long? MinLength
    {
        get => RawAttributes.GetLong("minlength", min: 0L);
        set => RawAttributes.SetLong("minlength", value, min: 0L);
    }

    public string? Name
    {
        get => RawAttributes["name"];
        set => RawAttributes["name"] = value;
    }

    public string? Placeholder
    {
        get => RawAttributes["placeholder"];
        set => RawAttributes["placeholder"] = value;
    }

    public long? Rows
    {
        get => RawAttributes.GetLong("rows", min: 1L);
        set => RawAttributes.SetLong("rows", value, min: 1L);
    }

    public sealed override string TagName => "textarea";

    public WrapOption? Wrap
    {
        get => RawAttributes.GetEnum<WrapOption>("wrap");
        set => RawAttributes.SetEnum("wrap", value);
    }

    public enum WrapOption
    {
        Soft,
        Hard
    }
}

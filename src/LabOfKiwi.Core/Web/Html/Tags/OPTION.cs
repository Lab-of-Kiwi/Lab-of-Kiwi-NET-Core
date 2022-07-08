namespace LabOfKiwi.Web.Html.Tags;

public class OPTION : Element
{
    public bool IsDisabled
    {
        get => RawAttributes.GetBoolean("disabled");
        set => RawAttributes.SetBoolean("disabled", value);
    }

    public bool IsSelected
    {
        get => RawAttributes.GetBoolean("selected");
        set => RawAttributes.SetBoolean("selected", value);
    }

    public string? Label
    {
        get => RawAttributes["label"];
        set => RawAttributes["label"] = value;
    }

    public sealed override string TagName => "option";

    public string? Value
    {
        get => RawAttributes["value"];
        set => RawAttributes["value"] = value;
    }
}

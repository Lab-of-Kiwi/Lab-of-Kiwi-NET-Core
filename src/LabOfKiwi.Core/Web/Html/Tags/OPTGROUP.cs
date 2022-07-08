namespace LabOfKiwi.Web.Html.Tags;

public class OPTGROUP : Element
{
    public bool IsDisabled
    {
        get => RawAttributes.GetBoolean("disabled");
        set => RawAttributes.SetBoolean("disabled", value);
    }

    public string? Label
    {
        get => RawAttributes["label"];
        set => RawAttributes["label"] = value;
    }

    public sealed override string TagName => "optgroup";
}

namespace LabOfKiwi.Web.Html.Tags;

public class DATA : Element
{
    public sealed override string TagName => "data";

    public string? Value
    {
        get => RawAttributes["value"];
        set => RawAttributes["value"] = value;
    }
}

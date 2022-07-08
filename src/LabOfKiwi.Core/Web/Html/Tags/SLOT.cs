namespace LabOfKiwi.Web.Html.Tags;

public class SLOT : Element
{
    public string? Name
    {
        get => RawAttributes["name"];
        set => RawAttributes["name"] = value;
    }

    public sealed override string TagName => "slot";
}

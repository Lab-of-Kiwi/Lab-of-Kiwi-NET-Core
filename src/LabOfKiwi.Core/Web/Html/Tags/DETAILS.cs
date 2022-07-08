namespace LabOfKiwi.Web.Html.Tags;

public class DETAILS : Element
{
    public bool IsOpen
    {
        get => RawAttributes.GetBoolean("open");
        set => RawAttributes.SetBoolean("open", value);
    }

    public sealed override string TagName => "details";
}

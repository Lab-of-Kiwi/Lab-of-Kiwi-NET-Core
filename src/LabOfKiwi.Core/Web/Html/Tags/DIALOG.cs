namespace LabOfKiwi.Web.Html.Tags;

public class DIALOG : Element
{
    public bool IsOpen
    {
        get => RawAttributes.GetBoolean("open");
        set => RawAttributes.SetBoolean("open", value);
    }

    public sealed override string TagName => "dialog";
}

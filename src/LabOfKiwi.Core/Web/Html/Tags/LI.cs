namespace LabOfKiwi.Web.Html.Tags;

public class LI : Element
{
    public sealed override string TagName => "li";

    public long? Value
    {
        get => RawAttributes.GetLong("value");
        set => RawAttributes.SetLong("value", value);
    }
}

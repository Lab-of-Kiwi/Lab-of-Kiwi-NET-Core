namespace LabOfKiwi.Web.Html.Tags;

public class COLGROUP : Element
{
    public long? Span
    {
        get => RawAttributes.GetLong("span", min: 1);
        set => RawAttributes.SetLong("span", value, min: 1);
    }

    public sealed override string TagName => "colgroup";
}

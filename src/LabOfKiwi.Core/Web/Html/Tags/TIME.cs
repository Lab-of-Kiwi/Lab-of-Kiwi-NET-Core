namespace LabOfKiwi.Web.Html.Tags;

public class TIME : Element
{
    // TODO
    public string? Datetime
    {
        get => RawAttributes["datetime"];
        set => RawAttributes["datatime"] = value;
    }

    public sealed override string TagName => "time";
}

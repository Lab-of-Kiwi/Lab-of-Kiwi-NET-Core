using LabOfKiwi.Web.Html.Attributes;

namespace LabOfKiwi.Web.Html.Tags;

public class BODY : Element
{
    public new BodyEvents Events => new(RawAttributes);

    public sealed override string TagName => "body";
}

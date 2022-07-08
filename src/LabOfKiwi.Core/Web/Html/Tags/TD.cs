using LabOfKiwi.Web.Html.Attributes;
using System.Collections.Generic;

namespace LabOfKiwi.Web.Html.Tags;

public class TD : Element
{
    public long? Colspan
    {
        get => RawAttributes.GetLong("colspan", min: 1L);
        set => RawAttributes.SetLong("colspan", value, min: 1L);
    }

    public IList<string> Headers => RawAttributes.GetList<TokenParser, string>("headers");

    public long? Rowspan
    {
        get => RawAttributes.GetLong("rowspan", min: 0L);
        set => RawAttributes.SetLong("rowspan", value, min: 0L);
    }

    public sealed override string TagName => "td";
}

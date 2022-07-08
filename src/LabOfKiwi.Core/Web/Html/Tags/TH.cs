using LabOfKiwi.Web.Html.Attributes;
using System.Collections.Generic;

namespace LabOfKiwi.Web.Html.Tags;

public class TH : Element
{
    public string? Abbr
    {
        get => RawAttributes["abbr"];
        set => RawAttributes["abbr"] = value;
    }

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

    public ScopeOption? Scope
    {
        get => RawAttributes.GetEnum<ScopeOption>("scope");
        set => RawAttributes.SetEnum("scope", value);
    }

    public sealed override string TagName => "th";

    public enum ScopeOption
    {
        Row,
        Col,
        Rowgroup,
        Colgroup
    }
}

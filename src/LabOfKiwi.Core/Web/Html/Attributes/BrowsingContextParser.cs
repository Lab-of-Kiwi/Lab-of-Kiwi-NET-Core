namespace LabOfKiwi.Web.Html.Attributes;

public class BrowsingContextParser : Parser<BrowsingContextOption>
{
    protected override BrowsingContextOption InternalParse(string rawValue)
    {
        if (BrowsingContextOption.IsValid(rawValue))
        {
            return new(rawValue);
        }

        return base.InternalParse(rawValue);
    }

    protected override string InternalToString(BrowsingContextOption value)
        => value.ToString();
}

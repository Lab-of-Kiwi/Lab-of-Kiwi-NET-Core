namespace LabOfKiwi.Web.Html.Attributes;

public class StringParser : Parser<string>
{
    protected sealed override string InternalParse(string rawValue)
        => rawValue;

    protected sealed override string InternalToString(string value)
        => value;
}

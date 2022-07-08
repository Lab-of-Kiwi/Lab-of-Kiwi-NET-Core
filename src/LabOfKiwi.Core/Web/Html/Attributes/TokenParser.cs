namespace LabOfKiwi.Web.Html.Attributes;

public class TokenParser : StringParser
{
    public override bool IsValid(string value)
        => !HtmlHelper.HasASCIIWhitespace(value);
}

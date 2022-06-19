using System.Text;

namespace LabOfKiwi.Html.Tags;

public abstract class NonContainerTag : Tag
{
    protected NonContainerTag()
    {
    }

    internal override void CompleteToString(StringBuilder sb, int tabCount, HtmlFormatOptions formatOptions)
    {
        sb.Append(" />");
    }
}

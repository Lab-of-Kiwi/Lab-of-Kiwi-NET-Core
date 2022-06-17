using System.Text;

namespace LabOfKiwi.Html.Tags;

public abstract class NonContainerTag : Tag
{
    protected NonContainerTag()
    {
    }

    internal sealed override void CompleteToString(StringBuilder sb)
    {
        sb.Append(" />");
    }
}

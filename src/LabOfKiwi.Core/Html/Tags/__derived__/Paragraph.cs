namespace LabOfKiwi.Html.Tags;

public class Paragraph : ContainerTag<IElement>
{
    protected sealed override bool RequiresClosingTag => true;

    protected sealed override string TagName => "p";
}

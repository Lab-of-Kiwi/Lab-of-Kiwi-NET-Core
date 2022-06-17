namespace LabOfKiwi.Html.Tags;

public class Article : ContainerTag<IElement>
{
    protected sealed override bool RequiresClosingTag => true;

    protected sealed override string TagName => "article";
}

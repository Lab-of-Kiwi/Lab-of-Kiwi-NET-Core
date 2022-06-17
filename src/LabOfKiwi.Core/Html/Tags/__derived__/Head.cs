namespace LabOfKiwi.Html.Tags;

public class Head : ContainerTag<IElement>
{
    protected sealed override bool RequiresClosingTag => true;

    protected sealed override string TagName => "head";
}

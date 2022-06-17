namespace LabOfKiwi.Html.Tags;

public class Aside : ContainerTag<IElement>
{
    protected sealed override bool RequiresClosingTag => true;

    protected sealed override string TagName => "aside";
}

namespace LabOfKiwi.Html.Tags;

public class BiDirectionalIsolation : ContainerTag<IElement>
{
    protected sealed override bool RequiresClosingTag => true;

    protected sealed override string TagName => "bdi";
}

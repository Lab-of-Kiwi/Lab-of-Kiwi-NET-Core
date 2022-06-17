namespace LabOfKiwi.Html.Tags;

public class Body : ContainerTag<IElement>
{
    protected override bool RequiresClosingTag => true;

    protected override string TagName => "body";
}

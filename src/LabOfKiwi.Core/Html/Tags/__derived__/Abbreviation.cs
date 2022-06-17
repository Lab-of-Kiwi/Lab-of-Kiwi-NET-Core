namespace LabOfKiwi.Html.Tags;

public class Abbreviation : ContainerTag<IElement>
{
    protected sealed override bool RequiresClosingTag => true;

    protected sealed override string TagName => "abbr";
}

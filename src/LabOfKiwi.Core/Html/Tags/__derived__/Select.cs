namespace LabOfKiwi.Html.Tags;

public class Select : ContainerTag<ISelectChild>
{
    protected override bool RequiresClosingTag => true;

    protected override string TagName => "select";

    protected override bool IsValid(ISelectChild child)
    {
        return child is Option || child is OptionGroup;
    }
}

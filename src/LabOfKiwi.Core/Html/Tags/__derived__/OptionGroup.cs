using LabOfKiwi.Html.Tags.Attributes;
using System;

namespace LabOfKiwi.Html.Tags;

public class OptionGroup : ContainerTag<Option>, ISelectChild
{
    public bool IsDisabled
    {
        get => Attributes.Has("disabled");
        set => Attributes.Set("disabled", value);
    }

    public string? Label
    {
        get => new TextAttribute("label", Attributes, requiresEncoding: true).Value;
        set => new TextAttribute("label", Attributes, requiresEncoding: true).Value = value;
    }

    protected sealed override bool RequiresClosingTag => throw new NotImplementedException();

    protected sealed override string TagName => throw new NotImplementedException();
}

﻿namespace LabOfKiwi.Html.Tags;

public class Address : ContainerTag<IElement>
{
    protected sealed override bool RequiresClosingTag => true;

    protected sealed override string TagName => "address";
}
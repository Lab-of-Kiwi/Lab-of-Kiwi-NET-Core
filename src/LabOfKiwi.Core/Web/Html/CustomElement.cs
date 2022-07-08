using LabOfKiwi.Web.Html.Tags;
using System;

namespace LabOfKiwi.Web.Html;

public abstract class CustomElement : Element
{
    protected CustomElement(string tagName)
    {
        if (tagName == null)
        {
            throw new ArgumentNullException(nameof(tagName));
        }

        TagName = tagName;
    }

    public sealed override string TagName { get; }
}

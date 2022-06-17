using System.Collections.Generic;

namespace LabOfKiwi.Html;

public abstract class Element : IElement
{
    protected Element()
    {
    }

    public IEnumerable<IContainerElement> Ancestors
    {
        get
        {
            if (Parent == null)
            {
                yield break;
            }

            yield return Parent;

            foreach (var ancestor in Parent.Ancestors)
            {
                yield return ancestor;
            }
        }
    }

    public IContainerElement? Parent { get; internal set; }

    public IEnumerable<IElement> Siblings
    {
        get
        {
            if (Parent == null)
            {
                yield break;
            }

            foreach (var sibling in Parent.Children)
            {
                if (!ReferenceEquals(this, sibling))
                {
                    yield return sibling;
                }
            }
        }
    }

    IContainerElement? IElement.Parent
    {
        get => Parent;
        set => Parent = value;
    }
}

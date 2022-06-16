using System.Collections.Generic;

namespace LabOfKiwi.Cmd;

public abstract class StackElement<TSelf> : ViewElement where TSelf : StackElement<TSelf>
{
    private readonly List<ViewElement> _children;

    internal StackElement()
    {
        _children = new List<ViewElement>();
    }

    public IReadOnlyList<ViewElement> Children => _children.AsReadOnly();

    public TSelf Add(ViewElement? child)
    {
        if (child != null)
        {
            _children.Add(child);
        }

        return (TSelf)this;
    }

    public TSelf Add(string? text)
    {
        if (!string.IsNullOrEmpty(text))
        {
            _children.Add(new TextBlock().Add(text));
        }

        return (TSelf)this;
    }

    public TSelf Append(string? text)
    {
        if (!string.IsNullOrEmpty(text))
        {
            if (_children.Count > 0 && _children[^1] is TextBlock tb)
            {
                tb.Add(text);
            }
            else
            {
                _children.Add(new TextBlock().Add(text));
            }
        }

        return (TSelf)this;
    }
}

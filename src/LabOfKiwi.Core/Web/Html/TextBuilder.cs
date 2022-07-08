using System.Collections;
using System.Collections.Generic;

namespace LabOfKiwi.Web.Html;

public sealed class TextBuilder : IEnumerable<Node>
{
    private readonly List<Node> _nodes;

    internal TextBuilder()
    {
        _nodes = new List<Node>();
    }

    public TextBuilder Add(string? text)
    {
        if (!string.IsNullOrEmpty(text))
        {
            if (_nodes.Count != 0 && _nodes[^1] is Text prevText)
            {
                prevText.Append(" " + text);
            }
            else
            {
                _nodes.Add(new Text(text));
            }
        }

        return this;
    }

    public TextBuilder Add<TElement>(string? text = null) where TElement : Element, new()
    {
        TElement element = new();
        element.Add(text);
        _nodes.Add(element);
        return this;
    }

    IEnumerator<Node> IEnumerable<Node>.GetEnumerator() => _nodes.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _nodes.GetEnumerator();
}

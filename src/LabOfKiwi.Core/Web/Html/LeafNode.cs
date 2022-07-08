using System;
using System.Collections.Generic;
using System.Linq;

namespace LabOfKiwi.Web.Html;

public abstract class LeafNode : Node
{
    internal LeafNode()
    {
    }

    public sealed override int ChildNodeCount => 0;

    public sealed override IEnumerable<Node> ChildNodes => Enumerable.Empty<Node>();

    public sealed override Node this[int index] => throw new IndexOutOfRangeException();
}

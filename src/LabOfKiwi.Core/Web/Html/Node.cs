using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LabOfKiwi.Web.Html;

public abstract class Node
{
    private Node? _parentNode;
    private bool _parentHasBeenSet;

    internal Node()
    {
    }

    public abstract Node this[int index] { get; }

    public IEnumerable<Element> AllElements => AllNodes.OfType<Element>();

    public IEnumerable<Node> AllNodes
    {
        get
        {
            var root = RootNode;
            return root.DescendantNodes.Prepend(root);
        }
    }

    public IEnumerable<Element> AncestorElements => AncestorNodes.OfType<Element>();

    public IEnumerable<Node> AncestorNodes
    {
        get
        {
            var parentNode = ParentNode;

            while (parentNode != null)
            {
                yield return parentNode;
                parentNode = parentNode.ParentNode;
            }
        }
    }

    public int ChildElementCount => ChildElements.Count();

    public IEnumerable<Element> ChildElements => ChildNodes.OfType<Element>();

    public abstract int ChildNodeCount { get; }

    public abstract IEnumerable<Node> ChildNodes { get; }

    public IEnumerable<Node> DescendantElements => DescendantNodes.OfType<Element>();

    public IEnumerable<Node> DescendantNodes
    {
        get
        {
            foreach (var child in ChildNodes)
            {
                yield return child;
                
                foreach (var descendent in child.DescendantNodes)
                {
                    yield return descendent;
                }
            }
        }
    }

    public Element? NextElementSibling => NextElementSiblings.FirstOrDefault();

    public IEnumerable<Element> NextElementSiblings => NextNodeSiblings.OfType<Element>();

    public Node? NextNodeSibling => NextNodeSiblings.FirstOrDefault();

    public IEnumerable<Node> NextNodeSiblings
    {
        get
        {
            if (ParentNode != null)
            {
                return ParentNode.ChildNodes
                    .SkipWhile(n => this != n)
                    .Skip(1);
            }

            return Enumerable.Empty<Node>();
        }
    }

    public Document? OwnerDocument
    {
        get
        {
            if (this is Document doc)
            {
                return doc;
            }

            return AncestorNodes.OfType<Document>().FirstOrDefault();
        }
    }

    public Element? ParentElement
    {
        get
        {
            if (ParentNode is Element parentElement)
            {
                return parentElement;
            }

            if (ParentNode != null)
            {
                return ParentNode.ParentElement;
            }

            return null;
        }
    }

    public Node? ParentNode
    {
        get => _parentNode;

        internal set
        {
            Debug.Assert(this is not Document);

            if (value != null)
            {
                if (_parentHasBeenSet)
                {
                    throw new InvalidOperationException("Parent has already been set once before.");
                }

                _parentHasBeenSet = true;
            }

            _parentNode = value;
        }
    }

    public Element? PreviousElementSibling => PreviousElementSiblings.FirstOrDefault();

    public IEnumerable<Element> PreviousElementSiblings => PreviousNodeSiblings.OfType<Element>();

    public Node? PreviousNodeSibling => PreviousNodeSiblings.FirstOrDefault();

    public IEnumerable<Node> PreviousNodeSiblings
    {
        get
        {
            if (ParentNode != null)
            {
                return ParentNode.ChildNodes
                    .TakeWhile(n => this != n)
                    .Reverse();
            }

            return Enumerable.Empty<Node>();
        }
    }

    public Element? RootElement => AncestorElements.LastOrDefault();

    public Node RootNode
    {
        get
        {
            if (ParentNode == null)
            {
                return this;
            }

            return ParentNode.RootNode;
        }
    }

    public sealed override bool Equals(object? obj) => ReferenceEquals(this, obj);

    public sealed override int GetHashCode() => base.GetHashCode();

    public abstract override string ToString();
}

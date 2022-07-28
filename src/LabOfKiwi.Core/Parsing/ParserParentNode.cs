using LabOfKiwi.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LabOfKiwi.Parsing;

public abstract class ParserParentNode : ParserNode
{
    internal readonly List<ParserNode> _nextNodes;

    internal ParserParentNode()
    {
        _nextNodes = new List<ParserNode>();
    }

    public ParserParentNode GoesTo<TParserNode>(Action<TParserNode> callback) where TParserNode : ParserParentNode, new()
    {
        Debug.Assert(callback != null);

        TParserNode node = new();
        callback(node);
        _nextNodes.Add(node);

        return this;
    }

    public ParserParentNode GoesTo(char c, Action<ParserParentNode> callback)
    {
        Debug.Assert(callback != null);

        ParserTokenNode<CharacterToken> node = new(new(c));
        callback(node);
        _nextNodes.Add(node);

        return this;
    }

    public ParserParentNode GoesTo(string str, Action<ParserParentNode> callback)
    {
        Debug.Assert(callback != null);
        Debug.Assert(!string.IsNullOrEmpty(str));

        ParserTokenNode<WordToken> node = new(new(str));
        callback(node);
        _nextNodes.Add(node);

        return this;
    }

    public ParserParentNode GoesTo(ParserCompoundTokenNode.StartNode node)
    {
        Debug.Assert(node != null);
        _nextNodes.Add(node);
        return this;
    }

    public ParserParentNode GoesTo(ParserCompoundTokenNode.IntermediateNode node)
    {
        Debug.Assert(node != null);
        _nextNodes.Add(node);
        return this;
    }

    public ParserParentNode GoesTo(ParserCompoundTokenNode.EndNode node)
    {
        Debug.Assert(node != null);
        _nextNodes.Add(node);
        return this;
    }

    internal sealed override bool TryParse(RandomAccessString text, AbstractSyntaxTree results)
    {
        int textPosition = text.Index;
        int resultsCount = results.Count;

        if (InternalTryParse(text, results))
        {
            foreach (var node in _nextNodes)
            {
                if (node.TryParse(text, results))
                {
                    return true;
                }
            }
        }

        text.Index = textPosition;
        results.Count = resultsCount;
        return false;
    }

    internal abstract bool InternalTryParse(RandomAccessString text, AbstractSyntaxTree results);
}

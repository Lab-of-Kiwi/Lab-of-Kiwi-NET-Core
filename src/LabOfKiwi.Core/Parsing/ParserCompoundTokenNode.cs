using LabOfKiwi.Text;

namespace LabOfKiwi.Parsing;

public abstract class ParserCompoundTokenNode : ParserParentNode
{
    private readonly StartNode _startNode;
    private readonly EndNode _endNode;

    protected ParserCompoundTokenNode()
    {
        _startNode = new StartNode();
        _endNode = new EndNode();
        GenerateTree(_startNode, _endNode);
    }

    protected abstract void GenerateTree(StartNode startNode, EndNode endNode);

    internal override bool InternalTryParse(RandomAccessString text, AbstractSyntaxTree results)
    {
        results.Add(new AbstractSyntaxTree.Result(this, null));
        return _startNode.TryParse(text, results);
    }

    public sealed class StartNode : ParserParentNode
    {
        internal StartNode()
        {
        }

        internal override bool InternalTryParse(RandomAccessString text, AbstractSyntaxTree results)
        {
            return true;
        }
    }

    public sealed class IntermediateNode : ParserParentNode
    {
        public IntermediateNode()
        {
        }

        internal override bool InternalTryParse(RandomAccessString text, AbstractSyntaxTree results)
        {
            return true;
        }
    }

    public sealed class EndNode : ParserNode
    {
        internal EndNode()
        {
        }

        internal override bool TryParse(RandomAccessString text, AbstractSyntaxTree results)
        {
            return true;
        }
    }
}

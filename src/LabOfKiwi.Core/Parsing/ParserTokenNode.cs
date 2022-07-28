using LabOfKiwi.Text;

namespace LabOfKiwi.Parsing;

internal sealed class ParserTokenNode<TParser> : ParserParentNode where TParser : struct, IParserToken
{
    private readonly TParser _parser;

    public ParserTokenNode()
    {
        _parser = new();
    }

    public ParserTokenNode(TParser parser)
    {
        _parser = parser;
    }

    internal override bool InternalTryParse(RandomAccessString text, AbstractSyntaxTree results)
    {
        if (_parser.TryParse(text, out string? result))
        {
            results.Add(new AbstractSyntaxTree.Result(this, result));
            text.Index += result.Length;
            return true;
        }

        return false;
    }
}

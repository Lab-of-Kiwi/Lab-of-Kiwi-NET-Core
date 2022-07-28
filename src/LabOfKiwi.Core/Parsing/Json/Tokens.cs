namespace LabOfKiwi.Parsing.Json;

public sealed class Whitespace : ParserCompoundTokenNode
{
    protected override void GenerateTree(StartNode startNode, EndNode endNode)
    {
        var charStart = new IntermediateNode();
        var charEnd = new IntermediateNode();

        charStart.GoesTo(' ', node =>
        {
            node.GoesTo(charEnd);
        });

        charStart.GoesTo('\n', node =>
        {
            node.GoesTo(charEnd);
        });

        charStart.GoesTo('\r', node =>
        {
            node.GoesTo(charEnd);
        });

        charStart.GoesTo('\t', node =>
        {
            node.GoesTo(charEnd);
        });

        startNode.GoesTo(charStart);
        charEnd.GoesTo(charStart);
        startNode.GoesTo(endNode);
    }
}
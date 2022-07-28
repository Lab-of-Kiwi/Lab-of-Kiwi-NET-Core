using LabOfKiwi.Text;

namespace LabOfKiwi.Parsing;

public abstract class ParserNode
{
    internal ParserNode()
    {
    }

    internal abstract bool TryParse(RandomAccessString text, AbstractSyntaxTree results);
}

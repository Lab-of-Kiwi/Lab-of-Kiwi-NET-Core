using LabOfKiwi.Text;
using System.Diagnostics.CodeAnalysis;

namespace LabOfKiwi.Parsing;

public interface IParserToken
{
    bool TryParse(IIndexedString text, [MaybeNullWhen(false)] out string result);
}

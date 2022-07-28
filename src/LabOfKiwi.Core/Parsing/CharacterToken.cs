using LabOfKiwi.Text;
using System.Diagnostics.CodeAnalysis;

namespace LabOfKiwi.Parsing;

internal readonly struct CharacterToken : IParserToken
{
    private readonly char _char;

    public CharacterToken(char c)
    {
        _char = c;
    }

    public bool TryParse(IIndexedString text, [MaybeNullWhen(false)] out string result)
    {
        if (text.NextEquals(_char))
        {
            result = _char.ToString();
            return true;
        }

        result = default;
        return false;
    }
}

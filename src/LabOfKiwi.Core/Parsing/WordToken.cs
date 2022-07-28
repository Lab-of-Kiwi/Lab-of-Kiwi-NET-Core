using LabOfKiwi.Text;
using System.Diagnostics.CodeAnalysis;

namespace LabOfKiwi.Parsing;

internal readonly struct WordToken : IParserToken
{
    private readonly string _str;

    public WordToken(string str)
    {
        _str = str;
    }

    public bool TryParse(IIndexedString text, [MaybeNullWhen(false)] out string result)
    {
        if (text.NextEquals(_str))
        {
            result = _str;
            return true;
        }

        result = default;
        return false;
    }
}

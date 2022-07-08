using System.Diagnostics;

namespace LabOfKiwi.Web.Html.Attributes;

public sealed class BooleanParser : Parser<bool>
{
    private readonly string _true;
    private readonly string _false;

    internal BooleanParser(string trueString, string falseString)
    {
        Debug.Assert(!string.IsNullOrEmpty(trueString));
        Debug.Assert(!string.IsNullOrEmpty(falseString));
        Debug.Assert(trueString != falseString);
        _true = trueString;
        _false = falseString;
    }

    public sealed override bool IsValid(bool value) => true;

    protected sealed override bool InternalParse(string rawValue)
    {
        if (rawValue == _true)
        {
            return true;
        }

        if (rawValue == _false)
        {
            return false;
        }

        return base.InternalParse(rawValue);
    }

    protected sealed override string InternalToString(bool value)
        => value ? _true : _false;
}

namespace LabOfKiwi.Web.Html.Attributes;

public sealed class RangeLongParser : LongParser
{
    private readonly long _min;
    private readonly long _max;

    public RangeLongParser(long min, long max)
    {
        _min = min;
        _max = max;
    }

    public override bool IsValid(long value)
        => value >= _min && value <= _max;
}

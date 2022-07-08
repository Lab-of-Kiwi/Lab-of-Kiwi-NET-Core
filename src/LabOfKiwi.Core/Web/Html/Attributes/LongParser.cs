namespace LabOfKiwi.Web.Html.Attributes;

public class LongParser : Parser<long>
{
    protected sealed override long InternalParse(string rawValue)
    {
        if (long.TryParse(rawValue, out long result))
        {
            return result;
        }

        return base.InternalParse(rawValue);
    }

    protected sealed override string InternalToString(long value)
        => value.ToString();
}

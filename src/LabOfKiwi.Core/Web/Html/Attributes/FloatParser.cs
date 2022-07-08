namespace LabOfKiwi.Web.Html.Attributes;

public class FloatParser : Parser<float>
{
    protected sealed override float InternalParse(string rawValue)
    {
        if (float.TryParse(rawValue, out float result))
        {
            return result;
        }

        return base.InternalParse(rawValue);
    }

    protected sealed override string InternalToString(float value)
        => value.ToString();
}

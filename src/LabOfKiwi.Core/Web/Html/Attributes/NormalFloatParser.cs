namespace LabOfKiwi.Web.Html.Attributes;

public sealed class NormalFloatParser : FloatParser
{
    public override bool IsValid(float value)
    {
        return !float.IsNaN(value) && !float.IsInfinity(value);
    }
}

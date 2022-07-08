namespace LabOfKiwi.Web.Html.Tags;

public class OL : Element
{
    public bool IsReversed
    {
        get => RawAttributes.GetBoolean("reversed");
        set => RawAttributes.SetBoolean("reversed", value);
    }

    public long? Start
    {
        get => RawAttributes.GetLong("start");
        set => RawAttributes.SetLong("start", value);
    }

    public sealed override string TagName => "ol";

    public MarkerType? Type
    {
        get
        {
            string? rawValue = RawAttributes["type"];

            if (rawValue == null)
            {
                return null;
            }

            switch (rawValue)
            {
                case "1": return MarkerType.Decimal;
                case "a": return MarkerType.LowercaseAlphabet;
                case "A": return MarkerType.UppercaseAlphabet;
                case "i": return MarkerType.LowercaseRomanNumeral;
                case "I": return MarkerType.UppercaseRomanNumeral;
            }

            HtmlHelper.ThrowInvalidAttributeStateException("type", $"Invalid value of '{rawValue}' is set.");
            return default;
        }

        set
        {
            string? rawValue;

            if (value.HasValue)
            {
                rawValue = value.Value switch
                {
                    MarkerType.Decimal => "1",
                    MarkerType.LowercaseAlphabet => "a",
                    MarkerType.UppercaseAlphabet => "A",
                    MarkerType.LowercaseRomanNumeral => "i",
                    MarkerType.UppercaseRomanNumeral => "I",
                    _ => throw new System.NotImplementedException(),
                };
            }
            else
            {
                rawValue = null;
            }

            RawAttributes["type"] = rawValue;
        }
    }

    public enum MarkerType
    {
        Decimal,
        LowercaseAlphabet,
        UppercaseAlphabet,
        LowercaseRomanNumeral,
        UppercaseRomanNumeral
    }
}

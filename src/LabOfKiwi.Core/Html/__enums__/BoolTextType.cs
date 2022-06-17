namespace LabOfKiwi.Html;

public enum BoolTextType : byte
{
    TrueFalse = 0,
    YesNo = 1,
    OnOff = 2
}

public static class BoolTextTypeExtensions
{
    public static (string True, string False) GetStringValues(this BoolTextType value)
    {
        string trueText = value switch
        {
            BoolTextType.TrueFalse => "true",
            BoolTextType.YesNo => "yes",
            BoolTextType.OnOff => "on",
            _ => throw new System.NotImplementedException(),
        };

        string falseText = value switch
        {
            BoolTextType.TrueFalse => "false",
            BoolTextType.YesNo => "no",
            BoolTextType.OnOff => "off",
            _ => throw new System.NotImplementedException(),
        };

        return (trueText, falseText);
    }
}

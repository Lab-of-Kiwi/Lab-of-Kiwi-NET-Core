using LabOfKiwi.Web.Html.Attributes;

namespace LabOfKiwi.Web.Html.Tags;

public class PROGRESS : Element
{
    public float? Max
    {
        get => RawAttributes.GetStruct<NormalFloatParser, float>("max");
        set => RawAttributes.SetStruct<NormalFloatParser, float>("max", value);
    }

    public sealed override string TagName => "progress";

    public float? Value
    {
        get => RawAttributes.GetStruct<NormalFloatParser, float>("value");
        set => RawAttributes.SetStruct<NormalFloatParser, float>("value", value);
    }
}

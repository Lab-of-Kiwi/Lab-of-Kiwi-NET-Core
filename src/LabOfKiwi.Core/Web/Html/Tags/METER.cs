using LabOfKiwi.Web.Html.Attributes;

namespace LabOfKiwi.Web.Html.Tags;

public class METER : Element
{
    public float? High
    {
        get => RawAttributes.GetStruct<NormalFloatParser, float>("high");
        set => RawAttributes.SetStruct<NormalFloatParser, float>("high", value);
    }

    public float? Low
    {
        get => RawAttributes.GetStruct<NormalFloatParser, float>("low");
        set => RawAttributes.SetStruct<NormalFloatParser, float>("low", value);
    }

    public float? Max
    {
        get => RawAttributes.GetStruct<NormalFloatParser, float>("max");
        set => RawAttributes.SetStruct<NormalFloatParser, float>("max", value);
    }

    public float? Min
    {
        get => RawAttributes.GetStruct<NormalFloatParser, float>("min");
        set => RawAttributes.SetStruct<NormalFloatParser, float>("min", value);
    }

    public float? Optimum
    {
        get => RawAttributes.GetStruct<NormalFloatParser, float>("optimum");
        set => RawAttributes.SetStruct<NormalFloatParser, float>("optimum", value);
    }

    public sealed override string TagName => "meter";

    public float? Value
    {
        get => RawAttributes.GetStruct<NormalFloatParser, float>("value");
        set => RawAttributes.SetStruct<NormalFloatParser, float>("value", value);
    }
}

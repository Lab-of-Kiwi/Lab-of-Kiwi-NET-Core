﻿using LabOfKiwi.Web.Html.Attributes;

namespace LabOfKiwi.Web.Html.Tags;

public partial class INPUT
{
    public class Number : INPUT
    {
        public new string? AutoComplete
        {
            get => base.AutoComplete;
            set => base.AutoComplete = value;
        }

        public new bool IsReadOnly
        {
            get => base.IsReadOnly;
            set => base.IsReadOnly = value;
        }

        public new bool IsRequired
        {
            get => base.IsRequired;
            set => base.IsRequired = value;
        }

        public new string? List
        {
            get => base.List;
            set => base.List = value;
        }

        public new float? Max
        {
            get => RawAttributes.GetStruct<NormalFloatParser, float>("max");
            set => RawAttributes.SetStruct<NormalFloatParser, float>("max", value);
        }

        public new float? Min
        {
            get => RawAttributes.GetStruct<NormalFloatParser, float>("min");
            set => RawAttributes.SetStruct<NormalFloatParser, float>("min", value);
        }

        public new string? Placeholder
        {
            get => base.Placeholder;
            set => base.Placeholder = value;
        }

        public new StepOption? Step
        {
            get => base.Step;
            set => base.Step = value;
        }

        public new float? Value
        {
            get => RawAttributes.GetStruct<NormalFloatParser, float>("value");
            set => RawAttributes.SetStruct<NormalFloatParser, float>("value", value);
        }

        internal sealed override InputType GetInputType() => InputType.Number;
    }
}

using LabOfKiwi.Web.Html.Attributes;
using System;
using System.Globalization;

namespace LabOfKiwi.Web.Html.Tags;

public partial class INPUT
{
    public class Date : INPUT
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

        public new DateOnly? Max
        {
            get => RawAttributes.GetStruct<DateParser, DateOnly>("max");
            set => RawAttributes.SetStruct<DateParser, DateOnly>("max", value);
        }

        public new DateOnly? Min
        {
            get => RawAttributes.GetStruct<DateParser, DateOnly>("min");
            set => RawAttributes.SetStruct<DateParser, DateOnly>("min", value);
        }

        public new StepOption? Step
        {
            get => base.Step;
            set => base.Step = value;
        }

        public new DateOnly? Value
        {
            get => RawAttributes.GetStruct<DateParser, DateOnly>("value");
            set => RawAttributes.SetStruct<DateParser, DateOnly>("value", value);
        }

        internal sealed override InputType GetInputType() => InputType.Date;

        private sealed class DateParser : Parser<DateOnly>
        {
            protected override DateOnly InternalParse(string rawValue)
            {
                if (DateOnly.TryParseExact(rawValue, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
                {
                    return result;
                }

                return base.InternalParse(rawValue);
            }

            protected override string InternalToString(DateOnly value)
                => value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }
    }
}

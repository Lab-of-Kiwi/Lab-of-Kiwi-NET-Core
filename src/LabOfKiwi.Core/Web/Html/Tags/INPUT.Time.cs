using LabOfKiwi.Web.Html.Attributes;
using System;
using System.Globalization;

namespace LabOfKiwi.Web.Html.Tags;

public partial class INPUT
{
    public class Time : INPUT
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

        public new TimeOnly? Max
        {
            get => RawAttributes.GetStruct<TimeParser, TimeOnly>("max");
            set => RawAttributes.SetStruct<TimeParser, TimeOnly>("max", value);
        }

        public new TimeOnly? Min
        {
            get => RawAttributes.GetStruct<TimeParser, TimeOnly>("min");
            set => RawAttributes.SetStruct<TimeParser, TimeOnly>("min", value);
        }

        public new StepOption? Step
        {
            get => base.Step;
            set => base.Step = value;
        }

        public new TimeOnly? Value
        {
            get => RawAttributes.GetStruct<TimeParser, TimeOnly>("value");
            set => RawAttributes.SetStruct<TimeParser, TimeOnly>("value", value);
        }

        internal sealed override InputType GetInputType() => InputType.Time;

        private sealed class TimeParser : Parser<TimeOnly>
        {
            protected override TimeOnly InternalParse(string rawValue)
            {
                if (TimeOnly.TryParseExact(rawValue, "HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
                {
                    return result;
                }

                return base.InternalParse(rawValue);
            }

            protected override string InternalToString(TimeOnly value)
                => value.ToString("HH:mm:ss", CultureInfo.InvariantCulture);
        }
    }
}

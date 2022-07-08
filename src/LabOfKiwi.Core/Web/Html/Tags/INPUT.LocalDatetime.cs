using LabOfKiwi.Web.Html.Attributes;
using System;
using System.Globalization;

namespace LabOfKiwi.Web.Html.Tags;

public partial class INPUT
{
    public class LocalDatetime : INPUT
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

        public new DateTime? Max
        {
            get => RawAttributes.GetStruct<DateTimeParser, DateTime>("max");
            set => RawAttributes.SetStruct<DateTimeParser, DateTime>("max", value);
        }

        public new DateTime? Min
        {
            get => RawAttributes.GetStruct<DateTimeParser, DateTime>("min");
            set => RawAttributes.SetStruct<DateTimeParser, DateTime>("min", value);
        }

        public new StepOption? Step
        {
            get => base.Step;
            set => base.Step = value;
        }

        public new DateTime? Value
        {
            get => RawAttributes.GetStruct<DateTimeParser, DateTime>("value");
            set => RawAttributes.SetStruct<DateTimeParser, DateTime>("value", value);
        }

        internal sealed override InputType GetInputType() => InputType.LocalDatetime;

        private sealed class DateTimeParser : Parser<DateTime>
        {
            protected override DateTime InternalParse(string rawValue)
            {
                if (DateTime.TryParseExact(rawValue, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var result))
                {
                    return result;
                }

                return base.InternalParse(rawValue);
            }

            protected override string InternalToString(DateTime value)
                => value.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        }
    }
}

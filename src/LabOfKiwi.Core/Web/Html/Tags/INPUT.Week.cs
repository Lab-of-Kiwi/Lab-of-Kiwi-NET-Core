using LabOfKiwi.Web.Html.Attributes;
using System;
using System.Globalization;

namespace LabOfKiwi.Web.Html.Tags;

public partial class INPUT
{
    public class Week : INPUT
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
            get => RawAttributes.GetStruct<WeekParser, DateOnly>("max");
            set => RawAttributes.SetStruct<WeekParser, DateOnly>("max", value);
        }

        public new DateOnly? Min
        {
            get => RawAttributes.GetStruct<WeekParser, DateOnly>("min");
            set => RawAttributes.SetStruct<WeekParser, DateOnly>("min", value);
        }

        public new StepOption? Step
        {
            get => base.Step;
            set => base.Step = value;
        }

        public new DateOnly? Value
        {
            get => RawAttributes.GetStruct<WeekParser, DateOnly>("value");
            set => RawAttributes.SetStruct<WeekParser, DateOnly>("value", value);
        }

        internal sealed override InputType GetInputType() => InputType.Week;

        private sealed class WeekParser : Parser<DateOnly>
        {
            protected override DateOnly InternalParse(string rawValue)
            {
                if (rawValue.Length == 8 && rawValue[4] == '-' && rawValue[5] == 'W')
                {
                    if (int.TryParse(rawValue[0..4], out int year) && int.TryParse(rawValue[6..], out int week))
                    {
                        try
                        {
                            var dateTime = ISOWeek.ToDateTime(year, week, DayOfWeek.Monday);
                            return DateOnly.FromDateTime(dateTime);
                        }
                        catch { }
                    }
                }

                return base.InternalParse(rawValue);
            }

            protected override string InternalToString(DateOnly value)
            {
                int year = value.Year;
                int weekOfYear = ISOWeek.GetWeekOfYear(value.ToDateTime(TimeOnly.MinValue));
                return $"{year.ToString().PadLeft(4, '0')}-W{weekOfYear.ToString().PadLeft(2, '0')}";
            }
        }
    }
}

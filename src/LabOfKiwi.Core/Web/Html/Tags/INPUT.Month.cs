using LabOfKiwi.Web.Html.Attributes;
using System;
using System.Globalization;

namespace LabOfKiwi.Web.Html.Tags;

public partial class INPUT
{
    public class Month : INPUT
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
            get => RawAttributes.GetStruct<YearMonthParser, DateOnly>("max");
            set => RawAttributes.SetStruct<YearMonthParser, DateOnly>("max", value);
        }

        public new DateOnly? Min
        {
            get => RawAttributes.GetStruct<YearMonthParser, DateOnly>("min");
            set => RawAttributes.SetStruct<YearMonthParser, DateOnly>("min", value);
        }

        public new StepOption? Step
        {
            get => base.Step;
            set => base.Step = value;
        }

        public new DateOnly? Value
        {
            get => RawAttributes.GetStruct<YearMonthParser, DateOnly>("value");
            set => RawAttributes.SetStruct<YearMonthParser, DateOnly>("value", value);
        }

        internal sealed override InputType GetInputType() => InputType.Month;

        private sealed class YearMonthParser : Parser<DateOnly>
        {
            protected override DateOnly InternalParse(string rawValue)
            {
                if (DateOnly.TryParseExact(rawValue + "-01", "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly result))
                {
                    return result;
                }

                return base.InternalParse(rawValue);
            }

            protected override string InternalToString(DateOnly value)
                => value.ToString("yyyy-MM", CultureInfo.InvariantCulture);
        }
    }
}

using LabOfKiwi.Web.Html.Attributes;
using System.Drawing;

namespace LabOfKiwi.Web.Html.Tags;

public partial class INPUT
{
    public class Color : INPUT
    {
        public new string? AutoComplete
        {
            get => base.AutoComplete;
            set => base.AutoComplete = value;
        }

        public new string? List
        {
            get => base.List;
            set => base.List = value;
        }

        public new System.Drawing.Color? Value
        {
            get => RawAttributes.GetStruct<ColorParser, System.Drawing.Color>("value");
            set => RawAttributes.SetStruct<ColorParser, System.Drawing.Color>("value", value);
        }

        internal sealed override InputType GetInputType() => InputType.Color;

        private sealed class ColorParser : Parser<System.Drawing.Color>
        {
            protected override System.Drawing.Color InternalParse(string rawValue)
            {
                try
                {
                    return ColorTranslator.FromHtml(rawValue);
                }
                catch
                {
                    return base.InternalParse(rawValue);
                }
            }

            protected override string InternalToString(System.Drawing.Color value)
            {
                return "#" + value.R.ToString("x2") + value.G.ToString("x2") + value.B.ToString("x2");
            }
        }
    }
}

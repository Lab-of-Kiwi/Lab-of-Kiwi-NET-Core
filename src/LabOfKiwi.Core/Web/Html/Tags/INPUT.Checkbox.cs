namespace LabOfKiwi.Web.Html.Tags;

public partial class INPUT
{
    public class Checkbox : INPUT
    {
        public new bool IsChecked
        {
            get => base.IsChecked;
            set => base.IsChecked = value;
        }

        public new bool IsRequired
        {
            get => base.IsRequired;
            set => base.IsRequired = value;
        }

        public new string? Value
        {
            get => RawAttributes["value"];
            set => RawAttributes["value"] = value;
        }

        internal sealed override InputType GetInputType() => InputType.Checkbox;
    }
}

namespace LabOfKiwi.Web.Html.Tags;

public partial class INPUT
{
    public class Hidden : INPUT
    {
        public new string? AutoComplete
        {
            get => base.AutoComplete;
            set => base.AutoComplete = value;
        }

        public new string? Value
        {
            get => RawAttributes["value"];
            set => RawAttributes["value"] = value;
        }

        internal sealed override InputType GetInputType() => InputType.Hidden;
    }
}

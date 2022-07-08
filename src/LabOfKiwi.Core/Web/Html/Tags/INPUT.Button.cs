namespace LabOfKiwi.Web.Html.Tags;

public partial class INPUT
{
    public class Button : INPUT
    {
        public new string? Value
        {
            get => RawAttributes["value"];
            set => RawAttributes["value"] = value;
        }

        internal sealed override InputType GetInputType() => InputType.Button;
    }
}

namespace LabOfKiwi.Web.Html.Tags;

public partial class INPUT
{
    public class Password : INPUT
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

        public new long? MaxLength
        {
            get => base.MaxLength;
            set => base.MaxLength = value;
        }

        public new long? MinLength
        {
            get => base.MinLength;
            set => base.MinLength = value;
        }

        public new string? Pattern
        {
            get => base.Pattern;
            set => base.Pattern = value;
        }

        public new string? Placeholder
        {
            get => base.Placeholder;
            set => base.Placeholder = value;
        }

        public new long? Size
        {
            get => base.Size;
            set => base.Size = value;
        }

        public new string? Value
        {
            get => RawAttributes["value"];
            set => RawAttributes["value"] = value;
        }

        internal sealed override InputType GetInputType() => InputType.Password;
    }
}

namespace LabOfKiwi.Web.Html.Tags;

public partial class INPUT
{
    public class FileUpload : INPUT
    {
        public new string? Accept
        {
            get => base.Accept;
            set => base.Accept = value;
        }

        public new bool AllowMultiple
        {
            get => base.AllowMultiple;
            set => base.AllowMultiple = value;
        }

        public new bool IsRequired
        {
            get => base.IsRequired;
            set => base.IsRequired = value;
        }

        // TODO
        public new string? Value
        {
            get => RawAttributes["value"];
            set => RawAttributes["value"] = value;
        }

        internal sealed override InputType GetInputType() => InputType.FileUpload;
    }
}

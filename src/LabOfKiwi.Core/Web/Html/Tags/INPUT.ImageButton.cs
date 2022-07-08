using System;

namespace LabOfKiwi.Web.Html.Tags;

public partial class INPUT
{
    public class ImageButton : INPUT
    {
        public new bool AllowFormValidation
        {
            get => base.AllowFormValidation;
            set => base.AllowFormValidation = value;
        }

        public new string? Alt
        {
            get => base.Alt;
            set => base.Alt = value;
        }

        public new Uri? FormAction
        {
            get => base.FormAction;
            set => base.FormAction = value;
        }

        public new EncodingTypeOption? FormEncodingType
        {
            get => base.FormEncodingType;
            set => base.FormEncodingType = value;
        }

        public new FormSubmissionMethod? FormMethod
        {
            get => base.FormMethod;
            set => base.FormMethod = value;
        }

        public new BrowsingContextOption? FormTarget
        {
            get => base.FormTarget;
            set => base.FormTarget = value;
        }

        public new long? Height
        {
            get => base.Height;
            set => base.Height = value;
        }

        public new Uri? Source
        {
            get => base.Source;
            set => base.Source = value;
        }

        public new long? Width
        {
            get => base.Width;
            set => base.Width = value;
        }

        public new string? Value
        {
            get => RawAttributes["value"];
            set => RawAttributes["value"] = value;
        }

        internal sealed override InputType GetInputType() => InputType.ImageButton;
    }
}

using LabOfKiwi.Web.Html.Attributes;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace LabOfKiwi.Web.Html.Tags;

public abstract partial class INPUT : Element
{
    internal INPUT()
    {
        Type = GetInputType();
    }

    internal string? Accept
    {
        get => RawAttributes.GetObject<AcceptParser, string>("accept");
        set => RawAttributes.SetObject<AcceptParser, string>("accept", value);
    }

    internal bool AllowFormValidation
    {
        get => !RawAttributes.GetBoolean("formnovalidate");
        set => RawAttributes.SetBoolean("formnovalidate", !value);
    }

    internal bool AllowMultiple
    {
        get => RawAttributes.GetBoolean("multiple");
        set => RawAttributes.SetBoolean("multiple", value);
    }

    internal string? Alt
    {
        get => RawAttributes["alt"];
        set => RawAttributes["alt"] = value;
    }

    // TODO
    internal string? AutoComplete
    {
        get => RawAttributes["autocomplete"];
        set => RawAttributes["autocomplete"] = value;
    }

    internal string? DirName
    {
        get => RawAttributes["dirname"];
        set => RawAttributes["dirname"] = value;
    }

    public string? Form
    {
        get => RawAttributes.GetObject<TokenParser, string>("form");
        set => RawAttributes.SetObject<TokenParser, string>("form", value);
    }

    internal Uri? FormAction
    {
        get => RawAttributes.GetObject<Attributes.UriParser, Uri>("formaction");
        set => RawAttributes.SetObject<Attributes.UriParser, Uri>("formaction", value);
    }

    internal EncodingTypeOption? FormEncodingType
    {
        get => RawAttributes.GetStruct<EncodingTypeParser, EncodingTypeOption>("formenctype");
        set => RawAttributes.SetStruct<EncodingTypeParser, EncodingTypeOption>("formenctype", value);
    }

    internal FormSubmissionMethod? FormMethod
    {
        get => RawAttributes.GetStruct<FormSubmissionMethodParser, FormSubmissionMethod>("formmethod");
        set => RawAttributes.SetStruct<FormSubmissionMethodParser, FormSubmissionMethod>("formmethod", value);
    }

    internal BrowsingContextOption? FormTarget
    {
        get => RawAttributes.GetStruct<BrowsingContextParser, BrowsingContextOption>("formtarget");
        set => RawAttributes.SetStruct<BrowsingContextParser, BrowsingContextOption>("formtarget", value);
    }

    internal long? Height
    {
        get => RawAttributes.GetLong("height", min: 0L);
        set => RawAttributes.SetLong("height", value, min: 0L);
    }

    internal bool IsChecked
    {
        get => RawAttributes.GetBoolean("checked");
        set => RawAttributes.SetBoolean("checked", value);
    }

    public bool IsDisabled
    {
        get => RawAttributes.GetBoolean("disabled");
        set => RawAttributes.SetBoolean("disabled", value);
    }

    internal bool IsReadOnly
    {
        get => RawAttributes.GetBoolean("readonly");
        set => RawAttributes.SetBoolean("readonly", value);
    }

    internal bool IsRequired
    {
        get => RawAttributes.GetBoolean("required");
        set => RawAttributes.SetBoolean("required", value);
    }

    internal string? List
    {
        get => RawAttributes.GetObject<TokenParser, string>("list");
        set => RawAttributes.SetObject<TokenParser, string>("list", value);
    }

    internal string? Max => RawAttributes["max"];

    internal long? MaxLength
    {
        get => RawAttributes.GetLong("maxlength", min: 0L);
        set => RawAttributes.SetLong("maxlength", value, min: 0L);
    }

    internal string? Min => RawAttributes["min"];

    internal long? MinLength
    {
        get => RawAttributes.GetLong("minlength", min: 0L);
        set => RawAttributes.SetLong("minlength", value, min: 0L);
    }

    public string? Name
    {
        get => RawAttributes["name"];
        set => RawAttributes["name"] = value;
    }

    // TODO
    internal string? Pattern
    {
        get => RawAttributes["pattern"];
        set => RawAttributes["pattern"] = value;
    }

    internal string? Placeholder
    {
        get => RawAttributes["placeholder"];
        set => RawAttributes["placeholder"] = value;
    }

    internal long? Size
    {
        get => RawAttributes.GetLong("size", min: 1);
        set => RawAttributes.SetLong("size", value, min: 1);
    }

    internal Uri? Source
    {
        get => RawAttributes.GetObject<Attributes.UriParser, Uri>("src");
        set => RawAttributes.SetObject<Attributes.UriParser, Uri>("src", value);
    }

    internal StepOption? Step
    {
        get
        {
            string? rawValue = RawAttributes["step"];

            if (rawValue == null)
            {
                return null;
            }

            if (StepOption.TryParse(rawValue, out StepOption result))
            {
                return result;
            }

            HtmlHelper.ThrowInvalidAttributeStateException("step", $"Invalid value of '{rawValue}' is set.");
            return default;
        }

        set => RawAttributes["step"] = value?.ToString();
    }

    public sealed override string TagName => "input";

    public InputType? Type
    {
        get
        {
            string? rawValue = RawAttributes["type"];

            if (rawValue == null)
            {
                if (GetInputType() != InputType.Text)
                {
                    HtmlHelper.ThrowInvalidAttributeStateException("type", $"Invalid value of '{rawValue}' is set.");
                    return default;
                }

                return null;
            }

            InputType? inputType = rawValue switch
            {
                "hidden" => InputType.Hidden,
                "text" => InputType.Text,
                "search" => InputType.Search,
                "tel" => InputType.Telephone,
                "url" => InputType.Url,
                "email" => InputType.Email,
                "password" => InputType.Password,
                "date" => InputType.Date,
                "month" => InputType.Month,
                "week" => InputType.Week,
                "time" => InputType.Time,
                "datetime-local" => InputType.LocalDatetime,
                "number" => InputType.Number,
                "range" => InputType.Range,
                "color" => InputType.Color,
                "checkbox" => InputType.Checkbox,
                "radio" => InputType.RadioButton,
                "file" => InputType.FileUpload,
                "submit" => InputType.SubmitButton,
                "image" => InputType.ImageButton,
                "reset" => InputType.ResetButton,
                "button" => InputType.Button,
                _ => null
            };

            if (inputType.HasValue && inputType.Value == GetInputType())
            {
                return inputType;
            }

            HtmlHelper.ThrowInvalidAttributeStateException("type", $"Invalid value of '{rawValue}' is set.");
            return default;
        }

        private set
        {
            string? rawValue;

            if (value.HasValue)
            {
                rawValue = value.Value switch
                {
                    InputType.Hidden => "hidden",
                    InputType.Text => "text",
                    InputType.Search => "search",
                    InputType.Telephone => "tel",
                    InputType.Url => "url",
                    InputType.Email => "email",
                    InputType.Password => "password",
                    InputType.Date => "date",
                    InputType.Month => "month",
                    InputType.Week => "week",
                    InputType.Time => "time",
                    InputType.LocalDatetime => "datetime-local",
                    InputType.Number => "number",
                    InputType.Range => "range",
                    InputType.Color => "color",
                    InputType.Checkbox => "checkbox",
                    InputType.RadioButton => "radio",
                    InputType.FileUpload => "file",
                    InputType.SubmitButton => "submit",
                    InputType.ImageButton => "image",
                    InputType.ResetButton => "reset",
                    InputType.Button => "button",
                    _ => throw new NotImplementedException(),
                };
            }
            else
            {
                rawValue = null;
            }

            RawAttributes["type"] = rawValue;
        }
    }

    public string? Value => RawAttributes["value"];

    internal long? Width
    {
        get => RawAttributes.GetLong("width", min: 0L);
        set => RawAttributes.SetLong("width", value, min: 0L);
    }

    public enum InputType
    {
        Hidden,
        Text,
        Search,
        Telephone,
        Url,
        Email,
        Password,
        Date,
        Month,
        Week,
        Time,
        LocalDatetime,
        Number,
        Range,
        Color,
        Checkbox,
        RadioButton,
        FileUpload,
        SubmitButton,
        ImageButton,
        ResetButton,
        Button
    }

    internal abstract InputType GetInputType();

    public readonly struct StepOption : IEquatable<StepOption>
    {
        public static readonly StepOption Any = default;

        private readonly float _value;

        private StepOption(float value)
        {
            _value = value;
        }

        public float? Value => _value != default ? _value : null;

        public override bool Equals([NotNullWhen(true)] object? obj)
            => obj is StepOption other && Equals(other);

        public bool Equals(StepOption other)
            => _value == other._value;

        public override int GetHashCode()
            => _value.GetHashCode();

        public override string ToString()
        {
            if (_value == default)
            {
                return "any";
            }

            return _value.ToString();
        }

        public static StepOption Create(float value)
        {
            if (float.IsNaN(value) || float.IsInfinity(value) || value <= 0F)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            return new StepOption(value);
        }

        public static bool TryParse(string? value, out StepOption result)
        {
            if (value != null)
            {
                if (value == "any")
                {
                    result = Any;
                    return true;
                }

                if (float.TryParse(value, out float fpResult))
                {
                    if (!(float.IsNaN(fpResult) || float.IsInfinity(fpResult) || fpResult <= 0F))
                    {
                        result = new StepOption(fpResult);
                        return true;
                    }
                }
            }

            result = default;
            return false;
        }

        public static bool operator ==(StepOption left, StepOption right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(StepOption left, StepOption right)
        {
            return !(left == right);
        }
    }

    private sealed class AcceptParser : StringParser
    {
        private static readonly string[] Predefined = new string[]
        {
        "audio/*", "video/*", "image/*"
        };

        public override bool IsValid(string value)
        {
            if (Predefined.Contains(value)) return true;

            if (value.Length == 0) return false;

            if (value[0] == '/' || value[^1] == '/') return false;

            int slashCount = 0;

            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] == '/') slashCount++;
                if (value[i] == ';') return false;
                if (value[i] == '*') return false;
            }

            return slashCount == 1;
        }
    }
}

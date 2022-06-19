using System;
using System.Diagnostics;
using System.Web;

namespace LabOfKiwi.Html.Tags.Attributes;

internal abstract class Attribute<TValue> : Attribute
{
    protected Attribute(string name, AttributeCollection attributes, bool requiresEncoding) : base(name, attributes)
    {
        RequiresEncoding = requiresEncoding;
    }

    private bool RequiresEncoding { get; }

    public TValue? Value
    {
        get
        {
            string? rawValue = GetRawValue();

            if (rawValue == null)
            {
                return default;
            }

            if (RequiresEncoding)
            {
                rawValue = HttpUtility.HtmlDecode(rawValue);
            }

            return ConvertToUserValue(rawValue);
        }

        set
        {
            if (typeof(TValue).IsValueType)
            {
                if (value!.Equals(default(TValue)))
                {
                    Delete();
                    return;
                }
                
            }
            else if (value == null)
            {
                Delete();
                return;
            }

            if (!IsValid(value))
            {
                throw new ArgumentException($"Invalid value for '{Name}' attribute: {value}");
            }

            string rawValue = ConvertToRawValue(value);
            Debug.Assert(rawValue != null);

            if (RequiresEncoding)
            {
                rawValue = HttpUtility.HtmlAttributeEncode(rawValue);
            }

            SetRawValue(rawValue);
        }
    }

    protected virtual bool IsValid(TValue value)
    {
        return true;
    }

    protected abstract TValue ConvertToUserValue(string rawValue);

    protected abstract string ConvertToRawValue(TValue value);
}

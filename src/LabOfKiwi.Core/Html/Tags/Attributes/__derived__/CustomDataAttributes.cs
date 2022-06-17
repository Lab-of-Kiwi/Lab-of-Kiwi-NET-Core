using System;
using System.Web;

namespace LabOfKiwi.Html.Tags.Attributes;

public sealed class CustomDataAttributes
{
    internal CustomDataAttributes(AttributeCollection attributes)
    {
        Attributes = attributes;
    }

    private AttributeCollection Attributes { get; }

    public string? this[string name]
    {
        get
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return Attributes.Get($"data-{name}");
        }

        set
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (value == null)
            {
                Attributes.Set($"data-{name}", false);
            }
            else
            {
                Attributes.Set($"data-{name}", HttpUtility.HtmlAttributeEncode(value));
            }
            
        }
    }
}

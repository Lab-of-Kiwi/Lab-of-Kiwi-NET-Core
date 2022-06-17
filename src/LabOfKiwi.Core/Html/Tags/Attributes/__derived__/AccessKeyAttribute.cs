using System;
using System.Diagnostics;

namespace LabOfKiwi.Html.Tags.Attributes;


internal class AccessKeyAttribute : CharAttribute
{
    public AccessKeyAttribute(string name, AttributeCollection attributes) : base(name, attributes) { }

    protected override bool IsValid(char? value)
    {
        Debug.Assert(value.HasValue);
        return char.IsLetterOrDigit(value.Value);
    }
}

using LabOfKiwi.Web.Html.Attributes;
using System;
using System.Text;

namespace LabOfKiwi.Web.Html.Tags;

public class FORM : Element
{
    public Encoding? AcceptCharset
    {
        get => RawAttributes.GetObject<EncodingParser, Encoding>("accept-charset");
        set => RawAttributes.SetObject<EncodingParser, Encoding>("accept-charset", value);
    }

    public Uri? Action
    {
        get => RawAttributes.GetObject<Attributes.UriParser, Uri>("action");
        set => RawAttributes.SetObject<Attributes.UriParser, Uri>("action", value);
    }

    public bool AllowValidation
    {
        get => !RawAttributes.GetBoolean("novalidate");
        set => RawAttributes.SetBoolean("novalidate", !value);
    }

    public bool? AutoComplete
    {
        get => RawAttributes.GetNullBoolean("autocomplete", "on", "off");
        set => RawAttributes.SetNullBoolean("autocomplete", value, "on", "off");
    }

    public EncodingTypeOption? EncodingType
    {
        get => RawAttributes.GetStruct<EncodingTypeParser, EncodingTypeOption>("enctype");
        set => RawAttributes.SetStruct<EncodingTypeParser, EncodingTypeOption>("enctype", value);
    }

    public FormSubmissionMethod? Method
    {
        get => RawAttributes.GetStruct<FormSubmissionMethodParser, FormSubmissionMethod>("method");
        set => RawAttributes.SetStruct<FormSubmissionMethodParser, FormSubmissionMethod>("method", value);
    }

    public string? Name
    {
        get => RawAttributes["name"];
        set => RawAttributes["name"] = value;
    }

    public sealed override string TagName => "form";

    public BrowsingContextOption? Target
    {
        get => RawAttributes.GetStruct<BrowsingContextParser, BrowsingContextOption>("target");
        set => RawAttributes.SetStruct<BrowsingContextParser, BrowsingContextOption>("target", value);
    }
}

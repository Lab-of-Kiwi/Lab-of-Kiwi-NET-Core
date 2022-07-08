using LabOfKiwi.Web.Html.Attributes;
using System;

namespace LabOfKiwi.Web.Html.Tags;

public class BUTTON : Element
{
    public bool AllowFormValidation
    {
        get => !RawAttributes.GetBoolean("formnovalidate");
        set => RawAttributes.SetBoolean("formnovalidate", !value);
    }

    public string? Form
    {
        get => RawAttributes.GetObject<TokenParser, string>("form");
        set => RawAttributes.SetObject<TokenParser, string>("form", value);
    }

    public Uri? FormAction
    {
        get => RawAttributes.GetObject<Attributes.UriParser, Uri>("formaction");
        set => RawAttributes.SetObject<Attributes.UriParser, Uri>("formaction", value);
    }

    public EncodingTypeOption? FormEncodingType
    {
        get => RawAttributes.GetStruct<EncodingTypeParser, EncodingTypeOption>("formenctype");
        set => RawAttributes.SetStruct<EncodingTypeParser, EncodingTypeOption>("formenctype", value);
    }

    public FormSubmissionMethod? FormMethod
    {
        get => RawAttributes.GetStruct<FormSubmissionMethodParser, FormSubmissionMethod>("formmethod");
        set => RawAttributes.SetStruct<FormSubmissionMethodParser, FormSubmissionMethod>("formmethod", value);
    }

    public BrowsingContextOption? FormTarget
    {
        get => RawAttributes.GetStruct<BrowsingContextParser, BrowsingContextOption>("formtarget");
        set => RawAttributes.SetStruct<BrowsingContextParser, BrowsingContextOption>("formtarget", value);
    }

    public bool IsDisabled
    {
        get => RawAttributes.GetBoolean("disabled");
        set => RawAttributes.SetBoolean("disabled", value);
    }

    public string? Name
    {
        get => RawAttributes["name"];
        set => RawAttributes["name"] = value;
    }

    public sealed override string TagName => "button";

    public ButtonType? Type
    {
        get => RawAttributes.GetEnum<ButtonType>("type");
        set => RawAttributes.SetEnum("type", value);
    }

    public string? Value
    {
        get => RawAttributes["value"];
        set => RawAttributes["value"] = value;
    }

    public enum ButtonType
    {
        Submit,
        Reset,
        Button
    }
}

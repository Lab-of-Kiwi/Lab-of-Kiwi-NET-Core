using LabOfKiwi.Web.Html.Attributes;
using System;

namespace LabOfKiwi.Web.Html.Tags;

public class IFRAME : Element
{
    // TODO
    public string? Allow
    {
        get => RawAttributes["allow"];
        set => RawAttributes["allow"] = value;
    }

    public bool AllowFullscreen
    {
        get => RawAttributes.GetBoolean("allowfullscreen");
        set => RawAttributes.SetBoolean("allowfullscreen", value);
    }

    public long? Height
    {
        get => RawAttributes.GetLong("height", min: 0L);
        set => RawAttributes.SetLong("height", value, min: 0L);
    }

    public LoadingOption? Loading
    {
        get => RawAttributes.GetEnum<LoadingOption>("loading");
        set => RawAttributes.SetEnum("loading", value);
    }

    // TODO
    public string? Name
    {
        get => RawAttributes["name"];
        set => RawAttributes["name"] = value;
    }

    public ReferrerPolicyOption? ReferrerPolicy
    {
        get => RawAttributes.GetStruct<ReferrerPolicyParser, ReferrerPolicyOption>("referrerpolicy");
        set => RawAttributes.SetStruct<ReferrerPolicyParser, ReferrerPolicyOption>("referrerpolicy", value);
    }

    public SandboxOption Sandbox
    {
        get
        {
            string? rawValue = RawAttributes["sandbox"];

            if (rawValue == null)
            {
                return SandboxOption.None;
            }

            if (SandboxOptionUtility.TryParse(rawValue, " ", out SandboxOption result))
            {
                return result;
            }

            HtmlHelper.ThrowInvalidAttributeStateException("sandbox", $"Invalid value of '{rawValue}' is set.");
            return default;
        }

        set
        {
            string? rawValue = SandboxOptionUtility.ToHTMLString(value, " ");
            RawAttributes["sandbox"] = rawValue;
        }
    }

    public Uri? Source
    {
        get => RawAttributes.GetObject<Attributes.UriParser, Uri>("src");
        set => RawAttributes.SetObject<Attributes.UriParser, Uri>("src", value);
    }

    // TODO
    public string? SourceDocument
    {
        get => RawAttributes["srcdoc"];
        set => RawAttributes["srcdoc"] = value;
    }

    public sealed override string TagName => "iframe";

    public long? Width
    {
        get => RawAttributes.GetLong("width", min: 0L);
        set => RawAttributes.SetLong("width", value, min: 0L);
    }
}

using LabOfKiwi.Html.Tags.Attributes;
using System;
using System.Globalization;
using System.Text;

namespace LabOfKiwi.Html.Tags;

public abstract class Tag : Element
{
    internal Tag()
    {
        Attributes = new AttributeCollection();
    }

    #region HTML Attributes
    [Obsolete("Using accesskeys is difficult because they may conflict with other key standards in the browser.")]
    public char? AccessKey
    {
        get => new AccessKeyAttribute("accesskey", Attributes).Value;
        set => new AccessKeyAttribute("accesskey", Attributes).Value = value;
    }

    public string? Class
    {
        get => new TextAttribute("class", Attributes).Value;
        set => new TextAttribute("class", Attributes).Value = value;
    }

    public bool? ContentEditable
    {
        get => new BoolAttribute("contenteditable", Attributes, BoolTextType.TrueFalse).Value;
        set => new BoolAttribute("contenteditable", Attributes, BoolTextType.TrueFalse).Value = value;
    }

    public CustomDataAttributes CustomData => new(Attributes);

    public DraggableOption? Draggable
    {
        get => new DraggableAttribute("draggable", Attributes).Value;
        set => new DraggableAttribute("draggable", Attributes).Value = value;
    }

    public string? Id
    {
        get => new TextAttribute("id", Attributes).Value;
        set => new TextAttribute("id", Attributes).Value = value;
    }

    public bool IsHidden
    {
        get => Attributes.Has("hidden");
        set => Attributes.Set("hidden", false);
    }

    public CultureInfo? Language
    {
        get => new LanguageAttribute("lang", Attributes).Value;
        set => new LanguageAttribute("lang", Attributes).Value = value;
    }

    public bool? Spellcheck
    {
        get => new BoolAttribute("spellcheck", Attributes, BoolTextType.TrueFalse).Value;
        set => new BoolAttribute("spellcheck", Attributes, BoolTextType.TrueFalse).Value = value;
    }

    public string? Style
    {
        get => new TextAttribute("style", Attributes).Value;
        set => new TextAttribute("style", Attributes).Value = value;
    }

    public int? TabIndex
    {
        get => new IntAttribute("tabindex", Attributes, minValue: 1).Value;
        set => new IntAttribute("tabindex", Attributes, minValue: 1).Value = value;
    }

    public TextDirectionOption? TextDirection
    {
        get => new TextDirectionAttribute("dir", Attributes).Value;
        set => new TextDirectionAttribute("dir", Attributes).Value = value;
    }

    public string? Title
    {
        get => new TextAttribute("title", Attributes, requiresEncoding: true).Value;
        set => new TextAttribute("title", Attributes, requiresEncoding: true).Value = value;
    }

    public bool? Translate
    {
        get => new BoolAttribute("translate", Attributes, BoolTextType.YesNo).Value;
        set => new BoolAttribute("translate", Attributes, BoolTextType.YesNo).Value = value;
    }
    #endregion

    #region Internal Members
    internal AttributeCollection Attributes { get; }

    protected abstract string TagName { get; }

    internal abstract void CompleteToString(StringBuilder sb, int tabCount, HtmlFormatOptions formatOptions);

    public override string ToFormattedString(int tabCount, HtmlFormatOptions formatOptions)
    {
        if (tabCount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(tabCount));
        }

        StringBuilder sb = new();
        sb.Append(formatOptions.GetTab(tabCount));
        sb.Append('<').Append(TagName);
        Attributes.PopulateToString(sb);
        CompleteToString(sb, tabCount, formatOptions);
        sb.Append(formatOptions.GetNewLine());
        return sb.ToString();
    }
    #endregion
}

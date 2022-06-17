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
    public Attribute<char?> AccessKey => new AccessKeyAttribute("accesskey", Attributes);

    public Attribute<string> Class => new TextAttribute("class", Attributes);

    public Attribute<bool?> ContentEditable => new BoolAttribute("contenteditable", Attributes, BoolTextType.TrueFalse);

    public CustomDataAttributes CustomData => new(Attributes);

    public Attribute<DraggableOption?> Draggable => new DraggableAttribute("draggable", Attributes);

    public ToggleableAttribute Hidden => new("hidden", Attributes);

    public Attribute<string> Id => new TextAttribute("id", Attributes);

    public Attribute<CultureInfo> Language => new LanguageAttribute("lang", Attributes);

    public Attribute<bool?> Spellcheck => new BoolAttribute("spellcheck", Attributes, BoolTextType.TrueFalse);

    public Attribute<string> Style => new TextAttribute("style", Attributes);

    public Attribute<int?> TabIndex => new IntAttribute("tabindex", Attributes, minValue: 1);

    public Attribute<TextDirectionOption?> TextDirection => new TextDirectionAttribute("dir", Attributes);

    public Attribute<string> Title => new TextAttribute("title", Attributes, requiresEncoding: true);

    public Attribute<bool?> Translate => new BoolAttribute("translate", Attributes, BoolTextType.YesNo);
    #endregion

    #region Public Methods
    public sealed override string ToString()
    {
        StringBuilder sb = new();
        sb.Append('<').Append(TagName);
        Attributes.PopulateToString(sb);
        CompleteToString(sb);
        return sb.ToString();
    }
    #endregion

    #region Internal Members
    internal AttributeCollection Attributes { get; }

    protected abstract string TagName { get; }

    internal abstract void CompleteToString(StringBuilder sb);
    #endregion
}

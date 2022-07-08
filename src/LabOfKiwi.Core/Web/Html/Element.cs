using LabOfKiwi.Web.Html.Attributes;
using LabOfKiwi.Web.Html.Compliance;
using LabOfKiwi.Web.Html.Tags;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace LabOfKiwi.Web.Html;

public abstract class Element : Node
{
    private readonly List<Node> _childNodes;

    internal Element()
    {
        _childNodes = new List<Node>();
        RawAttributes = new AttributeCollection();
    }

    public sealed override Node this[int index] => _childNodes[index];

    public sealed override int ChildNodeCount => _childNodes.Count;

    public sealed override IEnumerable<Node> ChildNodes => _childNodes.AsReadOnly();

    public bool HasAttributes => RawAttributes.Count != 0;

    public abstract string TagName { get; }

    #region Global Attributes
    public IList<char> AccessKey => RawAttributes.GetList<AccesskeyParser, char>("accesskey");

    public AutocapitalizeOption? Autocapitalize
    {
        get => RawAttributes.GetEnum<AutocapitalizeOption>("autocapitalize");
        set => RawAttributes.SetEnum("autocapitalize", value);
    }

    public IList<string> Class => RawAttributes.GetList<TokenParser, string>("class");

    public bool? ContentEditable
    {
        get => RawAttributes.GetNullBoolean("contenteditable");
        set => RawAttributes.SetNullBoolean("contenteditable", value);
    }

    public IDictionary<string, string> CustomData => new CustomAttributes("data-", RawAttributes);

    public DirectionOfTextOption? DirectionOfText
    {
        get
        {
            if (this is BDO)
            {
                return RawAttributes.GetStruct<BdoDirParser, DirectionOfTextOption>("dir");
            }

            return RawAttributes.GetEnum<DirectionOfTextOption>("dir");
        }

        set
        {
            if (this is BDO)
            {
                RawAttributes.SetStruct<BdoDirParser, DirectionOfTextOption>("dir", value);
            }
            else
            {
                RawAttributes.SetEnum("dir", value);
            }
        }
    }

    public bool? Draggable
    {
        get => RawAttributes.GetNullBoolean("contenteditable");
        set => RawAttributes.SetNullBoolean("contenteditable", value);
    }

    public EnterKeyHintOption? EnterKeyHint
    {
        get => RawAttributes.GetEnum<EnterKeyHintOption>("enterkeyhint");
        set => RawAttributes.SetEnum("enterkeyhint", value);
    }

    public Events Events => new(RawAttributes);

    public string? Id
    {
        get => RawAttributes.GetObject<TokenParser, string>("id");
        set => RawAttributes.SetObject<TokenParser, string>("id", value);
    }

    public InputModeOption? InputMode
    {
        get => RawAttributes.GetEnum<InputModeOption>("inputmode");
        set => RawAttributes.SetEnum("inputmode", value);
    }

    public string? Is
    {
        get => RawAttributes.GetObject<IsParser, string>("is");
        set => RawAttributes.SetObject<IsParser, string>("is", value);
    }

    public bool IsAutofocus
    {
        get => RawAttributes.GetBoolean("autofocus");
        set => RawAttributes.SetBoolean("autofocus", value);
    }

    public bool IsHidden
    {
        get => RawAttributes.GetBoolean("hidden");
        set => RawAttributes.SetBoolean("hidden", value);
    }

    public bool IsInert
    {
        get => RawAttributes.GetBoolean("inert");
        set => RawAttributes.SetBoolean("inert", value);
    }

    public bool IsItemScope
    {
        get => RawAttributes.GetBoolean("itemscope");
        set => RawAttributes.SetBoolean("itemscope", value);
    }

    public ItemAttributes ItemAttributes => new(RawAttributes);

    public CultureInfo? Language
    {
        get => RawAttributes.GetObject<CultureInfoParser, CultureInfo>("lang");
        set => RawAttributes.SetObject<CultureInfoParser, CultureInfo>("lang", value);
    }

    public string? Nonce
    {
        get => RawAttributes["nonce"];
        set => RawAttributes["nonce"] = value;
    }

    public string? Slot
    {
        get => RawAttributes.GetObject<TokenParser, string>("slot");
        set => RawAttributes.SetObject<TokenParser, string>("slot", value);
    }

    public bool? Spellcheck
    {
        get => RawAttributes.GetNullBoolean("spellcheck");
        set => RawAttributes.SetNullBoolean("spellcheck", value);
    }

    public string? Style
    {
        get => RawAttributes["style"];
        set => RawAttributes["style"] = value;
    }

    public long? TabIndex
    {
        get => RawAttributes.GetStruct<LongParser, long>("tabindex");
        set => RawAttributes.SetStruct<LongParser, long>("tabindex", value);
    }

    public string? Title
    {
        get => RawAttributes["title"];
        set => RawAttributes["title"] = value;
    }

    public bool? Translate
    {
        get => RawAttributes.GetNullBoolean("translate", trueString: "yes", falseString: "no");
        set => RawAttributes.SetNullBoolean("translate", value, trueString: "yes", falseString: "no");
    }
    #endregion

    #region Child Insert/Removal Methods
    public void Add(Node? node)
    {
        if (node != null)
        {
            if (this == node)
            {
                throw new ArgumentException("Cannot add self as a child.");
            }

            if (AncestorNodes.Contains(node))
            {
                throw new ArgumentException("Cannot add node that is already an ancestor as a child.");
            }

            InternalAdd(node);
        }
    }

    public void Add(Action<TextBuilder>? callback)
    {
        if (callback != null)
        {
            TextBuilder tb = new();
            callback(tb);
            InternalAddRange(tb);
        }
    }

    public void Add<TNode>(Action<TNode>? callback = null) where TNode : Node, new()
        => InternalAdd(new TNode(), callback);

    public void Add<TElement>(string? text) where TElement : Element, new()
    {
        TElement element = new();
        InternalAdd(element);
        element.Add(text);
    }

    public void Add(string? text, bool isRaw = false)
        => Insert(0, text, isRaw);

    public void AddComment(string? text)
        => InternalAdd(new Comment(text));

    public void Add(IEnumerable<Node>? collection)
    {
        if (collection != null)
        {
            InternalAddRange(collection);
        }
    }

    public void Insert(int index, Node? node)
    {
        if ((uint)index > (uint)_childNodes.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        if (node != null)
        {
            if (this == node)
            {
                throw new ArgumentException("Cannot insert self as a child.");
            }

            if (AncestorNodes.Contains(node))
            {
                throw new ArgumentException("Cannot insert node that is already an ancestor as a child.");
            }

            InternalInsert(index, node);
        }
    }

    public void Insert<TNode>(int index, Action<TNode?>? callback = null) where TNode : Node, new()
    {
        if ((uint)index > (uint)_childNodes.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        InternalInsert(index, new TNode(), callback);
    }

    public void Insert(int index, string? text, bool isRaw = false)
    {
        if ((uint)index > (uint)_childNodes.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        if (!string.IsNullOrEmpty(text))
        {
            if (!isRaw)
            {
                text = HttpUtility.HtmlEncode(text);
            }

            // Empty list.
            if (_childNodes.Count == 0)
            {
                Text newTextNode = new(text) { ParentNode = this };
                _childNodes.Add(newTextNode);
            }

            // Append if node before insert location is Text.
            else if (index > 0 && _childNodes[index - 1] is Text prevText)
            {
                prevText.Append(text);
            }

            // Prepend if node after insert location is Text.
            else if (index < _childNodes.Count - 1 && _childNodes[index + 1] is Text nextText)
            {
                nextText.Prepend(text);
            }

            // Else do insert.
            else
            {
                Text newTextNode = new(text) { ParentNode = this };
                _childNodes.Insert(index, newTextNode);
            }
        }
    }

    public void InsertComment(int index, string? text)
    {
        if ((uint)index > (uint)_childNodes.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        InternalInsert(index, new Comment(text));
    }

    public void Insert(int index, IEnumerable<Node>? collection)
    {
        if ((uint)index > (uint)_childNodes.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        if (collection != null)
        {
            InternalInsertRange(index, collection);
        }
    }

    public bool Remove(Node? node)
    {
        if (node != null)
        {
            int index = _childNodes.IndexOf(node);

            if (index >= 0)
            {
                InternalRemoveAt(index);
                return true;
            }
        }

        return false;
    }

    public void RemoveAt(int index)
    {
        if ((uint)index >= (uint)_childNodes.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        InternalRemoveAt(index);
    }

    private void InternalAdd(Node node)
    {
        if (node is Document)
        {
            throw new ArgumentException("Cannot add document node as a child.");
        }

        node.ParentNode = this;
        _childNodes.Add(node);
    }

    private void InternalAdd<TNode>(TNode node, Action<TNode>? callback) where TNode : Node
    {
        InternalAdd(node);

        if (callback != null)
        {
            try
            {
                callback(node);
            }
            catch (Exception)
            {
                _childNodes.Remove(node);
                node.ParentNode = null;
                throw;
            }
        }
    }

    private void InternalAddRange(IEnumerable<Node> collection)
    {
        try
        {
            foreach (var node in collection)
            {
                if (node != null)
                {
                    InternalAdd(node);
                }
            }
        }
        catch (Exception)
        {
            foreach (var node in collection)
            {
                if (node != null && node.ParentNode == this)
                {
                    _childNodes.Remove(node);
                    node.ParentNode = null;
                }
            }

            throw;
        }
    }

    private void InternalInsert(int index, Node node)
    {
        if (node is Document)
        {
            throw new ArgumentException("Cannot insert document node as a child.");
        }

        node.ParentNode = this;
        _childNodes.Insert(index, node);
    }

    private void InternalInsert<TNode>(int index, TNode node, Action<TNode>? callback) where TNode : Node
    {
        InternalInsert(index, node);

        if (callback != null)
        {
            try
            {
                callback(node);
            }
            catch (Exception)
            {
                _childNodes.Remove(node);
                node.ParentNode = null;
                throw;
            }
        }
    }

    private void InternalInsertRange(int index, IEnumerable<Node> collection)
    {
        try
        {
            foreach (var node in collection)
            {
                if (node != null)
                {
                    InternalInsert(index++, node);
                }
            }
        }
        catch (Exception)
        {
            foreach (var node in collection)
            {
                if (node != null && node.ParentNode == this)
                {
                    _childNodes.Remove(node);
                    node.ParentNode = null;
                }
            }

            throw;
        }
    }

    private void InternalRemoveAt(int index)
    {
        var child = _childNodes[index];
        _childNodes.RemoveAt(index);
        child.ParentNode = null;
    }
    #endregion

    public sealed override string ToString()
    {
        StringBuilder sb = new();

        sb.Append($"<{TagName}");

        foreach (var entry in RawAttributes)
        {
            sb.Append(' ').Append(entry.Key);

            if (entry.Value.Length != 0)
            {
                string encoded = HttpUtility.HtmlAttributeEncode(entry.Value);
                sb.Append($"=\"{encoded}\"");
            }
        }

        bool childFound = false;

        foreach (var child in ChildNodes)
        {
            if (!childFound)
            {
                sb.Append('>');
                childFound = true;
            }

            sb.Append(child.ToString());
        }

        if (childFound)
        {
            sb.Append($"</{TagName}>");
        }
        else if (TagRegistry.IsVoidTag(this))
        {
            sb.Append(" />");
        }
        else
        {
            sb.Append($"></{TagName}>");
        }

        return sb.ToString();
    }

    protected AttributeCollection RawAttributes { get; }

    /*private Element? FirstNonTransparentAncestor
    {
        get
        {
            if (ParentElement == null)
            {
                return this;
            }

            var reg = TagRegistry.GetRegistrationRecord(ParentElement);

            if (reg.Child.ContainsBits(ContentCategory.Transparent))
            {
                return ParentElement.FirstNonTransparentAncestor;
            }

            return ParentElement;
        }
    }*/

    internal void Report(ElementComplianceReport report)
    {
        ParentReport(report);
        ChildrenReport(report);
        CustomReport(report);
    }

    private void ParentReport(ElementComplianceReport report)
    {
        var reg = TagRegistry.GetRegistrationRecord(this);

        var parentElement = ParentElement;

        if (parentElement == null)
        {
            if (reg.Parent == ContentCategory.None)
            {
                report.AddInfo("Having no parent is in compliance.");
            }
            else
            {
                report.AddWarning("Element should have a parent.");
            }
        }
        else
        {
            if (reg.Parent == ContentCategory.None)
            {
                report.AddError("Element should not have a parent element.");
            }
            else
            {
                var parentReg = TagRegistry.GetRegistrationRecord(parentElement);

                if ((parentReg.Child & reg.Tag) != ContentCategory.None && (parentReg.Tag & reg.Parent) != ContentCategory.None)
                {
                    report.AddInfo($"Parent element, '{parentElement.TagName}', is in compliance.");
                }
                else
                {
                    if (parentReg.Child.ContainsBits(ContentCategory.Transparent))
                    {
                        // TODO
                        report.AddWarning("Transparent elements not yet implemented.");
                    }
                    else
                    {
                        report.AddError($"Parent/child relationship with parent '{parentElement.TagName}' is not in compliance.");
                    }
                }
            }
        }
    }

    private void ChildrenReport(ElementComplianceReport report)
    {
        var reg = TagRegistry.GetRegistrationRecord(this);

        if (reg.Child == ContentCategory.None)
        {
            if (ChildElementCount == 0)
            {
                report.AddInfo("Having no children is in compliance.");
            }
            else
            {
                report.AddError("Element is a void element and should not contain children.");
            }
        }
        else
        {
            bool hasText = false;
            bool hasPalpable = false;

            foreach (var node in ChildNodes)
            {
                if (node is Text)
                {
                    hasText = true;
                    hasPalpable = true;
                }
                else if (node is Element child)
                {
                    var childReg = TagRegistry.GetRegistrationRecord(child);

                    if (childReg.Tag.ContainsBits(ContentCategory.Palpable) && !child.IsHidden)
                    {
                        hasPalpable = true;
                    }

                    if ((reg.Child & childReg.Tag) != ContentCategory.None && (reg.Tag & childReg.Parent) != ContentCategory.None)
                    {
                        report.AddInfo($"Child element, '{child.TagName}', is in compliance.");
                    }
                    else
                    {
                        if (reg.Child.ContainsBits(ContentCategory.Transparent))
                        {
                            // TODO
                            report.AddWarning("Transparent elements not yet implemented.");
                        }
                        else
                        {
                            report.AddError($"Parent/child relationship with child '{child.TagName}' is not in compliance.");
                        }
                    }
                }
            }

            if (hasText && !reg.Child.ContainsBits(ContentCategory.Text))
            {
                report.AddError("Element should not contain text.");
            }

            if (!hasPalpable && (reg.Child.ContainsBits(ContentCategory.Flow) || reg.Child.ContainsBits(ContentCategory.Phrasing)))
            {
                report.AddWarning("Element should contain at least one non-hidden, palpable element or text node; ignore if placeholder for scripts.");
            }
        }
    }

    protected virtual void CustomReport(ElementComplianceReport report)
    {
    }

    private sealed class AccesskeyParser : CharParser
    {
        public override bool IsValid(char value)
            => !HtmlHelper.IsASCIIWhitespace(value);
    }

    private sealed class BdoDirParser : EnumParser<DirectionOfTextOption>
    {
        public override bool IsValid(DirectionOfTextOption value)
            => value != DirectionOfTextOption.Auto;
    }

    private sealed class IsParser : StringParser
    {
        public override bool IsValid(string value)
            => HtmlHelper.IsValidPotentialCustomElementName(value);
    }
}

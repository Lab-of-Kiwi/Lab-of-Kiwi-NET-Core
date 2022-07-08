using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace LabOfKiwi.Web.Html.Tags;

internal static class TagRegistry
{
    private static readonly Record CustomElementRecord = new(ContentCategory.Phrasing | ContentCategory.Palpable, ContentCategory.Phrasing, ContentCategory.Transparent);

    private static readonly Dictionary<string, Record> _records = new();

    private static readonly HashSet<string> _voidTags = new();

    private static readonly HashSet<string> _registedCustoms = new();

    private static readonly object _lock = new();

    public static void RegisterCustom(string tagName)
    {
        if (!_registedCustoms.Contains(tagName))
        {
            lock (_lock)
            {
                if (!_registedCustoms.Contains(tagName))
                {
                    if (IsStandardTag(tagName))
                    {
                        throw new ArgumentException("Tagname is a standard HTML tag.", nameof(tagName));
                    }

                    if (!HtmlHelper.IsValidPotentialCustomElementName(tagName, false))
                    {
                        throw new ArgumentException("Invalid custom element name.", nameof(tagName));
                    }

                    _registedCustoms.Add(tagName);
                }
            }
        }

    }

    public static Record GetRegistrationRecord(Element element)
    {
        string tagName = element?.TagName!;
        Debug.Assert(tagName != null);

        if (_records.TryGetValue(tagName, out Record? record))
        {
            return record;
        }

        return CustomElementRecord;
    }

    public static bool IsStandardTag(string tagName)
    {
        return _records.ContainsKey(tagName);
    }

    public static bool IsVoidTag(Element element)
    {
        string tagName = element?.TagName!;
        Debug.Assert(tagName != null);
        return _voidTags.Contains(tagName);
    }

    private static void Register<TElement>(ContentCategory tag, ContentCategory parent, ContentCategory child)
        where TElement : Element, new()
    {
        TElement element = new();
        Register(element.TagName, tag, parent, child);
    }

    private static void Register(string tagName, ContentCategory tag, ContentCategory parent, ContentCategory child)
    {
        if (child == ContentCategory.None)
        {
            bool voidAddResult = _voidTags.Add(tagName);
            Debug.Assert(voidAddResult);
        }

        ContentCategory tagCategory;

        if (tagName.Length == 2 && tagName[0] == 'h' && '1' <= tagName[1] && tagName[1] <= '6')
        {
            tagCategory = ContentCategory.TagH;
        }
        else
        {
            string contentCategoryFieldName = $"tag{tagName}";

            FieldInfo? field = typeof(ContentCategory).GetFields(BindingFlags.Static | BindingFlags.Public)
                .Where(f => f.Name.Equals(contentCategoryFieldName, StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault();

            Debug.Assert(field != null);
            var fieldValue = field.GetValue(null);
            Debug.Assert(fieldValue is ContentCategory);
            tagCategory = (ContentCategory)fieldValue;
        }

        tag |= tagCategory;

        CleanOutwards(ref tag);
        CleanInwards(ref parent);
        CleanInwards(ref child);

        bool recordAddResult = _records.TryAdd(tagName, new(tag, parent, child));
        Debug.Assert(recordAddResult);
    }

    private static void CleanOutwards(ref ContentCategory c)
    {
        if (c.ContainsBits(ContentCategory.Embedded))
        {
            c |= (ContentCategory.Phrasing | ContentCategory.Flow);
        }
        else if (c.ContainsBits(ContentCategory.Phrasing) || c.ContainsBits(ContentCategory.Interactive) || c.ContainsBits(ContentCategory.Heading) || c.ContainsBits(ContentCategory.Sectioning))
        {
            c |= ContentCategory.Flow;
        }
    }

    private static void CleanInwards(ref ContentCategory c)
    {
        if (c.ContainsBits(ContentCategory.Flow))
        {
            c |= (ContentCategory.Phrasing | ContentCategory.Embedded | ContentCategory.Interactive | ContentCategory.Heading | ContentCategory.Sectioning | ContentCategory.Text);
        }
        else if (c.ContainsBits(ContentCategory.Phrasing))
        {
            c |= (ContentCategory.Embedded | ContentCategory.Text);
        }
    }

    internal record Record(ContentCategory Tag, ContentCategory Parent, ContentCategory Child);

    static TagRegistry()
    {
        Register<A>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Interactive | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Transparent
        );

        Register<ABBR>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Phrasing
        );

        Register<ADDRESS>
        (
            tag: ContentCategory.Flow | ContentCategory.Palpable,
            parent: ContentCategory.Flow,
            child: ContentCategory.Flow
        );

        Register<AREA>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.None
        );

        Register<ARTICLE>
        (
            tag: ContentCategory.Flow | ContentCategory.Sectioning | ContentCategory.Palpable,
            parent: ContentCategory.Flow,
            child: ContentCategory.Flow
        );

        Register<ASIDE>
        (
            tag: ContentCategory.Flow | ContentCategory.Sectioning | ContentCategory.Palpable,
            parent: ContentCategory.Flow,
            child: ContentCategory.Flow
        );

        Register<AUDIO>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Embedded | ContentCategory.Interactive,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.TagSource | ContentCategory.TagTrack | ContentCategory.Transparent
        );

        Register<B>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Phrasing
        );

        Register<BASE>
        (
            tag: ContentCategory.Metadata,
            parent: ContentCategory.TagHead,
            child: ContentCategory.None
        );

        Register<BDI>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Phrasing
        );

        Register<BDO>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Phrasing
        );

        Register<BLOCKQUOTE>
        (
            tag: ContentCategory.Flow | ContentCategory.Palpable | ContentCategory.Sectioning, // Sectioning Root
            parent: ContentCategory.Flow,
            child: ContentCategory.Flow
        );

        Register<BODY>
        (
            tag: ContentCategory.Sectioning, // Sectioning Root
            parent: ContentCategory.TagHtml,
            child: ContentCategory.Flow
        );

        Register<BR>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.None
        );

        Register<BUTTON>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Interactive | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Phrasing
        );

        Register<CANVAS>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Embedded | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Transparent
        );

        Register<CAPTION>
        (
            tag: ContentCategory.None,
            parent: ContentCategory.TagTable,
            child: ContentCategory.Flow
        );

        Register<CITE>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Phrasing
        );

        Register<CODE>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Phrasing
        );

        Register<COL>
        (
            tag: ContentCategory.None,
            parent: ContentCategory.TagColgroup,
            child: ContentCategory.None
        );

        Register<COLGROUP>
        (
            tag: ContentCategory.None,
            parent: ContentCategory.TagTable,
            child: ContentCategory.TagCol | ContentCategory.TagTemplate
        );

        Register<DATA>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Phrasing
        );

        Register<DATALIST>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Phrasing | ContentCategory.TagOption | ContentCategory.ScriptSupporting
        );

        Register<DD>
        (
            tag: ContentCategory.None,
            parent: ContentCategory.TagDl | ContentCategory.TagDiv,
            child: ContentCategory.Flow
        );

        Register<DEL>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Transparent
        );

        Register<DETAILS>
        (
            tag: ContentCategory.Flow | ContentCategory.Interactive | ContentCategory.Palpable | ContentCategory.Sectioning, // Sectioning Root
            parent: ContentCategory.Flow,
            child: ContentCategory.TagSummary | ContentCategory.Flow
        );

        Register<DFN>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Phrasing
        );

        Register<DIALOG>
        (
            tag: ContentCategory.Flow | ContentCategory.Sectioning, // Sectioning Root
            parent: ContentCategory.Flow,
            child: ContentCategory.Flow
        );

        Register<DIV>
        (
            tag: ContentCategory.Flow | ContentCategory.Palpable,
            parent: ContentCategory.Flow | ContentCategory.TagDl,
            child: ContentCategory.Flow
        );

        Register<DL>
        (
            tag: ContentCategory.Flow,
            parent: ContentCategory.Flow,
            child: ContentCategory.TagDt | ContentCategory.TagDd | ContentCategory.TagDiv | ContentCategory.ScriptSupporting
        );

        Register<DT>
        (
            tag: ContentCategory.None,
            parent: ContentCategory.TagDl | ContentCategory.TagDiv,
            child: ContentCategory.Flow
        );

        Register<EM>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Phrasing
        );

        Register<EMBED>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Embedded | ContentCategory.Interactive | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.None
        );

        Register<FIELDSET>
        (
            tag: ContentCategory.Flow | ContentCategory.Palpable | ContentCategory.Sectioning, // Sectioning Root
            parent: ContentCategory.Flow,
            child: ContentCategory.TagLegend | ContentCategory.Flow
        );

        Register<FIGCAPTION>
        (
            tag: ContentCategory.None,
            parent: ContentCategory.TagFigure,
            child: ContentCategory.Flow
        );

        Register<FIGURE>
        (
            tag: ContentCategory.Flow | ContentCategory.Palpable | ContentCategory.Sectioning, // Sectioning Root
            parent: ContentCategory.Flow,
            child: ContentCategory.TagFigcaption | ContentCategory.Flow
        );

        Register<FOOTER>
        (
            tag: ContentCategory.Flow | ContentCategory.Palpable,
            parent: ContentCategory.Flow,
            child: ContentCategory.Flow
        );

        Register<FORM>
        (
            tag: ContentCategory.Flow | ContentCategory.Palpable,
            parent: ContentCategory.Flow,
            child: ContentCategory.Flow
        );

        Register<H1>
        (
            tag: ContentCategory.Flow | ContentCategory.Palpable | ContentCategory.Heading,
            parent: ContentCategory.TagLegend | ContentCategory.TagSummary | ContentCategory.Flow,
            child: ContentCategory.Phrasing
        );

        Register<H2>
        (
            tag: ContentCategory.Flow | ContentCategory.Palpable | ContentCategory.Heading,
            parent: ContentCategory.TagLegend | ContentCategory.TagSummary | ContentCategory.Flow,
            child: ContentCategory.Phrasing
        );

        Register<H3>
        (
            tag: ContentCategory.Flow | ContentCategory.Palpable | ContentCategory.Heading,
            parent: ContentCategory.TagLegend | ContentCategory.TagSummary | ContentCategory.Flow,
            child: ContentCategory.Phrasing
        );

        Register<H4>
        (
            tag: ContentCategory.Flow | ContentCategory.Palpable | ContentCategory.Heading,
            parent: ContentCategory.TagLegend | ContentCategory.TagSummary | ContentCategory.Flow,
            child: ContentCategory.Phrasing
        );

        Register<H5>
        (
            tag: ContentCategory.Flow | ContentCategory.Palpable | ContentCategory.Heading,
            parent: ContentCategory.TagLegend | ContentCategory.TagSummary | ContentCategory.Flow,
            child: ContentCategory.Phrasing
        );

        Register<H6>
        (
            tag: ContentCategory.Flow | ContentCategory.Palpable | ContentCategory.Heading,
            parent: ContentCategory.TagLegend | ContentCategory.TagSummary | ContentCategory.Flow,
            child: ContentCategory.Phrasing
        );

        Register<HEAD>
        (
            tag: ContentCategory.None,
            parent: ContentCategory.TagHtml,
            child: ContentCategory.Metadata
        );

        Register<HEADER>
        (
            tag: ContentCategory.Flow | ContentCategory.Palpable,
            parent: ContentCategory.Flow,
            child: ContentCategory.Flow
        );

        Register<HGROUP>
        (
            tag: ContentCategory.Flow | ContentCategory.Heading | ContentCategory.Palpable,
            parent: ContentCategory.TagLegend | ContentCategory.TagSummary | ContentCategory.Flow,
            child: ContentCategory.TagH | ContentCategory.ScriptSupporting
        );

        Register<HR>
        (
            tag: ContentCategory.Flow,
            parent: ContentCategory.Flow,
            child: ContentCategory.None
        );

        Register<HTML>
        (
            tag: ContentCategory.None,
            parent: ContentCategory.None,
            child: ContentCategory.TagHead | ContentCategory.TagBody
        );

        Register<I>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Phrasing
        );

        Register<IFRAME>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Embedded | ContentCategory.Interactive | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.None
        );

        Register<IMG>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Embedded | ContentCategory.Interactive | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing | ContentCategory.TagPicture,
            child: ContentCategory.None
        );

        Register
        (
            "input",
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Interactive,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.None
        );

        Register<INS>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Transparent
        );

        Register<KBD>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Phrasing
        );

        Register<LABEL>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Interactive | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Phrasing
        );

        Register<LEGEND>
        (
            tag: ContentCategory.None,
            parent: ContentCategory.TagFieldset,
            child: ContentCategory.Phrasing | ContentCategory.Heading
        );

        Register<LI>
        (
            tag: ContentCategory.None,
            parent: ContentCategory.TagOl | ContentCategory.TagUl | ContentCategory.TagMenu,
            child: ContentCategory.Flow
        );

        Register<LINK>
        (
            tag: ContentCategory.Metadata | ContentCategory.Flow | ContentCategory.Phrasing,
            parent: ContentCategory.TagHead | ContentCategory.TagNoscript | ContentCategory.Phrasing,
            child: ContentCategory.None
        );

        Register<MAIN>
        (
            tag: ContentCategory.Flow | ContentCategory.Palpable,
            parent: ContentCategory.Flow,
            child: ContentCategory.Flow
        );

        Register<MAP>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Transparent | ContentCategory.TagArea
        );

        Register<MARK>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Phrasing
        );

        Register<MENU>
        (
            tag: ContentCategory.Flow,
            parent: ContentCategory.Flow,
            child: ContentCategory.TagLi | ContentCategory.ScriptSupporting
        );

        Register<META>
        (
            tag: ContentCategory.Metadata | ContentCategory.Flow | ContentCategory.Phrasing,
            parent: ContentCategory.TagHead | ContentCategory.TagNoscript | ContentCategory.Phrasing,
            child: ContentCategory.None
        );

        Register<METER>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Phrasing
        );

        Register<NAV>
        (
            tag: ContentCategory.Flow | ContentCategory.Sectioning | ContentCategory.Palpable,
            parent: ContentCategory.Flow,
            child: ContentCategory.Flow
        );

        Register<NOSCRIPT>
        (
            tag: ContentCategory.Metadata | ContentCategory.Flow | ContentCategory.Phrasing,
            parent: ContentCategory.TagHead | ContentCategory.Phrasing,
            child: ContentCategory.Text
        );

        Register<OBJECT>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Embedded | ContentCategory.Interactive | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Transparent
        );

        Register<OL>
        (
            tag: ContentCategory.Flow,
            parent: ContentCategory.Flow,
            child: ContentCategory.TagLi | ContentCategory.ScriptSupporting
        );

        Register<OPTGROUP>
        (
            tag: ContentCategory.None,
            parent: ContentCategory.TagSelect,
            child: ContentCategory.TagOption | ContentCategory.ScriptSupporting
        );

        Register<OPTION>
        (
            tag: ContentCategory.None,
            parent: ContentCategory.TagSelect | ContentCategory.TagDatalist | ContentCategory.TagOptgroup,
            child: ContentCategory.Text
        );

        Register<OUTPUT>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Phrasing
        );

        Register<P>
        (
            tag: ContentCategory.Flow | ContentCategory.Palpable,
            parent: ContentCategory.Flow,
            child: ContentCategory.Phrasing
        );

        Register<PICTURE>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Embedded,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.TagSource | ContentCategory.TagImg | ContentCategory.ScriptSupporting
        );

        Register<PRE>
        (
            tag: ContentCategory.Flow | ContentCategory.Palpable,
            parent: ContentCategory.Flow,
            child: ContentCategory.Phrasing
        );

        Register<PROGRESS>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Phrasing
        );

        Register<Q>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Phrasing
        );

        Register<RP>
        (
            tag: ContentCategory.None,
            parent: ContentCategory.TagRuby,
            child: ContentCategory.Text
        );

        Register<RT>
        (
            tag: ContentCategory.None,
            parent: ContentCategory.TagRuby,
            child: ContentCategory.Phrasing
        );

        Register<RUBY>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Phrasing | ContentCategory.TagRt | ContentCategory.TagRp
        );

        Register<S>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Phrasing
        );

        Register<SAMP>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Phrasing
        );

        Register<SCRIPT>
        (
            tag: ContentCategory.Metadata | ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.ScriptSupporting,
            parent: ContentCategory.TagHead | ContentCategory.Phrasing | ContentCategory.ScriptSupporting,
            child: ContentCategory.Text
        );

        Register<SECTION>
        (
            tag: ContentCategory.Flow | ContentCategory.Sectioning | ContentCategory.Palpable,
            parent: ContentCategory.Flow,
            child: ContentCategory.Flow
        );

        Register<SELECT>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Interactive | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.TagOption | ContentCategory.TagOptgroup | ContentCategory.ScriptSupporting
        );

        Register<SLOT>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Transparent
        );

        Register<SMALL>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Phrasing
        );

        Register<SOURCE>
        (
            tag: ContentCategory.None,
            parent: ContentCategory.TagPicture | ContentCategory.TagVideo | ContentCategory.TagAudio,
            child: ContentCategory.None
        );

        Register<SPAN>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Phrasing
        );

        Register<STRONG>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Phrasing
        );

        Register<STYLE>
        (
            tag: ContentCategory.Metadata,
            parent: ContentCategory.TagHead | ContentCategory.TagNoscript,
            child: ContentCategory.Text
        );

        Register<SUB>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Phrasing
        );

        Register<SUMMARY>
        (
            tag: ContentCategory.None,
            parent: ContentCategory.TagDetails,
            child: ContentCategory.Phrasing | ContentCategory.Heading
        );

        Register<SUP>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Phrasing
        );

        Register<TABLE>
        (
            tag: ContentCategory.Flow | ContentCategory.Palpable,
            parent: ContentCategory.Flow,
            child: ContentCategory.TagCaption | ContentCategory.TagColgroup | ContentCategory.TagThead | ContentCategory.TagTbody | ContentCategory.TagTfoot | ContentCategory.TagTr | ContentCategory.ScriptSupporting
        );

        Register<TBODY>
        (
            tag: ContentCategory.None,
            parent: ContentCategory.TagTable,
            child: ContentCategory.TagTr | ContentCategory.ScriptSupporting
        );

        Register<TD>
        (
            tag: ContentCategory.Sectioning, // Sectioning Root
            parent: ContentCategory.TagTr,
            child: ContentCategory.Flow
        );

        Register<TEMPLATE>
        (
            tag: ContentCategory.Metadata | ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.ScriptSupporting,
            parent: ContentCategory.Metadata | ContentCategory.Phrasing | ContentCategory.ScriptSupporting | ContentCategory.TagColgroup,
            child: ContentCategory.None
        );

        Register<TEXTAREA>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Interactive | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Text
        );

        Register<TFOOT>
        (
            tag: ContentCategory.None,
            parent: ContentCategory.TagTable,
            child: ContentCategory.TagTr | ContentCategory.ScriptSupporting
        );

        Register<TH>
        (
            tag: ContentCategory.Interactive,
            parent: ContentCategory.TagTr,
            child: ContentCategory.Flow
        );

        Register<THEAD>
        (
            tag: ContentCategory.None,
            parent: ContentCategory.TagTable,
            child: ContentCategory.TagTr | ContentCategory.ScriptSupporting
        );

        Register<TIME>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Phrasing
        );

        Register<TITLE>
        (
            tag: ContentCategory.Metadata,
            parent: ContentCategory.TagHead,
            child: ContentCategory.Text
        );

        Register<TR>
        (
            tag: ContentCategory.None,
            parent: ContentCategory.TagTable | ContentCategory.TagThead | ContentCategory.TagTbody | ContentCategory.TagTfoot,
            child: ContentCategory.TagTh | ContentCategory.TagTd | ContentCategory.ScriptSupporting
        );

        Register<TRACK>
        (
            tag: ContentCategory.None,
            parent: ContentCategory.TagAudio | ContentCategory.TagVideo,
            child: ContentCategory.None
        );

        Register<U>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Phrasing
        );

        Register<UL>
        (
            tag: ContentCategory.Flow,
            parent: ContentCategory.Flow,
            child: ContentCategory.TagLi | ContentCategory.ScriptSupporting
        );

        Register<VAR>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.Phrasing
        );

        Register<VIDEO>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing | ContentCategory.Embedded | ContentCategory.Interactive | ContentCategory.Palpable,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.TagSource | ContentCategory.TagTrack | ContentCategory.Transparent
        );

        Register<WBR>
        (
            tag: ContentCategory.Flow | ContentCategory.Phrasing,
            parent: ContentCategory.Phrasing,
            child: ContentCategory.None
        );
    }
}

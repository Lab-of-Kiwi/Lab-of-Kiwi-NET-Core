using System;
using System.Diagnostics.CodeAnalysis;

namespace LabOfKiwi.Web.Html.Tags;

internal readonly struct ContentCategory : IEquatable<ContentCategory>
{
    private readonly ulong _hi;
    private readonly ulong _lo;

    private ContentCategory(ulong hi, ulong lo)
    {
        _hi = hi;
        _lo = lo;
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is ContentCategory other && Equals(other);

    public bool Equals(ContentCategory other)
        => _hi == other._hi && _lo == other._lo;

    public override int GetHashCode()
        => HashCode.Combine(_hi, _lo);

    public static bool operator ==(ContentCategory left, ContentCategory right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ContentCategory left, ContentCategory right)
    {
        return !(left == right);
    }

    public static ContentCategory operator |(ContentCategory left, ContentCategory right)
    {
        ulong hi = left._hi | right._hi;
        ulong lo = left._lo | right._lo;
        return new(hi, lo);
    }

    public static ContentCategory operator &(ContentCategory left, ContentCategory right)
    {
        ulong hi = left._hi & right._hi;
        ulong lo = left._lo & right._lo;
        return new(hi, lo);
    }

    public bool ContainsBits(ContentCategory other)
        => (this & other) != None;

    public static readonly ContentCategory None             = new(0UL, 0UL);
    public static readonly ContentCategory Flow             = new(0UL, 1UL <<  0);
    public static readonly ContentCategory Phrasing         = new(0UL, 1UL <<  1);
    public static readonly ContentCategory Embedded         = new(0UL, 1UL <<  2);
    public static readonly ContentCategory Interactive      = new(0UL, 1UL <<  3);
    public static readonly ContentCategory Heading          = new(0UL, 1UL <<  4);
    public static readonly ContentCategory Sectioning       = new(0UL, 1UL <<  5);
    public static readonly ContentCategory Metadata         = new(0UL, 1UL <<  6);
    public static readonly ContentCategory ScriptSupporting = new(0UL, 1UL <<  7);
    public static readonly ContentCategory Transparent      = new(0UL, 1UL <<  8);
    public static readonly ContentCategory Text             = new(0UL, 1UL <<  9);
    public static readonly ContentCategory Palpable         = new(0UL, 1UL << 10);

    public static readonly ContentCategory TagA             = new(0UL, 1UL << 22);
    public static readonly ContentCategory TagAbbr          = new(0UL, 1UL << 23);
    public static readonly ContentCategory TagAddress       = new(0UL, 1UL << 24);
    public static readonly ContentCategory TagArea          = new(0UL, 1UL << 25);
    public static readonly ContentCategory TagArticle       = new(0UL, 1UL << 26);
    public static readonly ContentCategory TagAside         = new(0UL, 1UL << 27);
    public static readonly ContentCategory TagAudio         = new(0UL, 1UL << 28);
    public static readonly ContentCategory TagB             = new(0UL, 1UL << 29);
    public static readonly ContentCategory TagBase          = new(0UL, 1UL << 30);
    public static readonly ContentCategory TagBdi           = new(0UL, 1UL << 31);
    public static readonly ContentCategory TagBdo           = new(0UL, 1UL << 32);
    public static readonly ContentCategory TagBlockquote    = new(0UL, 1UL << 33);
    public static readonly ContentCategory TagBody          = new(0UL, 1UL << 34);
    public static readonly ContentCategory TagBr            = new(0UL, 1UL << 35);
    public static readonly ContentCategory TagButton        = new(0UL, 1UL << 36);
    public static readonly ContentCategory TagCanvas        = new(0UL, 1UL << 37);
    public static readonly ContentCategory TagCaption       = new(0UL, 1UL << 38);
    public static readonly ContentCategory TagCite          = new(0UL, 1UL << 39);
    public static readonly ContentCategory TagCode          = new(0UL, 1UL << 40);
    public static readonly ContentCategory TagCol           = new(0UL, 1UL << 41);
    public static readonly ContentCategory TagColgroup      = new(0UL, 1UL << 42);
    public static readonly ContentCategory TagData          = new(0UL, 1UL << 43);
    public static readonly ContentCategory TagDatalist      = new(0UL, 1UL << 44);
    public static readonly ContentCategory TagDd            = new(0UL, 1UL << 45);
    public static readonly ContentCategory TagDel           = new(0UL, 1UL << 46);
    public static readonly ContentCategory TagDetails       = new(0UL, 1UL << 47);
    public static readonly ContentCategory TagDfn           = new(0UL, 1UL << 48);
    public static readonly ContentCategory TagDialog        = new(0UL, 1UL << 49);
    public static readonly ContentCategory TagDiv           = new(0UL, 1UL << 50);
    public static readonly ContentCategory TagDl            = new(0UL, 1UL << 51);
    public static readonly ContentCategory TagDt            = new(0UL, 1UL << 52);
    public static readonly ContentCategory TagEm            = new(0UL, 1UL << 53);
    public static readonly ContentCategory TagEmbed         = new(0UL, 1UL << 54);
    public static readonly ContentCategory TagFieldset      = new(0UL, 1UL << 55);
    public static readonly ContentCategory TagFigcaption    = new(0UL, 1UL << 56);
    public static readonly ContentCategory TagFigure        = new(0UL, 1UL << 57);
    public static readonly ContentCategory TagFooter        = new(0UL, 1UL << 58);
    public static readonly ContentCategory TagForm          = new(0UL, 1UL << 59);
    public static readonly ContentCategory TagH             = new(0UL, 1UL << 60);
    public static readonly ContentCategory TagHead          = new(0UL, 1UL << 61);
    public static readonly ContentCategory TagHeader        = new(0UL, 1UL << 62);
    public static readonly ContentCategory TagHgroup        = new(0UL, 1UL << 63);
    public static readonly ContentCategory TagHr            = new(1UL <<  0, 0UL);
    public static readonly ContentCategory TagHtml          = new(1UL <<  1, 0UL);
    public static readonly ContentCategory TagI             = new(1UL <<  2, 0UL);
    public static readonly ContentCategory TagIframe        = new(1UL <<  3, 0UL);
    public static readonly ContentCategory TagImg           = new(1UL <<  4, 0UL);
    public static readonly ContentCategory TagInput         = new(1UL <<  5, 0UL);
    public static readonly ContentCategory TagIns           = new(1UL <<  6, 0UL);
    public static readonly ContentCategory TagKbd           = new(1UL <<  7, 0UL);
    public static readonly ContentCategory TagLabel         = new(1UL <<  8, 0UL);
    public static readonly ContentCategory TagLegend        = new(1UL <<  9, 0UL);
    public static readonly ContentCategory TagLi            = new(1UL << 10, 0UL);
    public static readonly ContentCategory TagLink          = new(1UL << 11, 0UL);
    public static readonly ContentCategory TagMain          = new(1UL << 12, 0UL);
    public static readonly ContentCategory TagMap           = new(1UL << 13, 0UL);
    public static readonly ContentCategory TagMark          = new(1UL << 14, 0UL);
    public static readonly ContentCategory TagMenu          = new(1UL << 15, 0UL);
    public static readonly ContentCategory TagMeta          = new(1UL << 16, 0UL);
    public static readonly ContentCategory TagMeter         = new(1UL << 17, 0UL);
    public static readonly ContentCategory TagNav           = new(1UL << 18, 0UL);
    public static readonly ContentCategory TagNoscript      = new(1UL << 19, 0UL);
    public static readonly ContentCategory TagObject        = new(1UL << 20, 0UL);
    public static readonly ContentCategory TagOl            = new(1UL << 21, 0UL);
    public static readonly ContentCategory TagOptgroup      = new(1UL << 22, 0UL);
    public static readonly ContentCategory TagOption        = new(1UL << 23, 0UL);
    public static readonly ContentCategory TagOutput        = new(1UL << 24, 0UL);
    public static readonly ContentCategory TagP             = new(1UL << 25, 0UL);
    public static readonly ContentCategory TagPicture       = new(1UL << 26, 0UL);
    public static readonly ContentCategory TagPre           = new(1UL << 27, 0UL);
    public static readonly ContentCategory TagProgress      = new(1UL << 28, 0UL);
    public static readonly ContentCategory TagQ             = new(1UL << 29, 0UL);
    public static readonly ContentCategory TagRp            = new(1UL << 30, 0UL);
    public static readonly ContentCategory TagRt            = new(1UL << 31, 0UL);
    public static readonly ContentCategory TagRuby          = new(1UL << 32, 0UL);
    public static readonly ContentCategory TagS             = new(1UL << 33, 0UL);
    public static readonly ContentCategory TagSamp          = new(1UL << 34, 0UL);
    public static readonly ContentCategory TagScript        = new(1UL << 35, 0UL);
    public static readonly ContentCategory TagSection       = new(1UL << 36, 0UL);
    public static readonly ContentCategory TagSelect        = new(1UL << 37, 0UL);
    public static readonly ContentCategory TagSlot          = new(1UL << 38, 0UL);
    public static readonly ContentCategory TagSmall         = new(1UL << 39, 0UL);
    public static readonly ContentCategory TagSource        = new(1UL << 40, 0UL);
    public static readonly ContentCategory TagSpan          = new(1UL << 41, 0UL);
    public static readonly ContentCategory TagStrong        = new(1UL << 42, 0UL);
    public static readonly ContentCategory TagStyle         = new(1UL << 43, 0UL);
    public static readonly ContentCategory TagSub           = new(1UL << 44, 0UL);
    public static readonly ContentCategory TagSummary       = new(1UL << 45, 0UL);
    public static readonly ContentCategory TagSup           = new(1UL << 46, 0UL);
    public static readonly ContentCategory TagTable         = new(1UL << 47, 0UL);
    public static readonly ContentCategory TagTbody         = new(1UL << 48, 0UL);
    public static readonly ContentCategory TagTd            = new(1UL << 49, 0UL);
    public static readonly ContentCategory TagTemplate      = new(1UL << 50, 0UL);
    public static readonly ContentCategory TagTextarea      = new(1UL << 51, 0UL);
    public static readonly ContentCategory TagTfoot         = new(1UL << 52, 0UL);
    public static readonly ContentCategory TagTh            = new(1UL << 53, 0UL);
    public static readonly ContentCategory TagThead         = new(1UL << 54, 0UL);
    public static readonly ContentCategory TagTime          = new(1UL << 55, 0UL);
    public static readonly ContentCategory TagTitle         = new(1UL << 56, 0UL);
    public static readonly ContentCategory TagTr            = new(1UL << 57, 0UL);
    public static readonly ContentCategory TagTrack         = new(1UL << 58, 0UL);
    public static readonly ContentCategory TagU             = new(1UL << 59, 0UL);
    public static readonly ContentCategory TagUl            = new(1UL << 60, 0UL);
    public static readonly ContentCategory TagVar           = new(1UL << 61, 0UL);
    public static readonly ContentCategory TagVideo         = new(1UL << 62, 0UL);
    public static readonly ContentCategory TagWbr           = new(1UL << 63, 0UL);
}

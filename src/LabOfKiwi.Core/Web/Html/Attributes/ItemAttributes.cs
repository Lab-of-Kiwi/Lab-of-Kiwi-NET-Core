using System;
using System.Collections.Generic;

namespace LabOfKiwi.Web.Html.Attributes;

public readonly struct ItemAttributes : IItemAttributes
{
    private readonly AttributeCollection _rawAttributes;

    internal ItemAttributes(AttributeCollection attributeCollection)
    {
        _rawAttributes = attributeCollection;
    }

    public Uri? Id
    {
        get => _rawAttributes.GetObject<UriParser, Uri>("itemid");
        set => _rawAttributes.SetObject<UriParser, Uri>("itemid", value);
    }

    public IList<string> Properties => _rawAttributes.GetList<TokenParser, string>("itemprop");

    public IList<string> ReferencedElements => _rawAttributes.GetList<TokenParser, string>("itemref");

    public bool IsScope
    {
        get => _rawAttributes.GetBoolean("itemscope");
        set => _rawAttributes.SetBoolean("itemscope", value);
    }

    public IList<string> Types => _rawAttributes.GetList<TokenParser, string>("itemtype");
}

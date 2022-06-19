using System;
using System.Collections;
using System.Collections.Generic;

namespace LabOfKiwi.Html;

public interface IElement
{
    IEnumerable<IContainerElement> Ancestors { get; }

    IContainerElement? Parent { get; internal set; }

    IEnumerable<IElement> Siblings { get; }

    string ToFormattedString(int tabCount = 0, HtmlFormatOptions formatOptions = HtmlFormatOptions.None);
}

public interface IContainerElement : IElement, IList
{
    IEnumerable<IElement> Children { get; }
}

public interface IFixedContainerElement<TElement> : IContainerElement, IReadOnlyList<TElement> where TElement : IElement
{
    new IEnumerable<TElement> Children { get; }
}

public interface IContainerElement<TElement> : IFixedContainerElement<TElement>, IList<TElement> where TElement : IElement
{
    void Add<TChild>(TChild item, Action<TChild> callback) where TChild : TElement;
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LabOfKiwi.Html.Tags;

public class Html : Tag, IFixedContainerElement<Tag>
{
    public Html()
    {
        Head = new Head();
        Body = new Body();

        Head.Parent = this;
        Body.Parent = this;
    }

    public Head Head { get; }

    public Body Body { get; }

    protected override string TagName => "html";

    internal override void CompleteToString(StringBuilder sb, int tabCount, HtmlFormatOptions formatOptions)
    {
        sb.Append('>').Append(formatOptions.GetNewLine()).Append(Head.ToFormattedString(0, formatOptions)).Append(Body.ToFormattedString(0, formatOptions)).Append("</html>");
    }

    #region Explicit Interface Implementations
    object? IList.this[int index]
    {
        get
        {
            if (index == 0) return Head;
            if (index == 1) return Body;

            throw new IndexOutOfRangeException();
        }

        set
        {
            throw new InvalidOperationException();
        }
    }

    Tag IReadOnlyList<Tag>.this[int index]
    {
        get
        {
            if (index == 0) return Head;
            if (index == 1) return Body;

            throw new IndexOutOfRangeException();
        }
    }

    IEnumerable<IElement> IContainerElement.Children
    {
        get
        {
            yield return Head;
            yield return Body;
        }
    }

    IEnumerable<Tag> IFixedContainerElement<Tag>.Children
    {
        get
        {
            yield return Head;
            yield return Body;
        }
    }

    int ICollection.Count => 2;

    int IReadOnlyCollection<Tag>.Count => 2;

    bool IList.IsFixedSize => true;

    bool IList.IsReadOnly => true;

    bool ICollection.IsSynchronized => false;

    object ICollection.SyncRoot => this;

    int IList.Add(object? value)
    {
        throw new InvalidOperationException();
    }

    void IList.Clear()
    {
        throw new InvalidOperationException();
    }

    bool IList.Contains(object? value)
    {
        return ReferenceEquals(Body, value) || ReferenceEquals(Head, value);
    }

    void ICollection.CopyTo(Array array, int index)
    {
        Tag[] arr = new Tag[] { Head, Body };
        arr.CopyTo(array, index);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        yield return Head;
        yield return Body;
    }

    IEnumerator<Tag> IEnumerable<Tag>.GetEnumerator()
    {
        yield return Head;
        yield return Body;
    }

    int IList.IndexOf(object? value)
    {
        if (ReferenceEquals(Body, value))
        {
            return 0;
        }

        if (ReferenceEquals(Head, value))
        {
            return 1;
        }

        return -1;
    }

    void IList.Insert(int index, object? value)
    {
        throw new InvalidOperationException();
    }

    void IList.Remove(object? value)
    {
        throw new InvalidOperationException();
    }

    void IList.RemoveAt(int index)
    {
        throw new InvalidOperationException();
    }
    #endregion
}

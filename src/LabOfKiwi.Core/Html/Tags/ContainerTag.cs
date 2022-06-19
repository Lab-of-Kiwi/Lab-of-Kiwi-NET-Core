using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace LabOfKiwi.Html.Tags;

public abstract class ContainerTag<TElement> : Tag, IContainerElement<TElement>
    where TElement: IElement
{
    private readonly List<TElement> _elements;

    protected ContainerTag()
    {
        _elements = new List<TElement>();
    }

    #region Public Members
    public TElement this[int index]
    {
        get => _elements[index];

        set
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            ThrowIfInvalid(value);

            value.Parent = this;
            _elements[index] = value;
        }
    }

    public IEnumerable<TElement> Children => _elements.AsReadOnly();

    public int Count => _elements.Count;

    public void Add(TElement item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        ThrowIfInvalid(item);

        item.Parent = this;
        _elements.Add(item);
    }

    public void Add<TChild>(TChild item, Action<TChild> callback) where TChild : TElement
    {
        if (callback == null)
        {
            throw new ArgumentNullException(nameof(callback));
        }

        Add(item);
        callback(item);
    }

    public void Clear()
    {
        foreach (var child in _elements)
        {
            child.Parent = null;
        }

        _elements.Clear();
    }

    public bool Contains(TElement item)
    {
        return _elements.Contains(item);
    }

    public void CopyTo(TElement[] array, int arrayIndex)
    {
        _elements.CopyTo(array, arrayIndex);
    }

    public IEnumerator<TElement> GetEnumerator()
    {
        return _elements.GetEnumerator();
    }

    public int IndexOf(TElement item)
    {
        return _elements.IndexOf(item);
    }

    public void Insert(int index, TElement item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        ThrowIfInvalid(item);

        item.Parent = this;
        _elements.Insert(index, item);
    }

    public bool Remove(TElement item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        var index = _elements.IndexOf(item);

        if (index != -1)
        {
            RemoveAt(index);
        }

        return false;
    }

    public void RemoveAt(int index)
    {
        _elements[index].Parent = null;
        _elements.RemoveAt(index);
    }
    #endregion

    #region Internal Members
    protected abstract bool RequiresClosingTag { get; }

    protected virtual bool IsValid(TElement child)
    {
        return true;
    }

    private void ThrowIfInvalid(TElement child)
    {
        if (!IsValid(child))
        {
            throw new ArgumentException("Child element is not valid for this tag.");
        }
    }

    internal override void CompleteToString(StringBuilder sb, int tabCount, HtmlFormatOptions formatOptions)
    {
        if (_elements.Count == 0)
        {
            if (RequiresClosingTag)
            {
                sb.Append($"></{TagName}>");
            }
            else
            {
                sb.Append(" />");
            }
        }
        else
        {
            sb.Append('>');
            sb.Append(formatOptions.GetNewLine());

            foreach (var element in _elements)
            {
                sb.Append(element.ToFormattedString(tabCount + 1, formatOptions));
                Debug.Assert(formatOptions != HtmlFormatOptions.None || sb[^1] == '\n');
            }

            sb.Append(formatOptions.GetTab(tabCount));
            sb.Append($"</{TagName}>");
        }
    }
    #endregion

    #region Explicit Interface Properties
    object? IList.this[int index]
    {
        get => ((IList)_elements)[index];
        set => ((IList)_elements)[index] = value;
    }

    IEnumerable<IElement> IContainerElement.Children => _elements.Cast<IElement>();

    bool IList.IsFixedSize => ((IList)_elements).IsFixedSize;

    bool ICollection<TElement>.IsReadOnly => ((ICollection<TElement>)_elements).IsReadOnly;

    bool IList.IsReadOnly => ((IList)_elements).IsReadOnly;

    bool ICollection.IsSynchronized => ((ICollection)_elements).IsSynchronized;

    object ICollection.SyncRoot => ((ICollection)_elements).SyncRoot;
    #endregion

    #region Explicit Interface Methods
    int IList.Add(object? value)
    {
        if (value is TElement child)
        {
            Add(child);
            return 1;
        }
        else
        {
            return ((IList)_elements).Add(value);
        }
    }

    bool IList.Contains(object? value)
    {
        return ((IList)_elements).Contains(value);
    }

    void ICollection.CopyTo(Array array, int index)
    {
        ((ICollection)_elements).CopyTo(array, index);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_elements).GetEnumerator();
    }

    int IList.IndexOf(object? value)
    {
        return ((IList)_elements).IndexOf(value);
    }

    void IList.Insert(int index, object? value)
    {
        if (value is TElement child)
        {
            Insert(index, child);
        }
        else
        {
            ((IList)_elements).Insert(index, value);
        }
    }

    void IList.Remove(object? value)
    {
        if (value is TElement child)
        {
            Remove(child);
        }
        else
        {
            ((IList)_elements).Remove(value);
        }
    }

    void IList.RemoveAt(int index)
    {
        RemoveAt(index);
    }
    #endregion
}

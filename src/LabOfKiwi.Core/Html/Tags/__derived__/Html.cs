using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public Tag this[int index]
    {
        get
        {
            if (index == 0) return Head;
            if (index == 1) return Body;

            throw new IndexOutOfRangeException();
        }
    }

    public IEnumerable<Tag> Children
    {
        get
        {
            yield return Head;
            yield return Body;
        }
    }

    public bool IsFixedSize => throw new NotImplementedException();

    public bool IsReadOnly => throw new NotImplementedException();

    public int Count => throw new NotImplementedException();

    public bool IsSynchronized => throw new NotImplementedException();

    public object SyncRoot => throw new NotImplementedException();

    protected override string TagName => throw new NotImplementedException();

    IEnumerable<IElement> IContainerElement.Children => throw new NotImplementedException();

    public int Add(object? value) => throw new NotImplementedException();
    public void Clear() => throw new NotImplementedException();
    public bool Contains(object? value) => throw new NotImplementedException();
    public void CopyTo(Array array, int index) => throw new NotImplementedException();
    public IEnumerator<Tag> GetEnumerator() => throw new NotImplementedException();
    public int IndexOf(object? value) => throw new NotImplementedException();
    public void Insert(int index, object? value) => throw new NotImplementedException();
    public void Remove(object? value) => throw new NotImplementedException();
    public void RemoveAt(int index) => throw new NotImplementedException();
    internal override void CompleteToString(StringBuilder sb) => throw new NotImplementedException();
    IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();
}

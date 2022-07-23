using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace LabOfKiwi.Collections;

public class ListSet<T> : IOrderedSet<T>, IList, IReadOnlySet<T>
{
    private readonly List<T> _items;

    public ListSet()
    {
        _items = new List<T>();
        Comparer = EqualityComparer<T>.Default;
    }

    public ListSet(IEqualityComparer<T>? comparer)
    {
        _items = new List<T>();
        Comparer = comparer ?? EqualityComparer<T>.Default;
    }

    public ListSet(int capacity)
    {
        _items = new List<T>(capacity);
        Comparer = EqualityComparer<T>.Default;
    }

    public ListSet(int capacity, IEqualityComparer<T>? comparer)
    {
        _items = new List<T>(capacity);
        Comparer = comparer ?? EqualityComparer<T>.Default;
    }

    public T this[int index] => _items[index];

    public int Capacity
    {
        get => _items.Capacity;

        set => _items.Capacity = value;
    }

    public IEqualityComparer<T> Comparer { get; }

    public int Count => _items.Count;

    public bool Add(T item)
    {
        if (!Contains(item))
        {
            _items.Add(item);
            return true;
        }

        return false;
    }

    public void Clear()
    {
        _items.Clear();
    }

    public bool Contains(T item)
    {
        return IndexOf(item) >= 0;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        _items.CopyTo(array, arrayIndex);
    }

    public void ExceptWith(IEnumerable<T> other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other));
        }

        _items.RemoveAll(i => other.Any(e => Comparer.Equals(i, e)));
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    public int IndexOf(T item)
    {
        return _items.FindIndex(i => Comparer.Equals(i, item));
    }

    public bool Insert(int index, T item)
    {
        if (!Contains(item))
        {
            _items.Insert(index, item);
            return true;
        }

        if ((uint)index > (uint)Count)
        {
            _ = _items[index]; // Trigger Index out of Bounds
        }

        return false;
    }

    public void IntersectWith(IEnumerable<T> other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other));
        }

        _items.RemoveAll(i => !other.Any(e => Comparer.Equals(i, e)));
    }

    public bool IsProperSubsetOf(IEnumerable<T> other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other));
        }

        // A collection cannot be a proper subset of itself.
        if (this == other)
        {
            return false;
        }

        if (other.TryGetNonEnumeratedCount(out int otherCount))
        {
            if (_items.Count >= otherCount)
            {
                return false;
            }
        }

        var comparer = Comparer;
        return _items.All(i => other.Any(e => comparer.Equals(i, e))) && other.Any(e => _items.Any(i => !comparer.Equals(i, e)));
    }

    public bool IsProperSupersetOf(IEnumerable<T> other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other));
        }

        // A collection cannot be a proper superset of itself.
        if (this == other)
        {
            return false;
        }

        if (other.TryGetNonEnumeratedCount(out int otherCount))
        {
            if (_items.Count <= otherCount)
            {
                return false;
            }
        }

        var comparer = Comparer;
        return _items.All(i => other.Any(e => comparer.Equals(i, e))) && _items.Any(i => other.Any(e => !comparer.Equals(i, e)));
    }

    public bool IsSubsetOf(IEnumerable<T> other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other));
        }

        if (this == other || _items.Count == 0)
        {
            return true;
        }

        if (other.TryGetNonEnumeratedCount(out int otherCount))
        {
            if (_items.Count > otherCount)
            {
                return false;
            }
        }

        var comparer = Comparer;
        return _items.All(i => other.Any(e => comparer.Equals(i, e)));
    }

    public bool IsSupersetOf(IEnumerable<T> other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other));
        }

        if (this == other)
        {
            return true;
        }

        if (other.TryGetNonEnumeratedCount(out int otherCount))
        {
            if (otherCount == 0)
            {
                return true;
            }

            if (_items.Count < otherCount)
            {
                return false;
            }
        }

        var comparer = Comparer;
        return other.All(e => _items.Any(i => comparer.Equals(i, e)));
    }

    public bool Overlaps(IEnumerable<T> other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other));
        }

        if (this == other || _items.Count > 0)
        {
            return true;
        }

        if (_items.Count == 0)
        {
            return false;
        }

        if (other.TryGetNonEnumeratedCount(out int otherCount))
        {
            if (otherCount == 0)
            {
                return false;
            }
        }

        var comparer = Comparer;
        return _items.Any(i => other.Any(e => comparer.Equals(i, e)));
    }

    public bool SetEquals(IEnumerable<T> other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other));
        }

        if (this == other)
        {
            return true;
        }

        if (other.TryGetNonEnumeratedCount(out int otherCount))
        {
            if (_items.Count == 0 && otherCount == 0)
            {
                return true;
            }
        }

        var comparer = Comparer;
        return _items.All(i => other.Any(e => comparer.Equals(i, e))) && other.All(e => _items.Any(i => comparer.Equals(i, e)));
    }

    public bool Remove(T item)
    {
        int index = IndexOf(item);

        if (index >= 0)
        {
            _items.RemoveAt(index);
            return true;
        }

        return false;
    }

    public T RemoveAt(int index)
    {
        T value = _items[index];
        _items.RemoveAt(index);
        return value;
    }

    public bool Set(int index, T item)
    {
        return Set(index, item, out _);
    }

    public bool Set(int index, T item, [MaybeNullWhen(false)] out T oldValue)
    {
        T origValue = _items[index];

        if (Contains(item))
        {
            oldValue = default;
            return false;
        }

        _items[index] = item;
        oldValue = origValue;
        return true;
    }

    public void SymmetricExceptWith(IEnumerable<T> other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other));
        }

        List<T> toAdd = other.Where(e => !Contains(e)).Distinct(Comparer).ToList();
        List<T> toRemove = _items.FindAll(i => other.Any(e => Comparer.Equals(i, e)));

        foreach (var item in toRemove)
        {
            _items.Remove(item);
        }

        foreach (var item in toAdd)
        {
            _items.Add(item);
        }
    }

    public void UnionWith(IEnumerable<T> other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other));
        }

        foreach (var element in other)
        {
            Add(element);
        }
    }

    #region Explicit Interface Implementations
    T IList<T>.this[int index]
    {
        get => _items[index];

        set
        {
            if (!Set(index, value))
            {
                throw new ArgumentException("Value already exists in set.");
            }
        }
    }

    object? IList.this[int index]
    {
        get => _items[index];

        set
        {
            ThrowIfNullAndNullsAreIllegal(value);

            try
            {
                ((IList<T>)this)[index] = (T)value!;
            }
            catch (InvalidCastException)
            {
                throw new ArgumentException("Wrong value type.");
            }
        }
    }

    bool IList.IsFixedSize => false;

    bool ICollection<T>.IsReadOnly => false;

    bool IList.IsReadOnly => false;

    bool ICollection.IsSynchronized => false;

    object ICollection.SyncRoot => this;

    void ICollection<T>.Add(T item)
    {
        if (!Add(item))
        {
            throw new ArgumentException("Value already exists in set.");
        }
    }

    int IList.Add(object? value)
    {
        ThrowIfNullAndNullsAreIllegal(value);

        try
        {
            ((ICollection<T>)this).Add((T)value!);
        }
        catch (InvalidCastException)
        {
            throw new ArgumentException("Wrong value type.");
        }

        return Count - 1;
    }

    bool IList.Contains(object? value)
    {
        if (!IsCompatibleObject(value))
        {
            return false;
        }

        return Contains((T)value!);
    }

    void ICollection.CopyTo(Array array, int index)
    {
        ((IList)_items).CopyTo(array, index);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    int IList.IndexOf(object? value)
    {
        if (!IsCompatibleObject(value))
        {
            return -1;
        }

        return IndexOf((T)value!);
    }

    void IList<T>.Insert(int index, T item)
    {
        if (!Insert(index, item))
        {
            throw new ArgumentException("Value already exists in set.");
        }
    }

    void IList.Insert(int index, object? value)
    {
        ThrowIfNullAndNullsAreIllegal(value);

        try
        {
            ((IList<T>)this).Insert(index, (T)value!);
        }
        catch (InvalidCastException)
        {
            throw new ArgumentException("Wrong value type.");
        }
    }

    void IList.Remove(object? value)
    {
        if (IsCompatibleObject(value))
        {
            Remove((T)value!);
        }
    }

    void IList<T>.RemoveAt(int index)
    {
        RemoveAt(index);
    }

    void IList.RemoveAt(int index)
    {
        RemoveAt(index);
    }
    #endregion

    private static bool IsCompatibleObject(object? value)
    {
        return (value is T) || (value == null && default(T) == null);
    }

    private static void ThrowIfNullAndNullsAreIllegal(object? value)
    {
        if (value == null && default(T) != null)
        {
            throw new ArgumentException($"Null is not a valid value for type {typeof(T)}.");
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace LabOfKiwi.Collections;

/// <summary>
/// Represents a collection of value pairs with a one-to-one relationship.
/// </summary>
/// 
/// <typeparam name="T1">The first type.</typeparam>
/// <typeparam name="T2">The second type.</typeparam>
public class Map<T1, T2> : IMap<T1, T2>, IReadOnlyMap<T1, T2> where T1 : notnull where T2 : notnull
{
    private readonly Dictionary<T1, T2> _forward;
    private readonly Dictionary<T2, T1> _reverse;
    private readonly Map<T2, T1> _reverseMap;
    private readonly bool _isReverse;

    /// <summary>
    /// Initializes a new instance of the <see cref="Map{T1, T2}"/> class that is empty, has the default initial
    /// capacity, and uses the provided comparers (or the default if <c>null</c>).
    /// </summary>
    /// 
    /// <param name="comparer1">
    ///     The <see cref="IEqualityComparer{T}"/> implementation to use when comparing values of
    ///     <typeparamref name="T1"/>, or <c>null</c> to use the default <see cref="EqualityComparer{T}"/> for the type
    ///     of the value.
    /// </param>
    /// <param name="comparer2">
    ///     The <see cref="IEqualityComparer{T}"/> implementation to use when comparing values of
    ///     <typeparamref name="T2"/>, or <c>null</c> to use the default <see cref="EqualityComparer{T}"/> for the type
    ///     of the value.
    /// </param>
    public Map(IEqualityComparer<T1>? comparer1 = null, IEqualityComparer<T2>? comparer2 = null)
    {
        _forward = new Dictionary<T1, T2>(comparer1);
        _reverse = new Dictionary<T2, T1>(comparer2);
        _reverseMap = new Map<T2, T1>(this);
        _isReverse = false;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Map{T1, T2}"/> class that is empty, has the specified initial
    /// capacity, and uses the provided comparers (or the default if <c>null</c>).
    /// </summary>
    /// 
    /// <param name="capacity">The initial number of elements that the <see cref="Map{T1, T2}"/> can contain.</param>
    /// <param name="comparer1">
    ///     The <see cref="IEqualityComparer{T}"/> implementation to use when comparing keys of
    ///     <typeparamref name="T1"/>, or <c>null</c> to use the default <see cref="EqualityComparer{T}"/> for the type
    ///     of the key.
    /// </param>
    /// <param name="comparer2">
    ///     The <see cref="IEqualityComparer{T}"/> implementation to use when comparing keys of
    ///     <typeparamref name="T2"/>, or <c>null</c> to use the default <see cref="EqualityComparer{T}"/> for the type
    ///     of the key.
    /// </param>
    /// 
    /// <exception cref="ArgumentOutOfRangeException"/>
    public Map(int capacity, IEqualityComparer<T1>? comparer1 = null, IEqualityComparer<T2>? comparer2 = null)
    {
        _forward = new Dictionary<T1, T2>(capacity, comparer1);
        _reverse = new Dictionary<T2, T1>(capacity, comparer2);
        _reverseMap = new Map<T2, T1>(this);
        _isReverse = false;
    }

    // Internal constructor.
    private Map(Map<T2, T1> map)
    {
        _forward = map._reverse;
        _reverse = map._forward;
        _reverseMap = map;
        _isReverse = true;
    }

    /// <summary>
    /// Gets or sets the <typeparamref name="T2"/> value associated with the specified <typeparamref name="T1"/> value.
    /// </summary>
    /// 
    /// <param name="value1">
    ///     The <typeparamref name="T1"/> value of the <typeparamref name="T2"/> value to get or set.
    /// </param>
    /// <returns>
    ///     The <typeparamref name="T2"/> value associated with the <typeparamref name="T1"/> specified value.
    /// </returns>
    /// 
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="KeyNotFoundException"/>
    /// <exception cref="ArgumentException"/>
    public T2 this[T1 value1]
    {
        get => _forward[value1];

        set
        {
            if (value1 == null)
            {
                throw new ArgumentNullException(nameof(value1));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (_forward.TryGetValue(value1, out T2? currentForwardValue))
            {
                _forward[value1] = value;
                bool result = _reverse.Remove(currentForwardValue);
                Debug.Assert(result);

                try
                {
                    _reverse.Add(value, value1);
                }
                catch
                {
                    _forward[value1] = currentForwardValue;
                    Debug.Assert(_forward.Count == _reverse.Count);
                    throw;
                }
            }
            else
            {
                _forward.Add(value1, value);

                try
                {
                    _reverse.Add(value, value1);
                }
                catch
                {
                    _forward.Remove(value1);
                    Debug.Assert(_forward.Count == _reverse.Count);
                    throw;
                }
            }

            Debug.Assert(_forward.Count == _reverse.Count);
        }
    }

    /// <summary>
    /// Gets the <see cref="IEqualityComparer{T}"/> that is used to determine equality of <typeparamref name="T1"/>
    /// values.
    /// </summary>
    public IEqualityComparer<T1> Comparer1 => _forward.Comparer;

    /// <summary>
    /// Gets the <see cref="IEqualityComparer{T}"/> that is used to determine equality of <typeparamref name="T2"/>
    /// values.
    /// </summary>
    public IEqualityComparer<T2> Comparer2 => _reverse.Comparer;

    /// <summary>
    /// Gets the number of value pairs contained in the <see cref="Map{T1, T2}"/>.
    /// </summary>
    public int Count
    {
        get
        {
            Debug.Assert(_forward.Count == _reverse.Count);
            return _forward.Count;
        }
    }

    /// <inheritdoc/>
    public Map<T2, T1> Reverse => _reverseMap;

    /// <summary>
    /// Adds the specified values to the map.
    /// </summary>
    /// 
    /// <param name="value1">A value of type <typeparamref name="T1"/>.</param>
    /// <param name="value2">A value of type <typeparamref name="T2"/>.</param>
    /// 
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentException"/>
    public void Add(T1 value1, T2 value2)
    {
        if (value1 == null)
        {
            throw new ArgumentNullException(nameof(value1));
        }

        if (value2 == null)
        {
            throw new ArgumentNullException(nameof(value2));
        }

        _forward.Add(value1, value2);

        try
        {
            _reverse.Add(value2, value1);
        }
        catch
        {
            _forward.Remove(value1);
            Debug.Assert(_forward.Count == _reverse.Count);
            throw;
        }

        Debug.Assert(_forward.Count == _reverse.Count);
    }

    /// <summary>
    /// Returns a read-only <see cref="ReadOnlyMap{T1, T2}"/> wrapper for the current map.
    /// </summary>
    /// 
    /// <returns>An object that acts as a read-only wrapper around the current <see cref="Map{T1, T2}"/>.</returns>
    public ReadOnlyMap<T1, T2> AsReadOnly()
    {
        return new(this);
    }

    /// <summary>
    /// Removes all values from the <see cref="Map{T1, T2}"/>.
    /// </summary>
    public void Clear()
    {
        _forward.Clear();
        _reverse.Clear();
    }

    /// <summary>
    /// Determines whether the <see cref="Map{T1, T2}"/> contains the specified <typeparamref name="T1"/> value.
    /// </summary>
    /// 
    /// <param name="value1">
    ///     The <typeparamref name="T1"/> value to locate in the <see cref="Map{T1, T2}"/>.
    /// </param>
    /// <returns>
    ///     <c>true</c> if the <see cref="Map{T1, T2}"/> the specified <typeparamref name="T1"/> value; otherwise,
    ///     <c>false</c>.
    /// </returns>
    /// 
    /// <exception cref="ArgumentNullException"/>
    public bool ContainsKey(T1 value1)
    {
        return _forward.ContainsKey(value1);
    }

    /// <summary>
    /// Ensures that the map can hold up to a specified number of entries without any further expansion of its backing
    /// storage.
    /// </summary>
    /// 
    /// <param name="capacity">The number of entries.</param>
    /// <returns>The current capacity of the <see cref="Map{T1, T2}"/>.</returns>
    /// 
    /// <exception cref="ArgumentOutOfRangeException"/>
    public int EnsureCapacity(int capacity)
    {
        Debug.Assert(_forward.Count == _reverse.Count);
        int c1 = _forward.EnsureCapacity(capacity);
        int c2 = _reverse.EnsureCapacity(capacity);
        return Math.Min(c1, c2);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        if (obj is Map<T1, T2> other)
        {
            bool result = ReferenceEquals(_forward, other._forward);
            Debug.Assert(!result || ReferenceEquals(_reverse, other._reverse));
            return result;
        }

        if (obj is Map<T2, T1> revOther)
        {
            bool result = ReferenceEquals(_forward, revOther._reverse);
            Debug.Assert(!result || ReferenceEquals(_reverse, revOther._forward));
            return result;
        }

        return false;
    }

    /// <inheritdoc/>
    public IEnumerator<KeyValuePair<T1, T2>> GetEnumerator()
    {
        return _forward.GetEnumerator();
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        if (_isReverse)
        {
            return _reverseMap.GetHashCode();
        }

        return base.GetHashCode();
    }

    /// <summary>
    /// Removes the <typeparamref name="T1"/> value and its associated <typeparamref name="T2"/> value from the
    /// <see cref="Map{T1, T2}"/>.
    /// </summary>
    /// 
    /// <param name="value1">The <typeparamref name="T1"/> vakye to remove.</param>
    /// <returns>
    ///     <c>true</c> if the element is successfully found and removed; otherwise, <c>false</c>. This method returns
    ///     <c>false</c> if <paramref name="value1"/> is not found in the <see cref="Map{T1, T2}"/>.
    /// </returns>
    /// 
    /// <exception cref="ArgumentNullException"/>
    public bool Remove(T1 value1)
    {
        if (_forward.TryGetValue(value1, out T2? value))
        {
            bool result = _forward.Remove(value1) & _reverse.Remove(value);
            Debug.Assert(result);
            Debug.Assert(_forward.Count == _reverse.Count);
            return true;
        }

        Debug.Assert(_forward.Count == _reverse.Count);
        return false;
    }

    /// <summary>
    /// Sets the capacity of this map to what it would be if it had been originally initialized with all its entries.
    /// </summary>
    public void TrimExcess()
    {
        Debug.Assert(_forward.Count == _reverse.Count);
        _forward.TrimExcess();
        _reverse.TrimExcess();
    }

    /// <summary>
    /// Sets the capacity of this map to hold up a specified number of entries without any further expansion of its
    /// backing storage.
    /// </summary>
    /// 
    /// <param name="capacity">The new capacity.</param>
    /// 
    /// <exception cref="ArgumentOutOfRangeException"/>
    public void TrimExcess(int capacity)
    {
        Debug.Assert(_forward.Count == _reverse.Count);
        _forward.TrimExcess(capacity);
        _reverse.TrimExcess(capacity);
    }

    /// <summary>
    /// Attempts to add the specified values to the map.
    /// </summary>
    /// 
    /// <param name="value1">A value of type <typeparamref name="T1"/>.</param>
    /// <param name="value2">A value of type <typeparamref name="T2"/>.</param>
    /// <returns><c>true</c> if the values were added to the map successfully; otherwise, <c>false</c>.</returns>
    /// 
    /// <exception cref="ArgumentNullException"/>
    public bool TryAdd(T1 value1, T2 value2)
    {
        if (value1 == null)
        {
            throw new ArgumentNullException(nameof(value1));
        }

        if (value2 == null)
        {
            throw new ArgumentNullException(nameof(value2));
        }

        if (_forward.TryAdd(value1, value2))
        {
            if (_reverse.TryAdd(value2, value1))
            {
                Debug.Assert(_forward.Count == _reverse.Count);
                return true;
            }
            else
            {
                _forward.Remove(value1);
                
            }
        }

        Debug.Assert(_forward.Count == _reverse.Count);
        return false;
    }

    /// <summary>
    /// Gets the <typeparamref name="T2"/> value associated with the specified <typeparamref name="T1"/> value.
    /// </summary>
    /// 
    /// <param name="value1">The <typeparamref name="T1"/> value of the <typeparamref name="T2"/> value to get.</param>
    /// <param name="value2">
    ///     When this method returns, contains the <typeparamref name="T2"/> value associated with the specified
    ///     <typeparamref name="T1"/> value, if the <typeparamref name="T1"/> value is found; otherwise, the default
    ///     value of <typeparamref name="T2"/>. This parameter is passed uninitialized.
    /// </param>
    /// <returns>
    ///     <c>true</c> if the <see cref="Map{T1, T2}"/> contains an element with the specified value; otherwise,
    ///     <c>false</c>.
    /// </returns>
    /// 
    /// <exception cref="ArgumentNullException"/>
    public bool TryGetValue(T1 value1, [MaybeNullWhen(false)] out T2 value2)
    {
        return _forward.TryGetValue(value1, out value2);
    }

    #region Explicit Interface Implementations
    /// <inheritdoc/>
    bool ICollection<KeyValuePair<T1, T2>>.IsReadOnly => false;

    /// <inheritdoc/>
    ICollection<T1> IDictionary<T1, T2>.Keys => _forward.Keys;

    /// <inheritdoc/>
    IEnumerable<T1> IReadOnlyDictionary<T1, T2>.Keys => _forward.Keys;

    /// <inheritdoc/>
    IMap<T2, T1> IMap<T1, T2>.Reverse => _reverseMap;

    /// <inheritdoc/>
    IReadOnlyMap<T2, T1> IReadOnlyMap<T1, T2>.Reverse => _reverseMap.AsReadOnly();

    /// <inheritdoc/>
    ICollection<T2> IDictionary<T1, T2>.Values => _reverse.Keys;

    /// <inheritdoc/>
    IEnumerable<T2> IReadOnlyDictionary<T1, T2>.Values => _reverse.Keys;

    /// <inheritdoc/>
    void ICollection<KeyValuePair<T1, T2>>.Add(KeyValuePair<T1, T2> item)
    {
        Add(item.Key, item.Value);
    }

    /// <inheritdoc/>
    bool ICollection<KeyValuePair<T1, T2>>.Contains(KeyValuePair<T1, T2> item)
    {
        return ((IDictionary<T1, T2>)_forward).Contains(item);
    }

    /// <inheritdoc/>
    void ICollection<KeyValuePair<T1, T2>>.CopyTo(KeyValuePair<T1, T2>[] array, int arrayIndex)
    {
        ((IDictionary<T1, T2>)_forward).CopyTo(array, arrayIndex);
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_forward).GetEnumerator();
    }

    /// <inheritdoc/>
    bool ICollection<KeyValuePair<T1, T2>>.Remove(KeyValuePair<T1, T2> item)
    {
        if (((IDictionary<T1, T2>)_forward).Remove(item))
        {
            bool result = ((IDictionary<T2, T1>)_reverse).Remove(new KeyValuePair<T2, T1>(item.Value, item.Key));
            Debug.Assert(result);
            Debug.Assert(_forward.Count == _reverse.Count);
            return true;
        }

        Debug.Assert(_forward.Count == _reverse.Count);
        return false;
    }
    #endregion
}

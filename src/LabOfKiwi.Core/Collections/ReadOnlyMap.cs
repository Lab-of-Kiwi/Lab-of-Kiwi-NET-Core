using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LabOfKiwi.Collections;

/// <summary>
/// Provides the base class for a generic read-only map.
/// </summary>
/// 
/// <typeparam name="T1">The first type.</typeparam>
/// <typeparam name="T2">The second type.</typeparam>
public class ReadOnlyMap<T1, T2> : IReadOnlyMap<T1, T2>
{
    private readonly IMap<T1, T2> _map;
    private readonly ReadOnlyMap<T2, T1> _reverseMap;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReadOnlyMap{T1, T2}"/> class that is a read-only wrapper around the
    /// specified map.
    /// </summary>
    /// 
    /// <param name="map">The map to wrap.</param>
    /// <exception cref="ArgumentNullException"><paramref name="map"/> is <c>null</c>.</exception>
    public ReadOnlyMap(IMap<T1, T2> map)
    {
        _map = map ?? throw new ArgumentNullException(nameof(map));
        _reverseMap = new(_map.Reverse, this);
    }

    // Internal constructor.
    private ReadOnlyMap(IMap<T1, T2> map, ReadOnlyMap<T2, T1> reverseMap)
    {
        _map = map;
        _reverseMap = reverseMap;
    }

    /// <summary>
    /// Gets the <typeparamref name="T2"/> value associated with the specified <typeparamref name="T1"/> value.
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
    public T2 this[T1 value1] => _map[value1];

    /// <summary>
    /// Gets the number of value pairs contained in the <see cref="ReadOnlyMap{T1, T2}"/>.
    /// </summary>
    public int Count => _map.Count;

    /// <inheritdoc/>
    public IReadOnlyMap<T2, T1> Reverse => _reverseMap;

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
        return _map.ContainsKey(value1);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return _map.Equals(obj);
    }

    /// <inheritdoc/>
    public IEnumerator<KeyValuePair<T1, T2>> GetEnumerator()
    {
        return _map.GetEnumerator();
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return _map.GetHashCode();
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
        return _map.TryGetValue(value1, out value2);
    }

    #region Explicit Interface Definitions
    /// <inheritdoc/>
    IEnumerable<T1> IReadOnlyDictionary<T1, T2>.Keys => _map.Keys;

    /// <inheritdoc/>
    IEnumerable<T2> IReadOnlyDictionary<T1, T2>.Values => _map.Values;

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_map).GetEnumerator();
    }
    #endregion
}

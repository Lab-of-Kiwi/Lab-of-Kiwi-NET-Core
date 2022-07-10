using System.Collections.Generic;

namespace LabOfKiwi.Collections;

/// <summary>
/// Represents a read-only generic collection of value pairs with a one-to-one relationship.
/// </summary>
/// 
/// <typeparam name="T1">The first type.</typeparam>
/// <typeparam name="T2">The second type.</typeparam>
public interface IReadOnlyMap<T1, T2> : IReadOnlyDictionary<T1, T2>
{
    /// <summary>
    /// Returns this map as a map with its type arguments reversed.
    /// </summary>
    /// <returns>A map with the same backing storage, but type arguments reversed.</returns>
    IReadOnlyMap<T2, T1> Reverse { get; }
}

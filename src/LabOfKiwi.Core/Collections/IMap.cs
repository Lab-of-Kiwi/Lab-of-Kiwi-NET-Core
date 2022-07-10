using System.Collections.Generic;

namespace LabOfKiwi.Collections;

/// <summary>
/// Represents a generic collection of value pairs with a one-to-one relationship.
/// </summary>
/// 
/// <typeparam name="T1">The first type.</typeparam>
/// <typeparam name="T2">The second type.</typeparam>
public interface IMap<T1, T2> : IDictionary<T1, T2>
{
    /// <summary>
    /// Returns this map as a map with its type arguments reversed.
    /// </summary>
    /// <returns>A map with the same backing storage, but type arguments reversed.</returns>
    IMap<T2, T1> Reverse { get; }
}

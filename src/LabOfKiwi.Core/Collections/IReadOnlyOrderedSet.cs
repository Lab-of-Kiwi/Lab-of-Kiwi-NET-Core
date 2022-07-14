using System.Collections.Generic;

namespace LabOfKiwi.Collections;

/// <summary>
/// Represents a generic read-only list of unique items.
/// </summary>
/// 
/// <typeparam name="T">The type of elements.</typeparam>
public interface IReadOnlyOrderedSet<T> : IReadOnlySet<T>, IReadOnlyList<T>
{
}

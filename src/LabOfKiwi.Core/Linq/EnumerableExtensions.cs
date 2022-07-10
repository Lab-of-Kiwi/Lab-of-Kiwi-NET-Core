using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LabOfKiwi.Linq;

/// <summary>
/// Linq extensions.
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// Returns a read-only <see cref="ReadOnlyCollection{T}"/> wrapper for the provided collection.
    /// </summary>
    /// 
    /// <typeparam name="TSource">The type of elements in the collection.</typeparam>
    /// <param name="source">The collection to be turned into a <see cref="ReadOnlyCollection{T}"/>.</param>
    /// <returns>An object that acts as a read-only wrapper around the provided collection.</returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
    public static ReadOnlyCollection<TSource> ToReadOnlyList<TSource>(this IEnumerable<TSource> source)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (source is ReadOnlyCollection<TSource> roColl)
        {
            return roColl;
        }

        if (source is IList<TSource> list)
        {
            return new ReadOnlyCollection<TSource>(list);
        }

        return new ReadOnlyCollection<TSource>(source.ToArray());
    }
}

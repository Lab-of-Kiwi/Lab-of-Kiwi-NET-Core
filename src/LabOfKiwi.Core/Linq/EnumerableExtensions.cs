using System;
using System.Collections;
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
    /// Returns a read-only <see cref="IReadOnlyList{T}"/> wrapper for the provided collection.
    /// </summary>
    /// 
    /// <typeparam name="TSource">The type of elements in the collection.</typeparam>
    /// <param name="source">The collection to be turned into a <see cref="IReadOnlyList{T}"/>.</param>
    /// <returns>An object that acts as a read-only wrapper around the provided collection.</returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
    public static IReadOnlyList<TSource> AsReadOnlyList<TSource>(this IEnumerable<TSource> source)
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

        if (source is EnumerableList<TSource> enList)
        {
            return enList;
        }

        return new EnumerableList<TSource>(source);
    }

    // Internal wraper for enumerables to be lists.
    private readonly struct EnumerableList<TSource> : IReadOnlyList<TSource>
    {
        private readonly IEnumerable<TSource> _source;

        public EnumerableList(IEnumerable<TSource> source)
        {
            _source = source;
        }

        public TSource this[int index] => _source.ElementAt(index);

        public int Count => _source.Count();

        public IEnumerator<TSource> GetEnumerator()
        {
            return _source.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

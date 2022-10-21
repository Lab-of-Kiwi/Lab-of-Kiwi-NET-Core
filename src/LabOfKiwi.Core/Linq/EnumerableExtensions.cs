using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
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

    /// <summary>
    /// Combines the two provided <see cref="IReadOnlyList{T}"/> instances into a single <see cref="IReadOnlyList{T}"/>.
    /// </summary>
    /// 
    /// <typeparam name="TSource">The type of elements in the list.</typeparam>
    /// <param name="source">The first <see cref="IReadOnlyList{T}"/>.</param>
    /// <param name="other">The second <see cref="IReadOnlyList{T}"/>.</param>
    /// <returns>
    ///     A <see cref="IReadOnlyList{T}"/> that combines <paramref name="other"/> and <paramref name="source"/>.
    /// </returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">
    ///     The resulting <see cref="IReadOnlyList{T}"/> would have an element count that exceeds
    ///     <see cref="int.MaxValue"/>.
    /// </exception>
    public static IReadOnlyList<TSource> Combine<TSource>(this IReadOnlyList<TSource> source, IReadOnlyList<TSource>? other)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (other == null || other.Count == 0)
        {
            return source;
        }

        if (source.Count == 0)
        {
            return other;
        }

        if (source is CombinedList<TSource> multiList)
        {
            return multiList.AddList(other);
        }

        return new CombinedList<TSource>(new IReadOnlyList<TSource>[] { source, other });
    }

    /// <summary>
    /// Selects a random element from the provided collection.
    /// </summary>
    /// 
    /// <typeparam name="TSource">The type of elements in the list.</typeparam>
    /// <param name="source">The collection that a random element will be selected from.</param>
    /// <returns>A random element of type <typeparamref name="TSource"/>.</returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
    /// <exception cref="InvalidOperationException">The source sequence is empty.</exception>
    public static TSource Random<TSource>(this IEnumerable<TSource> source)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (TryRandom(source, out TSource? result))
        {
            return result;
        }

        throw new InvalidOperationException("Sequence contains no elements.");
    }

    /// <summary>
    /// Selects a random element from the provided collection.
    /// </summary>
    /// 
    /// <typeparam name="TSource">The type of elements in the list.</typeparam>
    /// <param name="source">The collection that a random element will be selected from.</param>
    /// <returns>
    ///     A random element of type <typeparamref name="TSource"/> or the default value of <paramref name="source"/> is
    ///     the collection is empty.
    /// </returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
    public static TSource? RandomOrDefault<TSource>(this IEnumerable<TSource> source)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (TryRandom(source, out TSource? result))
        {
            return result;
        }

        return default;
    }

    private static bool TryRandom<TSource>(IEnumerable<TSource> source, [MaybeNullWhen(false)] out TSource result)
    {
        var random = new Random();

        while (true)
        {
            int count = source.Count();

            if (count == 0)
            {
                result = default;
                return false;
            }
            
            int index = random.Next(0, count);

            try
            {
                result = source.ElementAt(index);
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                // Source collection was modified.
            }
        }
    }

    // Internal wrapper for combining lists.
    private readonly struct CombinedList<T> : IReadOnlyList<T>
    {
        private readonly IReadOnlyList<T>[] _lists;
        private readonly int _count;

        public CombinedList(IReadOnlyList<T>[] lists)
        {
            long lCount = 0;

            for (int i = 0; i < lists.Length; i++)
            {
                var list = lists[i];

                if (list != null)
                {
                    lCount += list.Count;

                    if (lCount > int.MaxValue)
                    {
                        throw new ArgumentException("Resulting list contains too many elements");
                    }
                }
            }

            _lists = lists;
            _count = (int)lCount;
        }

        public T this[int index]
        {
            get
            {
                if (index < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                for (int i = 0; i < _lists.Length; i++)
                {
                    var list = _lists[i];

                    if (index < list.Count)
                    {
                        return list[index];
                    }

                    index -= list.Count;
                }

                throw new ArgumentOutOfRangeException(nameof(index));
            }
        }

        public int Count => _count;

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _lists.Length; i++)
            {
                var list = _lists[i];

                for (int j = 0; j < list.Count; j++)
                {
                    yield return list[j];
                }
            }
        }

        public CombinedList<T> AddList(IReadOnlyList<T> list)
        {
            IReadOnlyList<T>[] array = new IReadOnlyList<T>[list.Count + 1];
            Array.Copy(_lists, array, array.Length);
            array[list.Count] = list;
            return new CombinedList<T>(array);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
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

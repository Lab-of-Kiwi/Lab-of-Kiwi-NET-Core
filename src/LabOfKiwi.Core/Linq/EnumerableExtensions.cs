using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LabOfKiwi.Linq;

public static class EnumerableExtensions
{
    public static IReadOnlyList<TSource> ToReadOnlyList<TSource>(this IEnumerable<TSource> source)
    {
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

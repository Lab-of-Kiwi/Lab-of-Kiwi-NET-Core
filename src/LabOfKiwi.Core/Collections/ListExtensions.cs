using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabOfKiwi.Collections
{
    public static class ListExtensions
    {
        public static void RemoveMiddleElement<T>(this IList<T> list)
        {
            ThrowHelper.NullCheck(nameof(list), list);

            if (list.Count == 0) return;

            list.RemoveAt(list.Count / 2);
        }

        public static void RemoveMiddleElementsUntil<T>(this IList<T> list, int targetCount)
        {
            ThrowHelper.NullCheck(nameof(list), list);
            ThrowHelper.RangeCheck(nameof(targetCount), targetCount, minValue: 0);

            if (targetCount == 0)
            {
                list.Clear();
                return;
            }

            while (list.Count > targetCount)
            {
                list.RemoveAt(list.Count / 2);
            }
        }
    }
}

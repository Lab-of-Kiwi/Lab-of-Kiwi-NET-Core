using System;
using System.Collections.Generic;

namespace LabOfKiwi.Collections;

public static class ListExtensions
{
    public static void SetLength<T>(this List<T> list, int value)
    {
        if (list == null)
        {
            throw new ArgumentNullException(nameof(list));
        }

        if (value == 0)
        {
            list.Clear();
            return;
        }

        int currentLength = list.Count;

        if ((uint)value > (uint)currentLength)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        int amountToRemove = currentLength - value;
        list.RemoveRange(value, amountToRemove);
    }
}

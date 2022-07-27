using System;
using System.Collections.Generic;

namespace LabOfKiwi;

public static class Enums
{
    public static Dictionary<TEnum, string> CreateCache<TEnum>() where TEnum : struct, Enum
    {
        return CreateCache<TEnum>(e => e.ToString());
    }

    public static Dictionary<TEnum, string> CreateCache<TEnum>(Func<TEnum, string> callback) where TEnum : struct, Enum
    {
        if (callback == null)
        {
            throw new ArgumentNullException(nameof(callback));
        }

        var cache = new Dictionary<TEnum, string>();

        foreach (var value in Enum.GetValues<TEnum>())
        {
            cache.Add(value, callback(value));
        }

        return cache;
    }
}

using System;
using System.Collections.Generic;

namespace LabOfKiwi;

public static class Enums
{
    public static Dictionary<TEnum, string> CreateDefaultCache<TEnum>() where TEnum : struct, Enum
    {
        return CreateCache<TEnum, string>(e => e.ToString());
    }

    public static Dictionary<TEnum, TOutput> CreateCache<TEnum, TOutput>(Func<TEnum, TOutput> callback)
        where TEnum : struct, Enum
        where TOutput : notnull
    {
        if (callback == null)
        {
            throw new ArgumentNullException(nameof(callback));
        }

        var cache = new Dictionary<TEnum, TOutput>();

        foreach (var value in Enum.GetValues<TEnum>())
        {
            cache.Add(value, callback(value));
        }

        return cache;
    }
}

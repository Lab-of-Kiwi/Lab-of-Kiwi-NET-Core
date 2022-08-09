using LabOfKiwi.Collections;
using System;
using System.Collections.Generic;

namespace LabOfKiwi;

/// <summary>
/// Helper methods for <see cref="Enum"/> types.
/// </summary>
public static class Enums
{
    /// <summary>
    /// Creates a <see cref="Dictionary{TKey, TValue}"/> mapping the values of <typeparamref name="TEnum"/> to their
    /// <see cref="Enum.ToString"/> values.
    /// </summary>
    /// 
    /// <typeparam name="TEnum">The type of <see cref="Enum"/>.</typeparam>
    /// <returns>
    ///     A <see cref="Dictionary{TKey, TValue}"/> mapping the values of <typeparamref name="TEnum"/> to their
    ///     <see cref="Enum.ToString"/> values.
    /// </returns>
    public static Dictionary<TEnum, string> CreateDefaultCache<TEnum>() where TEnum : struct, Enum
    {
        return CreateCache<TEnum, string>(e => e.ToString());
    }

    /// <summary>
    /// Creates a <see cref="Dictionary{TKey, TValue}"/> mapping the values of <typeparamref name="TEnum"/> to the
    /// values provided by a <see cref="Func{T, TResult}"/> callback delegate.
    /// </summary>
    /// 
    /// <typeparam name="TEnum">The type of <see cref="Enum"/>.</typeparam>
    /// <typeparam name="TOutput">
    ///     The type of output by the <see cref="Func{T, TResult}"/> callback delegate.
    /// </typeparam>
    /// <param name="callback">
    ///     The <see cref="Func{T, TResult}"/> delegate that accepts a value of type <typeparamref name="TEnum"/> and
    ///     provides the value of type <typeparamref name="TOutput"/>.
    /// </param>
    /// <returns>
    ///     A <see cref="Dictionary{TKey, TValue}"/> mapping the values of <typeparamref name="TEnum"/> to the values
    ///     provided by a <see cref="Func{T, TResult}"/> callback delegate.
    /// </returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="callback"/> is <c>null</c>.</exception>
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

    /// <summary>
    /// Creates a <see cref="Map{TKey, TValue}"/> mapping the values of <typeparamref name="TEnum"/> to their
    /// <see cref="Enum.ToString"/> values.
    /// </summary>
    /// 
    /// <typeparam name="TEnum">The type of <see cref="Enum"/>.</typeparam>
    /// <returns>
    ///     A <see cref="Map{TKey, TValue}"/> mapping the values of <typeparamref name="TEnum"/> to their
    ///     <see cref="Enum.ToString"/> values.
    /// </returns>
    public static Map<TEnum, string> CreateDefaultMapCache<TEnum>() where TEnum : struct, Enum
    {
        return CreateMapCache<TEnum, string>(e => e.ToString());
    }

    /// <summary>
    /// Creates a <see cref="Map{TKey, TValue}"/> mapping the values of <typeparamref name="TEnum"/> to the values
    /// provided by a <see cref="Func{T, TResult}"/> callback delegate.
    /// </summary>
    /// 
    /// <typeparam name="TEnum">The type of <see cref="Enum"/>.</typeparam>
    /// <typeparam name="TOutput">
    ///     The type of output by the <see cref="Func{T, TResult}"/> callback delegate.
    /// </typeparam>
    /// <param name="callback">
    ///     The <see cref="Func{T, TResult}"/> delegate that accepts a value of type <typeparamref name="TEnum"/> and
    ///     provides the value of type <typeparamref name="TOutput"/>.
    /// </param>
    /// <returns>
    ///     A <see cref="Map{TKey, TValue}"/> mapping the values of <typeparamref name="TEnum"/> to the values provided
    ///     by a <see cref="Func{T, TResult}"/> callback delegate.
    /// </returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="callback"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">
    ///     A duplicate value of type <typeparamref name="TOutput"/> is provided.
    /// </exception>
    public static Map<TEnum, TOutput> CreateMapCache<TEnum, TOutput>(Func<TEnum, TOutput> callback)
        where TEnum : struct, Enum
        where TOutput : notnull
    {
        if (callback == null)
        {
            throw new ArgumentNullException(nameof(callback));
        }

        var cache = new Map<TEnum, TOutput>();

        foreach (var value in Enum.GetValues<TEnum>())
        {
            cache.Add(value, callback(value));
        }

        return cache;
    }
}

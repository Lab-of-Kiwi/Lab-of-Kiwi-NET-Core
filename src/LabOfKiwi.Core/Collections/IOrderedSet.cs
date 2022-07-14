using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using System;

namespace LabOfKiwi.Collections;

/// <summary>
/// Represents a generic list of unique items.
/// </summary>
/// 
/// <typeparam name="T">The type of elements.</typeparam>
public interface IOrderedSet<T> : ISet<T>, IList<T>
{
    /// <summary>
    /// Gets the element at the specified index in the ordered set.
    /// </summary>
    /// 
    /// <param name="index">The zero-based index of the element to get.</param>
    /// <returns>The element at the specified index in the ordered set.</returns>
    /// 
    /// <exception cref="ArgumentOutOfRangeException"/>
    /// <exception cref="NotSupportedException"/>
    new T this[int index] { get; }

    /// <summary>
    /// Removes the element at the specified index in the ordered set.
    /// </summary>
    /// 
    /// <param name="index">The zero-based index of the element to remove.</param>
    /// <returns>The element that was removed.</returns>
    /// 
    /// <exception cref="ArgumentOutOfRangeException"/>
    /// <exception cref="NotSupportedException"/>
    new T RemoveAt(int index);

    /// <summary>
    /// Inserts an element at the specified index in the ordered set.
    /// </summary>
    /// 
    /// <param name="index">The zero-based index at which the element is to be inserted.</param>
    /// <param name="item">The element to be inserted.</param>
    /// <returns>
    ///     <c>true</c> if the element was inserted; otherwise, <c>false</c> indicating the element already exists in
    ///     the ordered set.
    /// </returns>
    /// 
    /// <exception cref="ArgumentOutOfRangeException"/>
    /// <exception cref="NotSupportedException"/>
    new bool Insert(int index, T item);

    /// <summary>
    /// Sets the element at the specified index.
    /// </summary>
    /// 
    /// <param name="index">The zero-based index at which the element is to be inserted.</param>
    /// <param name="item">The element to set at the zero-based index.</param>
    /// <returns>
    ///     <c>true</c> if the element was set; otherwise, <c>false</c> indicating the element already exists in the
    ///     ordered set.
    /// </returns>
    /// 
    /// <exception cref="ArgumentOutOfRangeException"/>
    bool Set(int index, T item);

    /// <summary>
    /// Sets the element at the specified index and provides the old value if successfuly set.
    /// </summary>
    /// 
    /// <param name="index">The zero-based index at which the element is to be inserted.</param>
    /// <param name="item">The element to set at the zero-based index.</param>
    /// <param name="oldValue">
    ///     The old value if successfuly set; otherwise, the default value of <typeparamref name="T"/>.
    /// </param>
    /// <returns>
    ///     <c>true</c> if the element was set; otherwise, <c>false</c> indicating the element already exists in the
    ///     ordered set.
    /// </returns>
    /// 
    /// <exception cref="ArgumentOutOfRangeException"/>
    bool Set(int index, T item, [MaybeNullWhen(false)] out T oldValue);
}

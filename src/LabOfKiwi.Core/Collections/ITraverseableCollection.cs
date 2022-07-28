using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LabOfKiwi.Collections;

public interface ITraverseableCollection<T> : IIndexedCollection<T>, IEnumerable<T>
{
    new int Index { get; set; }

    new int Remaining { get; set; }

    int Move(int count);

    int MoveToStart();

    int MoveToEnd();

    int MoveWhileTrue(Predicate<T> predicate);

    T[] Read(int count);

    int Read(Span<T> array);

    int Read(T[] array, int offset, int count);

    T[] ReadWhileTrue(Predicate<T> predicate);

    bool TryRead([MaybeNullWhen(false)] out T result);
}

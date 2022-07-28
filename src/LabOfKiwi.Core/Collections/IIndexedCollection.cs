using System;
using System.Diagnostics.CodeAnalysis;

namespace LabOfKiwi.Collections;

public interface IIndexedCollection<T>
{
    T this[int index] { get; }

    int Index { get; }

    int Length { get; }

    int Remaining { get; }

    bool NextEquals(T value);

    T[] Peek(int count);

    int Peek(Span<T> array);

    int Peek(T[] array, int offset, int count);

    T[] PeekAhead(int offset, int count);

    T[] PeekAt(int index, int count);

    T[] PeekWhileTrue(Predicate<T> predicate);

    bool TryPeek([MaybeNullWhen(false)] out T result);
}

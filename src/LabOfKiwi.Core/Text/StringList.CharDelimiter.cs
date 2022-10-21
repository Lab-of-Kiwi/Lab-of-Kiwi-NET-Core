using System;
using System.Collections.Generic;

namespace LabOfKiwi.Text;

public readonly partial struct StringList
{
    public StringList Append(string? value, char delimiter = ',', bool includeEmptyElements = false)
    {
        return Append(value, delimiter.ToString(), includeEmptyElements);
    }

    public StringList ChangeDelimiter(char newDelimiter, char oldDelimiter = ',', bool includeEmptyElements = false)
    {
        return ChangeDelimiter(newDelimiter.ToString(), oldDelimiter.ToString(), includeEmptyElements);
    }

    public StringList Compact(char delimiter = ',')
    {
        return Compact(delimiter.ToString());
    }

    public bool Contains(string? value, char delimiter = ',', bool includeEmptyElements = false, IEqualityComparer<string>? comparer = null)
    {
        return Contains(value, delimiter.ToString(), includeEmptyElements, comparer);
    }

    public List<TOutput> ConvertAll<TOutput>(Func<string, TOutput> converter, char delimiter = ',', bool includeEmptyElements = false)
    {
        return ConvertAll(converter, delimiter.ToString(), includeEmptyElements);
    }

    public List<TOutput> ConvertAll<TOutput>(Func<string, int, TOutput> converter, char delimiter = ',', bool includeEmptyElements = false)
    {
        return ConvertAll(converter, delimiter.ToString(), includeEmptyElements);
    }

    public int ElementCount(string? value, char delimiter = ',', bool includeEmptyElements = false, IEqualityComparer<string>? comparer = null)
    {
        return ElementCount(value, delimiter.ToString(), includeEmptyElements, comparer);
    }

    public bool Exists(Func<string, bool> match, char delimiter = ',', bool includeEmptyElements = false)
    {
        return Exists(match, delimiter.ToString(), includeEmptyElements);
    }

    public bool Exists(Func<string, int, bool> match, char delimiter = ',', bool includeEmptyElements = false)
    {
        return Exists(match, delimiter.ToString(), includeEmptyElements);
    }

    public string? Find(Func<string, bool> match, char delimiter = ',', bool includeEmptyElements = false)
    {
        return Find(match, delimiter.ToString(), includeEmptyElements);
    }

    public string? Find(Func<string, int, bool> match, char delimiter = ',', bool includeEmptyElements = false)
    {
        return Find(match, delimiter.ToString(), includeEmptyElements);
    }

    public StringList FindAll(Func<string, bool> match, char delimiter = ',', bool includeEmptyElements = false)
    {
        return FindAll(match, delimiter.ToString(), includeEmptyElements);
    }

    public StringList FindAll(Func<string, int, bool> match, char delimiter = ',', bool includeEmptyElements = false)
    {
        return FindAll(match, delimiter.ToString(), includeEmptyElements);
    }

    public int FindIndex(Func<string, bool> match, char delimiter = ',', bool includeEmptyElements = false)
    {
        return FindIndex(match, delimiter.ToString(), includeEmptyElements);
    }

    public int FindIndex(Func<string, int, bool> match, char delimiter = ',', bool includeEmptyElements = false)
    {
        return FindIndex(match, delimiter.ToString(), includeEmptyElements);
    }

    public string? FindLast(Func<string, bool> match, char delimiter = ',', bool includeEmptyElements = false)
    {
        return FindLast(match, delimiter.ToString(), includeEmptyElements);
    }

    public string? FindLast(Func<string, int, bool> match, char delimiter = ',', bool includeEmptyElements = false)
    {
        return FindLast(match, delimiter.ToString(), includeEmptyElements);
    }

    public int FindLastIndex(Func<string, bool> match, char delimiter = ',', bool includeEmptyElements = false)
    {
        return FindLastIndex(match, delimiter.ToString(), includeEmptyElements);
    }

    public int FindLastIndex(Func<string, int, bool> match, char delimiter = ',', bool includeEmptyElements = false)
    {
        return FindLastIndex(match, delimiter.ToString(), includeEmptyElements);
    }

    public void ForEach(Action<string> action, char delimiter = ',', bool includeEmptyElements = false)
    {
        ForEach(action, delimiter.ToString(), includeEmptyElements);
    }

    public void ForEach(Action<string, int> action, char delimiter = ',', bool includeEmptyElements = false)
    {
        ForEach(action, delimiter.ToString(), includeEmptyElements);
    }

    public string GetAt(int index, char delimiter = ',', bool includeEmptyElements = false)
    {
        return GetAt(index, delimiter.ToString(), includeEmptyElements);
    }

    public bool IndexExists(int index, char delimiter = ',', bool includeEmptyElements = false)
    {
        return IndexExists(index, delimiter.ToString(), includeEmptyElements);
    }

    public int IndexOf(string? value, char delimiter = ',', bool includeEmptyElements = false, IEqualityComparer<string>? comparer = null)
    {
        return IndexOf(value, delimiter.ToString(), includeEmptyElements, comparer);
    }

    public StringList InsertAt(int index, string? value, char delimiter = ',', bool includeEmptyElements = false)
    {
        return InsertAt(index, value, delimiter.ToString(), includeEmptyElements);
    }

    public int LastIndexOf(string? value, char delimiter = ',', bool includeEmptyElements = false, IEqualityComparer<string>? comparer = null)
    {
        return LastIndexOf(value, delimiter.ToString(), includeEmptyElements, comparer);
    }

    public int Length(char delimiter = ',', bool includeEmptyElements = false)
    {
        return Length(delimiter.ToString(), includeEmptyElements);
    }

    public StringList ModifyEach(Func<string, string> modifier, char delimiter = ',', ModifyOption modifyOption = ModifyOption.RemoveEmpty)
    {
        return ModifyEach(modifier, delimiter.ToString(), modifyOption);
    }

    public StringList ModifyEach(Func<string, int, string> modifier, char delimiter = ',', ModifyOption modifyOption = ModifyOption.RemoveEmpty)
    {
        return ModifyEach(modifier, delimiter.ToString(), modifyOption);
    }

    public StringList Prepend(string? value, char delimiter = ',', bool includeEmptyElements = false)
    {
        return Prepend(value, delimiter.ToString(), includeEmptyElements);
    }

    public StringList Qualify(string? qualifier, char delimiter = ',', bool includeEmptyElements = false)
    {
        return Qualify(qualifier, delimiter.ToString(), includeEmptyElements);
    }

    public StringList RemoveAt(int index, char delimiter = ',', bool includeEmptyElements = false)
    {
        return RemoveAt(index, delimiter.ToString(), includeEmptyElements);
    }

    public StringList RemoveAll(Func<string, bool> match, char delimiter = ',', bool includeEmptyElements = false)
    {
        return RemoveAll(match, delimiter.ToString(), includeEmptyElements);
    }

    public StringList RemoveRange(int startIndex, int count, char delimiter = ',', bool includeEmptyElements = false)
    {
        return RemoveRange(startIndex, count, delimiter.ToString(), includeEmptyElements);
    }

    public StringList Reverse(char delimiter = ',', bool includeEmptyElements = false)
    {
        return Reverse(delimiter.ToString(), includeEmptyElements);
    }

    public StringList SetAt(int index, string? value, char delimiter = ',', bool includeEmptyElements = false)
    {
        return SetAt(index, value, delimiter.ToString(), includeEmptyElements);
    }

    public StringList Slice(int startIndex, char delimiter = ',', bool includeEmptyElements = false)
    {
        return Slice(startIndex, delimiter.ToString(), includeEmptyElements);
    }

    public StringList Slice(int startIndex, int count, char delimiter = ',', bool includeEmptyElements = false)
    {
        return Slice(startIndex, count, delimiter.ToString(), includeEmptyElements);
    }

    public StringList Sort(char delimiter = ',', bool includeEmptyElements = false, IComparer<string>? comparer = null)
    {
        return Sort(delimiter.ToString(), includeEmptyElements, comparer);
    }

    public string[] ToArray(char delimiter = ',', bool includeEmptyElements = false)
    {
        return ToArray(delimiter.ToString(), includeEmptyElements);
    }

    public List<string> ToList(char delimiter = ',', bool includeEmptyElements = false)
    {
        return ToList(delimiter.ToString(), includeEmptyElements);
    }

    public StringList TrimElements(char delimiter = ',', bool includeEmptyElements = false)
    {
        return TrimElements(delimiter.ToString(), includeEmptyElements);
    }

    public bool TrueForAll(Func<string, bool> match, char delimiter = ',', bool includeEmptyElements = false)
    {
        return TrueForAll(match, delimiter.ToString(), includeEmptyElements);
    }
}

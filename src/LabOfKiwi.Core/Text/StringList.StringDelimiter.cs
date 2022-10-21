using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LabOfKiwi.Text;

public readonly partial struct StringList
{
    public StringList Append(string? value, string? delimiter, bool includeEmptyElements = false)
    {
        return AppendPrepend(value, delimiter, includeEmptyElements, false);
    }

    public StringList ChangeDelimiter(string? newDelimiter, string? oldDelimiter, bool includeEmptyElements = false)
    {
        if (includeEmptyElements)
        {
            newDelimiter ??= string.Empty;
            oldDelimiter ??= string.Empty;

            if (newDelimiter == oldDelimiter)
            {
                return this;
            }
        }

        ElementList listElements = new(this, oldDelimiter, includeEmptyElements);
        return new StringList(listElements, newDelimiter);
    }

    public StringList Compact(string? delimiter)
    {
        if (string.IsNullOrEmpty(delimiter))
        {
            return this;
        }

        string list = this;

        if (delimiter.Length == 1)
        {
            list = list.Trim(delimiter[0]);
        }
        else
        {
            while (list.StartsWith(delimiter))
            {
                list = list[delimiter.Length..];
            }

            while (list.EndsWith(delimiter))
            {
                list = list[0..^delimiter.Length];
            }
        }

        return new StringList(list);
    }

    public bool Contains(string? value, string? delimiter, bool includeEmptyElements = false, IEqualityComparer<string>? comparer = null)
    {
        return IndexOf(value, delimiter, includeEmptyElements, comparer) >= 0;
    }

    public List<TOutput> ConvertAll<TOutput>(Func<string, TOutput> converter, string? delimiter, bool includeEmptyElements = false)
    {
        if (converter == null)
        {
            throw new ArgumentNullException(nameof(converter));
        }

        ElementList listElements = new(this, delimiter, includeEmptyElements);
        List<TOutput> list = new(listElements.Count);

        foreach (string e in listElements)
        {
            list.Add(converter(e));
        }

        return list;
    }

    public List<TOutput> ConvertAll<TOutput>(Func<string, int, TOutput> converter, string? delimiter, bool includeEmptyElements = false)
    {
        if (converter == null)
        {
            throw new ArgumentNullException(nameof(converter));
        }

        ElementList listElements = new(this, delimiter, includeEmptyElements);
        List<TOutput> list = new(listElements.Count);

        for (int i = 0; i < listElements.Count; i++)
        {
            list.Add(converter(listElements[i], i));
        }

        return list;
    }

    public int ElementCount(string? value, string? delimiter, bool includeEmptyElements = false, IEqualityComparer<string>? comparer = null)
    {
        if (!includeEmptyElements && string.IsNullOrEmpty(value))
        {
            return 0;
        }

        ElementList listElements = new(this, delimiter, includeEmptyElements);
        int count = 0;
        value ??= string.Empty;
        comparer ??= EqualityComparer<string>.Default;

        foreach (string element in listElements)
        {
            if (comparer.Equals(value, element))
            {
                count++;
            }
        }

        return count;
    }

    public bool Exists(Func<string, bool> match, string? delimiter, bool includeEmptyElements = false)
    {
        return FindIndex(match, delimiter, includeEmptyElements) >= 0;
    }

    public bool Exists(Func<string, int, bool> match, string? delimiter, bool includeEmptyElements = false)
    {
        return FindIndex(match, delimiter, includeEmptyElements) >= 0;
    }

    public string? Find(Func<string, bool> match, string? delimiter, bool includeEmptyElements = false)
    {
        if (match == null)
        {
            throw new ArgumentNullException(nameof(match));
        }

        ElementList listElements = new(this, delimiter, includeEmptyElements);

        foreach (string e in listElements)
        {
            if (match(e))
            {
                return e;
            }
        }

        return null;
    }

    public string? Find(Func<string, int, bool> match, string? delimiter, bool includeEmptyElements = false)
    {
        if (match == null)
        {
            throw new ArgumentNullException(nameof(match));
        }

        ElementList listElements = new(this, delimiter, includeEmptyElements);

        for (int i = 0; i < listElements.Count; i++)
        {
            string e = listElements[i];

            if (match(e, i))
            {
                return e;
            }
        }

        return null;
    }

    public StringList FindAll(Func<string, bool> match, string? delimiter, bool includeEmptyElements = false)
    {
        if (match == null)
        {
            throw new ArgumentNullException(nameof(match));
        }

        ElementList listElements = new(this, delimiter, includeEmptyElements);
        ElementList newElements = new(listElements.Count);

        foreach (string element in listElements)
        {
            if (match(element))
            {
                newElements.Add(element);
            }
        }

        return new StringList(newElements, delimiter);
    }

    public StringList FindAll(Func<string, int, bool> match, string? delimiter, bool includeEmptyElements = false)
    {
        if (match == null)
        {
            throw new ArgumentNullException(nameof(match));
        }

        ElementList listElements = new(this, delimiter, includeEmptyElements);
        ElementList newElements = new(listElements.Count);

        for (int i = 0; i < listElements.Count; i++)
        {
            string element = listElements[i];

            if (match(element, i))
            {
                newElements.Add(element);
            }
        }

        return new StringList(newElements, delimiter);
    }

    public int FindIndex(Func<string, bool> match, string? delimiter, bool includeEmptyElements = false)
    {
        if (match == null)
        {
            throw new ArgumentNullException(nameof(match));
        }

        ElementList listElements = new(this, delimiter, includeEmptyElements);

        for (int i = 0; i < listElements.Count; i++)
        {
            if (match(listElements[i]))
            {
                return i;
            }
        }

        return -1;
    }

    public int FindIndex(Func<string, int, bool> match, string? delimiter, bool includeEmptyElements = false)
    {
        if (match == null)
        {
            throw new ArgumentNullException(nameof(match));
        }

        ElementList listElements = new(this, delimiter, includeEmptyElements);

        for (int i = 0; i < listElements.Count; i++)
        {
            if (match(listElements[i], i))
            {
                return i;
            }
        }

        return -1;
    }

    public string? FindLast(Func<string, bool> match, string? delimiter, bool includeEmptyElements = false)
    {
        if (match == null)
        {
            throw new ArgumentNullException(nameof(match));
        }

        ElementList listElements = new(this, delimiter, includeEmptyElements);

        for (int i = listElements.Count - 1; i >= 0; i--)
        {
            string e = listElements[i];

            if (match(e))
            {
                return e;
            }
        }

        return null;
    }

    public string? FindLast(Func<string, int, bool> match, string? delimiter, bool includeEmptyElements = false)
    {
        if (match == null)
        {
            throw new ArgumentNullException(nameof(match));
        }

        ElementList listElements = new(this, delimiter, includeEmptyElements);

        for (int i = listElements.Count - 1; i >= 0; i--)
        {
            string e = listElements[i];

            if (match(e, i))
            {
                return e;
            }
        }

        return null;
    }

    public int FindLastIndex(Func<string, bool> match, string? delimiter, bool includeEmptyElements = false)
    {
        if (match == null)
        {
            throw new ArgumentNullException(nameof(match));
        }

        ElementList listElements = new(this, delimiter, includeEmptyElements);

        for (int i = listElements.Count - 1; i >= 0; i--)
        {
            if (match(listElements[i]))
            {
                return i;
            }
        }

        return -1;
    }

    public int FindLastIndex(Func<string, int, bool> match, string? delimiter, bool includeEmptyElements = false)
    {
        if (match == null)
        {
            throw new ArgumentNullException(nameof(match));
        }

        ElementList listElements = new(this, delimiter, includeEmptyElements);

        for (int i = listElements.Count - 1; i >= 0; i--)
        {
            if (match(listElements[i], i))
            {
                return i;
            }
        }

        return -1;
    }

    public void ForEach(Action<string> action, string? delimiter, bool includeEmptyElements = false)
    {
        if (action == null)
        {
            throw new ArgumentNullException(nameof(action));
        }

        ElementList listElements = new(this, delimiter, includeEmptyElements);

        foreach (string e in listElements)
        {
            action(e);
        }
    }

    public void ForEach(Action<string, int> action, string? delimiter, bool includeEmptyElements = false)
    {
        if (action == null)
        {
            throw new ArgumentNullException(nameof(action));
        }

        ElementList listElements = new(this, delimiter, includeEmptyElements);

        for (int i = 0; i < listElements.Count; i++)
        {
            action(listElements[i], i);
        }
    }

    public string GetAt(int index, string? delimiter, bool includeEmptyElements = false)
    {
        ElementList listElements = new(this, delimiter, includeEmptyElements);
        return listElements[index];
    }

    public bool IndexExists(int index, string? delimiter, bool includeEmptyElements = false)
    {
        if (index < 0)
        {
            return false;
        }

        return index < Length(delimiter, includeEmptyElements);
    }

    public int IndexOf(string? value, string? delimiter, bool includeEmptyElements = false, IEqualityComparer<string>? comparer = null)
    {
        if (includeEmptyElements || !string.IsNullOrEmpty(value))
        {
            ElementList listElements = new(this, delimiter, includeEmptyElements);
            return listElements.IndexOf(value ?? string.Empty, comparer);
        }

        return -1;
    }

    public StringList InsertAt(int index, string? value, string? delimiter, bool includeEmptyElements = false)
    {
        ElementList listElements = new(this, delimiter, includeEmptyElements);
        ElementList valueElements = new(value, delimiter, includeEmptyElements);
        listElements.InsertRange(index, valueElements);
        return new StringList(listElements, delimiter);
    }

    public int LastIndexOf(string? value, string? delimiter, bool includeEmptyElements = false, IEqualityComparer<string>? comparer = null)
    {
        if (includeEmptyElements || !string.IsNullOrEmpty(value))
        {
            ElementList listElements = new(this, delimiter, includeEmptyElements);
            return listElements.LastIndexOf(value ?? string.Empty, comparer);
        }

        return -1;
    }

    public int Length(string? delimiter, bool includeEmptyElements = false)
    {
        ElementList listElements = new(this, delimiter, includeEmptyElements);
        return listElements.Count;
    }

    public StringList ModifyEach(Func<string, string> modifier, string? delimiter, ModifyOption modifyOption = ModifyOption.RemoveEmpty)
    {
        if (modifier == null)
        {
            throw new ArgumentNullException(nameof(modifier));
        }

        bool includeBefore = modifyOption.HasFlag(ModifyOption.IncludeEmptyBefore);
        bool includeAfter = !modifyOption.HasFlag(ModifyOption.IncludeEmptyAfter);

        ElementList listElements = new(this, delimiter, includeBefore);

        if (!includeAfter)
        {
            ElementList newElements = new(listElements.Count);

            foreach (var currElement in listElements)
            {
                string newElement = modifier(currElement);

                if (!string.IsNullOrEmpty(newElement))
                {
                    newElements.Add(newElement);
                }
            }

            listElements = newElements;
        }
        else
        {
            for (int i = 0; i < listElements.Count; i++)
            {
                string currElement = listElements[i];
                string newElement = modifier(currElement);
                listElements[i] = newElement;
            }
        }

        return new StringList(listElements, delimiter);
    }

    public StringList ModifyEach(Func<string, int, string> modifier, string? delimiter, ModifyOption modifyOption = ModifyOption.RemoveEmpty)
    {
        if (modifier == null)
        {
            throw new ArgumentNullException(nameof(modifier));
        }

        bool includeBefore = modifyOption.HasFlag(ModifyOption.IncludeEmptyBefore);
        bool includeAfter = !modifyOption.HasFlag(ModifyOption.IncludeEmptyAfter);

        ElementList listElements = new(this, delimiter, includeBefore);

        if (!includeAfter)
        {
            ElementList newElements = new(listElements.Count);

            for (int i = 0; i < listElements.Count; i++)
            {
                string currElement = listElements[i];
                string newElement = modifier(currElement, i);

                if (!string.IsNullOrEmpty(newElement))
                {
                    newElements.Add(newElement);
                }
            }

            listElements = newElements;
        }
        else
        {
            for (int i = 0; i < listElements.Count; i++)
            {
                string currElement = listElements[i];
                string newElement = modifier(currElement, i);
                listElements[i] = newElement;
            }
        }

        return new StringList(listElements, delimiter);
    }

    public StringList Prepend(string? value, string? delimiter, bool includeEmptyElements = false)
    {
        return AppendPrepend(value, delimiter, includeEmptyElements, true);
    }

    public StringList Qualify(string? qualifier, string? delimiter, bool includeEmptyElements = false)
    {
        qualifier ??= string.Empty;

        ElementList listElements = new(this, delimiter, includeEmptyElements);

        for (int i = 0; i < listElements.Count; i++)
        {
            string currElement = listElements[i];
            string newElement = $"{qualifier}{currElement}{qualifier}";
            listElements[i] = newElement;
        }

        return new StringList(listElements, delimiter);
    }

    public StringList RemoveAt(int index, string? delimiter, bool includeEmptyElements = false)
    {
        ElementList listElements = new(this, delimiter, includeEmptyElements);
        listElements.RemoveAt(index);
        return new StringList(listElements, delimiter);
    }

    public StringList RemoveAll(Func<string, bool> match, string? delimiter, bool includeEmptyElements = false)
    {
        if (match == null)
        {
            throw new ArgumentNullException(nameof(match));
        }

        ElementList listElements = new(this, delimiter, includeEmptyElements);
        listElements.RemoveAll(e => match(e));
        return new StringList(listElements, delimiter);
    }

    public StringList RemoveRange(int startIndex, int count, string? delimiter, bool includeEmptyElements = false)
    {
        ElementList listElements = new(this, delimiter, includeEmptyElements);
        listElements.RemoveRange(startIndex, count);
        return new StringList(listElements, delimiter);
    }

    public StringList Reverse(string? delimiter, bool includeEmptyElements = false)
    {
        ElementList listElements = new(this, delimiter, includeEmptyElements);
        listElements.Reverse();
        return new StringList(listElements, delimiter);
    }

    public StringList SetAt(int index, string? value, string? delimiter, bool includeEmptyElements = false)
    {
        ElementList listElements = new(this, delimiter, includeEmptyElements);
        ElementList valueElements = new(value, delimiter, includeEmptyElements);

        if (valueElements.Count == 0)
        {
            listElements.RemoveAt(index);
        }
        if (valueElements.Count == 1)
        {
            listElements[index] = valueElements[0];
        }
        else
        {
            listElements.RemoveAt(index);
            listElements.InsertRange(index, valueElements);
        }

        return new StringList(listElements, delimiter);
    }

    public StringList Slice(int startIndex, string? delimiter, bool includeEmptyElements = false)
    {
        ElementList listElements = new(this, delimiter, includeEmptyElements);
        listElements = listElements[startIndex..];
        return new StringList(listElements, delimiter);
    }

    public StringList Slice(int startIndex, int count, string? delimiter, bool includeEmptyElements = false)
    {
        ElementList listElements = new(this, delimiter, includeEmptyElements);
        int endIndex = startIndex + count;
        listElements = listElements[startIndex..endIndex];
        return new StringList(listElements, delimiter);
    }

    public StringList Sort(string? delimiter, bool includeEmptyElements = false, IComparer<string>? comparer = null)
    {
        ElementList listElements = new(this, delimiter, includeEmptyElements);
        listElements.Sort(comparer);
        return new StringList(listElements, delimiter);
    }

    public string[] ToArray(string? delimiter, bool includeEmptyElements = false)
    {
        return ToList(delimiter, includeEmptyElements).ToArray();
    }

    public List<string> ToList(string? delimiter, bool includeEmptyElements = false)
    {
        ElementList listElements = new(this, delimiter, includeEmptyElements);
        return listElements.ToList();
    }

    public StringList TrimElements(string? delimiter, bool includeEmptyElements = false)
    {
        string list = this;

        StringSplitOptions options = StringSplitOptions.TrimEntries;

        if (!includeEmptyElements)
        {
            options |= StringSplitOptions.RemoveEmptyEntries;
        }

        string[] elements = list.Split(delimiter, options);
        list = string.Join(delimiter, elements);
        return new StringList(list);
    }

    public bool TrueForAll(Func<string, bool> match, string? delimiter, bool includeEmptyElements = false)
    {
        if (match == null)
        {
            throw new ArgumentNullException(nameof(match));
        }

        ElementList listElements = new(this, delimiter, includeEmptyElements);
        return listElements.TrueForAll(e => match(e));
    }

    private StringList AppendPrepend(string? value, string? delimiter, bool includeEmptyElements, bool isPrepending)
    {
        ElementList listElements  = new(this, delimiter, includeEmptyElements);
        ElementList valueElements = new(value, delimiter, includeEmptyElements);

        ElementList head = isPrepending ? valueElements : listElements;
        ElementList tail = isPrepending ? listElements  : valueElements;

        head.AddRange(tail);
        return new StringList(head, delimiter);
    }
}

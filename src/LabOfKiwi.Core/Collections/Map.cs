using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace LabOfKiwi.Collections;

public class Map<T1, T2> where T1 : notnull where T2 : notnull
{
    private readonly Dictionary<T1, T2> _forward;
    private readonly Dictionary<T2, T1> _reverse;

    public Map()
    {
        _forward = new Dictionary<T1, T2>();
        _reverse = new Dictionary<T2, T1>();
    }

    public T2 this[T1 key]
    {
        get => _forward[key];
    }

    public T1 this[T2 key]
    {
        get => _reverse[key];
    }

    public void Add(T1 value1, T2 value2)
    {
        _forward.Add(value1, value2);

        try
        {
            _reverse.Add(value2, value1);
        }
        catch
        {
            _forward.Remove(value1);
            throw;
        }
    }

    public void Clear()
    {
        _forward.Clear();
        _reverse.Clear();
    }

    public bool Contains(T1 key)
        => _forward.ContainsKey(key);

    public bool Contains(T2 key)
        => _reverse.ContainsKey(key);

    public bool Remove(T1 key)
    {
        if (_forward.TryGetValue(key, out T2? value))
        {
            bool result = _forward.Remove(key) & _reverse.Remove(value);
            Debug.Assert(result);
            return true;
        }

        return false;
    }

    public bool TryGetValue(T1 key, [MaybeNullWhen(false)] out T2 value)
        => _forward.TryGetValue(key, out value);

    public bool TryGetValue(T2 key, [MaybeNullWhen(false)] out T1 value)
        => _reverse.TryGetValue(key, out value);
}

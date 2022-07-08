using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace LabOfKiwi.Web.Html.Attributes;

internal sealed class CustomAttributes : IReadOnlyDictionary<string, string>, IDictionary<string, string>
{
    private readonly string _prefix;
    private readonly AttributeCollection _rawAttributes;

    private enum InsertionBehavior
    {
        None,
        OverwriteExisting,
        ThrowOnExisting
    }

    public CustomAttributes(string prefix, AttributeCollection rawAttributes)
    {
        Debug.Assert(prefix != null && IsValid(prefix));
        Debug.Assert(rawAttributes != null);
        _prefix = prefix;
        _rawAttributes = rawAttributes;
    }

    #region Public Properties
    public int Count => FilteredRawAttributes().Count();

    public bool IsReadOnly => false;

    public ICollection<string> Keys => FilteredRawAttributes()
        .Select(e => e.Key[_prefix.Length..]).ToArray();

    public ICollection<string> Values => FilteredRawAttributes()
        .Select(e => e.Value).ToArray();

    public string this[string name]
    {
        get
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (IsValid(name))
            {
                string? value = _rawAttributes[_prefix + name];

                if (value != null)
                {
                    return value;
                }
            }

            throw new KeyNotFoundException(string.Format("The given key '{0}' was not present in the dictionary.", name));
        }

        set
        {
            bool modified = TryInsert(name, value, InsertionBehavior.OverwriteExisting);
            Debug.Assert(modified);
        }
    }
    #endregion

    #region Public Methods
    public void Add(string name, string value)
    {
        bool modified = TryInsert(name, value, InsertionBehavior.ThrowOnExisting);
        Debug.Assert(modified);
    }

    public void Clear()
    {
        string[] keysToRemove = FilteredRawAttributes().Select(e => e.Key).ToArray();

        for (int i = 0; i < keysToRemove.Length; i++)
        {
            _rawAttributes[keysToRemove[i]] = null;
        }
    }

    public bool ContainsKey(string name)
    {
        if (name == null || !IsValid(name))
        {
            return false;
        }

        return _rawAttributes[_prefix + name] != null;
    }

    public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
    {
        foreach (var entry in FilteredRawAttributes())
        {
            yield return new KeyValuePair<string, string>(entry.Key[_prefix.Length..], entry.Value);
        }
    }

    public bool Remove(string name)
    {
        if (name == null || !IsValid(name))
        {
            return false;
        }

        string? oldValue = _rawAttributes[_prefix + name];

        if (oldValue == null)
        {
            return false;
        }

        _rawAttributes[_prefix + name] = null;
        return true;
    }

    public bool TryGetValue(string name, [MaybeNullWhen(false)] out string value)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        ThrowIfNotValid(name);

        value = _rawAttributes[_prefix + name];
        return value != null;
    }
    #endregion

    #region Explicit Interface Implementations
    IEnumerable<string> IReadOnlyDictionary<string, string>.Keys => Keys.ToList().AsReadOnly();

    IEnumerable<string> IReadOnlyDictionary<string, string>.Values => Values.ToList().AsReadOnly();

    void ICollection<KeyValuePair<string, string>>.Add(KeyValuePair<string, string> item)
    {
        Add(item.Key, item.Value);
    }

    bool ICollection<KeyValuePair<string, string>>.Contains(KeyValuePair<string, string> item)
    {
        string name = item.Key;
        string value = item.Value;

        if (name == null || value == null || !IsValid(name))
        {
            return false;
        }

        return value == _rawAttributes[_prefix + name];
    }

    void ICollection<KeyValuePair<string, string>>.CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
    {
        var thisArray = this.ToArray();
        Array.Copy(thisArray, 0, array, arrayIndex, thisArray.Length);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    bool ICollection<KeyValuePair<string, string>>.Remove(KeyValuePair<string, string> item)
    {
        string name = item.Key;
        string value = item.Value;

        if (name == null || value == null  || !IsValid(name))
        {
            return false;
        }

        if (value != _rawAttributes[_prefix + name])
        {
            return false;
        }

        _rawAttributes[_prefix + name] = null;
        return true;
    }
    #endregion

    private bool TryInsert(string name, string value, InsertionBehavior insertionBehavior)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        ThrowIfNotValid(name);

        string key = _prefix + name;

        if (insertionBehavior != InsertionBehavior.OverwriteExisting)
        {
            string? oldValue = _rawAttributes[key];

            if (oldValue != null)
            {
                if (insertionBehavior == InsertionBehavior.ThrowOnExisting)
                {
                    throw new ArgumentException(string.Format("An item with the same key has already been added. Key: {0}", name));
                }

                Debug.Assert(insertionBehavior == InsertionBehavior.None);
                return false;
            }
        }

        _rawAttributes[key] = value;
        return true;
    }

    private IEnumerable<KeyValuePair<string, string>> FilteredRawAttributes()
        => _rawAttributes.Where(e => e.Key.StartsWith(_prefix));

    private void ThrowIfNotValid(string name)
    {
        if (!IsValid(name))
        {
            throw new ArgumentException($"Invalid attribute name: {_prefix}{name}.");
        }
    }

    private static bool IsValid(string name)
        => !(name.Length == 0 || HtmlHelper.HasASCIIWhitespace(name));
}

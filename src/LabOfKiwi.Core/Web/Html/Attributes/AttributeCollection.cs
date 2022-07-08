using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace LabOfKiwi.Web.Html.Attributes;

public sealed class AttributeCollection : IReadOnlyCollection<KeyValuePair<string, string>>
{
    private readonly List<KeyValuePair<string, string>> _values;

    internal AttributeCollection()
    {
        _values = new List<KeyValuePair<string, string>>();
    }

    public int Count => _values.Count;

    public string? this[string attributeName]
    {
        get
        {
            Debug.Assert(attributeName != null);

            foreach (var entry in _values)
            {
                if (entry.Key == attributeName)
                {
                    return entry.Value;
                }
            }

            return null;
        }

        set
        {
            Debug.Assert(attributeName != null);

            for (int i = 0; i < _values.Count; i++)
            {
                var entry = _values[i];

                if (entry.Key == attributeName)
                {
                    if (value == null)
                    {
                        _values.RemoveAt(i);
                    }
                    else if (entry.Value != value)
                    {
                        _values[i] = new KeyValuePair<string, string>(attributeName, value);
                    }

                    return;
                }
            }

            if (value != null)
            {
                _values.Add(new KeyValuePair<string, string>(attributeName, value));
            }
        }
    }

    #region Public Getters
    public bool GetBoolean(string attributeName)
    {
        string? value = this[attributeName];

        if (value == null)
        {
            return false;
        }

        if (value.Length != 0)
        {
            HtmlHelper.ThrowInvalidAttributeStateException(attributeName, "Expected empty string.");
        }

        return true;
    }

    public TValue? GetEnum<TValue>(string attributeName) where TValue : struct, Enum
        => GetStruct<EnumParser<TValue>, TValue>(attributeName);

    public IList<TValue> GetList<TParser, TValue>(string attributeName, string delimiter = " ", bool allowDuplicates = false)
        where TParser : Parser<TValue>, new()
    {
        return CreateParser<TParser, TValue>(attributeName).AsList(delimiter, allowDuplicates);
    }

    public long? GetLong(string attributeName)
    {
        var parser = CreateParser<LongParser, long>(attributeName);
        return GetStruct(parser);
    }

    public long? GetLong(string attributeName, long min = long.MinValue, long max = long.MaxValue)
    {
        Parser<long> parser;

        if (min == long.MinValue && max == long.MaxValue)
        {
            parser = CreateParser<LongParser, long>(attributeName);
        }
        else
        {
            parser = new RangeLongParser(min, max);
            SetupParser(parser, attributeName);
        }

        return GetStruct(parser);
    }

    public bool? GetNullBoolean(string attributeName, string trueString = "true", string falseString = "false")
    {
        var parser = new BooleanParser(trueString, falseString);
        SetupParser(parser, attributeName);
        return GetStruct(parser);
    }

    public TValue? GetObject<TParser, TValue>(string attributeName)
        where TParser : Parser<TValue>, new()
        where TValue : class
    {
        var parser = CreateParser<TParser, TValue>(attributeName);
        return GetObject(parser);
    }

    public TValue? GetStruct<TParser, TValue>(string attributeName)
        where TParser : Parser<TValue>, new()
        where TValue : struct
    {
        var parser = CreateParser<TParser, TValue>(attributeName);
        return GetStruct(parser);
    }
    #endregion

    #region Public Setters
    public void SetBoolean(string attributeName, bool value)
    {
        this[attributeName] = value ? string.Empty : null;
    }

    public void SetEnum<TValue>(string attributeName, TValue? value) where TValue : struct, Enum
    {
        var parser = CreateParser<EnumParser<TValue>, TValue>(attributeName);
        SetStruct(parser, value);
    }

    public void SetLong(string attributeName, long? value)
    {
        var parser = CreateParser<LongParser, long>(attributeName);
        SetStruct(parser, value);
    }

    public void SetLong(string attributeName, long? value, long min = long.MinValue, long max = long.MaxValue)
    {
        Parser<long> parser;

        if (min == long.MinValue && max == long.MaxValue)
        {
            parser = CreateParser<LongParser, long>(attributeName);
        }
        else
        {
            parser = new RangeLongParser(min, max);
            SetupParser(parser, attributeName);
        }

        SetStruct(parser, value);
    }

    public void SetNullBoolean(string attributeName, bool? value, string trueString = "true", string falseString = "false")
    {
        var parser = new BooleanParser(trueString, falseString);
        SetupParser(parser, attributeName);
        SetStruct(parser, value);
    }

    public void SetObject<TParser, TValue>(string attributeName, TValue? value)
        where TParser : Parser<TValue>, new()
        where TValue : class
    {
        var parser = CreateParser<TParser, TValue>(attributeName);
        SetObject(parser, value);
    }

    public void SetStruct<TParser, TValue>(string attributeName, TValue? value)
        where TParser : Parser<TValue>, new()
        where TValue : struct
    {
        var parser = CreateParser<TParser, TValue>(attributeName);
        SetStruct(parser, value);
    }
    #endregion

    private TParser CreateParser<TParser, TValue>(string attributeName) where TParser : Parser<TValue>, new()
    {
        var parser = new TParser();
        SetupParser(parser, attributeName);
        return parser;
    }

    private void SetupParser<TValue>(Parser<TValue> parser, string attributeName)
    {
        Debug.Assert(attributeName != null);
        parser.RawAttributes = this;
        parser.Name = attributeName;
    }

    private static TValue? GetObject<TValue>(Parser<TValue> parser) where TValue : class
    {
        if (parser.TryGet(out TValue? value))
        {
            return value;
        }

        return null;
    }

    private static TValue? GetStruct<TValue>(Parser<TValue> parser) where TValue : struct
    {
        if (parser.TryGet(out TValue value))
        {
            return value;
        }

        return null;
    }

    private void SetObject<TValue>(Parser<TValue> parser, TValue? value) where TValue : class
    {
        if (value != null)
        {
            parser.Set(value);
        }
        else
        {
            this[parser.Name] = null;
        }
    }

    private void SetStruct<TValue>(Parser<TValue> parser, TValue? value) where TValue : struct
    {
        if (value.HasValue)
        {
            parser.Set(value.Value);
        }
        else
        {
            this[parser.Name] = null;
        }
    }

    public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
    {
        foreach (var entry in _values)
        {
            yield return entry;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace LabOfKiwi.Web.Html.Attributes;

public abstract class Parser<T>
{
    protected Parser()
    {
    }

    internal bool TryGet([MaybeNullWhen(false)] out T value)
    {
        string? rawValue = RawAttributes[Name];

        if (rawValue != null)
        {
            value = Parse(rawValue);
            return true;
        }

        value = default;
        return false;
    }

    internal void Set(T value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (!IsValid(value))
        {
            HtmlHelper.ThrowInvalidAttributeValueException(Name, value);
        }

        RawAttributes[Name] = ToString(value);
    }

    protected internal AttributeCollection RawAttributes { get; internal set; } = null!;

    protected internal string Name { get; internal set; } = null!;

    private T Parse(string rawValue)
    {
        Debug.Assert(rawValue != null);

        T value = InternalParse(rawValue);
        Debug.Assert(IsValid(value));

        return value;
    }

    private string ToString(T value)
    {
        Debug.Assert(value != null && IsValid(value));
        
        string rawValue = InternalToString(value);
        Debug.Assert(rawValue != null);

        return rawValue;
    }

    public virtual bool IsValid(T value)
        => true;

    protected virtual T InternalParse(string rawValue)
    {
        HtmlHelper.ThrowInvalidAttributeStateException(Name, $"Invalid value of '{rawValue}' is set.");
        return default!;
    }

    protected abstract string InternalToString(T value);

    internal IList<T> AsList(string delimiter, bool allowDuplicates)
        => new ParserList(this, delimiter, allowDuplicates);

    private sealed class ParserList : IList<T>
    {
        private readonly Parser<T> _parser;
        private readonly string _delimiter;
        private readonly bool _allowDuplicates;

        public ParserList(Parser<T> parser, string delimeter, bool allowDuplicates)
        {
            Debug.Assert(!string.IsNullOrEmpty(delimeter));
            _parser = parser;
            _delimiter = delimeter;
            _allowDuplicates = allowDuplicates;
        }

        public int Count => RawList.Count;

        public bool IsReadOnly => false;

        public T this[int index]
        {
            get => _parser.Parse(RawList[index]);

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (!_parser.IsValid(value))
                {
                    HtmlHelper.ThrowInvalidAttributeValueException(_parser.Name, value);
                }

                string rawValue = _parser.ToString(value);

                IList<string> rawList = RawList;

                if (_allowDuplicates)
                {
                    rawList[index] = rawValue;
                }
                else if (rawList[index] == rawValue)
                {
                    return;
                }
                else
                {
                    int indexOfOther = rawList.IndexOf(rawValue);
                    Debug.Assert(index != indexOfOther);

                    rawList[index] = rawValue;

                    if (indexOfOther >= 0)
                    {
                        rawList.RemoveAt(indexOfOther);
                    }
                }

                SetList(rawList);
            }
        }

        public void Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (!_parser.IsValid(item))
            {
                HtmlHelper.ThrowInvalidAttributeValueException(_parser.Name, item);
            }

            string rawValue = _parser.ToString(item);

            IList<string> rawList = RawList;

            if (_allowDuplicates || rawList.Count == 0)
            {
                rawList.Add(rawValue);
            }
            else if (rawList[^1] == rawValue)
            {
                return;
            }
            else
            {
                int indexOfOther = rawList.IndexOf(rawValue);
                Debug.Assert(indexOfOther != rawList.Count - 1);

                rawList.Add(rawValue);
                
                if (indexOfOther >= 0)
                {
                    rawList.RemoveAt(indexOfOther);
                }
            }

            SetList(rawList);
        }

        public void Clear()
            => _parser.RawAttributes[_parser.Name] = null;

        public bool Contains(T item)
        {
            if (item == null || !_parser.IsValid(item))
            {
                return false;
            }

            string rawValue = _parser.ToString(item);
            return RawList.Contains(rawValue);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            T[] thisArray = this.ToArray();
            Array.Copy(thisArray, 0, array, arrayIndex, thisArray.Length);
        }

        public IEnumerator<T> GetEnumerator()
            => RawList.Select(v => _parser.Parse(v)).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public int IndexOf(T item)
        {
            if (item == null || !_parser.IsValid(item))
            {
                return -1;
            }

            string rawValue = _parser.ToString(item);
            return RawList.IndexOf(rawValue);
        }

        public void Insert(int index, T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (!_parser.IsValid(item))
            {
                HtmlHelper.ThrowInvalidAttributeValueException(_parser.Name, item);
            }

            string rawValue = _parser.ToString(item);

            IList<string> rawList = RawList;

            if (_allowDuplicates)
            {
                rawList.Insert(index, rawValue);
            }
            else if (rawList[index] == rawValue)
            {
                return;
            }
            else
            {
                int indexOfOther = rawList.IndexOf(rawValue);
                Debug.Assert(index != indexOfOther);

                rawList.Insert(index, rawValue);

                if (indexOfOther > index)
                {
                    rawList.RemoveAt(indexOfOther + 1);
                }
                else if (indexOfOther >= 0)
                {
                    rawList.RemoveAt(indexOfOther);
                }
            }

            SetList(rawList);
        }

        public bool Remove(T item)
        {
            if (item == null || !_parser.IsValid(item))
            {
                return false;
            }

            string rawValue = _parser.ToString(item);
            IList<string> rawList = RawList;
            bool result = rawList.Remove(rawValue);
            SetList(rawList);
            return result;
        }

        public void RemoveAt(int index)
        {
            IList<string> rawList = RawList;
            rawList.RemoveAt(index);
            SetList(rawList);
        }

        private IList<string> RawList
        {
            get
            {
                string? rawValue = _parser.RawAttributes[_parser.Name];

                if (rawValue == null)
                {
                    return new List<string>();
                }

                string[] rawValues = rawValue.Split(_delimiter, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                Debug.Assert(_allowDuplicates || rawValues.Length == rawValues.Distinct().Count());

                return new List<string>(rawValues);
            }
        }

        private void SetList(IList<string> rawList)
        {
            if (rawList.Count == 0)
            {
                _parser.RawAttributes[_parser.Name] = null;
            }
            else
            {
                _parser.RawAttributes[_parser.Name] = string.Join(_delimiter, rawList);
            }
        }
    }
}

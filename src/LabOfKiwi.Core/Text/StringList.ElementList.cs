using System;
using System.Collections.Generic;

namespace LabOfKiwi.Text;

public readonly partial struct StringList
{
    private sealed class ElementList : List<string>
    {
        public ElementList(List<string> elements) : base(elements)
        {
        }

        public ElementList(int capacity) : base(capacity)
        {
        }

        public ElementList(string? list, string? delimiter, bool includeEmptyElements) : base(Split(list, delimiter, includeEmptyElements))
        {
        }

        private static string[] Split(string? list, string? delimiter, bool includeEmptyElements)
        {
            string[] elements;

            if (string.IsNullOrEmpty(list))
            {
                elements = Array.Empty<string>();
            }
            else if (string.IsNullOrEmpty(delimiter))
            {
                elements = new string[] { list };
            }
            else
            {
                StringSplitOptions option = includeEmptyElements ? StringSplitOptions.None : StringSplitOptions.RemoveEmptyEntries;
                elements = list.Split(delimiter, option);
            }

            return elements;
        }

        public int IndexOf(string value, IEqualityComparer<string>? comparer)
        {
            if (Count > 0)
            {
                comparer ??= EqualityComparer<string>.Default;

                for (int i = 0; i < Count; i++)
                {
                    string element = this[i];

                    if (comparer.Equals(value, element))
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        public int LastIndexOf(string value, IEqualityComparer<string>? comparer)
        {
            if (Count > 0)
            {
                comparer ??= EqualityComparer<string>.Default;

                for (int i = Count - 1; i >= 0; i--)
                {
                    string element = this[i];

                    if (comparer.Equals(value, element))
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        public string ToString(string? delimiter)
        {
            return string.Join(delimiter, this);
        }

        public List<string> ToList()
        {
            return this;
        }

        public ElementList Slice(int startIndex, int count)
        {
            var subrange = GetRange(startIndex, count);
            return new ElementList(subrange);
        }
    }
}

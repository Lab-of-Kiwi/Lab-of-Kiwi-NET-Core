using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LabOfKiwi.Html.Tags.Attributes;

internal sealed class AttributeCollection
{
    private readonly List<RawAttribute> _attributes;

    public AttributeCollection()
    {
        _attributes = new List<RawAttribute>();
    }

    #region Helpers
    private int FindIndex(string attrName)
    {
        for (int i = 0; i < _attributes.Count; i++)
        {
            if (_attributes[i].Name == attrName)
            {
                return i;
            }
        }

        return -1;
    }

    public string? Get(string attr)
    {
        int index = FindIndex(attr);

        if (index != -1)
        {
            return _attributes[index].Value;
        }

        return null;
    }

    public bool Has(string attrName)
    {
        return FindIndex(attrName) != -1;
    }

    public void Set(string attr, string value)
    {
        Debug.Assert(value != null);
        int index = FindIndex(attr);

        if (index != -1)
        {
            _attributes[index].Value = value;
        }
        else
        {
            _attributes.Add(new RawAttribute(attr) { Value = value });
        }
    }

    public void Set(string attr, bool value)
    {
        int index = FindIndex(attr);

        if (value == true && index == -1)
        {
            _attributes.Add(new RawAttribute(attr) { Value = null });
        }
        else if (value == false && index != -1)
        {
            _attributes.RemoveAt(index);
        }
    }
    #endregion

    #region Misc Methods
    public void PopulateToString(StringBuilder sb)
    {
        foreach (var attr in _attributes)
        {
            sb.Append(' ').Append(attr.ToString());
        }
    }
    #endregion

    private sealed class RawAttribute
    {
        public RawAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public string? Value { get; set; }

        public override string ToString()
        {
            if (Value == null)
            {
                return Name;
            }
            else
            {
                return $"{Name}=\"{Value}\"";
            }
        }
    }
}



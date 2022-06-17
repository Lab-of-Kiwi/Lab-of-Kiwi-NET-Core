using LabOfKiwi.Html.Tags;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace LabOfKiwi.Html;

public sealed class Text : Element
{
    private string _text;
    private readonly List<IElement> _nextElements;

    public Text(string? text)
    {
        _text = text ?? string.Empty;
        _nextElements = new();
    }

    public Text Append(string? text)
    {
        if (text != null)
        {
            if (_nextElements.Count == 0)
            {
                _text += text;
            }
            else if (_nextElements[^1] is Text otherText)
            {
                return otherText.Append(text);
            }
            else
            {
                Text newText = new(text);
                _nextElements.Add(newText);
                return newText;
            }
        }

        return this;
    }

    public Text Bold(Action<Bold>? callback = null)
    {
        Bold tag = new();
        _nextElements.Add(tag);
        callback?.Invoke(tag);
        return this;
    }

    public override string ToString()
    {
        StringBuilder sb = new();

        sb.Append(HttpUtility.HtmlEncode(_text));

        foreach (var e in _nextElements)
        {
            sb.Append(e.ToString());
        }

        return sb.ToString();
    }
}

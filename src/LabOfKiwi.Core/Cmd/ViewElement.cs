using LabOfKiwi.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace LabOfKiwi.Cmd;

public abstract class ViewElement
{
    private readonly Spacing _margin = new();
    private readonly Spacing _padding = new();

    protected ViewElement()
    {
    }

    #region Public Properties
    public BorderType Border { get; set; }

    public char PaddingChar { get; set; } = ' ';

    public char MarginChar { get; set; } = ' ';

    public string Height
    {
        set
        {
            ThrowHelper.NullCheck(nameof(value), value);

            if (!SizeOption.TryParse(value, out SizeOption result))
            {
                throw new ArgumentException("Invalid height value: " + value, nameof(value));
            }

            HeightSizeOption = result;
        }
    }

    public int Margin
    {
        set => _margin.Set(value);
    }

    public int Padding
    {
        set => _padding.Set(value);
    }

    public TextAlignOption TextAlignment { get; set; } = TextAlignOption.Inherit;

    public string Width
    {
        set
        {
            ThrowHelper.NullCheck(nameof(value), value);

            if (!SizeOption.TryParse(value, out SizeOption result))
            {
                throw new ArgumentException("Invalid width value: " + value, nameof(value));
            }

            WidthSizeOption = result;
        }
    }
    #endregion

    #region Public Methods
    public void Print() => Print(Console.Out);

    public void Print(TextWriter writer)
    {
        ThrowHelper.NullCheck(nameof(writer), writer);

        var dimensionAllocation = DetermineDimensions(this, Console.WindowWidth, null);

        int width = dimensionAllocation.Width;
        int? height = dimensionAllocation.Height;
        var textAlignment = TextAlignment == TextAlignOption.Inherit ? TextAlignOption.Left : TextAlignment;

        foreach (var row in Rows(width, height, textAlignment))
        {
            writer.WriteLine(row);
        }
    }

    public void SetMargin(int value)
        => _margin.Set(value);

    public void SetMargin(int topBottomValue, int leftRightValue)
        => _margin.Set(topBottomValue, leftRightValue);

    public void SetMargin(int topValue, int leftRightValue, int bottomValue)
        => _margin.Set(topValue, leftRightValue, bottomValue);

    public void SetMargin(int topValue, int rightValue, int bottomValue, int leftValue)
        => _margin.Set(topValue, rightValue, bottomValue, leftValue);

    public void SetPadding(int value)
        => _padding.Set(value);

    public void SetPadding(int topBottomValue, int leftRightValue)
        => _padding.Set(topBottomValue, leftRightValue);

    public void SetPadding(int topValue, int leftRightValue, int bottomValue)
        => _padding.Set(topValue, leftRightValue, bottomValue);

    public void SetPadding(int topValue, int rightValue, int bottomValue, int leftValue)
        => _padding.Set(topValue, rightValue, bottomValue, leftValue);
    #endregion

    #region Internals
    internal SizeOption HeightSizeOption { get; private set; }

    internal SizeOption WidthSizeOption { get; private set; }

    protected static (int Width, int? Height) DetermineDimensions(ViewElement element, int allocatedWidth, int? allocatedHeight)
    {
        Debug.Assert(element != null);
        Debug.Assert(allocatedWidth >= 0);
        Debug.Assert(!allocatedHeight.HasValue || allocatedHeight.Value >= 0);

        int width;
        int? height;

        SizeOption widthSizeOption = element.WidthSizeOption;
        SizeOption heightSizeOption = element.HeightSizeOption;

        if (widthSizeOption.IsAuto)
        {
            width = allocatedWidth;
        }
        else if (widthSizeOption.IsPercent)
        {
            width = (int)Math.Floor(allocatedWidth * widthSizeOption.AsPercent);
        }
        else
        {
            width = widthSizeOption.Value;
        }

        width = Math.Min(width, allocatedWidth);

        if (allocatedHeight.HasValue)
        {
            if (heightSizeOption.IsAuto)
            {
                height = allocatedHeight.Value;
            }
            else if (heightSizeOption.IsPercent)
            {
                height = (int)Math.Floor(allocatedHeight.Value * heightSizeOption.AsPercent);
            }
            else
            {
                height = heightSizeOption.Value;
            }

            height = Math.Min(height.Value, allocatedHeight.Value);
        }
        else if (!(heightSizeOption.IsAuto || heightSizeOption.IsPercent))
        {
            height = heightSizeOption.Value;
        }
        else
        {
            height = null;
        }

        return (width, height);
    }

    private List<string> ContentRows(int allocatedWidth, int? allocatedHeight, TextAlignOption textAlignment)
    {
        Debug.Assert(textAlignment != TextAlignOption.Inherit);

        List<string> rows = new();

        if (allocatedWidth == 0)
        {
            if (allocatedHeight.HasValue)
            {
                for (int i = 0; i < allocatedHeight.Value; i++)
                {
                    rows.Add(string.Empty);
                }
            }
        }
        else if (!allocatedHeight.HasValue || allocatedHeight.Value > 0)
        {
            rows = InternalContentRows(allocatedWidth, allocatedHeight, textAlignment);

            if (allocatedHeight.HasValue)
            {
                if (rows.Count < allocatedHeight.Value)
                {
                    string emptyRow = new(' ', allocatedWidth);

                    for (int i = 0; i < allocatedHeight.Value - rows.Count; i++)
                    {
                        rows.Add(emptyRow);
                    }
                }
                else
                {
                    while (rows.Count > allocatedHeight.Value)
                    {
                        rows.RemoveAt(rows.Count - 1);
                    }
                }
            }
        }

        CleanWidth(rows, allocatedWidth, textAlignment);

        return rows;
    }

    protected static void CleanWidth(IList<string> rows, int targetWidth, TextAlignOption textAlignment)
    {
        for (int i = 0; i < rows.Count; i++)
        {
            rows[i] = CleanWidth(rows[i], targetWidth, textAlignment);
        }
    }

    protected static string CleanWidth(string row, int targetWidth, TextAlignOption textAlignment)
    {
        if (row.Length < targetWidth)
        {
            return textAlignment switch
            {
                TextAlignOption.Left    => row.PadRight(targetWidth),
                TextAlignOption.Center  => row.PadCenter(targetWidth),
                TextAlignOption.Right   => row.PadLeft(targetWidth),
                TextAlignOption.Inherit => throw new InvalidOperationException(),
                _                       => throw new NotImplementedException(),
            };
        }
        else if (row.Length > targetWidth)
        {
            return row[0..targetWidth];
        }

        return row;
    }

    protected abstract List<string> InternalContentRows(int allocatedWidth, int? allocatedHeight, TextAlignOption textAlignment);

    internal IEnumerable<string> Rows(int allocatedWidth, int? allocatedHeight, TextAlignOption textAlignment)
    {
        if (TextAlignment != TextAlignOption.Inherit)
        {
            textAlignment = TextAlignment;
        }

        allocatedWidth = Math.Max(allocatedWidth, 0); // Normalize to 0

        var m = _margin.Deconstruct();
        var p = _padding.Deconstruct();
        var b = Border.GetDefinition();

        List<string> rows = new();

        string tbMarginLine = new(MarginChar, allocatedWidth);
        string lMargin = new(MarginChar, m.Left);
        string rMargin = new(MarginChar, m.Right);
        string lPadding = new(PaddingChar, p.Left);
        string rPadding = new(PaddingChar, p.Right);

        int contentWidth = Math.Max(allocatedWidth - (m.Left + m.Right + p.Left + p.Right + (b.HasValue ? 2 : 0)), 0);
        int? contentHeight;

        string tbPaddingLine = new(PaddingChar, contentWidth);

        if (allocatedHeight == null)
        {
            contentHeight = null;
        }
        else
        {
            contentHeight = Math.Max(allocatedHeight.Value - (m.Top + m.Bottom + p.Top + p.Bottom + (b.HasValue ? 2 : 0)), 0);
        }

        for (int i = 0; i < m.Top; i++)
        {
            rows.Add(tbMarginLine);
        }

        if (b.HasValue)
        {
            StringBuilder sb = new();

            sb.Append(lMargin);
            sb.Append(b.Value.TopLeftCorner);

            for (int i = 0; i < allocatedWidth - 2 - lMargin.Length - rMargin.Length; i++)
            {
                sb.Append(b.Value.Horizontal);
            }

            sb.Append(b.Value.TopRightCorner);
            sb.Append(rMargin);

            rows.Add(sb.ToString());
        }

        var contentRows = Enumerable.Repeat(tbPaddingLine, p.Top).Concat(ContentRows(contentWidth, contentHeight, textAlignment)).Concat(Enumerable.Repeat(tbPaddingLine, p.Bottom));

        foreach (string contentRow in contentRows)
        {
            Debug.Assert(contentRow.Length == contentWidth);
            StringBuilder sb = new();

            sb.Append(lMargin);
            if (b.HasValue) sb.Append(b.Value.Vertical);
            sb.Append(lPadding);
            sb.Append(contentRow);
            sb.Append(rPadding);
            if (b.HasValue) sb.Append(b.Value.Vertical);
            sb.Append(rMargin);

            rows.Add(sb.ToString());
        }

        if (b.HasValue)
        {
            StringBuilder sb = new();

            sb.Append(lMargin);
            sb.Append(b.Value.BottomLeftCorner);

            for (int i = 0; i < allocatedWidth - 2 - lMargin.Length - rMargin.Length; i++)
            {
                sb.Append(b.Value.Horizontal);
            }

            sb.Append(b.Value.BottomRightCorner);
            sb.Append(rMargin);

            rows.Add(sb.ToString());
        }

        for (int i = 0; i < m.Bottom; i++)
        {
            rows.Add(tbMarginLine);
        }

        if (allocatedHeight.HasValue)
        {
            rows.RemoveMiddleElementsUntil(allocatedHeight.Value);
        }

        foreach (var row in rows)
        {
            yield return row.RemoveMiddleCharsUnti(allocatedWidth);
        }
    }
    #endregion
}

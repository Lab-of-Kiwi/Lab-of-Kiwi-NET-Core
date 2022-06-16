using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabOfKiwi.Cmd;

public class HorizontalStack : StackElement<HorizontalStack>
{
    protected override List<string> InternalContentRows(int allocatedWidth, int? allocatedHeight, TextAlignOption textAlignment)
    {
        var children = Children;
        var elements = new List<List<string>>(children.Count);

        int[] colWidths = new int[children.Count];

        int autoCount = 0;

        for (int i = 0; i < children.Count; i++)
        {
            var child = children[i];
            var widthOption = child.WidthSizeOption;

            if (widthOption.IsAuto)
            {
                autoCount++;
            }
            else if (widthOption.IsPercent)
            {
                colWidths[i] = (int)Math.Floor(allocatedWidth * widthOption.AsPercent);
            }
            else
            {
                colWidths[i] = widthOption.Value;
            }
        }

        if (autoCount > 0)
        {
            int remainingWidth = allocatedWidth;

            foreach (int w in colWidths)
            {
                remainingWidth -= w;
            }

            int autoWidth = remainingWidth / autoCount;

            for (int i = 0; i < children.Count; i++)
            {
                var widthOption = children[i].WidthSizeOption;

                if (widthOption.IsAuto)
                {
                    colWidths[i] = autoWidth;
                }
            }
        }

        for (int i = 0; i < children.Count; i++)
        {
            var child = children[i];
            var childRows = child.Rows(colWidths[i], allocatedHeight, textAlignment).ToList();
            elements.Add(childRows);
        }

        int maxHeight = elements.Select(e => e.Count).Max();
        var rows = new List<string>(maxHeight);
        
        for (int i = 0; i < maxHeight; i++)
        {
            StringBuilder sb = new(allocatedWidth);

            for (int j = 0; j < elements.Count; j++)
            {
                var element = elements[j];

                string row = i < element.Count ? element[i] : string.Empty;

                row = CleanWidth(row, colWidths[j], textAlignment);
                sb.Append(row);
            }

            rows.Add(sb.ToString());
        }

        return rows;
    }
}

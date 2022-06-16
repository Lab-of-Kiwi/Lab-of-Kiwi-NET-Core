using System;
using System.Collections.Generic;

namespace LabOfKiwi.Cmd
{
    public class VerticalStack : StackElement<VerticalStack>
    {
        protected override List<string> InternalContentRows(int allocatedWidth, int? allocatedHeight, TextAlignOption textAlignment)
        {
            var children = Children;
            var rows = new List<string>();

            int?[] rowHeights = new int?[children.Count];

            if (allocatedHeight.HasValue)
            {
                int autoCount = 0;

                for (int i = 0; i < children.Count; i++)
                {
                    var child = children[i];
                    var heightOption = child.HeightSizeOption;

                    if (heightOption.IsAuto)
                    {
                        autoCount++;
                    }
                    else if (heightOption.IsPercent)
                    {
                        rowHeights[i] = (int)Math.Floor(allocatedHeight.Value * heightOption.AsPercent);
                    }
                    else
                    {
                        rowHeights[i] = heightOption.Value;
                    }
                }

                if (autoCount > 0)
                {
                    int remainingHeight = allocatedHeight.Value;

                    foreach (int? h in rowHeights)
                    {
                        if (h.HasValue)
                        {
                            remainingHeight -= remainingHeight;
                        }
                    }

                    int autoHeight = remainingHeight / autoCount;

                    for (int i = 0; i < children.Count; i++)
                    {
                        var heightOption = children[i].HeightSizeOption;

                        if (heightOption.IsAuto)
                        {
                            rowHeights[i] = autoHeight;
                        }
                    }
                }
            }

            for (int i = 0; i < children.Count; i++)
            {
                var child = children[i];
                var childRows = child.Rows(allocatedWidth, rowHeights[i], textAlignment);
                rows.AddRange(childRows);
            }

            return rows;
        }
    }
}

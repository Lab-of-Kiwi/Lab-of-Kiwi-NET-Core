using LabOfKiwi.Web.Html.Tags;
using System.Collections;
using System.Collections.Generic;

namespace LabOfKiwi.Web.Html.Composites;

public abstract class Select<T> : IEnumerable<Element>
{
    internal Select()
    {
    }

    protected abstract IReadOnlyList<T> Items { get; }

    protected abstract string Name { get; }

    protected abstract string GetItemValue(T item);

    internal abstract bool IsSelected(T item);

    protected abstract void OptionCallback(int index, T item, bool isSelected, OPTION optionElement);

    protected abstract void SelectCallback(SELECT selectElement);

    public IEnumerator<Element> GetEnumerator()
    {
        SELECT selectElement = new()
        {
            Name = Name
        };

        for (int i = 0; i < Items.Count; i++)
        {
            T item = Items[i];
            bool isSelected = IsSelected(item);
            string itemValue = GetItemValue(item);

            selectElement.Add<OPTION>(opt =>
            {
                opt.IsSelected = isSelected;
                opt.Value = itemValue;
                OptionCallback(i, item, isSelected, opt);
            });
        }

        SelectCallback(selectElement);
        yield return selectElement;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

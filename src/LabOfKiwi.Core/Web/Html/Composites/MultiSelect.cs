using System;
using System.Collections.Generic;
using System.Linq;

namespace LabOfKiwi.Web.Html.Composites;

public abstract class MultiSelect<T> : Select<T>
{
    private readonly HashSet<T> _selectedItems;

    protected MultiSelect()
    {
        _selectedItems = new HashSet<T>();
    }

    public int SelectionCount => _selectedItems.Count;

    public IReadOnlyList<T> Selections => _selectedItems.ToList().AsReadOnly();

    public void AddSelection(T item)
    {
        _selectedItems.Add(item);
    }

    public void AddSelections(IEnumerable<T> items)
    {
        if (items == null)
        {
            throw new ArgumentNullException(nameof(items));
        }

        _selectedItems.UnionWith(items);
    }

    public void ClearSelections()
    {
        _selectedItems.Clear();
    }

    public void RemoveSelection(T item)
    {
        _selectedItems.Remove(item);
    }

    public void RemoveSelections(IEnumerable<T> items)
    {
        if (items == null)
        {
            throw new ArgumentNullException(nameof(items));
        }

        _selectedItems.ExceptWith(items);
    }

    internal sealed override bool IsSelected(T item) => _selectedItems.Contains(item);
}

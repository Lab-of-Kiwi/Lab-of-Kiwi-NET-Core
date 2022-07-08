namespace LabOfKiwi.Web.Html.Composites;

public abstract class SingleSelect<T> : Select<T>
{
    protected SingleSelect()
    {
    }

    public T? SelectedValue { get; set; }

    internal sealed override bool IsSelected(T item)
    {
        if (SelectedValue == null)
        {
            return false;
        }

        return Equals(SelectedValue, item);
    }
}

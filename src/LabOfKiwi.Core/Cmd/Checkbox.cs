using System;
using System.Collections.Generic;
using System.Linq;

namespace LabOfKiwi.Cmd;

public abstract class Checkbox : LoopedDisplayObject
{
    protected Checkbox()
    {
    }

    #region Virtual Internal Methods
    protected virtual string FormatTitle(string title, bool isChecked)
    {
        char check = isChecked ? 'x' : ' ';
        return $"[{check}] {title}";
    }

    protected abstract IEnumerable<(string Title, Func<bool> IsCheckedPredicate, Action<int, bool> Callback)> GetOptions();
    #endregion

    #region Internal Methods
    protected sealed override bool RunIteration()
    {
        var menu = new CheckboxMenu(this);
        menu.Run();
        return menu.IsCompleted;
    }
    #endregion

    #region Internal Types
    private sealed class CheckboxMenu : Menu
    {
        private readonly Checkbox _checkbox;

        public CheckboxMenu(Checkbox checkbox)
        {
            _checkbox = checkbox;
        }

        public bool IsCompleted { get; private set; } = false;

        protected override IEnumerable<(string Title, Action<int> Callback)> GetOptions()
        {
            var options = _checkbox.GetOptions().ToArray();

            foreach (var (Title, IsCheckedPredicate, Callback) in options)
            {
                bool isChecked = IsCheckedPredicate();
                yield return (_checkbox.FormatTitle(Title, isChecked), i => Callback(i, !isChecked));
            }

            yield return ("Exit Checkbox", _ => IsCompleted = true);
        }
    }
    #endregion
}

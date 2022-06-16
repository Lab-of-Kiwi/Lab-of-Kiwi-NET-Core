using System;
using System.Collections.Generic;
using System.Linq;

namespace LabOfKiwi.Cmd;

public abstract class Radio : LoopedDisplayObject
{
    protected Radio()
    {
    }

    #region Internal Virtual Methods
    protected abstract IEnumerable<(string Title, Func<bool> IsCheckedPredicate, Action<int, bool> Callback)> GetOptions();

    protected virtual string FormatTitle(string title, bool isChecked)
    {
        char check = isChecked ? 'x' : ' ';
        return $"[{check}] {title}";
    }
    #endregion

    protected sealed override bool RunIteration()
    {
        var menu = new RadioMenu(this);
        menu.Run();
        return menu.IsCompleted;
    }

    private sealed class RadioMenu : Menu
    {
        private readonly Radio _radio;

        public RadioMenu(Radio radio)
        {
            _radio = radio;
        }

        public bool IsCompleted { get; private set; } = false;

        protected override IEnumerable<(string Title, Action<int> Callback)> GetOptions()
        {
            var options = _radio.GetOptions().ToArray();
            int checkedCount = options.Select(o => o.IsCheckedPredicate()).Count();

            if (checkedCount > 1)
            {
                Console.WriteLine("Invalid radio state: too many options checked.");
                IsCompleted = true;
                yield break;
            }

            for (int i = 0; i < options.Length; i++)
            {
                var (Title, IsCheckedPredicate, _) = options[i];

                yield return (_radio.FormatTitle(Title, IsCheckedPredicate()), i =>
                {
                    for (int j = 0; j < options.Length; j++)
                    {
                        var otherOption = options[j];
                        otherOption.Callback(i, i == j);
                    }
                }
                );
            }

            yield return ("Exit Radio", _ => IsCompleted = true);
        }
    }
}

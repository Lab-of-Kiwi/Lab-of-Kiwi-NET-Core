using System;

namespace LabOfKiwi.Cmd;

public abstract class View : DisplayObject
{
    protected internal Program Program { get; internal set; } = null!;

    private View? PreviousView { get; set; } = null;

    private View? NextView { get; set; } = null!;

    public sealed override void Run()
    {
        if (Program == null)
        {
            throw new InvalidOperationException("Must call Goto");
        }

        InternalRun();
    }

    protected abstract void InternalRun();

    protected void GoBack()
    {
        PreviousView?.Run();
    }

    protected void GoForward()
    {
        NextView?.Run();
    }

    protected void GoHome()
    {
        // This would be the Home View
        if (PreviousView == null)
        {
            Run();
        }
        else
        {
            Goto(Program.CreateHomeView());
        }
    }

    protected void Goto(View view)
    {
        if (ReferenceEquals(this, view))
        {
            view = this;
        }
        else
        {
            NextView = view ?? throw new ArgumentNullException(nameof(view));
            view.Program = Program;
            view.PreviousView = this;
        }
        
        view.Run();
    }
}

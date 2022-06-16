namespace LabOfKiwi.Cmd;

public abstract class LoopedDisplayObject : DisplayObject
{
    protected LoopedDisplayObject()
    {
    }

    public sealed override void Run()
    {
        bool isCompleted = false;

        if (OnStart())
        {
            while (!isCompleted)
            {
                OnIterationStart();
                isCompleted = RunIteration();
                OnIterationCompletion();
            }
        }

        OnCompletion();
    }

    #region Internal Virtual Methods
    protected abstract bool RunIteration();

    protected virtual void OnCompletion()
    {
        // Do nothing for override.
    }

    protected virtual void OnIterationCompletion()
    {
        // Do nothing for override.
    }

    protected virtual void OnIterationStart()
    {
        // Do nothing for override.
    }

    protected virtual bool OnStart()
    {
        return true;
    }
    #endregion
}

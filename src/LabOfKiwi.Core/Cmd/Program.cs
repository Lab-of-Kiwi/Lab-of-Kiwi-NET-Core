using System;

namespace LabOfKiwi.Cmd;

public abstract class Program : IRunnable
{
    private readonly string[] _args;

    protected Program(params string[] args)
    {
        _args = args ?? throw new ArgumentNullException(nameof(args));
    }

    public string[] Arguments
    {
        get
        {
            string[] args = new string[_args.Length];
            Array.Copy(_args, args, args.Length);
            return args;
        }
    }

    public void Run()
    {
        var view = CreateHomeView();
        view.Program = this;
        view.Run();
    }

    protected internal abstract View CreateHomeView();
}

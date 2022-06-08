using System;

namespace LabOfKiwi;

/// <summary>
/// Utility methods for objects. Non-extensions go here.
/// </summary>
public static class Objects
{
    /// <summary>
    /// Calls the <see cref="IDisposable.Dispose"/> or <see cref="IAsyncDisposable.DisposeAsync"/> methods on the
    /// provided value, if the value implements either interface, then the value is set to its default regardless of
    /// what type it is.
    /// </summary>
    /// 
    /// <typeparam name="T">The type of <paramref name="value"/>.</typeparam>
    /// <param name="value">The value to dispose and set to its default.</param>
    public static void Dispose<T>(ref T? value)
    {
        try
        {
            if (value is IDisposable disp)
            {
                disp.Dispose();
            }
            else if (value is IAsyncDisposable asyncDisp)
            {
                asyncDisp.DisposeAsync().AsTask().Wait();
            }
        }
        finally
        {
            value = default;
        }
    }
}

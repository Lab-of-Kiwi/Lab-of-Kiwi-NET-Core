using System;

namespace LabOfKiwi;

/// <summary>
/// Provides methodology to cache a value for a specified time, and generates the value again after said time.
/// </summary>
/// 
/// <typeparam name="T">The type of value to cache.</typeparam>
public sealed class CachedObject<T>
{
    // Thread safety lock for synchronization.
    private readonly object _syncLock = new();

    // The function that supplies the value.
    private readonly Func<T> _supplier;

    // The allowed time to pass before timing out.
    private readonly TimeSpan _timeout;

    // The current value, if set.
    private T _value = default!;

    // Flag determining if value is set.
    private volatile bool _isSet = false;

    // Time in which value was set.
    private DateTime _setAt;

    // Exception thrown during last call to supplier.
    private volatile Exception? _exception = null;

    /// <summary>
    /// Creates a new instance of <see cref="CachedObject{T}"/>.
    /// </summary>
    /// 
    /// <param name="timeout">The amount of time to keep the value when generated.</param>
    /// <param name="supplier">The function that supplies the value.</param>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="supplier"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="timeout"/> is negative.</exception>
    public CachedObject(TimeSpan timeout, Func<T> supplier)
    {
        _supplier = supplier ?? throw new ArgumentNullException(nameof(supplier));

        if (timeout < TimeSpan.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(timeout));
        }

        _timeout = timeout;
    }

    /// <summary>
    /// The amount of time to keep the generated value.
    /// </summary>
    public TimeSpan Timeout => _timeout;

    /// <summary>
    /// The remaining amount of time before the value will have to be re-created.
    /// </summary>
    public TimeSpan TimeRemaining
    {
        get
        {
            var now = DateTime.Now;

            lock (_syncLock)
            {
                if (_isSet)
                {
                    var elapsedTime = now - _setAt;

                    if (elapsedTime < _timeout)
                    {
                        return _timeout - elapsedTime;
                    }
                }

                return TimeSpan.Zero;
            }
        }
    }

    /// <summary>
    /// Gets the value.
    /// </summary>
    public T Value
    {
        get
        {
            var now = DateTime.Now;

            lock (_syncLock)
            {
                if (_isSet && now - _setAt < _timeout)
                {
                    if (_exception != null)
                    {
                        throw _exception;
                    }

                    return _value;
                }

                try
                {
                    _value = _supplier();
                    _exception = null;
                }
                catch (Exception e)
                {
                    _exception = e;
                    throw;
                }
                finally
                {
                    _setAt = DateTime.Now;
                    _isSet = true;
                }

                return _value;
            }
        }
    }

    /// <summary>
    /// Clears the state of this instance, forcing the next call to <see cref="Value"/> to re-create the value.
    /// </summary>
    public void Reset()
    {
        lock (_syncLock)
        {
            _isSet = false;
            _setAt = default;
            _exception = null;
            _value = default!;
        }
    }
}

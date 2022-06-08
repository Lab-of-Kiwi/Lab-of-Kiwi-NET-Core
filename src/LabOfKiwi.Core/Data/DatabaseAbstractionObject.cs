using System;
using System.Data;

namespace LabOfKiwi.Data
{
    public abstract class DatabaseAbstractionObject<TDbConnection> : IDisposable where TDbConnection : IDbConnection
    {
        private readonly TDbConnection _connection;
        private bool _disposed;

        internal DatabaseAbstractionObject(TDbConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        protected TDbConnection Connection
        {
            get
            {
                ThrowIfDisposed();
                return _connection;
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                try
                {
                    DisposeResources();
                }
                finally
                {
                    _disposed = true;
                }
            }
        }

        protected virtual void DisposeResources()
        {
        }

        protected void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }
    }
}

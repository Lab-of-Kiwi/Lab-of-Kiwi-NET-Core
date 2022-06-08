using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace LabOfKiwi.Data
{
    public abstract class DatabaseCommand<TDbConnection, TDbCommand> : DatabaseAbstractionObject<TDbConnection> where TDbConnection : IDbConnection where TDbCommand : class, IDbCommand
    {
        private TDbCommand? _dbCommand;

        protected DatabaseCommand(TDbConnection connection) : base(connection)
        {
        }

        protected abstract string CommandText { get;}

        private TDbCommand DbCommand
        {
            get
            {
                ThrowIfDisposed();

                if (_dbCommand == null)
                {
                    IDbCommand rawCommand = Connection.CreateCommand();

                    if (rawCommand is TDbCommand cmd)
                    {
                        cmd.CommandText = CommandText;

                        foreach (IDbDataParameter parameter in GetParameters())
                        {
                            cmd.Parameters.Add(parameter);
                        }

                        CustomDbCommandPreparation(cmd);
                        _dbCommand = cmd;
                    }
                    else
                    {
                        throw new InvalidOperationException($"Connection of type {typeof(TDbConnection)} did not return command of type {typeof(TDbCommand)}.");
                    }
                }

                var values = GetParameterValues();

                foreach (var (ParameterName, Value) in values)
                {
                    _dbCommand.Parameters[ParameterName] = Value ?? DBNull.Value;
                }

                return _dbCommand;
            }
        }

        #region Command Executions
        protected IReadOnlyList<TModel> ExecuteAll<TReader, TModel>(Func<TReader, TModel> modelBinder) where TReader : IDataReader
        {
            Debug.Assert(modelBinder != null);
            List<TModel> list = new();

            using (TReader reader = ExecuteReader<TReader>())
            {
                while (reader.Read())
                {
                    TModel model = modelBinder(reader);
                    list.Add(model);
                }
            }

            return list.AsReadOnly();
        }

        protected TModel ExecuteFirst<TReader, TModel>(Func<TReader, TModel> modelBinder) where TReader : IDataReader
        {
            Debug.Assert(modelBinder != null);

            using (TReader reader = ExecuteReader<TReader>())
            {
                if (reader.Read())
                {
                    TModel model = modelBinder(reader);
                    return model;
                }
            }

            throw new InvalidOperationException("Database result contained no rows.");
        }

        protected TModel? ExecuteFirstOrDefault<TReader, TModel>(Func<TReader, TModel> modelBinder, TModel? defaultValue = default) where TReader : IDataReader
        {
            Debug.Assert(modelBinder != null);

            using (TReader reader = ExecuteReader<TReader>())
            {
                if (reader.Read())
                {
                    TModel model = modelBinder(reader);
                    return model;
                }
            }

            return defaultValue;
        }

        protected TModel ExecuteSingle<TReader, TModel>(Func<TReader, TModel> modelBinder) where TReader : IDataReader
        {
            Debug.Assert(modelBinder != null);

            using (TReader reader = ExecuteReader<TReader>())
            {
                if (reader.Read())
                {
                    TModel model = modelBinder(reader);
                    
                    if (reader.Read())
                    {
                        throw new InvalidOperationException("Database results contained more than one row.");
                    }
                }
            }

            throw new InvalidOperationException("Database results contained no rows.");
        }

        protected TModel? ExecuteSingleOrDefault<TReader, TModel>(Func<TReader, TModel> modelBinder, TModel? defaultValue = default) where TReader : IDataReader
        {
            Debug.Assert(modelBinder != null);

            using (TReader reader = ExecuteReader<TReader>())
            {
                if (reader.Read())
                {
                    TModel model = modelBinder(reader);

                    if (reader.Read())
                    {
                        throw new InvalidOperationException("Database results contained more than one row.");
                    }
                }
            }

            return defaultValue;
        }

        protected int ExecuteNonQuery()
        {
            return DbCommand.ExecuteNonQuery();
        }

        protected IDataReader ExecuteReader()
        {
            return DbCommand.ExecuteReader();
        }

        protected TReader ExecuteReader<TReader>() where TReader : IDataReader
        {
            if (DbCommand.ExecuteReader() is TReader reader)
            {
                return reader;
            }

            throw new InvalidOperationException($"Connection of type {typeof(TDbConnection)} did not return command of type {typeof(TReader)}.");
        }

        protected object? ExecuteScalar()
        {
            return DbCommand.ExecuteScalar();
        }

        protected bool TryExecuteScalar<T>([MaybeNullWhen(false)] out T result)
        {
            object? rawResult = ExecuteScalar();

            if (rawResult is T tResult)
            {
                result = tResult;
                return true;
            }

            if (rawResult == null)
            {
                result = default;
                return false;
            }

            result = (T)rawResult;
            return true;
        }
        #endregion

        protected virtual void CustomDbCommandPreparation(TDbCommand command)
        {
        }

        protected abstract IEnumerable<IDbDataParameter> GetParameters();

        protected abstract IEnumerable<(string ParameterName, object? Value)> GetParameterValues();

        protected override void DisposeResources()
        {
            try
            {
                Objects.Dispose(ref _dbCommand);
            }
            finally
            {
                base.DisposeResources();
            }
        }
    }
}

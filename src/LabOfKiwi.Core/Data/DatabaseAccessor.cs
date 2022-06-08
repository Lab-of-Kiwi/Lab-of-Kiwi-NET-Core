using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabOfKiwi.Data
{
    /// <summary>
    /// Provides methods for accessing a database connection.
    /// </summary>
    /// 
    /// <typeparam name="T">The type of database connection.</typeparam>
    public abstract class DatabaseAccessor<TDbConnection> : DatabaseAbstractionObject<TDbConnection> where TDbConnection : IDbConnection
    {
        protected DatabaseAccessor(TDbConnection connection) : base(connection)
        {
        }
    }
}

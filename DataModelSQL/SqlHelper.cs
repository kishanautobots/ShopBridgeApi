
using DataModel;
using DataModelInfrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataModelSql
{
    /// <summary>This class will have all sql functions used view, add, update, delete data.</summary>
    public class SqlHelper : IDblHelper
    {
        private const int _DefaultCommandTimeout = 2400;
        private static SqlHelper _instance;
        private static readonly object _instanceSyncLock = new object();
        private string _connectionString;

        public SqlHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>The <see cref="Core"/> singleton instance.</summary>
        public static SqlHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceSyncLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new SqlHelper(ConnectionStringPath.Connection);
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// This will return data reader based on the query.
        /// </summary>
        /// <remarks>It is the responsibility of the caller to clean up the returned DataReader.</remarks>
        /// <param name="commandText">The text to set as the CommandText.</param>
        /// <param name="parameters">The <see cref="IDataParameter"/>s to give to the SQL.</param>
        /// <returns>An <see cref="SqlDataReader"/>.</returns>
        public IDataReader ExecuteDataReader(string commandText, IEnumerable<IDataParameter> commandParameters = null)
        {
            return ExecuteDataReader(commandText, CommandType.Text, commandParameters);
        }

        /// <summary>
        /// This will return data reader based on the query.
        /// </summary>
        /// <remarks>It is the responsibility of the caller to clean up the returned DataReader.</remarks>
        /// <param name="commandText">The text to set as the CommandText.</param>
        /// <param name="commandType"></param>
        /// <param name="parameters">The <see cref="IDataParameter"/>s to give to the SQL.</param>
        /// <returns>An <see cref="SqlDataReader"/>.</returns>
        public IDataReader ExecuteDataReader(
            string commandText,
            CommandType commandType,
            IEnumerable<IDataParameter> commandParameters = null
        )
        {
            // NOTE: No usings because we are returning the DataReader.  When the data reader closes, it will close the
            // command and connection.
            SqlConnection connection = CreateAndOpenConnection(_connectionString);
            SqlCommand command = CreateCommand(connection, commandText, commandType, commandParameters);

            // When returning datareader the connection need to be open, so by using command behaviour
            // "CommandBehavior.CloseConnection" this ensures that when datareader is used closed by the using
            // function the connection will automatically gets closed.
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary>This will execute a query in the database.</summary>
        /// <param name="commandText">The query to execute</param>
        /// <returns>The number of rows affected</returns>
        public int ExecuteNonQuery(string commandText)
        {
            return ExecuteNonQuery(commandText, CommandType.Text, null);
        }
        

        /// <summary>This will execute a query in the database.</summary>
        /// <param name="commandText">The query to execute</param>
        /// <param name="commandType">The <see cref="CommandType"/>.</param>
        /// <param name="commandParameters">The optional <see cref="IDataParameter"/>s.</param>
        /// <returns>The number of rows affected</returns>
        public int ExecuteNonQuery(
            string commandText,
            CommandType commandType,
            IEnumerable<IDataParameter> commandParameters
        )
        {
            using (var connection = CreateAndOpenConnection(_connectionString))
            {
                using (var command = CreateCommand(connection, commandText, commandType, commandParameters))
                {
                    return command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>Creates AND opens a new <see cref="SqlConnection"/> with the given connection string.</summary>
        /// <param name="connectionString">Connection string to use when connecting to DB.</param>
        /// <returns>An open <see cref="SqlConnection"/>.</returns>
        /// <remarks><see cref="SqlConnection"/> is <see cref="IDisposable"/>.  Callers of this function must be
        /// careful to dispose the returned connection properly.</remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public SqlConnection CreateAndOpenConnection()
        {
            return CreateAndOpenConnection(_connectionString);
        }

        /// <summary>
        /// Returns connection string.
        /// </summary>
        /// <returns></returns>
        public string GetConnectionString()
        {
            return _connectionString;
        }

        /// <summary>Creates AND opens a new <see cref="SqlConnection"/> with the given connection string.</summary>
        /// <param name="connectionString">Connection string to use when connecting to DB.</param>
        /// <returns>An open <see cref="SqlConnection"/>.</returns>
        /// <remarks><see cref="SqlConnection"/> is <see cref="IDisposable"/>.  Callers of this function must be
        /// careful to dispose the returned connection properly.</remarks>
        public static SqlConnection CreateAndOpenConnection(string connectionString)
        {
            var connection = new SqlConnection(connectionString);

            connection.Open();
            return connection;
        }

        /// <summary>
        /// Creates a new <see cref="SqlCommand"/> with default options.  The default options are hard-coded.
        /// For now, the only option is the timeout, which is set to infinite.
        /// </summary>
        /// <param name="connection">The <see cref="SqlConnection"/> to create the command on.</param>
        /// <returns>An <see cref="SqlCommand"/>.</returns>
        public static SqlCommand CreateDefaultCommand(SqlConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }

            var command = connection.CreateCommand();
            command.CommandTimeout = _DefaultCommandTimeout;
            return command;
        }

        /// <summary>Creates a <see cref="SqlCommand"/></summary>
        /// <remarks>NOTE: SqlComand is IDisposable</remarks>
        public static SqlCommand CreateCommand(SqlConnection connection, string text)
        {
            return CreateCommand(connection, text, CommandType.Text, null);
        }

        /// <summary>
        /// Creates a <see cref="SqlCommand"/>
        /// </summary>
        /// <remarks>NOTE: SqlComand is IDisposable</remarks>
         public static SqlCommand CreateCommand(
            SqlConnection connection,
            string text,
            CommandType type,
            IEnumerable<IDataParameter> commandParameters = null
        )
        {
            var command = CreateDefaultCommand(connection);
            command.CommandType = type;
            command.CommandText = text;
            if (commandParameters != null)
            {
                command.Parameters.AddRange(commandParameters.ToArray());
            }
            return command;
        }
    }
}
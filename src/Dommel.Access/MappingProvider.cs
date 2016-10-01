using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dommel;

namespace Dommel.Access
{
    public static class MappingProvider
    {
        public static void InitializeAccessBuilders()
        {
            DommelMapper.AddBuilders(typeof(AccessDbConnection), new AccessSqlBuilder(), new AccessUpdateBuilder());
        }

    }

    public class ConnectionHelper
    {
        public static IDbConnection GetAccessDbConnection(string inputPath)
        {
            var connStr =
                $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={inputPath}; Persist Security Info = False; ";
            var conn = new AccessDbConnection(new OleDbConnection(connStr));

            return conn;
        }
    }

    sealed class AccessDbConnection : WrappedDbConnection
    {
        internal AccessDbConnection(IDbConnection connection) : base(connection)
        {
        }
    }

    internal class WrappedDbConnection : IDbConnection
    {
        private readonly IDbConnection _conn;

        internal WrappedDbConnection(IDbConnection connection)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            _conn = connection;
        }

        public string ConnectionString
        {
            get { return _conn.ConnectionString; }
            set { _conn.ConnectionString = value; }
        }

        public int ConnectionTimeout
        {
            get { return _conn.ConnectionTimeout; }
        }

        public string Database
        {
            get { return _conn.Database; }
        }

        public ConnectionState State
        {
            get { return _conn.State; }
        }

        public IDbTransaction BeginTransaction()
        {
            return _conn.BeginTransaction();
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return _conn.BeginTransaction(il);
        }

        public void ChangeDatabase(string databaseName)
        {
            _conn.ChangeDatabase(databaseName);
        }

        public void Close()
        {
            _conn.Close();
        }

        public IDbCommand CreateCommand()
        {
            return new WrappedDbCommand(_conn.CreateCommand());
        }

        public void Dispose()
        {
            _conn.Dispose();
        }

        public void Open()
        {
            _conn.Open();
        }
    }

    public class WrappedDbCommand : IDbCommand
    {
        private readonly IDbCommand _cmd;
        public WrappedDbCommand(IDbCommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            _cmd = command;
        }

        public string CommandText
        {
            get { return _cmd.CommandText; }
            set { _cmd.CommandText = value; }
        }

        public int CommandTimeout
        {
            get { return _cmd.CommandTimeout; }
            set { _cmd.CommandTimeout = value; }
        }

        public CommandType CommandType
        {
            get { return _cmd.CommandType; }
            set { _cmd.CommandType = value; }
        }

        public IDbConnection Connection
        {
            get { return _cmd.Connection; }
            set { _cmd.Connection = value; }
        }

        public IDataParameterCollection Parameters
        {
            get { return _cmd.Parameters; }
        }

        public IDbTransaction Transaction
        {
            get { return _cmd.Transaction; }
            set { _cmd.Transaction = value; }
        }

        public UpdateRowSource UpdatedRowSource
        {
            get { return _cmd.UpdatedRowSource; }
            set { _cmd.UpdatedRowSource = value; }
        }

        public void Cancel()
        {
            _cmd.Cancel();
        }

        public IDbDataParameter CreateParameter()
        {
            return _cmd.CreateParameter();
        }

        public void Dispose()
        {
            _cmd.Dispose();
        }

        public int ExecuteNonQuery()
        {
            return _cmd.ExecuteNonQuery();
        }

        public IDataReader ExecuteReader()
        {
            return _cmd.ExecuteReader();
        }

        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            return _cmd.ExecuteReader();
        }

        public object ExecuteScalar()
        {
            return _cmd.ExecuteScalar();
        }

        public void Prepare()
        {
            _cmd.Prepare();
        }
    }
}

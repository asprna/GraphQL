using System;
using System.Data;
using System.IO;
using Microsoft.Data.Sqlite;

namespace Persistence.DBConnectionFactory
{
    public class SqliteConnectionFactory : IConnectionFactory, IDisposable
    {
        private readonly string _connectionString;
        private IDbConnection _connection;
        public SqliteConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Dispose()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Dispose();
            }
        }

        public IDbConnection GetDbConnection()
        {
            if (_connection == null || _connection.State != ConnectionState.Open)
            {
                _connection = new SqliteConnection(_connectionString);
                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
                _connection.Open();
            }
            return _connection;
        }
    }
}
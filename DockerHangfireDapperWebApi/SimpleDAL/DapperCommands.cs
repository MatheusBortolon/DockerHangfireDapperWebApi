using Dapper;
using Dapper.Bulk;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Docker.SimpleDAL
{
    public class DapperCommands : IDapperCommands
    {
        private readonly string _connectionString;

        public DapperCommands(IConfiguration configuration) =>
            _connectionString = configuration.GetConnectionString("Exemplo");

        public DapperCommands(string connectionString) =>
            _connectionString = connectionString;

        public bool Exists(string cmd) =>
            ExecuteCommand((cnn) => cnn.QueryFirstOrDefault<bool>(cmd));

        public IEnumerable<T> Query<T>(string command) =>
            ExecuteCommand((cnn) => cnn.Query<T>(command));

        private T ExecuteCommand<T>(Func<IDbConnection, T> execute)
        {
            T result;
            var connection = GetConnection();
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            result = execute(connection);

            connection.Close();

            return result;
        }

        public void BulkInsert<T>(IEnumerable<T> data, int batchSize = 5000, int timeOutSeconds = 90)
        {
            var cnn = GetConnection();

            if (cnn.State == ConnectionState.Closed)
                cnn.Open();

            cnn.BulkInsert(data, null, batchSize, timeOutSeconds);

            cnn.Close();
        }

        public (T, IDbConnection) ExecuteCommandWithoutClose<T>(Func<IDbConnection, T> execute)
        {
            T result;
            var connection = GetConnection();
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            result = execute(connection);

            return (result, connection);
        }

        private SqlConnection GetConnection() =>
            new SqlConnection(_connectionString);

    }

}

using System;
using System.Collections.Generic;
using System.Data;

namespace Docker.SimpleDAL
{
    public interface IDapperCommands
    {
        void BulkInsert<T>(IEnumerable<T> data, int batchSize = 5000, int timeOutSeconds = 90);
        (T, IDbConnection) ExecuteCommandWithoutClose<T>(Func<IDbConnection, T> execute);
        bool Exists(string cmd);
        IEnumerable<T> Query<T>(string command);

    }
}
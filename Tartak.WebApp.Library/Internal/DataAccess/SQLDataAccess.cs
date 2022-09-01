using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace TRMDataManager.Library.Internal.DataAccess
{
    public class SQLDataAccess : IDisposable, ISQLDataAccess
    {

        IDbConnection _dbConnection;
        IDbTransaction _dbTransaction;
        public SQLDataAccess(IConfiguration config, ILogger<SQLDataAccess> logger)
        {
            _config = config;
            _logger = logger;
        }
        public string GetConnectionString(string name)
        {
            return _config.GetConnectionString(name);
        }

        public List<T> GetData<T, U>(string storedProcedure, U parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection conn = new SqlConnection(connectionString))
            {
                List<T> output = conn.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure).ToList();

                return output;
            }
        }
        public void SetData<T>(string storedProcedure, T parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection conn = new SqlConnection(connectionString))
            {
                conn.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public void StartTransaction(string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);
            _dbConnection = new SqlConnection(connectionString);
            _dbConnection.Open();
            _dbTransaction = _dbConnection.BeginTransaction();
            isClosed = false;
        }

        public void CommitTransaction()
        {
            _dbTransaction?.Commit();
            _dbConnection?.Close();

            isClosed = true;
        }
        bool isClosed = false;
        private readonly IConfiguration _config;
        private readonly ILogger<SQLDataAccess> _logger;

        public void RollbackTransaction()
        {
            _dbTransaction?.Rollback();
            _dbConnection?.Close();

            isClosed = true;
        }
        public void SetDataInTransaction<T>(string storedProcedure, T parameters)
        {
            _dbConnection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _dbTransaction);
        }
        public List<T> GetDataInTransaction<T, U>(string storedProcedure, U parameters)
        {
            List<T> output = _dbConnection.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _dbTransaction).ToList();

            return output;
        }

        public void Dispose()
        {
            if (isClosed == false)
            {
                try
                {
                    CommitTransaction();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Commit transaction failed in the dispose method.");
                }
            }

            _dbTransaction = null;
            _dbConnection = null;
        }
    }
}

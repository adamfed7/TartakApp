using System.Collections.Generic;

namespace TRMDataManager.Library.Internal.DataAccess
{
    public interface ISQLDataAccess
    {
        void CommitTransaction();
        void Dispose();
        string GetConnectionString(string name);
        List<T> GetData<T, U>(string storedProcedure, U parameters, string connectionStringName);
        List<T> GetDataInTransaction<T, U>(string storedProcedure, U parameters);
        void RollbackTransaction();
        void SetData<T>(string storedProcedure, T parameters, string connectionStringName);
        void SetDataInTransaction<T>(string storedProcedure, T parameters);
        void StartTransaction(string connectionStringName);
    }
}
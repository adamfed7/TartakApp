using TRMDataManager.Library.Internal.DataAccess;
using TRMDataManager.Library.Models;

namespace Tartak.WebApp.Library.DataAccess
{
    public class UserData : IUserData
    {
        private readonly ISQLDataAccess _sqlDataAccess;

        public UserData(ISQLDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }
        public List<UserModel> GetUserByID(string ID)
        {

            var output = _sqlDataAccess.GetData<UserModel, dynamic>("spUserLookup", new { ID }, "TRMConnectionString");

            return output;
        }

    }
}

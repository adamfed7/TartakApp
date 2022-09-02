using TRMDataManager.Library.Models;

namespace Tartak.WebApp.Library.DataAccess
{
    public interface IUserData
    {
        List<UserModel> GetUserByID(string ID);
    }
}
using Galaxy.Taurus.AuthorizationAPI.Entitys;
using Galaxy.Taurus.AuthorizationAPI.IDBAccess;
using Galaxy.Taurus.DBUtil;

namespace Galaxy.Taurus.AuthorizationAPI.DBAccess
{
    public class UserInfoContext : BaseContext<UserInfo>, IUserInfoContext
    {
    }
}

using Galaxy.Taurus.AuthorizationAPI.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Galaxy.Taurus.AuthorizationAPI.ICached
{
    public interface IUserCached
    {
        void SetUserCached(string sid, UserInfo userInfo, List<UserRoleRelation> userRole);

        string GetSidByUserId(string userId);

        List<UserRoleRelation> GetUserRoleBySid(string sid);

        void UpdateExpire(string sid);

        bool SidExists(string sid);

        void RemoveUserCached(string sid);

        Task<UserInfo> GetUserInfoFromCached(string sid);
    }
}

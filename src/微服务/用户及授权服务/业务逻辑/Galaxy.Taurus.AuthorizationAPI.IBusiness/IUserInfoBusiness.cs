using Galaxy.Taurus.AuthorizationAPI.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Galaxy.Taurus.AuthorizationAPI.IBusiness
{
    public interface IUserInfoBusiness
    {
        /// <summary>
        /// 通过手机号登录
        /// </summary>
        /// <param name="phoneNumber">手机号</param>
        /// <param name="password">密码</param>
        /// <param name="resultCode">结果码 -1:用户不存在 -2:密码错误 1:登录成功</param>
        /// <returns>sid</returns>
        Dictionary<string, string> LoginByPhoneNumber(string phoneNumber, string password, out int resultCode);

        List<Role> GetUserRoleBySid(string sid, string shopId);

        List<Operation> GetUserOperationBySid(string sid, string shopId);

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns>结果码=》1：成功 -1：手机号码已注册</returns>
        int Add(UserInfo user);

        UserInfo GetByByPhoneNumber(string phoneNumber);

        void UpdateCachedExpire(string sid);

        bool CachedSidExists(string sid);

        void RemoveUserCached(string sid);

        Task<UserInfo> GetUserInfoFromCached(string sid);
    }
}

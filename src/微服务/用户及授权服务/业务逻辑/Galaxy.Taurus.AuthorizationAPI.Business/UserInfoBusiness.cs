using Galaxy.Taurus.AuthorizationAPI.Entitys;
using Galaxy.Taurus.AuthorizationAPI.IBusiness;
using Galaxy.Taurus.AuthorizationAPI.ICached;
using Galaxy.Taurus.AuthorizationAPI.IDBAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxy.Taurus.AuthorizationAPI.Business
{
    public class UserInfoBusiness : IUserInfoBusiness
    {
        private IUserInfoContext userInfoContext;
        private IUserCached userCached;
        private IUserRoleRelationContext userRoleRelationContext;

        private IRoleOperationRelationBusiness roleOperationRelationBusiness;

        public UserInfoBusiness(IUserInfoContext userInfoContext, IUserCached userCached, 
            IUserRoleRelationContext userRoleRelationContext,
            IRoleOperationRelationBusiness roleOperationRelationBusiness)
        {
            this.userInfoContext = userInfoContext;
            this.userCached = userCached;
            this.userRoleRelationContext = userRoleRelationContext;
            this.roleOperationRelationBusiness = roleOperationRelationBusiness;
        }

        /// <summary>
        /// 通过手机号登录
        /// </summary>
        /// <param name="phoneNumber">手机号</param>
        /// <param name="password">密码</param>
        /// <param name="resultCode">结果码 -1:用户不存在 -2:密码错误 1:登录成功</param>
        /// <returns>sid</returns>
        public Dictionary<string, string> LoginByPhoneNumber(string phoneNumber, string password, out int resultCode)
        {
            var user = userInfoContext.SingleOrDefault(u => u.PhoneNumber == phoneNumber);

            if (user == null)
            {
                resultCode = -1;
                return null;
            }
            else if (user.Psw != password)
            {
                resultCode = -2;
                return null;
            }

            string sid = userCached.GetSidByUserId(user.Id);

            if (string.IsNullOrEmpty(sid))
            {
                var userRoleRelations = userRoleRelationContext.List(urr => urr.UserId == user.Id);
                if (userRoleRelations != null)
                    foreach (var userRole in userRoleRelations)
                        userRole.UserId = "";

                sid = Guid.NewGuid().ToString("N");
                userCached.SetUserCached(sid, user, userRoleRelations);
            }

            resultCode = 1;
            Dictionary<string, string> resDict = new Dictionary<string, string>();
            resDict.Add("sid", sid);
            resDict.Add(nameof(UserInfo.UserName), user.UserName);
            return resDict;
        }

        public List<Role> GetUserRoleBySid(string sid, string shopId)
        {
            var userRoles = userCached.GetUserRoleBySid(sid);

            if (userRoles != null && userRoles.Count > 0)
            {
                var roles = Roles.All()?.ToDictionary(r => r.Name);
                var res = new List<Role>();

                foreach (var role in userRoles)
                {
                    if (role.ShopId == shopId && roles.ContainsKey(role.RoleName))
                        res.Add(roles[role.RoleName]);
                }

                return res;
            }

            return null;
        }

        public List<Operation> GetUserOperationBySid(string sid, string shopId)
        {
            var userRoles = GetUserRoleBySid(sid, shopId);

            if (userRoles != null && userRoles.Count > 0)
            {
                List<string> roleNames = new List<string>();
                foreach (var role in userRoles)
                    roleNames.Add(role.Name);

                return roleOperationRelationBusiness.GetByRoleNames(roleNames.ToArray());
            }

            return null;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns>结果码=》1：成功 -1：手机号码已注册</returns>
        public int Add(UserInfo user)
        {
            UserInfo userTemp = userInfoContext.FirstOrDefault(u => u.PhoneNumber == user.PhoneNumber);

            if (userTemp != null)
            {
                return -1;
            }

            return userInfoContext.Add(user);
        }

        public UserInfo GetByByPhoneNumber(string phoneNumber)
        {
            return userInfoContext.FirstOrDefault(user => user.PhoneNumber == phoneNumber);
        }

        public void UpdateCachedExpire(string sid)
        {
            userCached.UpdateExpire(sid);
        }

        public bool CachedSidExists(string sid)
        {
            return userCached.SidExists(sid);
        }

        public void RemoveUserCached(string sid)
        {
            userCached.RemoveUserCached(sid);
        }

        public async Task<UserInfo> GetUserInfoFromCached(string sid)
        {
            return await userCached.GetUserInfoFromCached(sid);
        }
    }
}

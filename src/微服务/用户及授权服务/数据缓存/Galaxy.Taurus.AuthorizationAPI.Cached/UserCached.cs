using Galaxy.Taurus.AuthorizationAPI.Entitys;
using Galaxy.Taurus.AuthorizationAPI.ICached;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Galaxy.Taurus.AuthorizationAPI.Cached
{
    public class UserCached : IUserCached
    {
        private const string preUserKey = "u_";
        private const string preSidKey = "s_";
        private const int keyExpire = 30;

        public void SetUserCached(string sid, UserInfo userInfo, List<UserRoleRelation> userRole)
        {
            string sidKey = $"{preSidKey}{sid}";
            string userKey = $"{preUserKey}{userInfo.Id}";
            RedisStoreHelper.HashSetString(userKey, "Sid", sid);
            RedisStoreHelper.HashSetValue<UserInfo>(userKey, "UserInfo", userInfo);
            RedisStoreHelper.HashSetValue<List<UserRoleRelation>>(userKey, "UserRole", userRole);
            RedisStoreHelper.KeyExpire(userKey, TimeSpan.FromMinutes(keyExpire));

            RedisStoreHelper.SetString(sidKey, userKey, TimeSpan.FromMinutes(keyExpire));
        }

        public string GetSidByUserId(string userId)
        {
            string userKey = $"{preUserKey}{userId}";

            if (RedisStoreHelper.KeyExists(userKey))
            {
                return RedisStoreHelper.HashGetString(userKey, "Sid");
            }

            return null;
        }

        public List<UserRoleRelation> GetUserRoleBySid(string sid)
        {
            string sidKey = $"{preSidKey}{sid}";

            if (RedisStoreHelper.KeyExists(sidKey))
            {
                string userKey = RedisStoreHelper.GetString(sidKey);
                return RedisStoreHelper.HashGetValue<List<UserRoleRelation>>(userKey, "UserRole");
            }

            return null;
        }

        public void UpdateExpire(string sid)
        {
            string sidKey = $"{preSidKey}{sid}";

            if (RedisStoreHelper.KeyExists(sidKey))
            {
                string userKey = RedisStoreHelper.GetString(sidKey);
                RedisStoreHelper.KeyExpire(userKey, TimeSpan.FromMinutes(keyExpire));
                RedisStoreHelper.KeyExpire(sidKey, TimeSpan.FromMinutes(keyExpire));
            }
        }

        public bool SidExists(string sid)
        {
            string sidKey = $"{preSidKey}{sid}";
            return RedisStoreHelper.KeyExists(sidKey);
        }

        public void RemoveUserCached(string sid)
        {
            string sidKey = $"{preSidKey}{sid}";

            if (RedisStoreHelper.KeyExists(sidKey))
            {
                string userKey = RedisStoreHelper.GetString(sidKey);
                RedisStoreHelper.KeyDelete(userKey);

                RedisStoreHelper.KeyDelete(sidKey);
            }
        }

        public async Task<UserInfo> GetUserInfoFromCached(string sid)
        {
            string sidKey = $"{preSidKey}{sid}";

            if (RedisStoreHelper.KeyExists(sidKey))
            {
                string userKey = RedisStoreHelper.GetString(sidKey);
                return await RedisStoreHelper.HashGetValueAsync<UserInfo>(userKey, "UserInfo");
            }

            return null;
        }
    }
}

using Galaxy.Taurus.AuthorizationAPI.Entitys;
using Galaxy.Taurus.AuthorizationAPI.IBusiness;
using Galaxy.Taurus.AuthorizationAPI.ICached;
using Galaxy.Taurus.AuthorizationAPI.IDBAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Galaxy.Taurus.AuthorizationAPI.Business
{
    public class UserRoleRelationBusiness : IUserRoleRelationBusiness
    {
        private IUserCached userCached;
        private IUserRoleRelationContext userRoleRelationContext;

        public UserRoleRelationBusiness(IUserCached userCached, IUserRoleRelationContext userRoleRelationContext)
        {
            this.userCached = userCached;
            this.userRoleRelationContext = userRoleRelationContext;
        }

        public List<string> GetUserShopIdsBySid(string sid)
        {
            string userId = userCached.GetUserInfoFromCached(sid).GetAwaiter().GetResult()?.Id;
            List<UserRoleRelation> userRoleRelationList = userRoleRelationContext.List(u => u.UserId == userId);

            if (userRoleRelationList != null && userRoleRelationList.Count > 0)
            {
                Dictionary<string, UserRoleRelation> userRoleRelationDict = new Dictionary<string, UserRoleRelation>();
                List<string> shipIds = new List<string>();

                foreach (var item in userRoleRelationList)
                {
                    if (!userRoleRelationDict.ContainsKey(item.ShopId))
                    {
                        userRoleRelationDict.Add(item.ShopId, item);
                        shipIds.Add(item.ShopId);
                    }
                }

                return shipIds;
            }

            return null;
        }
    }
}

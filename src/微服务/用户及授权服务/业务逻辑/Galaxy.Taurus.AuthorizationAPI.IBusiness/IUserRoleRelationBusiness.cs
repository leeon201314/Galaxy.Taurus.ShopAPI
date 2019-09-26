using Galaxy.Taurus.AuthorizationAPI.Entitys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Taurus.AuthorizationAPI.IBusiness
{
    public interface IUserRoleRelationBusiness
    {
        List<string> GetUserShopIdsBySid(string sid);
    }
}

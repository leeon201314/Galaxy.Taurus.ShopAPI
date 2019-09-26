using Galaxy.Taurus.AuthorizationAPI.Entitys;
using System.Collections.Generic;

namespace Galaxy.Taurus.AuthorizationAPI.IBusiness
{
    public interface IRoleOperationRelationBusiness
    {
        List<Operation> GetByRoleNames(params string[] roleNames);
    }
}

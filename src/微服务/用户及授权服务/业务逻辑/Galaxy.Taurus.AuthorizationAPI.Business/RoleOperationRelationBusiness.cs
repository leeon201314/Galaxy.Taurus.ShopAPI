using Galaxy.Taurus.AuthorizationAPI.Entitys;
using Galaxy.Taurus.AuthorizationAPI.IBusiness;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Galaxy.Taurus.AuthorizationAPI.Business
{
    public class RoleOperationRelationBusiness : IRoleOperationRelationBusiness
    {
        public List<Operation> GetByRoleNames(params string[] roleNames)
        {
            Dictionary<string, ReadOnlyCollection<Operation>> dict =
                RoleOperationRelations.All().ToDictionary(r => r.Role.Name, r => r.Operations);
            Dictionary<string, Operation> ops = new Dictionary<string, Operation>();

            foreach (var roleName in roleNames)
            {
                if (dict.ContainsKey(roleName))
                {
                    foreach (var opItem in dict[roleName])
                    {
                        if (!ops.ContainsKey(opItem.Name))
                        {
                            ops.Add(opItem.Name, opItem);
                        }
                    }
                }
            }

            return ops.Values.ToList();
        }
    }
}

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Galaxy.Taurus.AuthorizationAPI.Entitys
{
    public class RoleOperationRelations
    {
        public static RoleOperationRelation ShopOwnerOperations
        {
            get
            {
                return new RoleOperationRelation(Roles.ShopOwner, new Operation[] {
                    Operations.ShopInfoManage,
                    Operations.GoodInfoManage,
                    Operations.FileInfoManage,
                    Operations.OrderManage
                });
            }
        }

        public static RoleOperationRelation MemberOperations
        {
            get
            {
                return new RoleOperationRelation(Roles.Member, new Operation[] {
                    Operations.SubmitOrder
                });
            }
        }

        public static List<RoleOperationRelation> All()
        {
            return new List<RoleOperationRelation> { ShopOwnerOperations, MemberOperations };
        }
    }

    public class RoleOperationRelation
    {
        public Role Role { get; private set; }

        public ReadOnlyCollection<Operation> Operations { get; private set; }

        public RoleOperationRelation(Role role, Operation[] operations)
        {
            Role = role;
            Operations = new ReadOnlyCollection<Operation>(operations);
        }
    }
}

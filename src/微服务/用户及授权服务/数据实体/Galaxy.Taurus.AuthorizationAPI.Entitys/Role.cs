using System.Collections.Generic;

namespace Galaxy.Taurus.AuthorizationAPI.Entitys
{
    public class Roles
    {
        public static readonly Role ShopOwner = new Role("ShopOwner", "店铺拥有者");

        public static readonly Role Member = new Role("Member", "会员");

        public static List<Role> All()
        {
            List<Role> roles = new List<Role>();
            roles.Add(Roles.ShopOwner);
            roles.Add(Roles.Member);
            return roles;
        }
    }

    public class Role
    {
        public string Name { get; private set; }

        public string RoleDesc { get; private set; }

        public Role(string name, string roleDesc)
        {
            Name = name;
            RoleDesc = roleDesc;
        }
    }
}

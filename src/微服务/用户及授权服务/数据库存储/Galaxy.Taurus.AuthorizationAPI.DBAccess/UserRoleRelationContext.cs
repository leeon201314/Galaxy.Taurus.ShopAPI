using Galaxy.Taurus.AuthorizationAPI.Entitys;
using Galaxy.Taurus.AuthorizationAPI.IDBAccess;
using Galaxy.Taurus.DBUtil;
using Microsoft.EntityFrameworkCore;

namespace Galaxy.Taurus.AuthorizationAPI.DBAccess
{
    public class UserRoleRelationContext : BaseContext<UserRoleRelation>, IUserRoleRelationContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRoleRelation>().HasKey(t => new { t.UserId, t.ShopId, t.RoleName });
            base.OnModelCreating(modelBuilder);
        }
    }
}

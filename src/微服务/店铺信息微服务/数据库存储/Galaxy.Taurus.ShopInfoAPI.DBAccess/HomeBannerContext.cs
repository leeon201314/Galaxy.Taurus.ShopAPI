using Galaxy.Taurus.DBUtil;
using Galaxy.Taurus.ShopInfoAPI.Entitys;
using Galaxy.Taurus.ShopInfoAPI.IDBAccess;
using Microsoft.EntityFrameworkCore;

namespace Galaxy.Taurus.ShopInfoAPI.DBAccess
{
    public class HomeBannerContext : BaseContext<HomeBanner>, IHomeBannerContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HomeBanner>().HasKey(t => new { t.ShopId, t.ShowIndex });
            base.OnModelCreating(modelBuilder);
        }
    }
}

using Galaxy.Taurus.ShopInfoAPI.Entitys;
using System.Collections.Generic;

namespace Galaxy.Taurus.ShopInfoAPI.IBusiness
{
    public interface IHomeBannerBusiness
    {
        List<HomeBanner> GetByShopId(string shopId);

        void AddOrUpdate(HomeBanner homeBanner);

        void Delete(HomeBanner homeBanner);

        HomeBanner GetByShowIndex(string shopId, int showIndex);
    }
}

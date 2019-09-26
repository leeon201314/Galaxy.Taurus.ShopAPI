using Galaxy.Taurus.ShopInfoAPI.Entitys;
using Galaxy.Taurus.ShopInfoAPI.IBusiness;
using Galaxy.Taurus.ShopInfoAPI.IDBAccess;
using System.Collections.Generic;

namespace Galaxy.Taurus.ShopInfoAPI.Business
{
    public class HomeBannerBusiness : IHomeBannerBusiness
    {
        private IHomeBannerContext homeBannerContext;

        public HomeBannerBusiness(IHomeBannerContext homeBannerContext)
        {
            this.homeBannerContext = homeBannerContext;
        }

        public List<HomeBanner> GetByShopId(string shopId)
        {
            return homeBannerContext.List(b => b.ShopId == shopId);
        }

        public HomeBanner GetByShowIndex(string shopId, int showIndex)
        {
            return homeBannerContext.FirstOrDefault(b => b.ShopId == shopId && b.ShowIndex == showIndex);
        }

        public void AddOrUpdate(HomeBanner homeBanner)
        {
            HomeBanner oldHomeBanner = homeBannerContext.FirstOrDefault(h => h.ShopId == homeBanner.ShopId && h.ShowIndex == homeBanner.ShowIndex);

            if (oldHomeBanner == null)
            {
                homeBannerContext.Add(homeBanner);
            }
            else
            {
                if (oldHomeBanner.PicUrl != homeBanner.PicUrl)
                {
                    oldHomeBanner.PicUrl = homeBanner.PicUrl;
                    homeBannerContext.Update(oldHomeBanner);
                }
            }
        }

        public void Delete(HomeBanner homeBanner)
        {
            homeBannerContext.DeleteSingle(b => b.ShopId == homeBanner.ShopId && b.ShowIndex == homeBanner.ShowIndex);
        }
    }
}

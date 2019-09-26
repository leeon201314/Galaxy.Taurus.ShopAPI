using Galaxy.Taurus.ShopInfoAPI.Entitys;
using System.Collections.Generic;

namespace Galaxy.Taurus.ShopInfoAPI.IBusiness
{
    public interface IShopInfoBusiness
    {
        int Add(ShopInfo shopInfo);

        void Update(ShopInfo shopInfo);

        void Delete(string shopId);

        ShopInfo GetById(string shopId);

        List<ShopInfo> GetByIds(string[] shopIds);
    }
}

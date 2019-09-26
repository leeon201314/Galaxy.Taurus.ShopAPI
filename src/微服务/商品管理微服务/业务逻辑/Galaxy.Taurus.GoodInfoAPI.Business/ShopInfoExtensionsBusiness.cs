using Galaxy.Taurus.GoodInfoAPI.Entitys;
using Galaxy.Taurus.GoodInfoAPI.IBusiness;
using Galaxy.Taurus.GoodInfoAPI.IDBAccess;

namespace Galaxy.Taurus.GoodInfoAPI.Business
{
    public class ShopInfoExtensionsBusiness : IShopInfoExtensionsBusiness
    {
        private IShopInfoExtensionsContext shopInfoExtensionsContext;

        public ShopInfoExtensionsBusiness(IShopInfoExtensionsContext shopInfoExtensionsContext)
        {
            this.shopInfoExtensionsContext = shopInfoExtensionsContext;
        }

        public ShopInfoExtensions Add(ShopInfoExtensions shopInfoExtensions)
        {
            shopInfoExtensionsContext.Add(shopInfoExtensions);
            return shopInfoExtensionsContext.SingleOrDefault(f => f.ShopId == shopInfoExtensions.ShopId);
        }

        /// <summary>
        /// 删除数据库中的记录
        /// </summary>
        public bool Delete(string shopId)
        {
            shopInfoExtensionsContext.DeleteSingle(fs => fs.ShopId == shopId);
            return true;
        }

        public ShopInfoExtensions Get(string shopId)
        {
            return shopInfoExtensionsContext.SingleOrDefault(f => f.ShopId == shopId);
        }

        public ShopInfoExtensions Update(ShopInfoExtensions shopInfoExtensions)
        {
            shopInfoExtensionsContext.Update(shopInfoExtensions);
            return shopInfoExtensionsContext.SingleOrDefault(f => f.ShopId == shopInfoExtensions.ShopId);
        }
    }
}

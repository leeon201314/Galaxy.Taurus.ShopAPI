using Galaxy.Taurus.GoodInfoAPI.Entitys;

namespace Galaxy.Taurus.GoodInfoAPI.IBusiness
{
    public interface IShopInfoExtensionsBusiness
    {
        ShopInfoExtensions Add(ShopInfoExtensions shopInfoExtensions);

        ShopInfoExtensions Update(ShopInfoExtensions shopInfoExtensions);

        bool Delete(string shopId);

        ShopInfoExtensions Get(string shopId);
    }
}

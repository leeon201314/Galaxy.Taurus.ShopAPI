using Galaxy.Taurus.FileAPI.Entitys;

namespace Galaxy.Taurus.FileAPI.IBusiness
{
    public interface IShopInfoExtensionsBusiness
    {
        ShopInfoExtensions Add(ShopInfoExtensions shopInfoExtensions);

        ShopInfoExtensions Update(ShopInfoExtensions shopInfoExtensions);

        bool Delete(string shopId);

        ShopInfoExtensions Get(string shopId);
    }
}

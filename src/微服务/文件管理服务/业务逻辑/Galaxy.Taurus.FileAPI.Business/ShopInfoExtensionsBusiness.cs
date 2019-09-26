using Galaxy.Taurus.FileAPI.Entitys;
using Galaxy.Taurus.FileAPI.IBusiness;
using Galaxy.Taurus.FileAPI.IDBRepository;
using Galaxy.Taurus.FileAPI.ServiceConfig;
using System.IO;

namespace Galaxy.Taurus.FileAPI.Business
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
        /// 删除set对应的文件夹下的内容，并删除数据库中的记录
        /// </summary>
        public bool Delete(string shopId)
        {
            string basePath = System.IO.Path.GetFullPath($"{ServiceConfigInfo.Single.MainDirectory}/{shopId}");

            if (Directory.Exists(basePath))
            {
                DirectoryInfo dir = new DirectoryInfo(basePath);
                dir.Delete(true);
            }

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

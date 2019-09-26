using Galaxy.Taurus.FileAPI.Dependency;
using Galaxy.Taurus.FileAPI.Entitys;
using Galaxy.Taurus.FileAPI.IBusiness;
using System;
using Xunit;

namespace Galaxy.Taurus.FileAPI.UTest
{
    public class FileGroupBusinessTest : BaseBusinessTest
    {
        [Fact]
        public void Test()
        {
            string shopId = "1";
            ShopInfoExtensions shopInfoExtensions = new ShopInfoExtensions { ShopId = shopId, LimitFileNum = 100, LimitGroupNum = 100 };
            shopInfoExtensions.GroupDataVersion = Guid.NewGuid().ToString("N");
            shopInfoExtensions.FileDataVersion = Guid.NewGuid().ToString("N");
            IShopInfoExtensionsBusiness shopInfoExtensionsBusiness = BusinessFactory.GetBusiness<IShopInfoExtensionsBusiness>();
            shopInfoExtensionsBusiness.Delete(shopId);
            shopInfoExtensionsBusiness.Add(shopInfoExtensions);

            IFileGroupBusiness fileGroupBusiness = BusinessFactory.GetBusiness<IFileGroupBusiness>();
            var f = fileGroupBusiness.Get(shopId, "123");
            Assert.Null(f);
            FileGroup fg = new FileGroup();
            fg.Name = "test";
            fg.ShopId = shopId;
            var res = fileGroupBusiness.Add(fg).Result;
            Assert.False(res.Code == 0);
            fg.Name = "test2";
            fg = fileGroupBusiness.Update(fg);
            Assert.False(fg.Name != "test2");
            var res2 = fileGroupBusiness.GetByShopId(shopId).Result;
            Assert.False(res2.Count == 0);
            fileGroupBusiness.Delete(shopId, fg.Id);
            var res3 = fileGroupBusiness.GetByShopId(shopId).Result;
            Assert.True(res3.Count == 0);
        }
    }
}

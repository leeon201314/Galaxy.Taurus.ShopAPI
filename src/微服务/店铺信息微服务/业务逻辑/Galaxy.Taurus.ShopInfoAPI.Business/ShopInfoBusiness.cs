using Galaxy.Taurus.ShopInfoAPI.Entitys;
using Galaxy.Taurus.ShopInfoAPI.IBusiness;
using Galaxy.Taurus.ShopInfoAPI.IDBAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Galaxy.Taurus.ShopInfoAPI.Business
{
    public class ShopInfoBusiness : IShopInfoBusiness
    {
        private IShopInfoContext shopInfoContext;

        public ShopInfoBusiness(IShopInfoContext shopInfoContext)
        {
            this.shopInfoContext = shopInfoContext;
        }

        public int Add(ShopInfo shopInfo)
        {
            return shopInfoContext.Add(shopInfo); ;
        }

        public void Update(ShopInfo shopInfo)
        {
            shopInfoContext.Update(shopInfo);
        }

        public void Delete(string shopId)
        {
            shopInfoContext.DeleteSingle(s => s.Id == shopId);
        }

        public ShopInfo GetById(string shopId)
        {
            return shopInfoContext.FirstOrDefault(s => s.Id == shopId);
        }

        public List<ShopInfo> GetByIds(string[] shopIds)
        {
            return shopInfoContext.List(shopInfo => shopIds.Contains(shopInfo.Id));
        }
    }
}

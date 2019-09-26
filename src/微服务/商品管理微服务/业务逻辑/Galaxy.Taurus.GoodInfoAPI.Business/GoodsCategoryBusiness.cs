using Galaxy.Taurus.GoodInfoAPI.Entitys;
using Galaxy.Taurus.GoodInfoAPI.IBusiness;
using Galaxy.Taurus.GoodInfoAPI.IDBAccess;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Galaxy.Taurus.GoodInfoAPI.Business
{
    public class GoodsCategoryBusiness : IGoodsCategoryBusiness
    {
        private IGoodsCategoryContext goodsCategoryContext;

        public GoodsCategoryBusiness(IGoodsCategoryContext goodsCategoryContext)
        {
            this.goodsCategoryContext = goodsCategoryContext;
        }

        public List<GoodsCategory> GetByShopId(string shopId)
        {
            return goodsCategoryContext.GetByShopId(shopId);
        }

        public Task<DataResult> Add(GoodsCategory goodsCategory)
        {
            goodsCategory.Id = Guid.NewGuid().ToString("N");
            return goodsCategoryContext.AddGoodsCategory(goodsCategory);
        }

        public GoodsCategory Update(GoodsCategory goodsCategory)
        {
            goodsCategoryContext.Update(goodsCategory);
            return goodsCategoryContext.SingleOrDefault(f => f.Id == goodsCategory.Id);
        }

        public bool Delete(string shopId, string categoryId)
        {
            goodsCategoryContext.DeleteSingle(f => f.Id == categoryId && f.ShopId == shopId);
            return true;
        }

        public Task<GoodsCategory> Get(string shopId, string categoryId)
        {
            return goodsCategoryContext.SingleOrDefaultAsync(f => f.Id == categoryId && f.ShopId == shopId);
        }
    }
}

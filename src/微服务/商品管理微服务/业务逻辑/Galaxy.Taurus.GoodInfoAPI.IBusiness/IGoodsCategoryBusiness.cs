using Galaxy.Taurus.GoodInfoAPI.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Galaxy.Taurus.GoodInfoAPI.IBusiness
{
    public interface IGoodsCategoryBusiness
    {
        List<GoodsCategory> GetByShopId(string shopId);

        Task<DataResult> Add(GoodsCategory goodsCategory);

        GoodsCategory Update(GoodsCategory goodsCategory);

        bool Delete(string shopId, string categoryId);

        Task<GoodsCategory> Get(string shopId, string categoryId);
    }
}

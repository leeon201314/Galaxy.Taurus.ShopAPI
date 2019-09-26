using Galaxy.Taurus.GoodInfoAPI.Entitys;
using Galaxy.Taurus.IDBUtil;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Galaxy.Taurus.GoodInfoAPI.IDBAccess
{
    public interface IGoodsCategoryContext : IBaseContext<GoodsCategory>
    {
        List<GoodsCategory> GetByShopId(string shopId);

        Task<DataResult> AddGoodsCategory(GoodsCategory goodsCategory);
    }
}

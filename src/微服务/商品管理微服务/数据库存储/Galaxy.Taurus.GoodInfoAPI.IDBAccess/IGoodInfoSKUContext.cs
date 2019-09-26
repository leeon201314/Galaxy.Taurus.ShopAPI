using Galaxy.Taurus.GoodInfoAPI.Entitys;
using Galaxy.Taurus.IDBUtil;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Galaxy.Taurus.GoodInfoAPI.IDBAccess
{
    public interface IGoodInfoSKUContext : IBaseContext<GoodInfoSKU>
    {
        Task<bool> DeleteAndAddSKU(List<GoodInfoSKU> skuList);

        Task<List<GoodInfoSKU>> GetByGoodId(string shopId, string goodId);
    }
}

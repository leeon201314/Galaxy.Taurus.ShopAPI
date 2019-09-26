using Galaxy.Taurus.GoodInfoAPI.Entitys;
using Galaxy.Taurus.GoodInfoAPI.Entitys.DTO;
using Galaxy.Taurus.IDBUtil;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Galaxy.Taurus.GoodInfoAPI.IDBAccess
{
    public interface IGoodInfoContext : IBaseContext<GoodInfo>
    {
        PageDataDTO<List<GoodInfo>> GetByFilter(string shopId, string categoryId, int status, int recommendStatus, int pageSize, int pageIndex);

        Task<DataResult> AddGoodInfo(GoodInfo goodInfo, GoodInfoExtensions goodInfoExtensions, List<GoodInfoSKU> skuList);

        void DeleteGoodInfo(string shopId, string goodId);
    }
}

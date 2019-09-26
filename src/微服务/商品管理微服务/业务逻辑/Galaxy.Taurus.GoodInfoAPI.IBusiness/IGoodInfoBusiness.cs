using Galaxy.Taurus.GoodInfoAPI.Entitys;
using Galaxy.Taurus.GoodInfoAPI.Entitys.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Galaxy.Taurus.GoodInfoAPI.IBusiness
{
    public interface IGoodInfoBusiness
    {
        PageDataDTO<List<GoodInfo>> GetByFilter(string shopId, string categoryId, int status, int recommendStatus, int pageSize, int pageIndex);

        GoodInfo GetById(string shopId, string goodId);

        void Update(GoodInfo goodInfo);

        Task<DataResult> AddGoodInfo(GoodInfo goodInfo, GoodInfoExtensionsDTO goodInfoExtensionsDTO, List<GoodInfoSKU> skuList);

        void DeleteGoodInfo(string shopId, string goodId);
    }
}

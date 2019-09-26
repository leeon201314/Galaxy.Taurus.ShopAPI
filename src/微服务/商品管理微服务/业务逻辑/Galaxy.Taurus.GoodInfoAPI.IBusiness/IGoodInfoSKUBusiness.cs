using Galaxy.Taurus.GoodInfoAPI.Entitys;
using Galaxy.Taurus.GoodInfoAPI.Entitys.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Galaxy.Taurus.GoodInfoAPI.IBusiness
{
    public interface IGoodInfoSKUBusiness
    {
        Task<List<GoodInfoSKU>> GetByGoodId(string shopId, string goodId);

        Task<bool> Update(string shopId, string goodId, List<GoodInfoSKU> skuList);

        Task<bool> InitSKU(string shopId, string goodId, List<GoodInfoSpecificationItem> specificationList);

        Task<GoodInfoSKU> GetSKUBySpecification(string shopId, string goodId,
            string specificationValue1,
            string specificationValue2,
            string specificationValue3);
    }
}

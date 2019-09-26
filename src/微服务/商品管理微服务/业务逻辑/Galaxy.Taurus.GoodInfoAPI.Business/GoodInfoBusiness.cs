using Galaxy.Taurus.GoodInfoAPI.Entitys;
using Galaxy.Taurus.GoodInfoAPI.Entitys.DTO;
using Galaxy.Taurus.GoodInfoAPI.IBusiness;
using Galaxy.Taurus.GoodInfoAPI.IDBAccess;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Galaxy.Taurus.GoodInfoAPI.Business
{
    public class GoodInfoBusiness : IGoodInfoBusiness
    {
        private IGoodInfoContext goodInfoContext;

        public GoodInfoBusiness(IGoodInfoContext goodInfoContext)
        {
            this.goodInfoContext = goodInfoContext;
        }

        public PageDataDTO<List<GoodInfo>> GetByFilter(string shopId, string categoryId, int status, int recommendStatus, int pageSize, int pageIndex)
        {
            return goodInfoContext.GetByFilter(shopId, categoryId, status, recommendStatus, pageSize, pageIndex);
        }

        public GoodInfo GetById(string shopId, string goodId)
        {
            return goodInfoContext.FirstOrDefault(g => g.ShopId == shopId && g.Id == goodId);
        }

        public void Update(GoodInfo goodInfo)
        {
            goodInfoContext.Update(goodInfo);
        }

        public Task<DataResult> AddGoodInfo(GoodInfo goodInfo, GoodInfoExtensionsDTO goodInfoExtensionsDTO, List<GoodInfoSKU> skuList)
        {
            GoodInfoExtensions goodInfoExtensions = new GoodInfoExtensions();
            goodInfoExtensions.ShopId = goodInfoExtensionsDTO.ShopId;
            goodInfoExtensions.GoodInfoId = goodInfoExtensionsDTO.GoodInfoId;

            if (goodInfoExtensionsDTO.BannerList != null && goodInfoExtensionsDTO.BannerList.Count > 0)
                goodInfoExtensions.Banner = JsonConvert.SerializeObject(goodInfoExtensionsDTO.BannerList);

            if (goodInfoExtensionsDTO.DescMediaList != null && goodInfoExtensionsDTO.DescMediaList.Count > 0)
                goodInfoExtensions.DescMedia = JsonConvert.SerializeObject(goodInfoExtensionsDTO.DescMediaList);

            if (goodInfoExtensionsDTO.SpecificationList != null && goodInfoExtensionsDTO.SpecificationList.Count > 0)
                goodInfoExtensions.Specification = JsonConvert.SerializeObject(goodInfoExtensionsDTO.SpecificationList);

            return goodInfoContext.AddGoodInfo(goodInfo, goodInfoExtensions, skuList);
        }

        public void DeleteGoodInfo(string shopId, string goodId)
        {
            goodInfoContext.DeleteGoodInfo(shopId, goodId);
        }
    }
}

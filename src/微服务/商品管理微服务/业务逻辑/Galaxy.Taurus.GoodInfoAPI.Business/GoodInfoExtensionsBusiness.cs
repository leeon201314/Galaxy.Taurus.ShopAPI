using Galaxy.Taurus.GoodInfoAPI.Entitys;
using Galaxy.Taurus.GoodInfoAPI.Entitys.DTO;
using Galaxy.Taurus.GoodInfoAPI.IBusiness;
using Galaxy.Taurus.GoodInfoAPI.IDBAccess;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Galaxy.Taurus.GoodInfoAPI.Business
{
    public class GoodInfoExtensionsBusiness : IGoodInfoExtensionsBusiness
    {
        private IGoodInfoExtensionsContext goodInfoExtensionsContext;

        public GoodInfoExtensionsBusiness(IGoodInfoExtensionsContext goodInfoExtensionsContext)
        {
            this.goodInfoExtensionsContext = goodInfoExtensionsContext;
        }

        public async Task<GoodInfoExtensionsDTO> GetById(string shopId, string goodId)
        {
            GoodInfoExtensions goodInfoExtensions = await goodInfoExtensionsContext.FirstOrDefaultAsync(g => g.ShopId == shopId && g.GoodInfoId == goodId);

            if (goodInfoExtensions == null)
                return null;

            GoodInfoExtensionsDTO goodInfoExtensionsDTO = new GoodInfoExtensionsDTO();
            goodInfoExtensionsDTO.ShopId = goodInfoExtensions.ShopId;
            goodInfoExtensionsDTO.GoodInfoId = goodInfoExtensions.GoodInfoId;

            try
            {
                if (!string.IsNullOrEmpty(goodInfoExtensions.Banner))
                    goodInfoExtensionsDTO.BannerList = JsonConvert.DeserializeObject<List<MediaItem>>(goodInfoExtensions.Banner);
            }
            catch { }

            try
            {
                if (!string.IsNullOrEmpty(goodInfoExtensions.DescMedia))
                    goodInfoExtensionsDTO.DescMediaList = JsonConvert.DeserializeObject<List<MediaItem>>(goodInfoExtensions.DescMedia);
            }
            catch { }

            try
            {
                if (!string.IsNullOrEmpty(goodInfoExtensions.Specification))
                    goodInfoExtensionsDTO.SpecificationList = JsonConvert.DeserializeObject<List<GoodInfoSpecificationItem>>(goodInfoExtensions.Specification);
            }
            catch { }

            return goodInfoExtensionsDTO;
        }

        public void Update(GoodInfoExtensionsDTO goodInfoExtensionsDTO)
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

            goodInfoExtensionsContext.Update(goodInfoExtensions);
        }
    }
}

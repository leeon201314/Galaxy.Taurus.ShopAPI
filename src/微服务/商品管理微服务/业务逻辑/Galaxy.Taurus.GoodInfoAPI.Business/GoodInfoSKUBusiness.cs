using Galaxy.Taurus.GoodInfoAPI.Entitys;
using Galaxy.Taurus.GoodInfoAPI.Entitys.DTO;
using Galaxy.Taurus.GoodInfoAPI.IBusiness;
using Galaxy.Taurus.GoodInfoAPI.IDBAccess;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Galaxy.Taurus.GoodInfoAPI.Business
{
    public class GoodInfoSKUBusiness : IGoodInfoSKUBusiness
    {
        private IGoodInfoSKUContext goodInfoSKUContext;
        private IGoodInfoExtensionsBusiness goodInfoExtensionsBusiness;

        public GoodInfoSKUBusiness(IGoodInfoSKUContext goodInfoSKUContext, IGoodInfoExtensionsBusiness goodInfoExtensionsBusiness)
        {
            this.goodInfoSKUContext = goodInfoSKUContext;
            this.goodInfoExtensionsBusiness = goodInfoExtensionsBusiness;
        }

        public Task<List<GoodInfoSKU>> GetByGoodId(string shopId, string goodId)
        {
            return goodInfoSKUContext.GetByGoodId(shopId, goodId);
        }

        public Task<GoodInfoSKU> GetSKUBySpecification(string shopId, string goodId,
            string specificationValue1, 
            string specificationValue2, 
            string specificationValue3)
        {
            return goodInfoSKUContext.FirstOrDefaultAsync(sku => sku.ShopId == shopId && sku.GoodInfoId == goodId
            && sku.SpecificationValue1 == specificationValue1
            && sku.SpecificationValue2 == specificationValue2
            && sku.SpecificationValue3 == specificationValue3);
        }

        public async Task<bool> Update(string shopId, string goodId, List<GoodInfoSKU> skuList)
        {
            foreach (var skuItem in skuList)
            {
                skuItem.ShopId = shopId;
                skuItem.GoodInfoId = goodId;
            }

            await goodInfoSKUContext.DeleteAndAddSKU(skuList);
            return true;
        }

        public async Task<bool> InitSKU(string shopId, string goodId, List<GoodInfoSpecificationItem> specificationList)
        {
            if (specificationList != null
                && specificationList.Count > 0)
            {
                List<GoodInfoSKU> skuList = new List<GoodInfoSKU>();

                if (specificationList.Count == 1)
                {
                    GoodInfoSpecificationItem spItem = specificationList[0];

                    foreach (var spValueItem in spItem.Values)
                    {
                        skuList.Add(new GoodInfoSKU
                        {
                            SKUId = Guid.NewGuid().ToString("N"),
                            ShopId = shopId,
                            GoodInfoId = goodId,
                            Specification1 = $"{spItem.Name}_{spValueItem.Name}"
                        });
                    }
                }

                if (specificationList.Count == 2)
                {
                    GoodInfoSpecificationItem spItem1 = specificationList[0];
                    GoodInfoSpecificationItem spItem2 = specificationList[1];

                    foreach (var spValueItem1 in spItem1.Values)
                    {
                        foreach (var spValueItem2 in spItem2.Values)
                        {
                            skuList.Add(new GoodInfoSKU
                            {
                                SKUId = Guid.NewGuid().ToString("N"),
                                ShopId = shopId,
                                GoodInfoId = goodId,
                                Specification1 = $"{spItem1.Name}_{spValueItem1.Name}",
                                Specification2 = $"{spItem2.Name}_{spValueItem2.Name}"
                            });
                        }
                    }
                }

                if (specificationList.Count == 3)
                {
                    GoodInfoSpecificationItem spItem1 = specificationList[0];
                    GoodInfoSpecificationItem spItem2 = specificationList[1];
                    GoodInfoSpecificationItem spItem3 = specificationList[3];

                    foreach (var spValueItem1 in spItem1.Values)
                    {
                        foreach (var spValueItem2 in spItem2.Values)
                        {
                            foreach (var spValueItem3 in spItem3.Values)
                            {
                                skuList.Add(new GoodInfoSKU
                                {
                                    SKUId = Guid.NewGuid().ToString("N"),
                                    ShopId = shopId,
                                    GoodInfoId = goodId,
                                    Specification1 = $"{spItem1.Name}_{spValueItem1.Name}",
                                    Specification2 = $"{spItem2.Name}_{spValueItem2.Name}",
                                    Specification3 = $"{spItem3.Name}_{spValueItem3.Name}"
                                });
                            }
                        }
                    }
                }

                return await goodInfoSKUContext.DeleteAndAddSKU(skuList);
            }

            return false;
        }
    }
}

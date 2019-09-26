using Galaxy.Taurus.GoodInfoAPI.Entitys;
using Galaxy.Taurus.GoodInfoAPI.Entitys.DTO;
using Galaxy.Taurus.GoodInfoAPI.IBusiness;
using Galaxy.Taurus.GoodInfoAPI.RequestParams;
using Galaxy.Taurus.GoodInfoAPI.Util;
using Galaxy.Taurus.GoodInfoAPI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Galaxy.Taurus.GoodInfoAPI.Controllers
{
    /// <summary>
    /// 商品信息
    /// </summary>
    [Route("goodInfoAPI/[controller]")]
    [ApiController]
    public class GoodInfoController : ControllerBase
    {
        private IGoodInfoBusiness goodInfoBusiness;
        private IGoodInfoExtensionsBusiness goodInfoExtensionsBusiness;
        private IGoodInfoSKUBusiness goodInfoSKUBusiness;

        /// <summary>
        /// 商品信息
        /// </summary>
        public GoodInfoController(IGoodInfoBusiness goodInfoBusiness,
            IGoodInfoExtensionsBusiness goodInfoExtensionsBusiness,
            IGoodInfoSKUBusiness goodInfoSKUBusiness)
        {
            this.goodInfoBusiness = goodInfoBusiness;
            this.goodInfoExtensionsBusiness = goodInfoExtensionsBusiness;
            this.goodInfoSKUBusiness = goodInfoSKUBusiness;
        }

        /// <summary>
        /// 查询商品信息
        /// </summary>
        [HttpPost("GetListByFilter")]
        public ResultViewModel GetListByFilter([FromBody]GoodInfoQueryParams goodInfoQueryParams)
        {
            if (goodInfoQueryParams == null || string.IsNullOrEmpty(goodInfoQueryParams.ShopId))
                return new ResultViewModel { Code = ResultCode.ParamsError, Message = "参数错误" };

            if (goodInfoQueryParams.PageIndex < 0)
                goodInfoQueryParams.PageIndex = 0;

            if (goodInfoQueryParams.PageSize >= 200)
                goodInfoQueryParams.PageSize = 200;
            else if (goodInfoQueryParams.PageSize < 0)
                goodInfoQueryParams.PageSize = 20;

            return new ResultViewModel
            {
                Code = ResultCode.Success,
                Message = ResultMessage.Success,
                Data = goodInfoBusiness.GetByFilter(goodInfoQueryParams.ShopId, goodInfoQueryParams.CategoryId,
                    goodInfoQueryParams.Status, goodInfoQueryParams.recommendStatus,
                    goodInfoQueryParams.PageSize, goodInfoQueryParams.PageIndex)
            };
        }

        /// <summary>
        /// 通过ID查询商品信息
        /// </summary>
        /// <param name="shopId">店铺ID</param>
        /// <param name="goodId">商品ID</param>
        [HttpGet("GetById/{shopId}/{goodId}")]
        public async Task<ResultViewModel> GetById(string shopId, string goodId)
        {
            GoodInfo basicInfo = goodInfoBusiness.GetById(shopId, goodId);
            GoodInfoExtensionsDTO goodInfoExtensions = await goodInfoExtensionsBusiness.GetById(shopId, goodId);
            List<GoodInfoSKU> skuList = await goodInfoSKUBusiness.GetByGoodId(shopId, goodId);

            if (basicInfo == null || goodInfoExtensions == null)
                return new ResultViewModel
                {
                    Code = ResultCode.ParamsError,
                    Message = "参数错误，找不到指定商品信息！",
                };

            GoodDetailInfo goodDetailInfo = AutoMapperUtil.Singleton.Map<GoodDetailInfo>(goodInfoExtensions);
            goodDetailInfo.BasicInfo = AutoMapperUtil.Singleton.Map<GoodBasicInfoDTO>(basicInfo);
            goodDetailInfo.SKUList = AutoMapperUtil.Singleton.Map<List<GoodInfoSKUDTO>>(skuList);

            return new ResultViewModel
            {
                Code = ResultCode.Success,
                Message = ResultMessage.Success,
                Data = goodDetailInfo
            };
        }

        /// <summary>
        /// 通过规格查询商品价格
        /// </summary>
        /// <param name="getGoodPriceParamsList">参数</param>
        [HttpGet("GetPrice")]
        public async Task<ResultViewModel> GetPrice([FromBody] List<GetGoodPriceParams> getGoodPriceParamsList)
        {
            List<GoodPriceViewModel> goodPriceList = new List<ViewModel.GoodPriceViewModel>();

            foreach (var paramsItem in getGoodPriceParamsList)
            {
                if (string.IsNullOrEmpty(paramsItem.ShopId) || string.IsNullOrEmpty(paramsItem.GoodInfoId))
                    break;
                GoodInfoExtensionsDTO goodInfoExtensions = await goodInfoExtensionsBusiness.GetById(paramsItem.ShopId, paramsItem.GoodInfoId);
                if (goodInfoExtensions == null)
                    break;

                //如果没有规格，直接返回默认值
                if (goodInfoExtensions.SpecificationList == null || goodInfoExtensions.SpecificationList.Count <= 0)
                {
                    GoodInfo basicInfo = goodInfoBusiness.GetById(paramsItem.ShopId, paramsItem.GoodInfoId);
                    if (basicInfo == null)
                        break;
                    GoodPriceViewModel price = AutoMapperUtil.Singleton.Map<GoodPriceViewModel>(paramsItem);
                    price.Price = basicInfo.Price;
                    break;
                }

                GoodInfoSKU sku = await goodInfoSKUBusiness.GetSKUBySpecification(paramsItem.ShopId, paramsItem.GoodInfoId,
                    paramsItem.SpecificationValue1,
                    paramsItem.SpecificationValue2,
                    paramsItem.SpecificationValue3);

                
                if (sku != null)
                {
                    GoodPriceViewModel price = AutoMapperUtil.Singleton.Map<GoodPriceViewModel>(paramsItem);
                    price.Price = sku.Price;
                }
                else //如果没有SKU，直接返回默认值
                {
                    GoodInfo basicInfo = goodInfoBusiness.GetById(paramsItem.ShopId, paramsItem.GoodInfoId);
                    if (basicInfo == null)
                        break;
                    GoodPriceViewModel price = AutoMapperUtil.Singleton.Map<GoodPriceViewModel>(paramsItem);
                    price.Price = basicInfo.Price;
                    break;
                }
            }

            return new ResultViewModel
            {
                Code = ResultCode.Success,
                Message = ResultMessage.Success,
                Data = goodPriceList
            };
        }

        /// <summary>
        /// 更新商品信息
        /// </summary>
        [HttpPost("UpdateGoodInfo")]
        [Authorize("GoodInfoManage")]
        public async Task<ResultViewModel> UpdateGoodInfo([FromBody]GoodDetailInfo goodDetailInfo)
        {
            if (goodDetailInfo == null || string.IsNullOrEmpty(goodDetailInfo.GoodInfoId))
                return new ResultViewModel { Code = ResultCode.ParamsError, Message = "参数错误" };

            ResultViewModel valRes = GoodDetailInfoValidator.Validate(goodDetailInfo);
            if (valRes.Code != ResultCode.Success)
                return valRes;

            goodDetailInfo.ShopId = this.CurrentAuthShopId();
            goodDetailInfo.BasicInfo.ShopId = this.CurrentAuthShopId();
            goodDetailInfo.BasicInfo.Id = goodDetailInfo.GoodInfoId;

            GoodInfo goodBasicInfo = AutoMapperUtil.Singleton.Map<GoodInfo>(goodDetailInfo.BasicInfo);
            goodInfoBusiness.Update(goodBasicInfo);

            GoodInfoExtensionsDTO goodInfoExtensions = AutoMapperUtil.Singleton.Map<GoodInfoExtensionsDTO>(goodDetailInfo);
            goodInfoExtensionsBusiness.Update(goodInfoExtensions);

            List<GoodInfoSKU> skuList = AutoMapperUtil.Singleton.Map<List<GoodInfoSKU>>(goodDetailInfo.SKUList);
            await goodInfoSKUBusiness.Update(this.CurrentAuthShopId(), goodDetailInfo.GoodInfoId, skuList);

            return new ResultViewModel { Code = ResultCode.Success, Message = "操作成功！" };
        }

        /// <summary>
        /// 添加商品信息
        /// </summary>
        [HttpPost("AddGoodInfo")]
        [Authorize("GoodInfoManage")]
        public async Task<ResultViewModel> AddGoodInfo([FromBody]GoodDetailInfo goodDetailInfo)
        {
            if (goodDetailInfo == null)
                return new ResultViewModel { Code = ResultCode.ParamsError, Message = "参数错误" };

            ResultViewModel valRes = GoodDetailInfoValidator.Validate(goodDetailInfo);
            if (valRes.Code != ResultCode.Success)
                return valRes;

            goodDetailInfo.ShopId = this.CurrentAuthShopId();
            goodDetailInfo.GoodInfoId = Guid.NewGuid().ToString("N");
            goodDetailInfo.BasicInfo.ShopId = this.CurrentAuthShopId();
            goodDetailInfo.BasicInfo.Id = goodDetailInfo.GoodInfoId;
            GoodInfo goodBasicInfo = AutoMapperUtil.Singleton.Map<GoodInfo>(goodDetailInfo.BasicInfo);
            GoodInfoExtensionsDTO goodInfoExtensions = AutoMapperUtil.Singleton.Map<GoodInfoExtensionsDTO>(goodDetailInfo);
            List<GoodInfoSKU> skuList = AutoMapperUtil.Singleton.Map<List<GoodInfoSKU>>(goodDetailInfo.SKUList);
            foreach (var skuItem in skuList)
            {
                skuItem.ShopId = this.CurrentAuthShopId();
                skuItem.GoodInfoId = goodDetailInfo.GoodInfoId;
            }

            DataResult dataResult = await goodInfoBusiness.AddGoodInfo(goodBasicInfo, goodInfoExtensions, skuList);

            return AutoMapperUtil.Singleton.Map<DataResult, ResultViewModel>(dataResult);
        }

        /// <summary>
        /// 删除商品信息
        /// </summary>
        [HttpPost("DeleteGoodInfo/{goodId}")]
        [Authorize("GoodInfoManage")]
        public ResultViewModel DeleteGoodInfo(string goodId)
        {
            goodInfoBusiness.DeleteGoodInfo(this.CurrentAuthShopId(), goodId);
            return new ResultViewModel { Code = ResultCode.Success, Message = "操作成功！" };
        }
    }
}

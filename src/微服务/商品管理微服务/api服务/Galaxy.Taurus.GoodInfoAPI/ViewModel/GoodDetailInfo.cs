using Galaxy.Taurus.GoodInfoAPI.Entitys.DTO;
using System.Collections.Generic;

namespace Galaxy.Taurus.GoodInfoAPI.ViewModel
{
    /// <summary>
    /// 商品详细信息
    /// </summary>
    public class GoodDetailInfo
    {
        /// <summary>
        /// 商品Id
        /// </summary>
        public string GoodInfoId { get; set; }

        /// <summary>
        /// 店铺Id
        /// </summary>
        public string ShopId { get; set; }

        /// <summary>
        /// 基本信息
        /// </summary>
        public GoodBasicInfoDTO BasicInfo { get; set; }

        /// <summary>
        /// 横幅的图片或视频
        /// </summary>
        public List<MediaItem> BannerList { get; set; }

        /// <summary>
        /// 详细信息图片或视频
        /// </summary>
        public List<MediaItem> DescMediaList { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public List<GoodInfoSpecificationItem> SpecificationList { get; set; }

        /// <summary>
        /// SKU
        /// </summary>
        public List<GoodInfoSKUDTO> SKUList { get; set; }
    }

    /// <summary>
    /// 商品详细信息验证
    /// </summary>
    public class GoodDetailInfoValidator
    {
        /// <summary>
        /// 验证参数
        /// </summary>
        public static ResultViewModel Validate(GoodDetailInfo goodDetailInfo)
        {
            if (goodDetailInfo.BasicInfo == null)
                return new ResultViewModel { Code = ResultCode.ParamsError, Message = "商品基本信息缺失！" };

            ResultViewModel validateBannerRes = ValidateBanner(goodDetailInfo);
            if (validateBannerRes.Code != ResultCode.Success)
                return validateBannerRes;

            ResultViewModel validateDescRes = ValidateDesc(goodDetailInfo);
            if (validateDescRes.Code != ResultCode.Success)
                return validateDescRes;

            ResultViewModel validateSpecificationRes = ValidateSpecification(goodDetailInfo);
            if (validateSpecificationRes.Code != ResultCode.Success)
                return validateSpecificationRes;

            ResultViewModel validateSKURes = ValidateSKU(goodDetailInfo);
            if (validateSKURes.Code != ResultCode.Success)
                return validateSKURes;

            return new ResultViewModel { Code = ResultCode.Success };
        }

        /// <summary>
        /// 验证商品横幅信息
        /// </summary>
        private static ResultViewModel ValidateBanner(GoodDetailInfo goodDetailInfo)
        {
            if (goodDetailInfo.BannerList != null && goodDetailInfo.BannerList.Count > 0)
            {
                if (goodDetailInfo.BannerList.Count > 6)
                {
                    return new ResultViewModel { Code = ResultCode.ParamsError, Message = "最多允许6张横幅图片" };
                }

                foreach (MediaItem item in goodDetailInfo.BannerList)
                {
                    if (item.MediaUrl.Length > 250)
                        return new ResultViewModel { Code = ResultCode.ParamsError, Message = "横图图片URL超长" };
                }
            }

            return new ResultViewModel { Code = ResultCode.Success };
        }

        /// <summary>
        /// 验证商品描述信息
        /// </summary>
        private static ResultViewModel ValidateDesc(GoodDetailInfo goodDetailInfo)
        {
            if (goodDetailInfo.DescMediaList != null && goodDetailInfo.DescMediaList.Count > 0)
            {
                if (goodDetailInfo.DescMediaList.Count > 15)
                {
                    return new ResultViewModel { Code = ResultCode.ParamsError, Message = "最多允许15张详细介绍图片" };
                }

                foreach (MediaItem item in goodDetailInfo.DescMediaList)
                {
                    if (item.MediaUrl.Length > 250)
                        return new ResultViewModel { Code = ResultCode.ParamsError, Message = "描述图片URL超长" };
                }
            }

            return new ResultViewModel { Code = ResultCode.Success };
        }

        /// <summary>
        /// 验证商品规格信息
        /// </summary>
        private static ResultViewModel ValidateSpecification(GoodDetailInfo goodDetailInfo)
        {
            if (goodDetailInfo.SpecificationList != null && goodDetailInfo.SpecificationList.Count > 0)
            {
                if (goodDetailInfo.SpecificationList.Count > 3)
                {
                    return new ResultViewModel { Code = ResultCode.ParamsError, Message = "最多允许3种规格" };
                }

                Dictionary<string, string> spDict = new Dictionary<string, string>();

                foreach (GoodInfoSpecificationItem item in goodDetailInfo.SpecificationList)
                {
                    if (item.Name.Length > 30)
                        return new ResultViewModel { Code = ResultCode.ParamsError, Message = "规格名称超长" };

                    if (spDict.ContainsKey(item.Name))
                        return new ResultViewModel { Code = ResultCode.ParamsError, Message = $"存在相同规格名称->{item.Name}" };
                    else
                        spDict.Add(item.Name, item.Name);

                    if (item.Values != null && item.Values.Count > 0)
                    {
                        if (item.Values.Count > 30)
                            return new ResultViewModel { Code = ResultCode.ParamsError, Message = "规格选项超过30种" };

                        Dictionary<string, string> valueDict = new Dictionary<string, string>();

                        foreach (var valueItem in item.Values)
                        {
                            if (valueItem.Name.Length > 30)
                                return new ResultViewModel { Code = ResultCode.ParamsError, Message = "规格选项名称超长" };

                            if (valueDict.ContainsKey(valueItem.Name))
                                return new ResultViewModel { Code = ResultCode.ParamsError, Message = $"(规格:{item.Name})存在相同规格值->{valueItem.Name}" };
                            else
                                valueDict.Add(valueItem.Name, valueItem.Name);
                        }
                    }
                }
            }

            return new ResultViewModel { Code = ResultCode.Success };
        }


        /// <summary>
        /// 验证商品SKU
        /// </summary>
        private static ResultViewModel ValidateSKU(GoodDetailInfo goodDetailInfo)
        {
            if (goodDetailInfo.SKUList != null && goodDetailInfo.SKUList.Count > 0)
            {
                if (goodDetailInfo.SpecificationList.Count > 600)
                {
                    return new ResultViewModel { Code = ResultCode.ParamsError, Message = "SKU列表超长！" };
                }
            }

            return new ResultViewModel { Code = ResultCode.Success };
        }
    }
}

using Galaxy.Taurus.ShopInfoAPI.Entitys;
using Galaxy.Taurus.ShopInfoAPI.IBusiness;
using Galaxy.Taurus.ShopInfoAPI.RequestParams;
using Galaxy.Taurus.ShopInfoAPI.Util;
using Galaxy.Taurus.ShopInfoAPI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Galaxy.Taurus.ShopInfoAPI.Controllers
{
    /// <summary>
    /// HomeBanner API
    /// </summary>
    [Route("shopInfoAPI/[controller]")]
    public class HomeBannerController : Controller
    {
        private IHomeBannerBusiness homeBannerBusiness;

        /// <summary>
        /// HomeBanner控制器
        /// </summary>
        public HomeBannerController(IHomeBannerBusiness homeBannerBusiness)
        {
            this.homeBannerBusiness = homeBannerBusiness;
        }

        /// <summary>
        /// 通过shopId获取HomeBanner
        /// </summary>
        [HttpGet("GetByShopId/{shopId}")]
        public ResultViewModel GetByShopId(string shopId)
        {
            if (string.IsNullOrEmpty(shopId))
                return null;

            return new ResultViewModel
            {
                Code = ResultCode.Success,
                Data = homeBannerBusiness.GetByShopId(shopId)
            };
        }

        /// <summary>
        /// 获取主页Banner图片
        /// </summary>
        [HttpPost("GetByShowIndex")]
        public MessageViewModel GetByShowIndex([FromBody]BannerGetByShowIndexParams bannerGetByShowIndexParams)
        {
            if (bannerGetByShowIndexParams == null
                || string.IsNullOrEmpty(bannerGetByShowIndexParams.ShopId)
                || bannerGetByShowIndexParams.ShowIndex > 6
                || bannerGetByShowIndexParams.ShowIndex < 1)
                return new MessageViewModel { Code = MessageCode.ParamsError, Message = "参数错误！" };

            return new MessageViewModel
            {
                Code = MessageCode.Success,
                Message = "获取成功！",
                Data = homeBannerBusiness.GetByShowIndex(bannerGetByShowIndexParams.ShopId, bannerGetByShowIndexParams.ShowIndex)
            };
        }

        /// <summary>
        /// 添加或更新homeBanner
        /// </summary>
        [HttpPost("AddOrUpdate")]
        [Authorize("ShopInfoManage")]
        public MessageViewModel AddOrUpdate([FromBody]HomeBanner homeBanner)
        {
            if (homeBanner == null || homeBanner.ShowIndex > 6 || homeBanner.ShowIndex < 0 || string.IsNullOrEmpty(homeBanner.PicUrl))
            {
                return new MessageViewModel
                {
                    Code = MessageCode.ParamsError,
                    Message = "参数错误！"
                };
            }

            homeBanner.ShopId = this.CurrentAuthShopId();
            homeBannerBusiness.AddOrUpdate(homeBanner);

            return new MessageViewModel
            {
                Code = MessageCode.Success,
                Message = "操作成功！"
            };
        }

        /// <summary>
        /// 删除homeBanner
        /// </summary>
        [HttpPost("Delete")]
        [Authorize("ShopInfoManage")]
        public MessageViewModel Delete([FromBody]HomeBanner homeBanner)
        {
            string authShopId = this.CurrentAuthShopId();

            if (homeBanner == null || homeBanner.ShowIndex > 6 || homeBanner.ShowIndex < 0)
            {
                return new MessageViewModel
                {
                    Code = MessageCode.ParamsError,
                    Message = "参数错误！"
                };
            }

            homeBanner.ShopId = this.CurrentAuthShopId();
            homeBannerBusiness.Delete(homeBanner);

            return new MessageViewModel
            {
                Code = MessageCode.Success,
                Message = "操作成功！"
            };
        }
    }
}

using Galaxy.Taurus.GoodInfoAPI.Entitys;
using Galaxy.Taurus.GoodInfoAPI.IBusiness;
using Galaxy.Taurus.GoodInfoAPI.RequestParams;
using Galaxy.Taurus.GoodInfoAPI.Util;
using Galaxy.Taurus.GoodInfoAPI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Galaxy.Taurus.GoodInfoAPI.Controllers
{
    [Route("goodInfoAPI/[controller]")]
    [ApiController]
    public class GoodsCategoryController : ControllerBase
    {
        private IGoodsCategoryBusiness goodsCategoryBusiness;

        public GoodsCategoryController(IGoodsCategoryBusiness goodsCategoryBusiness)
        {
            this.goodsCategoryBusiness = goodsCategoryBusiness;
        }

        /// <summary>
        /// 通过shopId获取商品分类信息
        /// </summary>
        /// <param name="shopId">商店ID</param>
        [HttpGet("GetByShopId/{shopId}")]
        public ResultViewModel GetByShopId(string shopId)
        {
            if (string.IsNullOrEmpty(shopId))
                return null;

            return new ResultViewModel
            {
                Code = ResultCode.Success,
                Data = goodsCategoryBusiness.GetByShopId(shopId)
            };
        }

        /// <summary>
        /// 删除分组
        /// </summary>
        [HttpPost("delete/{categoryId}")]
        [Authorize("GoodInfoManage")]
        public ResultViewModel Delete(string categoryId)
        {
            if (string.IsNullOrEmpty(categoryId))
                return new ResultViewModel { Code = ResultCode.ParamsError, Message = "分组Id不允许空" };

            goodsCategoryBusiness.Delete(this.CurrentAuthShopId(), categoryId);
            return new ResultViewModel { Code = ResultCode.Success, Message = "删除成功！" };
        }

        /// <summary>
        ///添加分组
        /// </summary>
        [HttpPost("Add")]   
        [Authorize("GoodInfoManage")]
        public async Task<ResultViewModel> Add([FromBody] AddCategoryParams addCategoryParams)
        {
            ResultViewModel msg = AddCategoryParamsValidator.Validate(addCategoryParams);
            if (msg.Code != ResultCode.Success)
                return msg;

            GoodsCategory goodsCategory = new GoodsCategory();
            goodsCategory.ShopId = this.CurrentAuthShopId();
            goodsCategory.Name = addCategoryParams.Name;
            goodsCategory.ShowIndex = addCategoryParams.ShowIndex;
            DataResult dataResult = await goodsCategoryBusiness.Add(goodsCategory);

            return AutoMapperUtil.Singleton.Map<DataResult, ResultViewModel>(dataResult);
        }

        /// <summary>
        /// 修改分组
        /// </summary>
        [HttpPost("Update")]
        [Authorize("GoodInfoManage")]
        public async Task<ResultViewModel> Update([FromBody] UpdateCategoryParams updateCategoryParams)
        {
            ResultViewModel msg = UpdateCategoryParamsValidator.Validate(updateCategoryParams);
            if (msg.Code != ResultCode.Success)
                return msg;

            GoodsCategory goodsCategory = await goodsCategoryBusiness.Get(this.CurrentAuthShopId(), updateCategoryParams.Id);

            if (goodsCategory == null)
                return new ResultViewModel { Code = ResultCode.Fail, Message = "分组不存在" };

            goodsCategory.Name = updateCategoryParams.Name;
            goodsCategory.ShowIndex = updateCategoryParams.ShowIndex;
            goodsCategoryBusiness.Update(goodsCategory);
            return new ResultViewModel { Code = ResultCode.Success, Message = "操作成功" };
        }
    }
}

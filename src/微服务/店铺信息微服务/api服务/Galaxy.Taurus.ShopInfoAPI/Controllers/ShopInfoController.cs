using Galaxy.Taurus.ShopInfoAPI.Entitys;
using Galaxy.Taurus.ShopInfoAPI.IBusiness;
using Galaxy.Taurus.ShopInfoAPI.Util;
using Galaxy.Taurus.ShopInfoAPI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Galaxy.Taurus.ShopInfoAPI.Controllers
{
    [Route("shopInfoAPI/[controller]")]
    [ApiController]
    public class ShopInfoController : Controller
    {
        private IShopInfoBusiness shopInfoBusiness;

        public ShopInfoController(IShopInfoBusiness shopInfoBusiness)
        {
            this.shopInfoBusiness = shopInfoBusiness;
        }

        /// <summary>
        /// 通过店铺Id获取店铺信息
        /// </summary>
        /// <param name="shopId">店铺Id</param>
        [HttpGet("getById/{shopId}")]
        public ResultViewModel GetById(string shopId)
        {
            ShopInfo shopInfo = shopInfoBusiness.GetById(shopId);

            if (shopInfo != null)
            {
                return new ResultViewModel
                {
                    Code = ResultCode.Success,
                    Data = new List<ShopInfoViewModel> { AutoMapper.Mapper.Map<ShopInfoViewModel>(shopInfo) }
                };
            }

            return null;
        }

        /// <summary>
        /// 通过店铺Id获取店铺信息
        /// </summary>
        /// <param name="shopIds">店铺Id:通过下横线连接 例如：1_2_3</param>
        [HttpGet("getByIds/{shopIds}")]
        public ResultViewModel GetByIds(string shopIds)
        {
            if (string.IsNullOrEmpty(shopIds))
            {
                return new ResultViewModel
                {
                    Code = ResultCode.Fail,
                    Message = "参数不能为空！"
                };
            }

            string[] shopIdArrary = shopIds.Split("_");

            if (shopIdArrary.Length > 0)
            {
                List<ShopInfo> shopInfos = shopInfoBusiness.GetByIds(shopIdArrary);

                if (shopInfos != null)
                {
                    return new ResultViewModel
                    {
                        Code = ResultCode.Success,
                        Message = "成功",
                        Data = AutoMapper.Mapper.Map<List<ShopInfoViewModel>>(shopInfos)
                    };
                }
            }

            return new ResultViewModel
            {
                Code = ResultCode.Success,
                Message = "成功",
                Data = null
            };
        }

        /// <summary>
        /// 更新商店信息
        /// </summary>
        /// <param name="shopInfo">商店信息</param>
        [HttpPost("Update")]
        [Authorize("ShopInfoManage")]
        public MessageViewModel Update([FromBody]ShopInfoViewModel shopInfo)
        {
            if (shopInfo == null)
                return new MessageViewModel { Code = MessageCode.ParamsError, Message = "店铺信息为空！" };

            ShopInfo shopInfoEntity = shopInfoBusiness.GetById(this.CurrentAuthShopId());

            if (shopInfoEntity == null)
                return new MessageViewModel { Code = MessageCode.ParamsError, Message = "找不到指定店铺！" };

            if (!string.IsNullOrEmpty(shopInfo.Name))
            {
                if (shopInfo.Name.Length > 50)
                    return new MessageViewModel { Code = MessageCode.ParamsError, Message = "店铺名称最多允许50个字！" };
                else
                    shopInfoEntity.Name = shopInfo.Name;
            }

            if (!string.IsNullOrEmpty(shopInfo.ShopDesc))
            {
                if (shopInfo.ShopDesc.Length > 250)
                    return new MessageViewModel { Code = MessageCode.ParamsError, Message = "店铺描述最多允许250个字！" };
                else
                    shopInfoEntity.ShopDesc = shopInfo.ShopDesc;
            }

            if (!string.IsNullOrEmpty(shopInfo.OpeningHours))
            {
                if (shopInfo.OpeningHours.Length > 50)
                    return new MessageViewModel { Code = MessageCode.ParamsError, Message = "营业时间描述最多允许50个字！" };
                else
                    shopInfoEntity.OpeningHours = shopInfo.OpeningHours;
            }

            if (!string.IsNullOrEmpty(shopInfo.Address))
            {
                if (shopInfo.Address.Length > 100)
                    return new MessageViewModel { Code = MessageCode.ParamsError, Message = "店铺地址最多允许100个字！" };
                else
                    shopInfoEntity.Address = shopInfo.Address;
            }

            if (!string.IsNullOrEmpty(shopInfo.LinkPhone))
            {
                if (shopInfo.LinkPhone.Length > 30)
                    return new MessageViewModel { Code = MessageCode.ParamsError, Message = "联系电话最多允许30个字！" };
                else
                    shopInfoEntity.LinkPhone = shopInfo.LinkPhone;
            }

            if (!string.IsNullOrEmpty(shopInfo.QQ))
            {
                if (shopInfo.QQ.Length > 30)
                    return new MessageViewModel { Code = MessageCode.ParamsError, Message = "QQ最多允许30个字！" };
                else
                    shopInfoEntity.QQ = shopInfo.LinkPhone;
            }

            if (!string.IsNullOrEmpty(shopInfo.WeChat))
            {
                if (shopInfo.WeChat.Length > 30)
                    return new MessageViewModel { Code = MessageCode.ParamsError, Message = "微信最多允许30个字！" };
                else
                    shopInfoEntity.WeChat = shopInfo.LinkPhone;
            }

            shopInfoBusiness.Update(shopInfoEntity);
            return new MessageViewModel { Code = MessageCode.Success, Message = "修改成功！" };
        }
    }
}

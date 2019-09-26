using Galaxy.Taurus.OrderingAPI.Entitys;
using Galaxy.Taurus.OrderingAPI.Entitys.DTO;
using Galaxy.Taurus.OrderingAPI.IBusiness;
using Galaxy.Taurus.OrderingAPI.ICached;
using Galaxy.Taurus.OrderingAPI.Util;
using Galaxy.Taurus.OrderingAPI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Galaxy.Taurus.OrderingAPI.Controllers
{
    /// <summary>
    /// 订单处理
    /// </summary>
    [Route("orderingAPI/[controller]")]
    public class OrderController : Controller
    {
        private IOrderBusiness orderBusiness;
        private IOrderLimitCached orderLimitCached;

        /// <summary>
        /// 订单处理
        /// </summary>
        public OrderController(IOrderBusiness orderBusiness, IOrderLimitCached orderLimitCached)
        {
            this.orderBusiness = orderBusiness;
            this.orderLimitCached = orderLimitCached;
        }

        /// <summary>
        /// 查询会员订单信息
        /// </summary>
        [HttpPost("GetMemberOrderListByFilter")]
        public async Task<ResultViewModel<PageDataDTO<List<OrderDTO>>>> GetMemberOrderListByFilter([FromBody]MemberOrderQueryParams orderQueryParams)
        {
            string shopId = "";
            string menberId = "";
            PageDataDTO<List<Order>> data = await orderBusiness.GetByFilter(shopId, menberId,
                    orderQueryParams.OrderStatus,
                    orderQueryParams.PageSize, orderQueryParams.PageIndex);

            List<OrderDTO> dataRes = AutoMapperUtil.Singleton.Map<List<OrderDTO>>(data.Data);
            PageDataDTO<List<OrderDTO>> res = new PageDataDTO<List<OrderDTO>>
            {
                Total = data.Total,
                Data = dataRes
            };

            return new ResultViewModel<PageDataDTO<List<OrderDTO>>>
            {
                Code = ResultCode.Success,
                Message = ResultMessage.Success,
                Data = res
            };
        }

        /// <summary>
        /// 查询店铺订单信息
        /// </summary>
        [Authorize("OrderManage")]
        [HttpPost("GetShopOrderListByFilter")]
        public async Task<ResultViewModel<PageDataDTO<List<OrderDTO>>>> GetShopOrderListByFilter([FromBody]ShopOrderQueryParams orderQueryParams)
        {
            string shopId = this.CurrentAuthShopId();
            PageDataDTO<List<Order>> data = await orderBusiness.GetByFilter(shopId, null,
                    orderQueryParams.OrderStatus,
                    orderQueryParams.PageSize, orderQueryParams.PageIndex);

            List<OrderDTO> dataRes = AutoMapperUtil.Singleton.Map<List<OrderDTO>>(data.Data);
            PageDataDTO<List<OrderDTO>> res = new PageDataDTO<List<OrderDTO>>
            {
                Total = data.Total,
                Data = dataRes
            };

            return new ResultViewModel<PageDataDTO<List<OrderDTO>>>
            {
                Code = ResultCode.Success,
                Message = ResultMessage.Success,
                Data = res
            };
        }

        /// <summary>
        /// 新增订单
        /// </summary>
        [HttpPost("AddOrder")]
        public ResultViewModel<bool> ResultViewModel(OrderDTO orderDTO)
        {
            string memberId = this.CurrentMemberId();
            bool enableAddOrder = orderLimitCached.EnableAddOrder(memberId);
            orderDTO.OrderId = Guid.NewGuid().ToString("N");
            orderDTO.MemberId = memberId;
            orderDTO.MemberName = this.CurrentMemberName();

            if (enableAddOrder)
            {
                orderLimitCached.AddOrderRecord(memberId, orderDTO.OrderId);

                return new ResultViewModel<bool>
                {
                    Code = ResultCode.Success,
                    Message = "操作成功"
                };
            }
            else
            {
                return new ResultViewModel<bool>
                {
                    Code = ResultCode.Fail,
                    Message = "操作频繁"
                };
            }
        }
    }
}

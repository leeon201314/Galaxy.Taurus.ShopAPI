using Galaxy.Taurus.OrderingAPI.Entitys;
using Galaxy.Taurus.OrderingAPI.Entitys.DTO;
using Galaxy.Taurus.OrderingAPI.IBusiness;
using Galaxy.Taurus.OrderingAPI.IDBAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Galaxy.Taurus.OrderingAPI.Business
{
    public class OrderBusiness : IOrderBusiness
    {
        private IOrderContext orderContext;

        public OrderBusiness(IOrderContext orderContext)
        {
            this.orderContext = orderContext;
        }

        public Task<PageDataDTO<List<Order>>> GetByFilter(string shopId, string menberId, int orderStatus, int pageSize, int pageIndex)
        {
            return orderContext.GetByFilter(shopId, menberId, orderStatus, pageSize, pageIndex);
        }
    }
}

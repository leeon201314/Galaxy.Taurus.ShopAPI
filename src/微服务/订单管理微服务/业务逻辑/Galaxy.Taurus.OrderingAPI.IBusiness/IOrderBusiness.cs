using Galaxy.Taurus.OrderingAPI.Entitys;
using Galaxy.Taurus.OrderingAPI.Entitys.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Galaxy.Taurus.OrderingAPI.IBusiness
{
    public interface IOrderBusiness
    {
        Task<PageDataDTO<List<Order>>> GetByFilter(string shopId, string menberId, int orderStatus, int pageSize, int pageIndex);
    }
}

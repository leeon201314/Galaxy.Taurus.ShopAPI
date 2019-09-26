using Galaxy.Taurus.DBUtil;
using Galaxy.Taurus.OrderingAPI.Entitys;
using Galaxy.Taurus.OrderingAPI.Entitys.DTO;
using Galaxy.Taurus.OrderingAPI.IDBAccess;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxy.Taurus.OrderingAPI.DBAccess
{
    public class OrderContext : BaseContext<Order>, IOrderContext
    {
        public async Task<PageDataDTO<List<Order>>> GetByFilter(string shopId, string menberId, int orderStatus, int pageSize, int pageIndex)
        {
            IQueryable<Order> query = CurrentDbSet.Where(c => c.ShopId == shopId);

            if (!string.IsNullOrEmpty(menberId))
            {
                query = query.Where(c => c.MemberId == menberId);
            }

            if (orderStatus >= 0 && orderStatus <= 5)
            {
                query = query.Where(c => c.OrderStatus == orderStatus);
            }

            int count = await query.CountAsync();

            if (pageSize > 0)
            {
                query = query.Skip(pageSize * (pageIndex - 1)).Take(pageSize);
            }

            return new PageDataDTO<List<Order>>
            {
                Total = count,
                Data = await query.OrderBy(g => g.CreateTime).ToListAsync()
            };
        }
    }
}

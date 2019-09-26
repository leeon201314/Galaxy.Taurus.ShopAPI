using Galaxy.Taurus.OrderingAPI.ICached;
using System;

namespace Galaxy.Taurus.OrderingAPI.Cached
{
    public class OrderLimitCached : IOrderLimitCached
    {
        public bool EnableAddOrder(string memberId)
        {
            string key = $"OrderLimit_{memberId}";
            long min = DateTime.Now.AddMinutes(-1).Ticks;
            var count = RedisHelper.StartPipe().ZRemRangeByScore(key, -1, min).ZCount(key, min, DateTime.Now.Ticks).EndPipe();
            return Convert.ToInt32(count[1]) >= 3 ? false : true;
        }

        public void AddOrderRecord(string memberId, string orderId)
        {
            string key = $"OrderLimit_{memberId}";
            RedisHelper.StartPipe().ZAdd(key, (DateTime.Now.Ticks, orderId)).ExpireAt(key, DateTime.Now.AddMinutes(3)).EndPipe();
        }
    }
}

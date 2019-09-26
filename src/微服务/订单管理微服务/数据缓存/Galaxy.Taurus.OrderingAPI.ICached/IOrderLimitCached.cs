using System.Threading.Tasks;

namespace Galaxy.Taurus.OrderingAPI.ICached
{
    public interface IOrderLimitCached
    {
        bool EnableAddOrder(string memberId);

        void AddOrderRecord(string memberId, string orderId);
    }
}

using Galaxy.Taurus.DBUtil;
using Galaxy.Taurus.ShopInfoAPI.Entitys;
using Galaxy.Taurus.ShopInfoAPI.IDBAccess;

namespace Galaxy.Taurus.ShopInfoAPI.DBAccess
{
    public class ShopInfoContext : BaseContext<ShopInfo>, IShopInfoContext
    {
    }
}

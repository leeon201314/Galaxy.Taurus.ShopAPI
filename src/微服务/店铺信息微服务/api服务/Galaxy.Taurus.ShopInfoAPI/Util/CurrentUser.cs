using Microsoft.AspNetCore.Mvc;

namespace Galaxy.Taurus.ShopInfoAPI.Util
{
    public static class CurrentUser
    {
        public static string CurrentAuthShopId(this Controller controller)
        {
            return controller.User.FindFirst("shopId")?.Value;
        }
    }
}

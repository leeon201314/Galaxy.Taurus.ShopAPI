using Microsoft.AspNetCore.Mvc;

namespace Galaxy.Taurus.OrderingAPI.Util
{
    /// <summary>
    /// 当前用户
    /// </summary>
    public static class CurrentUser
    {
        /// <summary>
        /// 获取用户授权店铺ID
        /// </summary>
        public static string CurrentAuthShopId(this Controller controller)
        {
            return controller.User.FindFirst("shopId")?.Value;
        }
    }
}

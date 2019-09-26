using Microsoft.AspNetCore.Mvc;

namespace Galaxy.Taurus.GoodInfoAPI.Util
{
    /// <summary>
    /// 当前授权用户信息
    /// </summary>
    public static class CurrentUser
    {
        /// <summary>
        /// 当前shopId
        /// </summary>
        public static string CurrentAuthShopId(this ControllerBase controller)
        {
            return controller.User.FindFirst("shopId")?.Value;
        }
    }
}

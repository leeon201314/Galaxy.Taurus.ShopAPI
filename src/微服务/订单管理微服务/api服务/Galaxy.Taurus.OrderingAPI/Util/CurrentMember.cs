using Microsoft.AspNetCore.Mvc;

namespace Galaxy.Taurus.OrderingAPI.Util
{
    /// <summary>
    /// 当前会员
    /// </summary>
    public static class CurrentMember
    {
        /// <summary>
        /// 当前会员ID
        /// </summary>
        public static string CurrentMemberId(this Controller controller)
        {
            return "leeon123";
            //return controller.User.FindFirst("memberId")?.Value;
        }

        /// <summary>
        /// 当前会员名称
        /// </summary>
        public static string CurrentMemberName(this Controller controller)
        {
            return "leeon123";
            //return controller.User.FindFirst("memberId")?.Value;
        }
    }
}

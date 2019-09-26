using Galaxy.Taurus.ShopInfoAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Galaxy.Taurus.ShopInfoAPI.Controllers
{
    /// <summary>
    /// 监控检测
    /// </summary>
    [Route("shopInfoAPI/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// 监控检测
        /// </summary>
        [HttpGet]
        public MessageViewModel Get() => new MessageViewModel { Code = MessageCode.Success, Message = "商铺信息API服务运行正常！" };
    }
}

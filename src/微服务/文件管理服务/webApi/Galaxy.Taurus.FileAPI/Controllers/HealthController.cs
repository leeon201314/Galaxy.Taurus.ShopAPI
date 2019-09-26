using Galaxy.Taurus.FileAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Galaxy.Taurus.FileAPI.Controllers
{
    /// <summary>
    /// 服务监控检测
    /// </summary>
    [Route("fileAPI/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// 健康监测
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public MessageViewModel Get() => new MessageViewModel { Code = MessageCode.Success, Message = "文件信息API服务运行正常！" };
    }
}

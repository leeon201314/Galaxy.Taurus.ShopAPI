using Galaxy.Taurus.GoodInfoAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Galaxy.Taurus.GoodInfoAPI.Controllers
{
    /// <summary>
    /// 健康监测
    /// </summary>
    [Route("goodInfoAPI/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// 健康监测
        /// </summary>
        [HttpGet]
        public ResultViewModel Get() => new ResultViewModel { Code = ResultCode.Success, Message = "商品信息服务运行正常！" };
    }
}

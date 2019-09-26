using Galaxy.Taurus.OrderingAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Galaxy.Taurus.OrderingAPI.Controllers
{
    /// <summary>
    /// 健康监测
    /// </summary>
    [Route("orderingAPI/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// 健康监测
        /// </summary>
        [HttpGet]
        public ResultViewModel<object> Get() => new ResultViewModel<object> { Code = ResultCode.Success, Message = "订单API服务运行正常！" };
    }
}

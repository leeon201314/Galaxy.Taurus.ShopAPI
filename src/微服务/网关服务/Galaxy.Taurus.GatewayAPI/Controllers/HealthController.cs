using Microsoft.AspNetCore.Mvc;

namespace Galaxy.Taurus.GatewayAPI.Controllers
{
    [Route("gatewayAPI/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get() => Ok("网关服务正常!");
    }
}

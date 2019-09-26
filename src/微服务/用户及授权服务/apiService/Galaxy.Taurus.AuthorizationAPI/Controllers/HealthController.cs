using Consul;
using Galaxy.Taurus.AuthorizationAPI.Config;
using Galaxy.Taurus.AuthorizationAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Galaxy.Taurus.AuthorizationAPI.Controllers
{
    /// <summary>
    /// 健康检查
    /// </summary>
    [Route("authorizationAPI/[controller]")]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// 健康检查
        /// </summary>
        [HttpGet]
        public MessageViewModel Get() => new MessageViewModel { Code = MessageCode.Success, Message = "用户授权API服务运行正常！"};

        /// <summary>
        /// 服务注销注册
        /// </summary>
        /// <param name="id">服务ID</param>
        [HttpGet("ServiceDeregister/{id}")]
        public ActionResult<string> ServiceDeregister(string id)
        {
            var consulClient = new ConsulClient(x => x.Address = new Uri(ServiceConfigInfo.ConsulUri));
            consulClient.Agent.ServiceDeregister(id);
            return Ok("ok:" + id);
        }
    }
}

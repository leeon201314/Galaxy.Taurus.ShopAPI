using Galaxy.Taurus.AuthorizationAPI.Entitys;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Galaxy.Taurus.AuthorizationAPI.Controllers
{
    /// <summary>
    /// 操作
    /// </summary>
    [Route("authorizationAPI/[controller]")]
    public class OperationController : ControllerBase
    {
        /// <summary>
        /// 获取所有操作
        /// </summary>
        [HttpGet("All")]
        public List<Operation> All()
        {
            return Operations.All();
        }
    }
}

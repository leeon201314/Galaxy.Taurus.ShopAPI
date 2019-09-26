using Galaxy.Taurus.AuthorizationAPI.Entitys;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Galaxy.Taurus.AuthorizationAPI.Controllers
{
    /// <summary>
    /// 角色API
    /// </summary>
    [Route("authorizationAPI/[controller]")]
    public class RoleController : ControllerBase
    {
        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        [HttpGet("All")]
        public List<Role> All()
        {
            return Roles.All();
        }
    }
}

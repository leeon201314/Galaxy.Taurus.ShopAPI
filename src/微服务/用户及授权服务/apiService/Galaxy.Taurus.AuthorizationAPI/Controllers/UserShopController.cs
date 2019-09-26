using Galaxy.Taurus.AuthorizationAPI.Entitys;
using Galaxy.Taurus.AuthorizationAPI.GalaxyAuth;
using Galaxy.Taurus.AuthorizationAPI.IBusiness;
using Galaxy.Taurus.AuthorizationAPI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Galaxy.Taurus.AuthorizationAPI.Controllers
{
    /// <summary>
    /// 用户授权商店控制器
    /// </summary>
    [Route("authorizationAPI/[controller]")]
    public class UserShopController : Controller
    {
        private IUserInfoBusiness userInfoBusiness;
        private IUserRoleRelationBusiness userRoleRelationBusiness;

        /// <summary>
        /// 用户授权
        /// </summary>
        public UserShopController(IUserInfoBusiness userInfoBusiness, IUserRoleRelationBusiness userRoleRelationBusiness)
        {
            this.userInfoBusiness = userInfoBusiness;
            this.userRoleRelationBusiness = userRoleRelationBusiness;
        }

        /// <summary>
        /// 获取用户已经被授权的商店
        /// </summary>
        [HttpGet("GetUserShopId")]
        [Authorize]
        public MessageViewModel GetUserShopId()
        {
            string sid = HttpContext.User.FindFirst("sid").Value;

            if (userInfoBusiness.CachedSidExists(sid))
            {
                userInfoBusiness.UpdateCachedExpire(sid);
                List<string> shopIdList = userRoleRelationBusiness.GetUserShopIdsBySid(sid);
                return new MessageViewModel { Code = MessageCode.Success, Message = "获取用户商店成功", Data = shopIdList };
            }
            else
            {
                return new MessageViewModel
                {
                    Code = MessageCode.NoLogin
                };
            }         
        }

        /// <summary>
        /// 获取店铺授权token
        /// </summary>
        /// <param name="shopId">店铺ID</param>
        [HttpGet("GetShopAuthorizeToken/{shopId}")]
        [Authorize]
        public MessageViewModel GetShopAuthorizeToken(string shopId)
        {
            string sid = HttpContext.User.FindFirst("sid").Value;

            if (userInfoBusiness.CachedSidExists(sid))
            {
                userInfoBusiness.UpdateCachedExpire(sid);
                string userName = HttpContext.User.FindFirst("username").Value;
                List<Operation> opList = userInfoBusiness.GetUserOperationBySid(sid, shopId);
                string token = GenerateShopAuthorizationJWT(sid, userName, shopId, opList);
                return new MessageViewModel { Code = MessageCode.Success, Data = token };
            }
            else
            {
                return new MessageViewModel
                {
                    Code = MessageCode.NoLogin
                };
            }
        }

        /// <summary>
        /// 生成Shop授权JWT
        /// </summary>
        private string GenerateShopAuthorizationJWT(string sid, string userName, string shopId, List<Operation> opList)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("sid", sid));
            claims.Add(new Claim("shopId", shopId));

            string opData = string.Empty;
            if (opList != null && opList.Count > 0)
            {
                List<string> opStr = new List<string>();
                foreach (var item in opList)
                {
                    opStr.Add(item.Name);
                }
                opData = string.Join("_", opStr);
            }
            claims.Add(new Claim("opList", opData));

            ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(userName, "TokenAuth"), claims);

            RsaSecurityKey rsaSecurityKey = new RsaSecurityKey(AuthRSAUtils.Singleton.PrivateKeys);
            SigningCredentials creds = new SigningCredentials(rsaSecurityKey, SecurityAlgorithms.RsaSha256Signature);
            var authTime = DateTime.UtcNow;
            var expiresAt = authTime.AddMinutes(10);

            var token = new SecurityTokenDescriptor
            {
                Issuer = GalaxyAuthExtensions.Issuer,
                Audience = GalaxyAuthExtensions.Audience,
                SigningCredentials = creds,
                Subject = identity,
                Expires = expiresAt
            };

            return new JwtSecurityTokenHandler().CreateEncodedJwt(token);
        }
    }
}

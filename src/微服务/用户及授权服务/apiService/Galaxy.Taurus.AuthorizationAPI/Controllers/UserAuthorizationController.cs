using Galaxy.Taurus.AuthorizationAPI.Entitys;
using Galaxy.Taurus.AuthorizationAPI.GalaxyAuth;
using Galaxy.Taurus.AuthorizationAPI.IBusiness;
using Galaxy.Taurus.AuthorizationAPI.Util;
using Galaxy.Taurus.AuthorizationAPI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Galaxy.Taurus.AuthorizationAPI.Controllers
{
    /// <summary>
    /// 用户授权控制器
    /// </summary>
    [Route("authorizationAPI/[controller]")]
    public class UserAuthorizationController : Controller
    {
        private IUserInfoBusiness userInfoBusiness;

        /// <summary>
        /// 用户授权
        /// </summary>
        public UserAuthorizationController(IUserInfoBusiness userInfoBusiness)
        {
            this.userInfoBusiness = userInfoBusiness;
        }

        /// <summary>
        /// 通过电话号码登录
        /// </summary>
        /// <param name="parm">电话号码</param>
        [HttpPost("LoginByPhoneNumber")]
        public MessageViewModel LoginByPhoneNumber([FromBody]LoginByPhoneNumberParams parm)
        {
            if (string.IsNullOrEmpty(parm.PhoneNumber) || string.IsNullOrEmpty(parm.Password))
            {
                return new MessageViewModel { Code = MessageCode.Fail, Message = "手机号码和密码不允许为空" };
            }

            int resCode;
            Dictionary<string, string> loginInfo = userInfoBusiness.LoginByPhoneNumber(parm.PhoneNumber, parm.Password, out resCode);
            var encodedJwt = GenerateIdentityJWT(loginInfo);

            switch (resCode)
            {
                case 1:
                    return new MessageViewModel { Code = MessageCode.Success, Message = "登录成功", Data = encodedJwt };
                case -1:
                    return new MessageViewModel { Code = MessageCode.NoUser, Message = "用户不存在" };
                case -2:
                    return new MessageViewModel { Code = MessageCode.Fail, Message = "密码错误" };
            }

            return null;
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        [HttpPost("Logout")]
        [Authorize]
        public MessageViewModel Logout()
        {
            string sid = HttpContext.User.FindFirst("sid").Value;
            userInfoBusiness.RemoveUserCached(sid);
            return new MessageViewModel { Code = MessageCode.Success, Message = "退出登录" };
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        [HttpPost("GetUserInfo")]
        [Authorize]
        public async Task<MessageViewModel> GetUserInfo()
        {
            string sid = HttpContext.User.FindFirst("sid").Value;
            UserInfo userInfo = await userInfoBusiness.GetUserInfoFromCached(sid);
            return new MessageViewModel
            {
                Code = MessageCode.Success,
                Message = "获取成功",
                Data = AutoMapperUtil.Singleton.Map<UserInfoViewModel>(userInfo)
            };
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        [HttpPost("CheckAuthorizeToken/{authorizeToken}")]
        public MessageViewModel CheckAuthorizeToken(string authorizeToken)
        {
            var handler = new JwtSecurityTokenHandler();
            ClaimsPrincipal principal = null;
            SecurityToken validToken = null;

            try
            {
                principal = handler.ValidateToken(authorizeToken, GalaxyAuthExtensions.CreateTokenValidationParameters(), out validToken);
                var validJwt = validToken as JwtSecurityToken;

                if (validJwt == null)
                {
                    return new MessageViewModel
                    {
                        Code = MessageCode.Fail,
                        Message = "无效的授权Token"
                    };
                }
            }
            catch
            {
                return new MessageViewModel
                {
                    Code = MessageCode.Fail,
                    Message = "无效的授权Token"
                };
            }

            return new MessageViewModel
            {
                Code = MessageCode.Success,
                Message = "有效的授权Token"
            };
        }

        /// <summary>
        /// 生成登录JWT
        /// </summary>
        private string GenerateIdentityJWT(Dictionary<string, string> loginInfo)
        {
            if (loginInfo == null)
                return null;

            var claims = new List<Claim>();
            foreach (var item in loginInfo)
            {
                claims.Add(new Claim(item.Key, item.Value));
            }

            RsaSecurityKey rsaSecurityKey = new RsaSecurityKey(AuthRSAUtils.Singleton.PrivateKeys);
            SigningCredentials creds = new SigningCredentials(rsaSecurityKey, SecurityAlgorithms.RsaSha256Signature);
            var authTime = DateTime.UtcNow;
            var expiresAt = authTime.AddDays(7);

            var token = new JwtSecurityToken(
                issuer: GalaxyAuthExtensions.Issuer,
                audience: GalaxyAuthExtensions.Audience,
                expires: expiresAt,
                claims: claims,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

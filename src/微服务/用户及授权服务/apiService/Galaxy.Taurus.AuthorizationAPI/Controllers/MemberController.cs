using Galaxy.Taurus.AuthorizationAPI.Entitys;
using Galaxy.Taurus.AuthorizationAPI.GalaxyAuth;
using Galaxy.Taurus.AuthorizationAPI.IBusiness;
using Galaxy.Taurus.AuthorizationAPI.ICached;
using Galaxy.Taurus.AuthorizationAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Galaxy.Taurus.AuthorizationAPI.Controllers
{
    /// <summary>
    /// 会员
    /// </summary>
    [Route("authorizationAPI/[controller]")]
    public class MemberController : Controller
    {
        private IMemberCached memberCached;
        private IMemberBusiness memberBusiness;

        /// <summary>
        /// 会员
        /// </summary>
        public MemberController(IMemberCached menberCached, IMemberBusiness memberBusiness)
        {
            this.memberCached = menberCached;
            this.memberBusiness = memberBusiness;
        }

        /// <summary>
        /// 通过手机验证码登录
        /// </summary>
        /// <param name="phoneNumber">手机号</param>
        /// <param name="verificationCode">验证码</param>
        [HttpGet]
        [Route("LoginByPhoneVerificationCode/{phoneNumber}/{verificationCode}")]
        public MessageViewModel LoginByPhoneVerificationCode(long phoneNumber, string verificationCode)
        {
            //string code = menberCached.GetLoginVerificationCodePhoneCached(phoneNumber.ToString());

            //if (code != verificationCode)
            //{
            //    return new MessageViewModel
            //    {
            //        Code = MessageCode.Fail,
            //        Message = "验证码错误！"
            //    };
            //}

            if (verificationCode.ToLower() != "zmkm")
            {
                return new MessageViewModel
                {
                    Code = MessageCode.Fail,
                    Message = "验证码错误！"
                };
            }

            Member member = memberBusiness.GetByPhoneNumber(phoneNumber.ToString());

            if (member == null)
            {
                Member tempMember = new Member
                {
                    Id = Guid.NewGuid().ToString("N"),
                    PhoneNumber = phoneNumber.ToString()
                };

                memberBusiness.AddMember(tempMember);
                member = memberBusiness.GetByPhoneNumber(phoneNumber.ToString());
            }

            member = memberBusiness.GetByPhoneNumber(phoneNumber.ToString());

            if (member != null)
            {
                Dictionary<string, string> loginInfo = new Dictionary<string, string>();
                loginInfo.Add("memberid", member.Id);
                loginInfo.Add("opList", "addOrder");
                var encodedJwt = GenerateIdentityJWT(loginInfo);
                return new MessageViewModel { Code = MessageCode.Success, Message = "登录成功", Data = encodedJwt };
            }
            else
            {
                return new MessageViewModel { Code = MessageCode.Fail, Message = "操作异常" };
            }
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

using Galaxy.Taurus.AuthorizationAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Galaxy.Taurus.AuthorizationAPI.Controllers
{
    /// <summary>
    /// 微信登录授权
    /// </summary>
    [Route("authorizationAPI/[controller]")]
    public class WXAuthController : Controller
    {
        private const string WXAppID = "wx88b314d3a4b59589";
        private const string WXAppSecret = "c894a3140876f9b268b12d1b13c5eb79";

        /// <summary>
        /// 微信登录
        /// </summary>
        /// <param name="loginCode">微信登录时返回的Code</param>
        [HttpGet("Login/{loginCode}")]
        public async Task<MessageViewModel> Login(string loginCode)
        {
            string url = $"https://api.weixin.qq.com/sns/jscode2session?appid={WXAppID}&secret={WXAppSecret}&js_code={loginCode}&grant_type=authorization_code";
            var urI = new Uri(url);
            string response = await new HttpClient().GetStringAsync(url);
            WXLoginRes res = JsonConvert.DeserializeObject<WXLoginRes>(response);

            if (res.errcode != 0)
            {
                return new MessageViewModel
                {
                    Code = MessageCode.Fail,
                    Data = res
                };
            }

            return new MessageViewModel
            {
                Code = MessageCode.Success,
                Data = res
            };
        }
    }
}

using Galaxy.Taurus.AuthorizationAPI.ICached;
using Galaxy.Taurus.AuthorizationAPI.VerificationCode;
using Galaxy.Taurus.AuthorizationAPI.ViewModel;
using Galaxy.Taurus.SMSUtil;
using Galaxy.Taurus.SMSUtil.TencentCloudSMS;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Taurus.AuthorizationAPI.Controllers
{
    /// <summary>
    /// 验证码
    /// </summary>
    [Route("authorizationAPI/[controller]")]
    public class VerificationCodeController : Controller
    {
        private IVerificationCodeCached verificationCodeCached;

        /// <summary>
        /// 验证码
        /// </summary>
        public VerificationCodeController(IVerificationCodeCached verificationCodeCached)
        {
            this.verificationCodeCached = verificationCodeCached;
        }

        /// <summary>
        /// 图片验证码
        /// </summary>
        [HttpGet]
        [Route("GetImageVerificationCode")]
        public async Task<MessageViewModel> GetImageVerificationCode()
        {
            var model = await VerificationCodeImage.CreateCode();
            string codeId = Guid.NewGuid().ToString("N");
            verificationCodeCached.SetImageVerificationCode(codeId, model.Code);

            return new MessageViewModel
            {
                Code = MessageCode.Success,
                Data = new { img = model.ImageBase64Str, codeId = codeId }
            };
        }

        /// <summary>
        /// 手机验证码
        /// </summary>
        /// <param name="imgCodeId">图片验证码的key</param>
        /// <param name="imgCode">图片验证码</param>
        /// <param name="phoneNumber">手机号</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPhoneVerificationCode/{imgCodeId}/{imgCode}/{phoneNumber}")]
        public async Task<MessageViewModel> GetPhoneVerificationCode(string imgCodeId, string imgCode, long phoneNumber)
        {
            string imgCodeCached = verificationCodeCached.GetImageVerificationCode(imgCodeId);
            verificationCodeCached.RemoveImageVerificationCode(imgCodeId);

            if (string.IsNullOrEmpty(imgCodeCached) || imgCodeCached.ToLower() != imgCode.ToLower())
            {
                return new MessageViewModel
                {
                    Code = 408,
                    Message = "验证码错误！"
                };
            }

            int seed = Guid.NewGuid().GetHashCode();
            Random random = new Random(seed);
            int code = random.Next(1000, 9999);

            SMSResult res = await new TencentCloudSmsUtil().SendLoginCode("86", phoneNumber, code);

            if (res.Result != 0)
            {
                return new MessageViewModel
                {
                    Code = 409,
                    Message = $"短信接口异常，请稍后重试！"
                };
            }

            verificationCodeCached.SetPhoneVerificationCode(phoneNumber.ToString(), code.ToString());

            return new MessageViewModel
            {
                Code = MessageCode.Success,
                Message = $"已发送验证码到手机{phoneNumber}"
            };
        }
    }
}

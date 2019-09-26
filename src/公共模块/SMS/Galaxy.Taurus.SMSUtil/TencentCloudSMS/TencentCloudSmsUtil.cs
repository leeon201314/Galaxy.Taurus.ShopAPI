using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Taurus.SMSUtil.TencentCloudSMS
{
    public class TencentCloudSmsUtil
    {
        public static string AppKey { get; private set; }

        public static string SdkAppId { get; private set; }

        public static int LoginCodeTplId { get; private set; }

        public static void Config(string appKey, string sdkAppId, int loginCodeTplId)
        {
            AppKey = appKey;
            SdkAppId = sdkAppId;
            LoginCodeTplId = loginCodeTplId;
        }

        public async Task<SMSResult> SendLoginCode(string countryCode, long mobile, int code)
        {
            if(string.IsNullOrEmpty(AppKey) || string.IsNullOrEmpty(SdkAppId) || LoginCodeTplId == 0)
            {
                throw new Exception("错误：腾讯短信模块未配置参数");
            }

            int randomNum = GetRandom();
            long time = DateTimeOffset.Now.ToUnixTimeSeconds();
            string sigStr = ComputeSignature(mobile, randomNum, time);

            var data = new
            {
                tel = new { nationcode = countryCode.Replace("+", ""), mobile = mobile.ToString() },
                //sign = sigStr,
                @params = new[] { code.ToString(), "5" },
                sig = ComputeSignature(mobile, randomNum, time),
                tpl_id = LoginCodeTplId,
                time = time,
                extend = "",
                ext = ""
            };

            string url = $"https://yun.tim.qq.com/v5/tlssmssvr/sendsms?sdkappid={SdkAppId}&random={randomNum}";
            var response = await new HttpClient().PostAsJsonAsync<dynamic>(url, data);
            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<SMSResult>(result);
        }

        /// <summary>
        /// 签名
        /// </summary>
        private string ComputeSignature(long mobile, int random, long timestamp)
        {
            var input = $"appkey={AppKey}&random={random}&time={timestamp}&mobile={mobile}";
            var hasBytes = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(input));
            return string.Join("", hasBytes.Select(b => b.ToString("x2")));
        }

        /// <summary>
        /// 获取随机数
        /// </summary>
        /// <returns>随机整数</returns>
        private int GetRandom()
        {
            int seed = Guid.NewGuid().GetHashCode();
            Random random = new Random(seed);
            return random.Next(0, 100000);
        }
    }
}

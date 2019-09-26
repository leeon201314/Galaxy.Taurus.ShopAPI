using Galaxy.Taurus.AuthorizationAPI.ICached;
using System;

namespace Galaxy.Taurus.AuthorizationAPI.Cached
{
    public class VerificationCodeCached : IVerificationCodeCached
    {
        private const string preImgVerificationCodeKey = "imgVCodeId";
        private const string prePhoneVerificationCodeKey = "phoneVCodeId";

        /// <summary>
        /// 缓存图片验证码
        /// </summary>
        public void SetImageVerificationCode(string key, string value)
        {
            string newKey = $"{preImgVerificationCodeKey}_{key}";
            RedisStoreHelper.SetString(newKey, value, TimeSpan.FromMinutes(3));
        }

        /// <summary>
        /// 获取图片验证码
        /// </summary>
        public string GetImageVerificationCode(string key)
        {
            string newKey = $"{preImgVerificationCodeKey}_{key}";
            return RedisStoreHelper.GetString(newKey);
        }

        /// <summary>
        /// 移除图片验证码
        /// </summary>
        public void RemoveImageVerificationCode(string key)
        {
            string newKey = $"{preImgVerificationCodeKey}_{key}";
            RedisStoreHelper.KeyDelete(newKey);
        }

        /// <summary>
        /// 存储手机验证码
        /// </summary>
        public void SetPhoneVerificationCode(string key, string value)
        {
            string newKey = $"{prePhoneVerificationCodeKey}_{key}";
            RedisStoreHelper.SetString(newKey, value, TimeSpan.FromMinutes(1));
        }

        /// <summary>
        /// 获取手机验证码
        /// </summary>
        public string GetPhoneVerificationCode(string key)
        {
            string newKey = $"{prePhoneVerificationCodeKey}_{key}";
            return RedisStoreHelper.GetString(newKey);
        }
    }
}

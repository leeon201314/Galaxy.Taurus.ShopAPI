using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Galaxy.Taurus.CachedUtil
{
    public class RedisStoreHelper
    {
        #region key操作（判断存在,删除,设置超时等）

        /// <summary>
        /// 是否存在某个key
        /// </summary>
        public static bool KeyExists(string key)
        {
            return RedisHelper.Exists(key);
        }

        /// <summary>
        /// 删除key
        /// </summary>
        public static bool KeyDelete(string key)
        {
            return RedisHelper.Del(key) > 0 ? true : false;
        }

        /// <summary>
        /// 设置key的过期时间
        /// </summary>
        public static bool KeyExpire(string key, TimeSpan timeSpan)
        {
            return RedisHelper.Expire(key, timeSpan);
        }

        /// <summary>
        /// 设置key的过期时间
        /// </summary>
        public static Task<bool> KeyExpireAsync(string key, TimeSpan timeSpan)
        {
            return RedisHelper.ExpireAsync(key, timeSpan);
        }

        #endregion

        #region string 存储

        public static string GetString(string key)
        {
            return RedisHelper.Get(key);
        }

        public static Task<string> GetStringAsync(string key)
        {
            return RedisHelper.GetAsync(key);
        }

        public static bool SetString(string key, string value, TimeSpan? timeSpan)
        {
            return RedisHelper.Set(key, value, ConverTimeSpan(timeSpan));
        }

        public static Task<bool> SetStringAsync(string key, string value, TimeSpan? timeSpan)
        {
            return RedisHelper.SetAsync(key, value, ConverTimeSpan(timeSpan));
        }

        public static T Get<T>(string key) where T : class
        {
            var strValue = GetString(key);
            return string.IsNullOrEmpty(strValue) ? null : JsonConvert.DeserializeObject<T>(strValue);
        }

        public static bool Set<T>(string key, T value, TimeSpan? timeSpan) where T : class
        {
            string strValue = JsonConvert.SerializeObject(value);
            return SetString(key, strValue, timeSpan);
        }

        #endregion

        #region hash 存储

        public static string HashGetString(string key, string filed)
        {
            return RedisHelper.HGet(key, filed);
        }

        public static bool HashSetString(string key, string filed, string value)
        {
            return RedisHelper.HSet(key, filed, value);
        }

        public static T HashGetValue<T>(string key, string filed) where T : class
        {
            return RedisHelper.HGet<T>(key, filed);
        }

        public static Task<T> HashGetValueAsync<T>(string key, string filed) where T : class
        {
            return RedisHelper.HGetAsync<T>(key, filed);
        }

        public static bool HashSetValue<T>(string key, string filed, T value) where T : class
        {
            string strValue = JsonConvert.SerializeObject(value);
            return RedisHelper.HSet(key, filed, strValue);
        }

        public static Task<bool> HashSetValueAsync<T>(string key, string filed, T value) where T : class
        {
            string strValue = JsonConvert.SerializeObject(value);
            return RedisHelper.HSetAsync(key, filed, strValue);
        }

        public static Dictionary<string, T> HashGetAll<T>(string key) where T : class
        {
            return RedisHelper.HGetAll<T>(key);
        }

        #endregion

        #region Set

        /// <summary>
        /// 向集合添加数据
        /// </summary>
        public static long SetAdd(string key, params string[] values)
        {
            return RedisHelper.SAdd(key, values);
        }

        /// <summary>
        /// 返回交集数据
        /// </summary>
        public static string[] SetInter(params string[] keys)
        {
            return RedisHelper.SInter(keys);
        }

        /// <summary>
        /// 返回并集数据
        /// </summary>
        public static string[] SetUnion(params string[] keys)
        {
            return RedisHelper.SUnion(keys);
        }

        #endregion

        private static int ConverTimeSpan(TimeSpan? timeSpan)
        {
            return timeSpan == null ? -1 : Convert.ToInt32(((TimeSpan)timeSpan).TotalSeconds);
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Taurus.FileAPI.Util
{
    public class HttpClientHelper
    {
        /// <summary>
        /// 异步GET请求
        /// </summary>
        public static async Task<string> GetStringAsync(string url, Dictionary<string, string> headers = null, int timeout = 0)
        {
            using (HttpClient client = new HttpClient())
            {
                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> header in headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }
                if (timeout > 0)
                {
                    client.Timeout = new TimeSpan(0, 0, timeout);
                }
                Byte[] resultBytes = await client.GetByteArrayAsync(url);
                return Encoding.Default.GetString(resultBytes);
            }
        }

        /// <summary>
        /// 异步GET请求
        /// </summary>
        public static async Task<T> GetAsync<T>(string url, Dictionary<string, string> headers = null, int timeout = 0) where T : class
        {
            string res = await GetStringAsync(url, headers, timeout);
            return string.IsNullOrEmpty(res) ? null : JsonConvert.DeserializeObject<T>(res);
        }

        /// <summary>
        /// 异步POST请求
        /// </summary>
        public static async Task<Byte[]> PostJsonAsync(string url, dynamic postData, Dictionary<string, string> headers = null)
        {
            using (HttpClient client = new HttpClient())
            {
                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> header in headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }

                var str = JsonConvert.SerializeObject(postData);
                HttpContent content = new StringContent(str);

                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                using (HttpResponseMessage responseMessage = await client.PostAsync(url, content))
                {
                    return await responseMessage.Content.ReadAsByteArrayAsync();
                }
            }
        }
    }
}

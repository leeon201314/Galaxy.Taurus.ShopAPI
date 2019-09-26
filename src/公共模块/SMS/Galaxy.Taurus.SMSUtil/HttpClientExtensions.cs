using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Taurus.SMSUtil
{
    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient httpClient, string requestUri, T value)
        {
            var stringContent = new StringContent(
                    JsonConvert.SerializeObject(value),
                    Encoding.UTF8,
                    "application/json");
            return await httpClient.PostAsync(requestUri, stringContent);
        }
    }
}

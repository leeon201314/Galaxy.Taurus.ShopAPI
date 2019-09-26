using Consul;
using Newtonsoft.Json;
using System;
using System.Text;

namespace Galaxy.Taurus.ShopInfoAPI.Configs
{
    public class ServiceConfigInfo
    {
        public static string ConsulUri { get; private set; }

        public static string Ip { get; private set; }

        public static int HttpPort { get; private set; }

        public static MainConfig Single { get; private set; }

        public static void Init(string consulUri, string configKey, string ip, int httpPort)
        {
            HttpPort = httpPort;
            Ip = ip;
            ConsulUri = consulUri;
            var consulClient = new ConsulClient(x => x.Address = new Uri(ServiceConfigInfo.ConsulUri));//请求注册的 Consul 地址

            var a = consulClient.Catalog.Service("authorizationAPI").GetAwaiter().GetResult();
            var kvPair = consulClient.KV.Get(configKey).GetAwaiter().GetResult();
            string valueStr = Encoding.UTF8.GetString(kvPair.Response.Value);
            Single = JsonConvert.DeserializeObject<MainConfig>(valueStr);
        }
    }
}

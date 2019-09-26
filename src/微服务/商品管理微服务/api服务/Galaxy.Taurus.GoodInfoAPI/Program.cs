using Galaxy.Taurus.GoodInfoAPI.Configs;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System;
using System.Net;

namespace Galaxy.Taurus.GoodInfoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel(SetHost)
                .UseStartup<Startup>();

        /// <summary>
        /// 配置Kestrel
        /// </summary>
        private static void SetHost(KestrelServerOptions options)
        {
            var address = IPAddress.Parse("0.0.0.0");
            options.Listen(address, ServiceConfigInfo.HttpPort);
            options.UseSystemd();
            options.Limits.KeepAliveTimeout = TimeSpan.FromSeconds(30);
            options.Limits.RequestHeadersTimeout = TimeSpan.FromSeconds(30);
        }
    }
}

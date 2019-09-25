using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace Galaxy.Taurus.GatewayAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, builder) => {
                    builder
                        .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                        .AddJsonFile("ocelotConfig.json");
                })
                .UseKestrel(SetHost)
                .UseStartup<Startup>();

        /// <summary>
        /// 配置Kestrel
        /// </summary>
        private static void SetHost(KestrelServerOptions options)
        {
            var address = IPAddress.Parse("0.0.0.0");
            options.Listen(address, 8880);
            options.UseSystemd();
        }
    }
}

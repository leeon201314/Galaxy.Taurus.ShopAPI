using Galaxy.Taurus.FileAPI.ServiceConfig;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;

namespace Galaxy.Taurus.FileAPI.UTest
{
    public class BaseBusinessTest
    {
        public BaseBusinessTest()
        {
            ///读取配置文件
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
                .Build();
            IConfigurationSection configSection = config.GetSection("ServiceConfig");
            ServiceConfigInfo.Init(configSection["consul"], configSection["configKey"], configSection["ip"], Convert.ToInt32(configSection["httpPort"]));
        }
    }
}

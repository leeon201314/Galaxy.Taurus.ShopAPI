using Consul;
using Galaxy.Taurus.AuthorizationAPI.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Galaxy.Taurus.AuthorizationAPI.ConsulExtensions
{
    public static class ServiceRegisterExtensions
    {
        public static IApplicationBuilder RegisterConsul(this IApplicationBuilder app, IApplicationLifetime lifetime, Uri consulUri)
        {
            string ip = ServiceConfigInfo.Ip;
            int port = ServiceConfigInfo.HttpPort;
            string serviceName = "authorizationAPI";

            var consulClient = new ConsulClient(x => x.Address = consulUri);//请求注册的 Consul 地址
            var httpCheck = new AgentServiceCheck()
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//服务启动多久后注册
                Interval = TimeSpan.FromSeconds(10),//健康检查时间间隔，或者称为心跳间隔
                HTTP = $"http://{ip}:{port}/authorizationAPI/health",//健康检查地址
                Timeout = TimeSpan.FromSeconds(5)
            };

            // 注册服务
            var registration = new AgentServiceRegistration()
            {
                Checks = new[] { httpCheck },
                ID = $"{ip}:{port}_{serviceName}",
                Name = serviceName,
                Address = ip,
                Port = port,
                Tags = new[] { $"urlprefix-/{serviceName}" }//添加 urlprefix-/servicename 格式的 tag 标签，以便 Fabio 识别
            };

            consulClient.Agent.ServiceRegister(registration).Wait();//服务启动时注册，内部实现其实就是使用 Consul API 进行注册（HttpClient发起）
            lifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();//服务停止时取消注册
            });

            return app;
        }
    }
}

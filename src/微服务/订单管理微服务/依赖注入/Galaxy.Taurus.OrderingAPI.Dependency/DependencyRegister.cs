using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Galaxy.Taurus.OrderingAPI.Dependency
{
    public class DependencyRegister
    {
        public Autofac.IContainer Register(IServiceCollection services = null)
        {
            ContainerBuilder builder = new ContainerBuilder();

            //注册数据访问层
            builder.RegisterAssemblyTypes(Assembly.Load("Galaxy.Taurus.OrderingAPI.DBAccess"))
                .AsImplementedInterfaces();

            //注册数据缓存层
            builder.RegisterAssemblyTypes(Assembly.Load("Galaxy.Taurus.OrderingAPI.Cached"))
                .AsImplementedInterfaces();

            //注册业务服务层
            builder.RegisterAssemblyTypes(Assembly.Load("Galaxy.Taurus.OrderingAPI.Business"))
                .AsImplementedInterfaces();

            if (services != null)
            {
                builder.Populate(services);
            }

            return builder.Build();
        }

        public Autofac.IContainer RegisterWeb(IServiceCollection services)
        {
            return Register(services);
        }
    }
}

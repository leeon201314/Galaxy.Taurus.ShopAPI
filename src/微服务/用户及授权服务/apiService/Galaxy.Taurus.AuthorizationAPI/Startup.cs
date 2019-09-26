using Autofac.Extensions.DependencyInjection;
using Galaxy.Taurus.AuthorizationAPI.Cached;
using Galaxy.Taurus.AuthorizationAPI.Config;
using Galaxy.Taurus.AuthorizationAPI.ConsulExtensions;
using Galaxy.Taurus.AuthorizationAPI.Dependency;
using Galaxy.Taurus.AuthorizationAPI.GalaxyAuth;
using Galaxy.Taurus.CachedUtil;
using Galaxy.Taurus.DBUtil;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace Galaxy.Taurus.AuthorizationAPI
{
    /// <summary>
    /// 启动配置
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 构造
        /// </summary>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 配置
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 容器
        /// </summary>
        public Autofac.IContainer ApplicationContainer { get; private set; }

        /// <summary>
        /// 服务配置
        /// </summary>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            IConfigurationSection configSection = Configuration.GetSection("ServiceConfig");
            ServiceConfigInfo.Init(configSection["consul"], configSection["configKey"], configSection["ip"], Convert.ToInt32(configSection["httpPort"]));

            services.AddGalaxyAuth();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCors(options =>
            {
                options.AddPolicy("any", builder =>
                {
                    builder
                        .WithOrigins("*")
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("any"));
            });

            DBConnectionProvider.Config(ServiceConfigInfo.Single.DBConfig.Server,
                ServiceConfigInfo.Single.DBConfig.Port,
                ServiceConfigInfo.Single.DBConfig.DBName,
                ServiceConfigInfo.Single.DBConfig.User,
                ServiceConfigInfo.Single.DBConfig.Password);
            CSRedisInitHelper.Init(ServiceConfigInfo.Single.RedisConfig.Server, System.Convert.ToInt32(ServiceConfigInfo.Single.RedisConfig.Port));

            //注册Swagger生成器，定义一个和多个Swagger 文档
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "AuthorizationAPI", Version = "v1" });
                c.IncludeXmlComments(AppDomain.CurrentDomain.BaseDirectory + "/Galaxy.Taurus.AuthorizationAPI.xml");
            });

            DependencyRegister dependencyRegister = new DependencyRegister();
            ApplicationContainer = dependencyRegister.RegisterWeb(services);
            return new AutofacServiceProvider(ApplicationContainer);//第三方IOC接管 core内置DI容器   
        }

        /// <summary>
        /// 应用配置
        /// </summary>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // app.UseHsts();
            }

            if (!env.IsDevelopment())
            {
                app.RegisterConsul(lifetime, new Uri(ServiceConfigInfo.ConsulUri));
            }

            app.UseAuthentication();

            // app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthorizationAPI V1");
            });
        }
    }
}

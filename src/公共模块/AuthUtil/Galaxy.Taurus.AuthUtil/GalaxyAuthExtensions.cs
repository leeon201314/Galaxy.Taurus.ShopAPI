using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;

namespace Galaxy.Taurus.AuthUtil
{
    /// <summary>
    /// 授权认证
    /// </summary>
    public static class GalaxyAuthExtensions
    {
        /// <summary>
        /// taoken签发者
        /// </summary>
        public const string Issuer = "Galaxy.Taurus.AuthorizationAPI";

        /// <summary>
        /// taoken接收者
        /// </summary>
        public const string Audience = "Galaxy.X";

        /// <summary>
        /// 授权Police名称
        /// </summary>
        public const string PoliceName = "ShopAuthorization";

        private static void SetAuthOption(JwtBearerOptions ops)
        {
            ops.Audience = GalaxyAuthExtensions.Audience;
            ops.RequireHttpsMetadata = false;

            ops.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = GalaxyAuthExtensions.Issuer,
                ValidAudience = GalaxyAuthExtensions.Audience,

                // 是否要求Token的Claims中必须包含Expires
                RequireExpirationTime = true,
                // 允许的服务器时间偏移量
                ClockSkew = TimeSpan.FromSeconds(300),
                // 是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                ValidateLifetime = true,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new RsaSecurityKey(RSAPublicKeyUtils.Singleton.PublicKeys)
            };
        }

        /// <summary>
        /// 授权认证
        /// </summary>
        public static void AddGalaxyAuth(this IServiceCollection services, params string[] oprations)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(ops =>
            {
                SetAuthOption(ops);
            });

            services.AddAuthorization(auth =>
            {
                //auth.AddPolicy("ShopAuthorization", new AuthorizationPolicyBuilder()
                //    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                //    .RequireAuthenticatedUser()
                //    .AddRequirements(new GalaxyAuthorizationRequirement())
                //    .Build());

                foreach (string opration in oprations)
                {
                    auth.AddPolicy(opration, policy => policy.AddRequirements(new GalaxyAuthorizationRequirement(opration)));
                }

            });

            // 注册验证要求的处理器，可通过这种方式对同一种要求添加多种验证
            services.AddSingleton<IAuthorizationHandler, GalaxyAuthorizationHandler>();
        }
    }
}

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;

namespace Galaxy.Taurus.AuthorizationAPI.GalaxyAuth
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

        private static void SetAuthOption(JwtBearerOptions ops)
        {
            ops.Audience = GalaxyAuthExtensions.Audience;
            ops.RequireHttpsMetadata = false;
            ops.TokenValidationParameters = CreateTokenValidationParameters();
        }

        /// <summary>
        /// 创建验证参数
        /// </summary>
        public static TokenValidationParameters CreateTokenValidationParameters()
        {
            return new TokenValidationParameters
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
                IssuerSigningKey = new RsaSecurityKey(AuthRSAUtils.Singleton.PublicKeys)
            };
        }

        /// <summary>
        /// 授权认证
        /// </summary>
        public static void AddGalaxyAuth(this IServiceCollection services)
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
        }
    }
}

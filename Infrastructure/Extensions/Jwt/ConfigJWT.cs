using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using Domain.Base.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Application.Jwt;

namespace Infrastructure.Extensions.JWT
{
    public static class ConfigJWT
    {
        public static IServiceCollection AddJWT(this IServiceCollection services, IConfiguration config)
        {

            services.Configure<JwtEntity>(config.GetSection("JwtSettings"));
            services.AddSingleton<IJwtEntity>(sp => sp.GetRequiredService<IOptions<JwtEntity>>().Value);
            services.AddSingleton<JwtService>();

            var jwtConfig = config.GetSection("JwtSettings").Get<JwtEntity>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig!.issuer,
                    ValidAudience = jwtConfig.audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.key))
                };
            });


            services.AddAuthorization(options =>
                {
                    options.AddPolicy("YourPolicy", policy => policy.RequireClaim("sub"));
                });

            return services;
        }
    }
}

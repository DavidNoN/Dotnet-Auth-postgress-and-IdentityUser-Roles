using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserAPI.Interfaces;
using UserAPI.Services;

namespace UserApi.Extensions
{
    public static class AuthenticationServiceExtensions
    {
        
        public static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<ISendVerificationMail, SendVerificationMail>();
            
            return services;
        }
    }
}
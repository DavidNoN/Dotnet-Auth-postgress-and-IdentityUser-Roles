
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserApi.Mappers.AutoMapperService;

namespace UserApi.Extensions
{
    public static class AutoMappersServiceExtensions
    {

        public static IServiceCollection AddAutoMappersService(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<UserEntityToLoginViewDao>();

            services.AddScoped<RegisterDtoToUserEntity>();
            
            services.AddScoped<UserEntityToRegisterViewDao>();

            return services;
        }
        
    }
}
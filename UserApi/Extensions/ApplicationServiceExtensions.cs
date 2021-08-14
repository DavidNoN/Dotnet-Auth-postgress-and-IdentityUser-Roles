using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserAPI.Interfaces;
using UserApi.Mappers.AutoMapperService;
using UserApi.Repositories;
using UserApi.SignalIR;


namespace UserAPI.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISaveUserRepository, SaveUserRepository>();

            services.AddScoped<ILoginUserRepository, LoginUserRepository>();

            services.AddScoped<IFindUserRolesAsync, FindUserRolesAsync>();

            services.AddSingleton<ContestTracker>();

            return services;
        }
    }
}
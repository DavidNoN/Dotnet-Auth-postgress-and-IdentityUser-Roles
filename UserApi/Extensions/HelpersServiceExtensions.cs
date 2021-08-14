using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserApi.Entities;
using UserAPI.Interfaces;
using UserApi.Mappers;
using UserApi.Mappers.AutoMapperService;
using UserAPI.Services.Helpers;

namespace UserApi.Extensions
{
    public static class HelpersServiceExtensions
    {
        public static IServiceCollection AddHelperServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPhoneAlreadyExist, PhoneAlreadyExist>();

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            services.AddRazorPages();

            return services;
        }
        
    }
}
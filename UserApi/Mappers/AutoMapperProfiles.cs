using AutoMapper;
using UserApi.Dtos;
using UserApi.Entities;
using UserAPI.Services.Helpers;
using UserApi.Views;

namespace UserApi.Mappers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterUserDto, UserEntity>();
            CreateMap<UserEntity, RegisterViewDao>()
                .ForMember(dest => dest.Age,
                    opt => opt.MapFrom(src => src.DateOfBirth.CalculateUserAge()));
            CreateMap<UserEntity, LoginViewDao>()
                .ForMember(dest => dest.Age,
                opt => opt.MapFrom(src => src.DateOfBirth.CalculateUserAge()));
        }
    }
}
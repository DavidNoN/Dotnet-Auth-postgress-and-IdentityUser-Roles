using AutoMapper;
using UserApi.Dtos;
using UserApi.Entities;

namespace UserApi.Mappers.AutoMapperService
{
    public class RegisterDtoToUserEntity
    {
        private readonly IMapper _mapper;

        public RegisterDtoToUserEntity(IMapper mapper)
        {
            _mapper = mapper;
        }

        public UserEntity RegisterDtoToUserEntityMapping(RegisterUserDto registerUserDto)
        {
            return _mapper.Map<UserEntity>(registerUserDto);
        }

    }
}
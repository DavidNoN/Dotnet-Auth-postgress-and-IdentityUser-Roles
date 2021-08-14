using AutoMapper;
using UserApi.Entities;
using UserApi.Views;

namespace UserApi.Mappers.AutoMapperService
{
    public class UserEntityToLoginViewDao
    {
        private readonly IMapper _mapper;

        public UserEntityToLoginViewDao(IMapper mapper)
        {
            _mapper = mapper;
        }

        public LoginViewDao UserEntityToLoginViewDaoMapping(UserEntity userEntity)
        {
            return _mapper.Map<LoginViewDao>(userEntity);
        }
    }
}
using AutoMapper;
using UserApi.Entities;
using UserApi.Views;

namespace UserApi.Mappers.AutoMapperService
{
    public class UserEntityToRegisterViewDao
    {
        private readonly IMapper _mapper;

        public UserEntityToRegisterViewDao(IMapper mapper)
        {
            _mapper = mapper;
        }
        
        public RegisterViewDao UserEntityToRegisterViewDaoMapping(UserEntity userEntity)
        {
            return _mapper.Map<RegisterViewDao>(userEntity);
        }
    }
}
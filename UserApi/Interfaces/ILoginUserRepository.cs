using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UserApi.Dtos;
using UserApi.Entities;

namespace UserAPI.Interfaces
{
    public interface ILoginUserRepository
    {
        Task<SignInResult> LoginUserAsync(UserEntity userEntity, string password);

        Task<UserEntity> ExistUserInDb(string usernameMailPassword);
        
    }
}
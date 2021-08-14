using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UserApi.Entities;

namespace UserAPI.Interfaces
{
    public interface ISaveUserRepository
    {
        Task<IdentityResult> SaveUserToDbAsync(UserEntity userEntity, string password);

        Task<IdentityResult> AddRoleToUserAsync(UserEntity userEntity, string role);
    }
}
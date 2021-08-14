using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UserApi.Entities;
using UserAPI.Interfaces;

namespace UserApi.Repositories
{
    public class SaveUserRepository : ISaveUserRepository
    {
        private readonly UserManager<UserEntity> _userManager;

        public SaveUserRepository(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }
        
        public async Task<IdentityResult> SaveUserToDbAsync(UserEntity userEntity, string password)
        {
            return await _userManager.CreateAsync(userEntity, password);
        }

        public async Task<IdentityResult> AddRoleToUserAsync(UserEntity userEntity, string role)
        {
            return await _userManager.AddToRoleAsync(userEntity, role);
        }

    }
}
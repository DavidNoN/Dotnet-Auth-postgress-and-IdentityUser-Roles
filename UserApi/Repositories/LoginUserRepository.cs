using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserApi.Dtos;
using UserApi.Entities;
using UserAPI.Interfaces;

namespace UserApi.Repositories
{
    public class LoginUserRepository : ILoginUserRepository
    {
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;

        public LoginUserRepository(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<SignInResult> LoginUserAsync(UserEntity userEntity, string password)
        {
            return await _signInManager
                .CheckPasswordSignInAsync(userEntity, password, false);
        }

        public async Task<UserEntity> ExistUserInDb(string usernameMailPassword)
        {
            return await (from us in _userManager.Users
                where us.Email == usernameMailPassword || 
                      us.UserName == usernameMailPassword ||
                      us.PhoneNumber == usernameMailPassword
                select us).AsQueryable().SingleOrDefaultAsync();
        }
    }
}
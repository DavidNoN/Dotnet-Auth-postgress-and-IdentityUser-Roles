using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserApi.Entities;
using UserAPI.Interfaces;

namespace UserApi.Repositories
{
    public class FindUserRolesAsync : IFindUserRolesAsync
    {
        private readonly UserManager<UserEntity> _userManager;

        public FindUserRolesAsync(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }
        public async Task<UserEntity> FindUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<UserEntity> FindUserByUserName(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<UserEntity> FindUserByEmailPhoneUsername(string userNameEmailPhone)
        {
            return await _userManager.Users
                .Where(u => u.UserName == userNameEmailPhone || u.Email == userNameEmailPhone ||
                            u.PhoneNumber == userNameEmailPhone)
                .Include(r => r.UserRoles)
                .ThenInclude(r => r.Role).AsQueryable().SingleAsync();
        }
    }
}
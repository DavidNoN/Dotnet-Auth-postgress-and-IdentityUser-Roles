using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserApi.Entities;
using UserAPI.Interfaces;

namespace UserAPI.Services.Helpers
{
    public class PhoneAlreadyExist : IPhoneAlreadyExist
    {
        private readonly UserManager<UserEntity> _userManager;

        public PhoneAlreadyExist(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }
        
        public async Task<bool> PhoneAlreadyExists(string phoneNumber)
        {
            return await _userManager.Users.AnyAsync(x => x.PhoneNumber == phoneNumber);
        }
    }
}
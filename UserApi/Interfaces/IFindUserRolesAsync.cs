using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UserApi.Entities;

namespace UserAPI.Interfaces
{
    public interface IFindUserRolesAsync
    {
        Task<UserEntity> FindUserByEmail(string email);

        Task<UserEntity> FindUserByUserName(string userName);
    }
}
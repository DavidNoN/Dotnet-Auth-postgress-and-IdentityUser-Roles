using System.Threading.Tasks;
using UserApi.Dtos;
using UserApi.Entities;

namespace UserAPI.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(UserEntity user);

        TokenInfoDto ValidateToken(string tokenEncoded);
    }
}
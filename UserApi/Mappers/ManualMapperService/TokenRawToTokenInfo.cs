using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using UserApi.Dtos;

namespace UserApi.Mappers.ManualMapperService
{
    public static class TokenRawToTokenInfo
    {
        public static TokenInfoDto TokenRawToTokenInfoMapping(JwtSecurityToken jwtSecurityToken)
        {
            var tokenObjResult = new TokenInfoDto
            {
                NameId = jwtSecurityToken.Claims.First(c => c.Type == "nameid").Value,
                UniqueName = jwtSecurityToken.Claims.First(c => c.Type == "unique_name").Value,
                GivenName = jwtSecurityToken.Claims.First(c => c.Type == "given_name").Value,
                Email = jwtSecurityToken.Claims.First(c => c.Type == "email").Value,
                Role = jwtSecurityToken.Claims.First(c => c.Type == "role").Value,
                PhoneNumber = jwtSecurityToken.Claims.First(c => c.Type == "mobile_phone").Value
            };

            return tokenObjResult;
        }
    }
}
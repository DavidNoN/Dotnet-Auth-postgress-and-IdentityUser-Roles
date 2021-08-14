using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using UserApi.Dtos;
using UserApi.Entities;
using UserAPI.Interfaces;
using UserApi.Mappers.ManualMapperService;

namespace UserAPI.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;

        private readonly IConfiguration _configuration;
        private readonly UserManager<UserEntity> _userManager;
        private readonly ILogger<TokenService> _logger;

        public TokenService(IConfiguration configuration, UserManager<UserEntity> userManager, ILogger<TokenService> logger)
        {
            _configuration = configuration;
            _userManager = userManager;
            _logger = logger;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
        }
        
        public async Task<string> CreateToken(UserEntity user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserNameComplete),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("mobile_phone", user.PhoneNumber)
            };

            var roles = await _userManager.GetRolesAsync(user);
            
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }

        public TokenInfoDto ValidateToken(string tokenEncoded)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(_configuration["TokenKey"])) ;
            
            tokenHandler.ValidateToken(tokenEncoded, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(hmac.Key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);
   
            var jwtToken = (JwtSecurityToken)validatedToken;

            var tokenInfo = TokenRawToTokenInfo.TokenRawToTokenInfoMapping(jwtToken);
            
            _logger.LogInformation("Token Info result: {@TokenInfo}", tokenInfo);

            return tokenInfo;

        }
    }
}
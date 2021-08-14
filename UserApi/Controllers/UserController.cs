
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserApi.Dtos;
using UserAPI.Interfaces;

namespace UserApi.Controllers
{
    public class UserController : BaseUserApi
    {
        private readonly IFindUserRolesAsync _findUserRolesAsync;
        private readonly ILogger<UserController> _logger;
        private readonly ITokenService _tokenService;


        public UserController(
            IFindUserRolesAsync findUserRolesAsync,
            ILogger<UserController> logger, 
            ITokenService tokenService)
        {
            _findUserRolesAsync = findUserRolesAsync;
            _logger = logger;
            _tokenService = tokenService;
        }
        
        [Authorize(Policy = "RequireUserAdminRole")]
        [HttpGet("getUser")]
        public async Task<ActionResult<TokenInfoDto>> GetUser([FromHeader(Name = "Authorization")] string tokenEncoded)
        {
            
            if (tokenEncoded == null) return BadRequest("You must be logged to do this");
            
            _logger.LogInformation("Token Info: {TokenInfo}", tokenEncoded);
            tokenEncoded = tokenEncoded.Substring(7);

            var userTokenInfo = _tokenService.ValidateToken(tokenEncoded!);

            var userDetailResult = await _findUserRolesAsync.FindUserByEmail(userTokenInfo.Email!);            

            return Ok(userDetailResult);

        }
        
        
    }
}
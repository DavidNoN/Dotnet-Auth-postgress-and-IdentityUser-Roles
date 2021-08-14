
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserApi.Dtos;
using UserApi.Entities;
using UserAPI.Interfaces;
using UserApi.Mappers.AutoMapperService;
using UserAPI.Services;
using UserAPI.Services.Helpers;
using UserApi.Views;

namespace UserApi.Controllers
{
    public class AccountController : BaseUserApi
    {
        private readonly IPhoneAlreadyExist _phoneAlreadyExist;
        private readonly ISaveUserRepository _saveUserRepository;
        private readonly ILogger<AccountController> _logger;
        private readonly ILoginUserRepository _loginUserRepository;
        private readonly RegisterDtoToUserEntity _registerDtoToUserEntity;
        private readonly UserEntityToRegisterViewDao _userEntityToRegisterViewDao;
        private readonly UserEntityToLoginViewDao _userEntityToLoginViewDao;
        private readonly ISendVerificationMail _sendVerificationMail;
        private readonly RoleManager<RoleEntity> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly IFindUserRolesAsync _findUserRolesAsync;

        public AccountController(
            IPhoneAlreadyExist phoneAlreadyExist,
            ISaveUserRepository saveUserRepository,
            ILogger<AccountController> logger,
            ILoginUserRepository loginUserRepository,
            RegisterDtoToUserEntity registerDtoToUserEntity,
            UserEntityToRegisterViewDao userEntityToRegisterViewDao,
            UserEntityToLoginViewDao userEntityToLoginViewDao,
            ISendVerificationMail sendVerificationMail,
            RoleManager<RoleEntity> roleManager,
            ITokenService tokenService,
            IFindUserRolesAsync findUserRolesAsync)
        {
            _phoneAlreadyExist = phoneAlreadyExist;
            _saveUserRepository = saveUserRepository;
            _logger = logger;
            _loginUserRepository = loginUserRepository;
            _registerDtoToUserEntity = registerDtoToUserEntity;
            _userEntityToRegisterViewDao = userEntityToRegisterViewDao;
            _userEntityToLoginViewDao = userEntityToLoginViewDao;
            _sendVerificationMail = sendVerificationMail;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _findUserRolesAsync = findUserRolesAsync;
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisterViewDao>> Register(RegisterUserDto registerUserDto)
        {

            var age = CalculateAge.CalculateAgeFromDatetime(DateTime.Parse(registerUserDto.DateOfBirth!));
            
            _logger.LogInformation("The age of the person is : {Age}", age);
            
            if (age < 18) return BadRequest("You must be 18 or older to register here");

            if (await _phoneAlreadyExist.PhoneAlreadyExists(registerUserDto.PhoneNumber))
                return BadRequest("Phone already used. Please Login using it.");

            registerUserDto.SecurityStamp = Guid.NewGuid().ToString();
            
            var saveUserResult = await _saveUserRepository.SaveUserToDbAsync(
                _registerDtoToUserEntity.RegisterDtoToUserEntityMapping(registerUserDto), registerUserDto.Password);
            
            if (!saveUserResult.Succeeded) return BadRequest(saveUserResult.Errors);
            
            var user = await _findUserRolesAsync.FindUserByEmail(registerUserDto.Email);
            
            var roleResult = await _saveUserRepository.AddRoleToUserAsync(user, "User");
            
            if (!roleResult.Succeeded) return BadRequest(roleResult.Errors);

            var registerViewResult = _userEntityToRegisterViewDao.UserEntityToRegisterViewDaoMapping(user);
            registerViewResult.Token = await _tokenService.CreateToken(user);

            return Created("The user was registered Successful",registerViewResult);
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<LoginViewDao>> Login(LoginUserDto loginDto)
        {
            var user = await _loginUserRepository.ExistUserInDb(loginDto.UserNameEmailPhone);

            if (user == null) return BadRequest("Could not find user with the data provided");
            
            var signInResult = await _loginUserRepository.LoginUserAsync(user, loginDto.Password!);

            if (!signInResult.Succeeded) return BadRequest("Username, phone, email or password was Invalid");

            var loginUserResult = _userEntityToLoginViewDao.UserEntityToLoginViewDaoMapping(user);

            loginUserResult.Token = await _tokenService.CreateToken(user);
            
            _sendVerificationMail.SmtpServer(user.Email, user.UserName);
            
            return Ok(loginUserResult);
        }
        
        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("addRoles")]
        public async Task<ActionResult<bool>> AddRoles()
        {
            var roles = new List<RoleEntity>
            {
                new RoleEntity {Name = "Admin"},
                new RoleEntity {Name = "Moderator"},
                new RoleEntity {Name = "User"}
            };

            foreach (var role in roles)
            {
                await _roleManager.CreateAsync(role);
            }

            return true;
        }
        
        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("addRole")]
        public async Task<ActionResult<bool>> AddRole([FromQuery(Name = "role")] string newRole)
        {
            var roleToCreate = new RoleEntity() {Name = newRole};

            var roleCreatedResult = await _roleManager.CreateAsync(roleToCreate);

            if (!roleCreatedResult.Succeeded) return BadRequest("The new Role Could not be created");

            return true;
        }

        /*
        [HttpPut("verify-email")]
        public  ActionResult<bool> VerifyEmail()
        {
          
        }*/
    }
}
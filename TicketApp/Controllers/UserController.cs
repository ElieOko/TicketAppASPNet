using Microsoft.AspNetCore.Mvc;
using TicketApp.Interfaces;
using TicketApp.Models;
using System.Threading.Tasks;
using static TicketApp.Controllers.BranchController;
using Microsoft.EntityFrameworkCore;
using TicketApp.Repository;
using TicketApp.Data;
using System;
using TicketApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace TicketApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _userRepository;
        private readonly DataContext _dataContext;
        private readonly ILogger<UserController> _logger;
        private readonly IConfiguration _configuration;
        private readonly JwtServices _jwtServices;
      
        public UserController(IUser userRepository,DataContext dataContext, ILogger<UserController> logger, IConfiguration configuration, JwtServices jwtServices)
        {
            _userRepository = userRepository;
            _dataContext = dataContext;
            _logger = logger;
            _jwtServices = jwtServices;
            _configuration = configuration;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<User>>>> GetUsers()
        {
            var users = await _userRepository.GetAll();
            var response = new ApiResponse<IEnumerable<User>>
            {
                Success = true,
                Message = "Users retrieved successfully.",
                Data = users
            };

            return Ok(response);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<User>>> GetUser(int id)
        {
            var user = await _userRepository.GetUser(id);
 
            if (user == null)
            {
                return NotFound(new ApiResponse<User>
                {
                    Success = false,
                    Message = "User not found",
                    Data = null
                });
            }

            var response = new ApiResponse<User>
            {
                Success = true,
                Message = "User created successfully.",
                Data = user
            };

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ApiResponse<User>> Register([FromBody] User user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName) || string.IsNullOrWhiteSpace(user.Password))
            {
                return new ApiResponse<User>
                {
                    Success = false,
                    Message = "Username and password are required.",
                    Data = null,
                };
            }

            var result = await _userRepository.Register(user);
            if (result == null)
            {
                return new ApiResponse<User>
                {
                    Success = false,
                    Message = "Username already exists.",
                    Data = null
                };
            }

            return new ApiResponse<User>
            {
                Success = true,
                Message = "User registered successfully.",
                Data = result
            };
        }

        [HttpPost("login")]
        public async Task<ApiResponse<UserLoginModel>> Login([FromBody] UserLoginModel userLogin)
        {
            if (string.IsNullOrWhiteSpace(userLogin.Username) || string.IsNullOrWhiteSpace(userLogin.Password))
                return new ApiResponse<UserLoginModel>
                {
                    Success = false,
                    Message = "Username and password are required.",
                    Data = null,

                };

            var userAccount = await _dataContext.Users.FirstOrDefaultAsync(x => x.UserName == userLogin.Username);
            _logger.LogInformation($"User account fetched: {userAccount}");

            if (userAccount == null)
            {
                return new ApiResponse<UserLoginModel>
                {
                    Success = false,
                    Message = "Invalid username",
                    Data = null,
                    //DebugInfo = $"Username attempted: {userLogin.Username}" // Ajoutez cette ligne
                };
            }

            byte[] storedSalt = Convert.FromBase64String(userAccount.UserSalt);
            _logger.LogInformation($"UBINGOOOOOOOOOOOOOOOO: {storedSalt.Length}");

            if (!PaaswordHasher.VerifyPasswordHash(userLogin.Password, Convert.FromBase64String(userAccount.Password), storedSalt))
            {
                return new ApiResponse<UserLoginModel>
                {
                    Success = false,
                    Message = "Invalid password.",
                    Data = null
                };
            }


            var tokenResponse = await _jwtServices.Authentificate(userLogin);
            _logger.LogInformation($"Obama: {tokenResponse}");


            if (tokenResponse == null)
            {
                return new ApiResponse<UserLoginModel>
                {
                    Success = false,
                    Message = "Authentication failed.",
                    Data = null
                };
            }
            userAccount.AccessToken = tokenResponse?.AccessToken;
            userAccount.ExpiresIn = tokenResponse?.ExpiresIn;


            _dataContext.Users.Update(userAccount);
            await _dataContext.SaveChangesAsync();

            return new ApiResponse<UserLoginModel>
            {
                Success = true,
                Message = "Authentication successful.",
                Data = tokenResponse
            };
        }
        [Authorize]
        [HttpPost("resetPassword/{id}")]
        public async Task<ApiResponse<User>> ChangePassword([FromBody] ResetPasswordModel resetPasswordModel, int id)
        {
            if (string.IsNullOrWhiteSpace(resetPasswordModel.NewPassword) ||
                string.IsNullOrWhiteSpace(resetPasswordModel.ConfirmPassword))
            {
                return new ApiResponse<User>
                {
                    Success = false,
                    Message = "New password and Confirm password are required.",
                    Data = null
                };
            }
            if (resetPasswordModel.NewPassword != resetPasswordModel.ConfirmPassword)
            {
                return new ApiResponse<User>
                {
                    Success = false,
                    Message = "New password and Confirm password must match.",
                    Data = null
                };
            }
            var result = await _userRepository.ResetPassword(id, resetPasswordModel);
            if (result == null)
            {
                return new ApiResponse<User>
                {
                    Success = false,
                    Message = "User not found.",
                    Data = null
                };
            }

            return new ApiResponse<User>
            {
                Success = true,
                Message = "Password changed successfully.",
                Data = result
            };

        }
    
    }
}
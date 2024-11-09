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
using Microsoft.AspNetCore.Http.HttpResults;
using Serilog;

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



        /// <summary>
        /// You can search for Accounts here.
        /// </summary>

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
                Message = "User retrieved successfully.",
                Data = user
            };

            return Ok(response);
        }

        

        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName) || string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest(new ApiResponse<UserLoginModel>
                {
                    Success = false,
                    Message = "Username and password are required.",
                    Data = null,
                });
            }

            var result = await _userRepository.Register(user);
            using var log = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("Logs/logs.txt")
                .CreateLogger();

            if (result == null)
            {
                log.Warning("Tentative d'enregistrement avec un nom d'utilisateur existant : {Username}", user.UserName);
                return BadRequest(new ApiResponse<User>
                {
                    Success = false,
                    Message = "Ce compte existe déjà",
                    Data = null
                });
            }
            var logEntry = new LogEntry
            {
                Action = "Création compte utilisateur",
                UserName = user.UserName,
                Timestamp = DateTime.UtcNow,
                Message = "Compte créé avec succès."
            };

            log.Information("{@LogEntry}", logEntry); // Enregistrer l'entrée de log

            return Ok( new ApiResponse<User>
            {
                Success = true,
                Message = "Compte créé avec succès",
                Data = result
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel userLogin)
        {
            if (string.IsNullOrWhiteSpace(userLogin.Username) || string.IsNullOrWhiteSpace(userLogin.Password))
            {
                return BadRequest(new ApiResponse<UserLoginModel>
                {
                    Success = false,
                    Message = "Username and password are required.",
                    Data = null,
                });
            }

            var userAccount = await _dataContext.Users
          .FirstOrDefaultAsync(x => x.UserName.ToLower() == userLogin.Username.ToLower());

            using var log = new LoggerConfiguration()
              .WriteTo.Console()
              .WriteTo.File("Logs/logs.txt")
              .CreateLogger();
            if (userAccount == null)
            {
                log.Warning("Échec de la tentative de connexion pour l'utilisateur inexistant : {Username}", userLogin.Username);
                return NotFound(new ApiResponse<UserLoginModel>
                {
                    Success = false,
                    Message = "Ce compte utilisateur n'existe pas",
                    Data = null,
                });
            }
            var supportHelp = $"Si vous avez oublié votre mot de passe, contactez le support d'aide au 0854434602";

            if (userAccount.Locked)
            {
                return BadRequest(new ApiResponse<UserLoginModel>
                {
                    Success = false,
                    Message = $"Votre compte est verrouillé en raison de nombreuses tentatives de connexion échouées. <br> {supportHelp}",
                    Data = null,
                });
            }

            byte[] storedSalt = Convert.FromBase64String(userAccount.UserSalt);

            if (!PaaswordHasher.VerifyPasswordHash(userLogin.Password, Convert.FromBase64String(userAccount.Password), storedSalt))
            {
                userAccount.MaxAttempt++;
               

                if (userAccount.MaxAttempt >= 5)
                {
                    userAccount.Locked = true;
                    await _dataContext.SaveChangesAsync();
                    log.Warning("Compte est verrouillé : {Username}", userLogin.Username);
                    return BadRequest(new ApiResponse<UserLoginModel>
                    {
                        Success = false,
                        Message = $"Votre compte est verrouillé en raison de nombreuses tentatives de connexion échouées. <br> {supportHelp}",
                        Data = null,
                    });
                }

              
                int ? tentativeRestantes = 5 - userAccount.MaxAttempt;
                string msg = tentativeRestantes == 1
                    ? $"Il vous reste une tentative avant que votre compte ne soit bloqué. <br> {supportHelp}"
                    : $"Tu as {tentativeRestantes} tentatives restantes avant que votre compte ne soit bloqué. <br> {supportHelp}";

                await _dataContext.SaveChangesAsync();
                log.Warning("Échec de la tentative de connexion pour Mot de passe incorrect : {Username}", userLogin.Username);
                return BadRequest(new ApiResponse<UserLoginModel>
                {
                    Success = false,
                    Message = $"Mot de passe incorrect.<br>{msg}",
                    Data = null
                });
            }

            userAccount.MaxAttempt = 0;

            var tokenResponse = await _jwtServices.Authentificate(userLogin);

            if (tokenResponse == null)
            {
                return BadRequest(new ApiResponse<UserLoginModel>
                {
                    Success = false,
                    Message = "Authentication failed.",
                    Data = null
                });
            }

            userAccount.AccessToken = tokenResponse?.AccessToken;
            userAccount.ExpiresIn = tokenResponse?.ExpiresIn;

            _dataContext.Users.Update(userAccount);
            await _dataContext.SaveChangesAsync();

            var logEntrySuccess = new LogEntry
            {
                Action = "Connexion",
                UserName = userLogin.Username,
                Timestamp = DateTime.UtcNow,
                Message = "Authentification réussie."
            };

            log.Information("{@LogEntry}", logEntrySuccess); 

            return Ok(new ApiResponse<UserLoginModel>
            {
                Success = true,
                Message = "Authentication reussie.",
                Data = tokenResponse
            });
        }
        [Authorize]
        [HttpPost("resetPassword/{id}")]
        public async Task<IActionResult> ChangePassword([FromBody] ResetPasswordModel resetPasswordModel, int id)
        {
            if (string.IsNullOrWhiteSpace(resetPasswordModel.NewPassword) ||
                string.IsNullOrWhiteSpace(resetPasswordModel.ConfirmPassword))
            {
                return BadRequest( new ApiResponse<User>
                {
                    Success = false,
                    Message = "New password and Confirm password are required.",
                    Data = null
                });
            }
            if (resetPasswordModel.NewPassword != resetPasswordModel.ConfirmPassword)
            {
                return NotFound( new ApiResponse<User>
                {
                    Success = false,
                    Message = "Le nouveau mot de passe et Confirmer le mot de passe doivent correspondre.",
                    Data = null
                });
            }
            var result = await _userRepository.ResetPassword(id, resetPasswordModel);
            if (result == null)
            {
                return NotFound( new ApiResponse<User>
                {
                    Success = false,
                    Message = "Compte utilisateur non trouvé.",
                    Data = null
                });
            }

            return Ok( new ApiResponse<User>
            {
                Success = true,
                Message = "Mot de passe changé avec succés.",
                Data = result
            });

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            var success = await _userRepository.Delete(id);
            if (!success)
            {
                return NotFound(new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Ce compte n'existe pas.",
                    Data = false
                });
            }

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                Message = "Compte utilisateur supprime avec succ.",
                Data = true
            });
        }

    }
}
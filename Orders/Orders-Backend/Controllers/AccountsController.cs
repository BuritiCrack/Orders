using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Orders_Backend.Helpers;
using Orders_Backend.UnitOfWork.Interfaces;
using Orders_Shared.DTOs;
using Orders_Shared.Entities;
using Orders_Shared.Enums;
using Orders_Shared.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Orders_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IUsersUnitOfWork _usersUnitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IFileStorage _fileStorage;
        private readonly IMailHelper _mailHelper;
        private readonly ILogger<AccountsController> _logger;
        private readonly string _container;

        public AccountsController(IUsersUnitOfWork usersUnitOfWork, IConfiguration configuration,
            IFileStorage fileStorage, IMailHelper mailHelper, ILogger<AccountsController> logger)
        {
            _usersUnitOfWork = usersUnitOfWork;
            _configuration = configuration;
            _fileStorage = fileStorage;
            _mailHelper = mailHelper;
            _logger = logger;
            _container = "users";
        }

        [HttpPost("RecoverPassword")]
        public async Task<IActionResult> RecoverPasswordAsync([FromBody] EmailDTO model)
        {
            var user = await _usersUnitOfWork.GetUserAsync(model.Email);
            if (user == null) 
            {
                return NotFound();
            }

            var myToken = await _usersUnitOfWork.GeneratePasswordResetTokenAsync(user);
            var tokenLink = Url.Action("ResetPassword", "accounts", new
            {
                UserId = user.Id,
                token = myToken
            }, HttpContext.Request.Scheme, _configuration["Url Frontend"]);

            var response = _mailHelper.SendMail(user.FullName, user.Email!,
                $"Orders - Recuperación de contraseña",
                $"<h1>Orders - Recuperación de contraseña</h1>" +
                $"<p>Para recuperar su contraseña, por favor hacer clic 'Recuperar Contraseña':</p>" +
                $"<b><a href ={tokenLink}>Recuperar Contraseña</a></b>");

            if (response.WasSuccess)
            {
                return NoContent();
            }

            return BadRequest(response.Message);
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] RecoverDTO model)
        {
            var user = await _usersUnitOfWork.GetUserAsync(model.Email);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _usersUnitOfWork.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                return NoContent();
            }

            return BadRequest(result.Errors.FirstOrDefault()!.Description);
        }

        [HttpPost("ResendToken")]
        public async Task<IActionResult> ResendTokenAsync([FromBody] EmailDTO model)
        {
            var user = await _usersUnitOfWork.GetUserAsync(model.Email);
            if (user == null)
            {
                return NotFound();
            }

            var response = await SendConfirmationEmailAsync(user);
            if (response.WasSuccess)
            {
                return NoContent();
            }

            return BadRequest(response.Message);
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmailAsync(string userId, string token)
        {
            _logger.LogInformation("Iniciando confirmación de email. UserId: {UserId}", userId);

            try
            {
                // Registrar el token recibido (solo los primeros caracteres por seguridad)
                string tokenPreview = token.Length > 10 ? token.Substring(0, 10) + "..." : token;
                _logger.LogDebug("Token recibido (parcial): {TokenPreview}", tokenPreview);

                token = token.Replace(" ", "+");
                _logger.LogDebug("Token después de reemplazar espacios por +");

                // Verificar que userId sea un GUID válido
                if (!Guid.TryParse(userId, out Guid userGuid))
                {
                    _logger.LogWarning("ID de usuario inválido: {UserId}", userId);
                    return BadRequest("ID de usuario inválido");
                }

                _logger.LogInformation("Buscando usuario con ID: {UserGuid}", userGuid);
                var user = await _usersUnitOfWork.GetUserAsync(userGuid);

                if (user == null)
                {
                    _logger.LogWarning("Usuario no encontrado con ID: {UserGuid}", userGuid);
                    return NotFound("Usuario no encontrado");
                }

                _logger.LogInformation("Usuario encontrado: {UserEmail}. Procediendo a confirmar email", user.Email);
                var result = await _usersUnitOfWork.ConfirmEmailAsync(user, token);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    _logger.LogWarning("Error al confirmar email: {Errors}", errors);
                    return BadRequest(result.Errors.FirstOrDefault());
                }

                _logger.LogInformation("Email confirmado exitosamente para usuario: {UserEmail}", user.Email);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error crítico en ConfirmEmailAsync. UserId: {UserId}, Mensaje: {Message}, StackTrace: {StackTrace}",
                    userId, ex.Message, ex.StackTrace);

                if (ex.InnerException != null)
                {
                    _logger.LogError("Inner Exception: {InnerMessage}, {InnerStackTrace}",
                        ex.InnerException.Message, ex.InnerException.StackTrace);
                }

                return StatusCode(500, "Se produjo un error interno al confirmar el email");
            }

        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PutAsync(User user)
        {
            try
            {
                var currentUser = await _usersUnitOfWork.GetUserAsync(User.Identity!.Name!);
                if (currentUser == null)
                {
                    return NotFound();
                }

                if (!string.IsNullOrEmpty(user.Photo))
                {
                    var photoUser = Convert.FromBase64String(user.Photo);
                    user.Photo = await _fileStorage.SaveFileAsync(photoUser, ".jpg", _container);
                }

                currentUser.Document = user.Document;
                currentUser.FirstName = user.FirstName;
                currentUser.LastName = user.LastName;
                currentUser.Address = user.Address;
                currentUser.PhoneNumber = user.PhoneNumber;
                currentUser.Photo = !string.IsNullOrEmpty(user.Photo) && user.Photo !=
                    currentUser.Photo ? user.Photo : currentUser.Photo;
                currentUser.CityId = user.CityId;


                var result = await _usersUnitOfWork.UpdateUserAsync(currentUser);
                if (result.Succeeded)
                {
                    return Ok(BuildToken(currentUser));
                }

                return BadRequest(result.Errors.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _usersUnitOfWork.GetUserAsync(User.Identity!.Name!));
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO model)
        {
            User user = model;
            
            if (!string.IsNullOrEmpty(model.Photo))
            {
                var photoUser = Convert.FromBase64String(model.Photo);
                model.Photo = await _fileStorage.SaveFileAsync(photoUser, ".jpg", _container);
            }

            var result = await _usersUnitOfWork.AddUserAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _usersUnitOfWork.AddUserToRoleAsync(user, user.UserType.ToString());
                var response = await SendConfirmationEmailAsync(user);
                if (response.WasSuccess)
                {
                    return NoContent();
                }

                return BadRequest(response.Message);
            }

            return BadRequest(result.Errors.FirstOrDefault());
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDTO model)
        {
            var result = await _usersUnitOfWork.LoginAsync(model);
            if (result.Succeeded)
            {
                var user = await _usersUnitOfWork.GetUserAsync(model.Email);
                return Ok(BuildToken(user));
            }

            if (result.IsLockedOut)
            {
                return BadRequest("Ha superado el máximo número de intentos, su cuenta está bloqueada, intente de nuevo en 5 minutos.");
            }

            if (result.IsNotAllowed)
            {
                return BadRequest("El usuario no ha sido habilitado, debes de seguir las instrucciones del correo enviado para poder habilitar el usuario.");
            }

            return BadRequest("Correo o contraseña incorrectos.");
        }

        [HttpPost("changePassword")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _usersUnitOfWork.GetUserAsync(User.Identity!.Name!);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _usersUnitOfWork.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.FirstOrDefault()!.Description);
            }

            return NoContent();
        }

        private async Task<ActionResponse<string>> SendConfirmationEmailAsync(User user)
        {
            try
            {
                _logger.LogInformation("Generando token para confirmación de email: {UserEmail}", user.Email);
                var myToken = await _usersUnitOfWork.GenerateEmailConfirmationTokenAsync(user);

                _logger.LogDebug("Creando enlace de confirmación");
                var tokenLink = Url.Action("ConfirmEmail", "accounts", new
                {
                    userid = user.Id,
                    token = myToken
                }, HttpContext.Request.Scheme, _configuration["Url Frontend"]);

                _logger.LogDebug("Enlace de confirmación generado: {TokenLink}", tokenLink);

                var mailResult = _mailHelper.SendMail(user.FullName, user.Email!,
                    $"Orders - Confirmación de cuenta",
                    $"<h1>Orders - Confirmación de cuenta</h1>" +
                    $"<p>Para habilitar el usuario, por favor hacer clic 'Confirmar Email':</p>" +
                    $"<b><a href ={tokenLink}>Confirmar Email</a></b>");

                if (mailResult.WasSuccess)
                {
                    _logger.LogInformation("Email de confirmación enviado correctamente a: {UserEmail}", user.Email);
                }
                else
                {
                    _logger.LogWarning("Error al enviar email de confirmación: {ErrorMessage}", mailResult.Message);
                }

                return mailResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al enviar email de confirmación: {Message}", ex.Message);
                return new ActionResponse<string>
                {
                    WasSuccess = false,
                    Message = $"Error al enviar email: {ex.Message}"
                };
            }
        }

        private TokenDTO BuildToken(User user)
        {
            var claims = new List<Claim>
            {
                new (ClaimTypes.Name, user.Email!),
                new (ClaimTypes.Role, user.UserType.ToString()),
                new ("Document", user.Document),
                new("FirstName", user.FirstName),
                new("LastName", user.LastName),
                new("Address", user.Address),
                new("Photo", user.Photo ?? string.Empty),
                new("CityId", user.CityId.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwtkey"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(30);
            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);

            return new TokenDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var response = await _usersUnitOfWork.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalPages")]
        public async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _usersUnitOfWork.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

    }
}
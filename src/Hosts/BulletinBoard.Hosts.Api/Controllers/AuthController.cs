using BulletinBoard.Application.AppServices.Authentication.Exceptions;
using BulletinBoard.Application.AppServices.Authentication.Services;
using BulletinBoard.Contracts.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.Hosts.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с авторизацией.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Инициализация сервиса.
        /// </summary>
        /// <param name="authService">Сервис.</param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Логин в систему.
        /// </summary>
        /// <param name="dto">Модель.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>JWT.</returns>
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(LoginUserDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(LoginUserDto dto, CancellationToken cancellationToken)
        {
            string token;

            try
            {
                token = await _authService.Login(dto, cancellationToken);
            }
            catch (Exception ex) when (ex is LoginNotFoundException || ex is InvalidPasswordException)
            {
                return BadRequest(ex.Message);
            }

            return Ok(token);
        }

        /// <summary>
        /// Регистрация в системе.
        /// </summary>
        /// <param name="dto">Модель.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>ID созданного пользователя.</returns>
        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CreateUserDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register(CreateUserDto dto, CancellationToken cancellationToken)
        {
            Guid id;

            try
            {
                id = await _authService.Register(dto, cancellationToken);
            }
            catch (LoginAlreadyExistsException ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(Register), id);
        }
    }
}

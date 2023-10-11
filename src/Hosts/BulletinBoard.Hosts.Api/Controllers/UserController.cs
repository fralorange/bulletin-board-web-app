using BulletinBoard.Application.AppServices.Authentication.Constants;
using BulletinBoard.Application.AppServices.Contexts.User.Services;
using BulletinBoard.Contracts.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulletinBoard.Hosts.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с пользовательскими методами.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Инициализирует экземпляр <see cref="UserController"/>
        /// </summary>
        /// <param name="userService"></param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Возвращает список всех пользователей.
        /// </summary>
        /// <param name="limit">Ограничение.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [Authorize(Roles = AuthRoles.Admin)]
        [HttpGet("get-all-with-limit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken, int limit = 10)
        {
            var result = await _userService.GetAllAsync(cancellationToken, limit);
            return Ok(result);
        }

        /// <summary>
        /// Возвращает пользователя по заданному идентификатору.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("get-by-id")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            //TO-DO: Сделать чтобы возвращал логин.
            var result = await _userService.GetByIdAsync(id, cancellationToken);
            if (result == null)
                return NotFound(result);
            return Ok(result);
        }

        /// <summary>
        /// Возвращает текущего пользователя.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("get-current-user")]
        [ProducesResponseType(typeof(InfoUserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetCurrentUserAsync(CancellationToken cancellationToken)
        {
            var result = await _userService.GetCurrentUser(cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Редактирует пользователя.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="dto">Модель.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateUserDto dto, CancellationToken cancellationToken)
        {
            var currentUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUserRole = HttpContext.User.FindFirstValue(ClaimTypes.Role);

            if (currentUserId != id.ToString() && currentUserRole != AuthRoles.Admin)
                return Forbid();
            var state = await _userService.UpdateAsync(id, dto, cancellationToken);
            if (!state)
                return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Удаляет пользователя.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var currentUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUserRole = HttpContext.User.FindFirstValue(ClaimTypes.Role);

            if (currentUserId != id.ToString() && currentUserRole != AuthRoles.Admin)
                return Forbid();
            var state = await _userService.DeleteAsync(id, cancellationToken);
            if (!state)
                return NotFound();
            return NoContent();
        }
    }
}

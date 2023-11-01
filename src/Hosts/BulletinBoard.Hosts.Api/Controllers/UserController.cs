using BulletinBoard.Application.AppServices.Authentication.Constants;
using BulletinBoard.Application.AppServices.Contexts.User.Services;
using BulletinBoard.Application.AppServices.Exceptions;
using BulletinBoard.Application.AppServices.Filtration.User.Filter;
using BulletinBoard.Application.AppServices.Filtration.User.Specification;
using BulletinBoard.Contracts.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<UserController> _logger;

        /// <summary>
        /// Инициализирует экземпляр <see cref="UserController"/>
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="memoryCache"></param>
        /// <param name="logger"></param>
        public UserController(IUserService userService, IMemoryCache memoryCache, ILogger<UserController> logger)
        {
            _userService = userService;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        /// <summary>
        /// Возвращает список всех пользователей.
        /// </summary>
        /// <param name="filter">Фильтр.</param>
        /// <param name="pageSize">Размер страницы.</param>
        /// <param name="pageIndex">Номер страницы.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [Authorize(Roles = AuthRoles.Admin)]
        [HttpGet("get-all-with-limit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAllAsync([FromQuery] UserFilter filter, CancellationToken cancellationToken, int pageSize = 10, int pageIndex = 0)
        {
            var spec = new UserSpecification(filter.Name, filter.Role);
            var result = await _userService.GetAllAsync(spec, pageSize, pageIndex, cancellationToken);
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
            var cacheKey = $"User_{id}";

            if (!_memoryCache.TryGetValue(cacheKey, out var result))
            {
                var user = await _userService.GetByIdAsync(id, cancellationToken);

                if (user != null)
                {
                    result = await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
                    {
                        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15);
                        entry.Priority = CacheItemPriority.Low;

                        _logger.LogInformation($"User with id {id} was successfully retrieved from the service and stored in the cache.");

                        return user;
                    });
                }
            }
            else
            {
                _logger.LogInformation($"User with id {id} was successfully retrieved from the cache.");
            }

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
            try
            {
                await _userService.UpdateAsync(id, dto, cancellationToken);
            }
            catch (EntityNotFoundException ex)
            {
                ModelState.AddModelError("NotFoundError", ex.Message);
                return NotFound(ModelState);
            }
            catch (EntityForbiddenException ex)
            {
                ModelState.AddModelError("ForbiddenError", ex.Message);
                return StatusCode(403, ModelState);
            }

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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _userService.DeleteAsync(id, cancellationToken);
            }
            catch (EntityNotFoundException ex)
            {
                ModelState.AddModelError("NotFoundError", ex.Message);
                return NotFound(ModelState);
            }
            catch (EntityForbiddenException ex)
            {
                ModelState.AddModelError("ForbiddenError", ex.Message);
                return StatusCode(403, ModelState);
            }

            return NoContent();
        }
    }
}

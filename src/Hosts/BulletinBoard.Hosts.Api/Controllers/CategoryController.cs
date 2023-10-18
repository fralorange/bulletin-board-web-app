using BulletinBoard.Application.AppServices.Authentication.Constants;
using BulletinBoard.Application.AppServices.Contexts.Category.Services;
using BulletinBoard.Application.AppServices.Exceptions;
using BulletinBoard.Contracts.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BulletinBoard.Hosts.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с категорями.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private const string CacheKey = "CategoryList";
        private readonly ICategoryService _categoryService;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<CategoryController> _logger;

        /// <summary>
        /// Инициализирует экземпляр <see cref="CategoryController"/>
        /// </summary>
        /// <param name="categoryService">Сервис для работы с категориями.</param>
        /// <param name="memoryCache">Кеш веб-сервера.</param>
        /// <param name="logger">Логгер.</param>
        public CategoryController(ICategoryService categoryService, IMemoryCache memoryCache, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        /// <summary>
        /// Возвращает ограниченный список всех категорий.
        /// </summary>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("get-all-with-limit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            if (!_memoryCache.TryGetValue(CacheKey, out var categoryList)) {
                categoryList = await _memoryCache.GetOrCreateAsync(CacheKey, async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24);
                    entry.Priority = CacheItemPriority.High;

                    var categories = await _categoryService.GetAllAsync(cancellationToken);

                    _logger.LogInformation("Categories were successfully retrieved from the service and stored in the cache.");

                    return categories;
                });
            } else
            {
                _logger.LogInformation("Categories were successfully retrieved from the cache.");
            }

            return Ok(categoryList);
        }

        /// <summary>
        /// Возвращает категорию по заданному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("get-by-id")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _categoryService.GetByIdAsync(id, cancellationToken);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Создаёт категорию.
        /// </summary>
        /// <param name="dto">Модель категории.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns></returns>
        [Authorize]
        [Authorize(Roles = AuthRoles.Admin)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCategoryDto dto, CancellationToken cancellationToken)
        {
            var dtoId = await _categoryService.CreateAsync(dto, cancellationToken);

            _memoryCache.Remove(CacheKey);
            _logger.LogInformation("Сache was reset after the new category was created.");

            return Created(nameof(CreateAsync), dtoId);
        }

        /// <summary>
        /// Редактирует категорию.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="dto">Модель категории.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        [Authorize]
        [Authorize(Roles = AuthRoles.Admin)]
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateCategoryDto dto, CancellationToken cancellationToken)
        {
            try
            {
                await _categoryService.UpdateAsync(id, dto, cancellationToken);
            }
            catch (EntityNotFoundException ex)
            {
                ModelState.AddModelError("NotFoundError", ex.Message);
                return NotFound(ModelState);
            }

            _memoryCache.Remove(CacheKey);
            _logger.LogInformation("Сache was reset after the category was updated.");

            return NoContent();
        }

        /// <summary>
        /// Удаляет категорию по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns></returns>
        [Authorize]
        [Authorize(Roles = AuthRoles.Admin)]
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _categoryService.DeleteAsync(id, cancellationToken);
            }
            catch (EntityNotFoundException ex)
            {
                ModelState.AddModelError("NotFoundError", ex.Message);
                return NotFound(ModelState);
            }

            _memoryCache.Remove(CacheKey);
            _logger.LogInformation("Сache was reset after the category was deleted.");

            return NoContent();
        }
    }
}

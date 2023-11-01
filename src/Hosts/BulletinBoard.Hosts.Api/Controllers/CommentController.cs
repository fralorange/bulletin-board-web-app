using BulletinBoard.Application.AppServices.Contexts.Comment.Services;
using BulletinBoard.Application.AppServices.Exceptions;
using BulletinBoard.Application.AppServices.Filtration.Comment.Filter;
using BulletinBoard.Application.AppServices.Filtration.Comment.Specification;
using BulletinBoard.Contracts.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BulletinBoard.Hosts.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с комментариями.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<CommentController> _logger;

        /// <summary>
        /// Инициализирует экземпляр <see cref="CommentController"/> 
        /// </summary>
        /// <param name="commentService"></param>
        /// <param name="memoryCache"></param>
        /// <param name="logger"></param>
        public CommentController(ICommentService commentService, IMemoryCache memoryCache, ILogger<CommentController> logger)
        {
            _commentService = commentService;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        /// <summary>
        /// Возвращает постраничные объявления.
        /// </summary>
        /// <param name="filter">Фильтр.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <param name="pageSize">Размер страницы.</param>
        /// <param name="pageIndex">Номер страницы.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("get-all-with-limit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync([FromQuery] CommentFilter filter, CancellationToken cancellationToken, int pageSize = 10, int pageIndex = 0)
        {
            var spec = new CommentSpecification(filter.MinRating, filter.MaxRating);
            var result = await _commentService.GetAllAsync(spec, pageSize, pageIndex, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Возвращает комментарий по заданному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("get-by-id")]
        [ProducesResponseType(typeof(CommentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var cacheKey = $"Comment_{id}";

            if (!_memoryCache.TryGetValue(cacheKey, out var result))
            {
                var comment = await _commentService.GetByIdAsync(id, cancellationToken);

                if (comment != null)
                {
                    result = await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
                    {
                        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15);
                        entry.Priority = CacheItemPriority.Low;

                        _logger.LogInformation($"Comment with id {id} was successfully retrieved from the service and stored in the cache.");

                        return comment;
                    });
                }
            }
            else
            {
                _logger.LogInformation($"Comment with id {id} was successfully retrieved from the cache.");
            }

            if (result == null)
                return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Создаёт комментарий.
        /// </summary>
        /// <param name="dto">Модель комментария.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCommentDto dto, CancellationToken cancellationToken)
        {
            var dtoId = await _commentService.CreateAsync(dto, cancellationToken);

            return Created(nameof(CreateAsync), dtoId);
        }

        /// <summary>
        /// Редактирует комментарий.
        /// </summary>
        /// <param name="id">Идентификатор комментария.</param>
        /// <param name="dto">Модель комментария.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        [Authorize]
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateCommentDto dto, CancellationToken cancellationToken)
        {
            try
            {
                await _commentService.UpdateAsync(id, dto, cancellationToken);
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
        /// Удаляет комментарий по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _commentService.DeleteAsync(id, cancellationToken);
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

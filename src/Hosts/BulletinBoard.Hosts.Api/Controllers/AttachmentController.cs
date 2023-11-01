using BulletinBoard.Application.AppServices.Authentication.Constants;
using BulletinBoard.Application.AppServices.Contexts.Attachment.Services;
using BulletinBoard.Application.AppServices.Exceptions;
using BulletinBoard.Contracts.Attachment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BulletinBoard.Hosts.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с изображениями.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AttachmentController : ControllerBase
    {
        private readonly IAttachmentService _attachmentService;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<AttachmentController> _logger;

        /// <summary>
        /// Инициализирует экземпляр <see cref="AttachmentController"/>
        /// </summary>
        /// <param name="attachmentService"><see cref="IAttachmentService"/></param>
        /// <param name="memoryCache"></param>
        /// <param name="logger"></param>
        public AttachmentController(IAttachmentService attachmentService, IMemoryCache memoryCache, ILogger<AttachmentController> logger)
        {
            _attachmentService = attachmentService;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        /// <summary>
        /// Возвращает ограниченный список всех изображений.
        /// </summary>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns></returns>
        [Authorize]
        [Authorize(Roles = AuthRoles.Admin)]
        [HttpGet("get-all-with-limit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _attachmentService.GetAllAsync(cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Возвращает изображение по заданному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("get-by-id")]
        [ProducesResponseType(typeof(AttachmentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var cacheKey = $"Att_{id}";

            if (!_memoryCache.TryGetValue(cacheKey, out var result))
            {
                var att = await _attachmentService.GetByIdAsync(id, cancellationToken);

                if (att != null)
                {
                    result = await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
                    {
                        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15);
                        entry.Priority = CacheItemPriority.Low;

                        _logger.LogInformation($"Attachment with id {id} was successfully retrieved from the service and stored in the cache.");

                        return att;
                    });
                }
            }
            else
            {
                _logger.LogInformation($"Attachment with id {id} was successfully retrieved from the cache.");
            }

            if (result == null)
                return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Создаёт изображение.
        /// </summary>
        /// <param name="dto">Модель изображения.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateAsync([FromForm] CreateAttachmentDto dto, CancellationToken cancellationToken)
        {
            Guid dtoId = Guid.Empty;

            try
            {
                dtoId = await _attachmentService.CreateAsync(dto, cancellationToken);
            }
            catch (EntityForbiddenException ex)
            {
                ModelState.AddModelError("ForbiddenError", ex.Message);
                return StatusCode(403, ModelState);
            }

            return Created(nameof(CreateAsync), dtoId);
        }

        /// <summary>
        /// Удаляет изображение по идентификатору.
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
                await _attachmentService.DeleteAsync(id, cancellationToken);
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

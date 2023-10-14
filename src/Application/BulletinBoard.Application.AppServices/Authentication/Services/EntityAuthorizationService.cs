using BulletinBoard.Application.AppServices.Contexts.Ad.Repositories;
using System.Security.Claims;

namespace BulletinBoard.Application.AppServices.Authentication.Services
{
    /// <inheritdoc cref="IEntityAuthorizationService"/>
    public class EntityAuthorizationService : IEntityAuthorizationService
    {
        private readonly IAdRepository _adRepository;

        /// <summary>
        /// Инициализация сервиса проверки прав доступа.
        /// </summary>
        /// <param name="adRepository"></param>
        public EntityAuthorizationService(IAdRepository adRepository)
        {
            _adRepository = adRepository;
        }

        /// <inheritdoc/>
        public Task<bool> Validate(ClaimsPrincipal user, Guid entityId, string role)
        {
            var entity = _adRepository.GetByIdAsync(entityId, CancellationToken.None).Result;
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRole = user.FindFirstValue(ClaimTypes.Role);

            return Task.FromResult(entity!.User.Id.ToString() != userId && userRole != role);
        }

        /// <inheritdoc/>
        public Task<bool> ValidateUserOnly(ClaimsPrincipal user, Guid userId, string role)
        {
            var currentUserId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUserRole = user.FindFirstValue(ClaimTypes.Role);

            return Task.FromResult(currentUserId != userId.ToString() &&  currentUserRole != role);
        }
    }
}

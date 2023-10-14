using System.Security.Claims;

namespace BulletinBoard.Application.AppServices.Authentication.Services
{
    /// <summary>
    /// Сервис для проверки прав доступа пользователя к сущности.
    /// </summary>
    public interface IEntityAuthorizationService
    {
        /// <summary>
        /// Валидация принадлежности сущности к пользователю или наличия у пользователя привилегий конкретной роли.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <param name="entityId">Идентификатор проверяемой сущности.</param>
        /// <param name="role">Роль.</param>
        /// <returns></returns>
        Task<bool> Validate(ClaimsPrincipal user, Guid entityId, string role);

        /// <summary>
        /// Валидация прав доступа пользователя по отношению к другому пользователю.
        /// </summary>
        /// <param name="user">Текущий пользователь.</param>
        /// <param name="userId">Идентификатор другого пользователя.</param>
        /// <param name="role">Роль другого пользователя.</param>
        /// <returns></returns>
        Task<bool> ValidateUserOnly(ClaimsPrincipal user, Guid userId, string role);
    }
}

using BulletinBoard.Contracts.User;

namespace BulletinBoard.Application.AppServices.Authentication.Services
{
    /// <summary>
    /// Сервис аутентификации и регистрации пользователей.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Регистрация пользователя и сохранение его в БД.
        /// </summary>
        /// <param name="dto">Модель.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Уникальный идентификатор созданного пользователя.</returns>
        Task<Guid> Register(CreateUserDto dto, CancellationToken cancellationToken);

        /// <summary>
        /// Логин пользователя в систему.
        /// </summary>
        /// <param name="dto">Модель.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>JWT.</returns>
        Task<string> Login(LoginUserDto dto, CancellationToken cancellationToken);
    }
}

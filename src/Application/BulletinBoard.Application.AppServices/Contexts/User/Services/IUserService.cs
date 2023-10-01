﻿using BulletinBoard.Contracts.User;

namespace BulletinBoard.Application.AppServices.Contexts.User.Services
{
    /// <summary>
    /// Сервис работы с пользователями.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Возвращает пользователя по заданному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Модель пользователя <see cref="UserDto"/>.</returns>
        Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает всех пользователей в заданных пределах (по-умолчанию: 10).
        /// </summary>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <param name="limit">Ограничение выборки пользователей.</param>
        /// <returns>Коллекция пользователей <see cref="UserDto"/>.</returns>
        Task<IReadOnlyCollection<UserDto>> GetAllAsync(CancellationToken cancellationToken, int limit = 10);

        /// <summary>
        /// Создает нового пользователя.
        /// </summary>
        /// <param name="dto">Модель пользователя..</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Идентификатор созданной сущности.</returns>
        Task<Guid> CreateAsync(CreateUserDto dto, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует данные пользователя.
        /// </summary>
        /// <param name="id">Идентификатор польхователя.</param>
        /// <param name="dto">Модель пользователя.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        Task UpdateAsync(Guid id, UpdateUserDto dto, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
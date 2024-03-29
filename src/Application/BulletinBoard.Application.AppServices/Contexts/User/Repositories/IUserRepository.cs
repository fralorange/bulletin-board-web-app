﻿using BulletinBoard.Application.AppServices.Filtration.User.Specification;
using BulletinBoard.Contracts.User;
using System.Linq.Expressions;

namespace BulletinBoard.Application.AppServices.Contexts.User.Repositories
{
    /// <summary>
    /// Репозиторий для работы с пользователями.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Возвращает пользователя по заданному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Модель пользователя <see cref="UserDto"/>.</returns>
        Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает ограниченный список пользователей.
        /// </summary>
        /// <param name="specification">Спецификация.</param>
        /// <param name="cancellationToken">Отмена операции</param>
        /// <returns>Коллекция пользователей <see cref="UserDto"/></returns>
        Task<IReadOnlyCollection<UserDto>> GetAllAsync(UserSpecification specification, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает пользователя по заданному условию.
        /// </summary>
        /// <param name="predicate">Предиката.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Модель пользователя <see cref="Domain.User.User"/>.</returns>
        Task<Domain.User.User?> GetByPredicate(Expression<Func<Domain.User.User, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает текущего пользователя.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Модель пользователя <see cref="InfoUserDto"/></returns>
        Task<InfoUserDto> GetCurrentUser(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Создает пользователя.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Идентификатор созданной сущности.</returns>
        Task<Guid> CreateAsync(Domain.User.User user, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует пользователя.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="user">Пользователь.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        Task UpdateAsync(Guid id, Domain.User.User user, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет пользователя по идентификатору.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        Task DeleteAsync(Domain.User.User user, CancellationToken cancellationToken);
    }
}

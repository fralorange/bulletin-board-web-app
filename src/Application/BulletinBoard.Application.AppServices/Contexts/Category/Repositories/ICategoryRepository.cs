using BulletinBoard.Contracts.Category;
using System.Linq.Expressions;

namespace BulletinBoard.Application.AppServices.Contexts.Category.Repositories
{
    /// <summary>
    /// Репозиторий для работы с категориями.
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        /// Возвращает категорию по заданному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Модель категории <see cref="CategoryDto"/>.</returns>
        Task<CategoryDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает ограниченный список категорий.
        /// </summary>
        /// <param name="cancellationToken">Отмена операции</param>
        /// <returns>Коллекция категорий <see cref="CategoryDto"/></returns>
        Task<IReadOnlyCollection<CategoryDto>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает категории по заданному условию.
        /// </summary>
        /// <param name="predicate">Предиката.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Сущность категории <see cref="Domain.Category.Category"/>.</returns>
        Task<Domain.Category.Category?> GetByPredicate(Expression<Func<Domain.Category.Category, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Создает категорию.
        /// </summary>
        /// <param name="category">Категория.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Идентификатор созданной сущности.</returns>
        Task<Guid> CreateAsync(Domain.Category.Category category, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует категорию.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="category">Категория.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        Task UpdateAsync(Guid id, Domain.Category.Category category, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет категорию по идентификатору.
        /// </summary>
        /// <param name="category">Категория.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        Task DeleteAsync(Domain.Category.Category category, CancellationToken cancellationToken);
    }
}

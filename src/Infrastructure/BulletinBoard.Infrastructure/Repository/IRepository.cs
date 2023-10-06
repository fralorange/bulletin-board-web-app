using System.Linq.Expressions;

namespace BulletinBoard.Infrastructure.Repository
{
    /// <summary>
    /// Базовый репозиторий.
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Возвращает коллекцию.
        /// </summary>
        /// <returns>Коллекцию <see cref="TEntity"/>.</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Возвращает отфильтрованную коллекцию.
        /// </summary>
        /// <param name="predicate">Предиката.</param>
        /// <returns>Коллекцию <see cref="TEntity"/>.</returns>
        IQueryable<TEntity> GetAllFiltered(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Возвращает модель по заданному идентификатору.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Модель <see cref="TEntity"/></returns>
        Task<TEntity?> GetByIdAsync(Guid id);

        /// <summary>
        /// Добавляет модель.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken">Отмена операции.</param>
        Task AddAsync(TEntity model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует модель.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken">Отмена операции.</param>
        Task UpdateAsync(TEntity model, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет модель.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken">Отмена операции.</param> 
        Task DeleteAsync(TEntity model, CancellationToken cancellationToken);
    }
}

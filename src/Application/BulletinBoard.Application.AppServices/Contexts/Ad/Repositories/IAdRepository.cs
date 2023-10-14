using BulletinBoard.Contracts.Ad;
using System.Linq.Expressions;

namespace BulletinBoard.Application.AppServices.Contexts.Ad.Repositories
{
    /// <summary>
    /// Репозиторий для работы с объявлениями.
    /// </summary>
    public interface IAdRepository
    {
        /// <summary>
        /// Возвращает объявлению по заданному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор объявления.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Модель объявления <see cref="AdDto"/>.</returns>
        Task<AdDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает объявления в пределах страницы.
        /// </summary>
        /// <param name="cancellationToken">Отмена операции</param>
        /// <param name="pageSize">Размер страницы.</param>
        /// <param name="pageIndex">Номер страницы.</param>
        /// <returns>Коллекция объявлений <see cref="AdDto"/></returns>
        Task<IReadOnlyCollection<AdDto>> GetAllAsync(CancellationToken cancellationToken, int pageSize = 10, int pageIndex = 0);

        /// <summary>
        /// Возвращает объявление по заданному условию.
        /// </summary>
        /// <param name="predicate">Предиката.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Модель объявления <see cref="Domain.Ad.Ad"/>.</returns>
        Task<Domain.Ad.Ad?> GetByPredicate(Expression<Func<Domain.Ad.Ad, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Создает объявление.
        /// </summary>
        /// <param name="ad">Объявление.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Идентификатор созданной сущности.</returns>
        Task<Guid> CreateAsync(Domain.Ad.Ad ad, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует объявление.
        /// </summary>
        /// <param name="id">Идентификатор объявления.</param>
        /// <param name="ad">Объявление.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        Task UpdateAsync(Guid id, Domain.Ad.Ad ad, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет объявление по идентификатору.
        /// </summary>
        /// <param name="ad">Объявление.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        Task DeleteAsync(Domain.Ad.Ad ad, CancellationToken cancellationToken);
    }
}

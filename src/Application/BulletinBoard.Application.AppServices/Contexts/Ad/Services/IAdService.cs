using BulletinBoard.Contracts.Ad;

namespace BulletinBoard.Application.AppServices.Contexts.Ad.Services
{
    /// <summary>
    /// Сервис работы с объявлениями.
    /// </summary>
    public interface IAdService
    {
        /// <summary>
        /// Возвращает объявления в пределах страницы.
        /// </summary>
        /// <param name="cancellationToken">Отмена операции</param>
        /// <param name="pageSize">Размер страницы.</param>
        /// <param name="pageIndex">Номер страницы.</param>
        /// <returns>Коллекция объявлений <see cref="AdDto"/>.</returns>
        Task<IReadOnlyCollection<AdDto>> GetAllAsync(CancellationToken cancellationToken, int pageSize, int pageIndex);

        /// <summary>
        /// Возвращает объявление по заданному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор объявления.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Модель объявления <see cref="AdDto"/>.</returns>
        Task<AdDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Создает объявление.
        /// </summary>
        /// <param name="dto">Модель объявления.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Идентификатор созданной сущности.</returns>
        Task<Guid> CreateAsync(CreateAdDto dto, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует объявление.
        /// </summary>
        /// <param name="id">Идентификатор объявления.</param>
        /// <param name="dto">Модель объявления.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        Task UpdateAsync(Guid id, UpdateAdDto dto, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет объявление по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор объявления.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
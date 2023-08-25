using BulletinBoard.Contracts.Ad;

namespace BulletinBoard.Application.AppServices.Contexts.Ad.Services
{
    /// <summary>
    /// Сервис работы с объявлениями.
    /// </summary>
    public interface IAdService
    {
        /// <summary>
        /// Возвращает объявлению по заданному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор объявления.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Модель объявления <see cref="AdDto"/>.</returns>
        Task<AdDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<AdDto>> GetAllAsync(CancellationToken cancellationToken, int pageSize = 10, int pageIndex = 0);
    }
}

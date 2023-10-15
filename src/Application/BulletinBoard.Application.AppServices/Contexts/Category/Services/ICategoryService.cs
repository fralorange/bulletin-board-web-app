using BulletinBoard.Contracts.Category;

namespace BulletinBoard.Application.AppServices.Contexts.Category.Services
{
    /// <summary>
    /// Сервис для работы с кагетогиями.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Возвращает категорию по заданному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Модель категории <see cref="CategoryDto"/>.</returns>
        Task<CategoryDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает все категории в заданных пределах (по-умолчанию: 10).
        /// </summary>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <param name="limit">Ограничение выборки категорий.</param>
        /// <returns>Коллекция категорий <see cref="CategoryDto"/>.</returns>
        Task<IReadOnlyCollection<CategoryDto>> GetAllAsync(CancellationToken cancellationToken, int limit = 10);

        /// <summary>
        /// Создаёт категорию.
        /// </summary>
        /// <param name="dto">Модель категории.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Идентификатор созданной категории.</returns>
        Task<Guid> CreateAsync(CreateCategoryDto dto, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует данные категории.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="dto">Модель категории.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        Task UpdateAsync(Guid id, UpdateCategoryDto dto, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет категорию по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}

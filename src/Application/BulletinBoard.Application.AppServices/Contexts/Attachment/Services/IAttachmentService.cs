using BulletinBoard.Contracts.Attachment;

namespace BulletinBoard.Application.AppServices.Contexts.Attachment.Services
{
    /// <summary>
    /// Сервис работы с изображениями.
    /// </summary>
    public interface IAttachmentService
    {
        /// <summary>
        /// Возвращает все изображения.
        /// </summary>
        /// <param name="cancellationToken">Отмена операции</param>
        /// <returns>Коллекция изображений <see cref="AttachmentDto"/></returns>
        Task<IReadOnlyCollection<AttachmentDto>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает изображение по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Модель изображения <see cref="AttachmentDto"/></returns>
        Task<AttachmentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Создает изображение.
        /// </summary>
        /// <param name="dto">Модель изображение.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Идентификатор созданной сущности.</returns>
        Task<Guid> CreateAsync(CreateAttachmentDto dto, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет изображение по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор изображения.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}

using BulletinBoard.Contracts.Attachment;
using System.Linq.Expressions;

namespace BulletinBoard.Application.AppServices.Contexts.Attachment.Repositories
{
    /// <summary>
    /// Репозиторий для работы с изображениями.
    /// </summary>
    public interface IAttachmentRepository
    {
        /// <summary>
        /// Возвращает изображение по заданному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор изображения.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Модель изображения <see cref="AttachmentDto"/>.</returns>
        Task<AttachmentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает все изображения.
        /// </summary>
        /// <param name="cancellationToken">Отмена операции</param>
        /// <returns>Коллекция изображений <see cref="AttachmentDto"/>.</returns>
        Task<IReadOnlyCollection<AttachmentDto>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает изображение по заданному условию.
        /// </summary>
        /// <param name="predicate">Предиката.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Модель объявления <see cref="Domain.Attachment.Attachment"/>.</returns>
        Task<Domain.Attachment.Attachment?> GetByPredicate(Expression<Func<Domain.Attachment.Attachment, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Создает изображение.
        /// </summary>
        /// <param name="attachment">Изображение.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Идентификатор созданной сущности.</returns>
        Task<Guid> CreateAsync(Domain.Attachment.Attachment attachment, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет изображение по идентификатору.
        /// </summary>
        /// <param name="attachment">Изображение.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        Task DeleteAsync(Domain.Attachment.Attachment attachment, CancellationToken cancellationToken);
    }
}

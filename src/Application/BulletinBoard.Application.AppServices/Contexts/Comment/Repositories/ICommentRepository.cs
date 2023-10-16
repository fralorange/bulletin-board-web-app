using BulletinBoard.Contracts.Comment;
using System.Linq.Expressions;

namespace BulletinBoard.Application.AppServices.Contexts.Comment.Repositories
{
    /// <summary>
    /// Репозиторий для работы с комментариями.
    /// </summary>
    public interface ICommentRepository
    {
        /// <summary>
        /// Возвращает комментарий по заданному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор комментария.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Модель комментария <see cref="CommentDto"/>.</returns>
        Task<CommentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает комментария в пределах страницы.
        /// </summary>
        /// <param name="cancellationToken">Отмена операции</param>
        /// <returns>Коллекция комментариев <see cref="CommentDto"/></returns>
        Task<IReadOnlyCollection<CommentDto>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает комментарий по заданному условию.
        /// </summary>
        /// <param name="predicate">Предиката.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Модель комментария <see cref="Domain.Comment.Comment"/>.</returns>
        Task<Domain.Comment.Comment?> GetByPredicate(Expression<Func<Domain.Comment.Comment, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Создает комментарий.
        /// </summary>
        /// <param name="comment">Комментарий.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Идентификатор созданной сущности.</returns>
        Task<Guid> CreateAsync(Domain.Comment.Comment comment, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует комментарий.
        /// </summary>
        /// <param name="id">Идентификатор комментария.</param>
        /// <param name="comment">Комментарий.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        Task UpdateAsync(Guid id, Domain.Comment.Comment comment, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет комментарий по идентификатору.
        /// </summary>
        /// <param name="comment">Комментарий.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        Task DeleteAsync(Domain.Comment.Comment comment, CancellationToken cancellationToken);
    }
}

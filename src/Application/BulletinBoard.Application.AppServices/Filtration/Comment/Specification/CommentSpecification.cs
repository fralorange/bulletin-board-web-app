using BulletinBoard.Application.AppServices.Filtration.Base.Specification;
using System.Linq.Expressions;

namespace BulletinBoard.Application.AppServices.Filtration.Comment.Specification
{
    /// <summary>
    /// Спецификация комментария.
    /// </summary>
    public class CommentSpecification : ISpecification<Domain.Comment.Comment>
    {
        /// <summary>
        /// Инициализация спецификации комментария.
        /// </summary>
        /// <param name="minRating">Минимальный рейтинг.</param>
        /// <param name="maxRating">Максимальный рейтинг.</param>
        public CommentSpecification(int? minRating, int? maxRating)
        {
            Criteria = c =>
                (!minRating.HasValue || c.Rating <= minRating)
                && (!maxRating.HasValue || c.Rating >= maxRating);
        }

        /// <inheritdoc/>
        public Expression<Func<Domain.Comment.Comment, bool>> Criteria { get; }
    }
}

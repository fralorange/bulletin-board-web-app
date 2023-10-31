using BulletinBoard.Application.AppServices.Filtration.Base.Specification;
using System.Linq.Expressions;

namespace BulletinBoard.Application.AppServices.Filtration.Ad.Specification
{
    /// <summary>
    /// Спецификация объявления.
    /// </summary>
    public class AdSpecification : ISpecification<Domain.Ad.Ad>
    {
        /// <summary>
        /// Инициализация спецификации объявления.
        /// </summary>
        /// <param name="title">Название.</param>
        /// <param name="minPrice">Минимальная цена.</param>
        /// <param name="maxPrice">Максимальная цена.</param>
        public AdSpecification(string? title, decimal? minPrice, decimal? maxPrice)
        {
            Criteria = a =>
                (string.IsNullOrEmpty(title) || a.Title.Contains(title))
                && (!minPrice.HasValue || a.Price >= minPrice.Value)
                && (!maxPrice.HasValue || a.Price <= maxPrice.Value);
        }

        /// <inheritdoc/>
        public Expression<Func<Domain.Ad.Ad, bool>> Criteria { get; }
    }
}

using System.Linq.Expressions;

namespace BulletinBoard.Application.AppServices.Filtration.Base.Specification
{
    /// <summary>
    /// Базовая спецификация.
    /// </summary>
    /// <typeparam name="T">Сущность к которой применяется спецификация.</typeparam>
    public interface ISpecification<T>
    {
        /// <summary>
        /// Критерий спецификации основывающийся на сущности.
        /// </summary>
        Expression<Func<T, bool>> Criteria { get; }
    }
}

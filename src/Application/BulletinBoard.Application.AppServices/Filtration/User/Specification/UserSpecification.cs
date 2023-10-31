using BulletinBoard.Application.AppServices.Filtration.Base.Specification;
using System.Linq.Expressions;

namespace BulletinBoard.Application.AppServices.Filtration.User.Specification
{
    /// <summary>
    /// Спецификация пользователя.
    /// </summary>
    public class UserSpecification : ISpecification<Domain.User.User>
    {
        /// <summary>
        /// Инициализация спецификации пользователя.
        /// </summary>
        /// <param name="name">Имя.</param>
        /// <param name="role">Роль.</param>
        public UserSpecification(string? name, string? role)
        {
            Criteria = u => 
                (string.IsNullOrEmpty(name) || u.Name.Contains(name))
                && (string.IsNullOrEmpty(role) || u.Role.Contains(role));
        }

        /// <inheritdoc/>
        public Expression<Func<Domain.User.User, bool>> Criteria { get; }
    }
}

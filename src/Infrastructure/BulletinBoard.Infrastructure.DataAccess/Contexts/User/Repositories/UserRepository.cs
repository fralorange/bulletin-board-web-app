using BulletinBoard.Application.AppServices.Contexts.User.Repositories;
using BulletinBoard.Contracts.User;
using BulletinBoard.Infrastructure.Repository;
using System.Linq.Expressions;
using UserEntity = BulletinBoard.Domain.User.User;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.User.Repositories
{
    /// <inheritdoc cref="IUserRepository"/>
    public class UserRepository : IUserRepository
    {
        private readonly IRepository<UserEntity> _repository;

        /// <summary>
        /// Инициализирует репозиторий пользователей.
        /// </summary>
        /// <param name="repository">Базовый репозиторий.</param>
        public UserRepository(IRepository<UserEntity> repository)
        {
            _repository = repository;
        }

        /// <inheritdoc/>
        public Task<IReadOnlyCollection<UserDto>> GetAllAsync(CancellationToken cancellationToken, int limit = 10)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<UserEntity?> GetByPredicate(Expression<Func<UserEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return Task.Run(() => _repository.GetAllFiltered(predicate).FirstOrDefault(), cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Guid> CreateAsync(UserEntity user, CancellationToken cancellationToken)
        {
            _repository.AddAsync(user, cancellationToken);
            return Task.Run(() => user.Id);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(Guid id, UserEntity user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

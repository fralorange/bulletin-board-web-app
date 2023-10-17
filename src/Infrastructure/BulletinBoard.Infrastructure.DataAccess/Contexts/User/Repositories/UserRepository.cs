using AutoMapper;
using BulletinBoard.Application.AppServices.Contexts.User.Repositories;
using BulletinBoard.Contracts.Ad;
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
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует репозиторий пользователей.
        /// </summary>
        /// <param name="repository">Базовый репозиторий.</param>
        /// <param name="mapper">Маппер.</param>
        public UserRepository(IRepository<UserEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public Task<IReadOnlyCollection<UserDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var userCollection = _repository.GetAll();
            var dtoCollection = _mapper.Map<List<UserDto>>(userCollection.ToList());
            IReadOnlyCollection<UserDto> readonlyCollection = dtoCollection.AsReadOnly();

            return Task.Run(() => readonlyCollection, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _repository.GetByIdAsync(id).ContinueWith(t =>
            {
                var user = t.Result;
                return _mapper.Map<UserDto?>(user);
            }, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<InfoUserDto> GetCurrentUser(Guid id, CancellationToken cancellationToken)
        {
            return _repository.GetByIdAsync(id).ContinueWith(t =>
            {
                var user = t.Result;
                return _mapper.Map<InfoUserDto>(user);
            }, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<UserEntity?> GetByPredicate(Expression<Func<UserEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return Task.Run(() => (_repository.GetAllFiltered(predicate).FirstOrDefault()), cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Guid> CreateAsync(UserEntity user, CancellationToken cancellationToken)
        {
            _repository.AddAsync(user, cancellationToken);
            return Task.FromResult(user.Id);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(Guid id, UserEntity user, CancellationToken cancellationToken)
        {
            return _repository.UpdateAsync(user, cancellationToken);
        }

        /// <inheritdoc/>
        public Task DeleteAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return _repository.DeleteAsync(user, cancellationToken);
        }
    }
}

using BulletinBoard.Application.AppServices.Contexts.User.Repositories;
using BulletinBoard.Contracts.User;

namespace BulletinBoard.Application.AppServices.Contexts.User.Services
{
    /// <inheritdoc cref="IUserService"/>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Инициализирует экземпляр <see cref="UserService"/>
        /// </summary>
        /// <param name="userRepository">Репозиторий.</param>
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
        public Task<Guid> CreateAsync(CreateUserDto dto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task UpdateAsync(Guid id, UpdateUserDto dto, CancellationToken cancellationToken)
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

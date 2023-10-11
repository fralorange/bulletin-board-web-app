using AutoMapper;
using BulletinBoard.Application.AppServices.Contexts.User.Repositories;
using BulletinBoard.Application.AppServices.Cryptography.Helpers;
using BulletinBoard.Contracts.User;
using BulletinBoard.Domain.Ad;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BulletinBoard.Application.AppServices.Contexts.User.Services
{
    /// <inheritdoc cref="IUserService"/>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует экземпляр <see cref="UserService"/>
        /// </summary>
        /// <param name="userRepository">Репозиторий.</param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="mapper">Маппер.</param>
        public UserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public Task<IReadOnlyCollection<UserDto>> GetAllAsync(CancellationToken cancellationToken, int limit)
        {
            return _userRepository.GetAllAsync(cancellationToken, limit);
        }

        /// <inheritdoc/>
        public Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _userRepository.GetByIdAsync(id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<InfoUserDto> GetCurrentUser(CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            return _userRepository.GetCurrentUser(userId, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<bool> UpdateAsync(Guid id, UpdateUserDto dto, CancellationToken cancellationToken)
        {
            var (Salt, Password) = PasswordHashHelper.HashPassword(dto.Password);
            var user = _mapper.Map<Domain.User.User>(dto);

            user.Salt = Salt;
            user.HashedPassword = Password;
            user.Role = GetByIdAsync(id, cancellationToken).Result!.Role;

            return _userRepository.UpdateAsync(id, user, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            return _userRepository.DeleteAsync(id, cancellationToken);
        }
    }
}

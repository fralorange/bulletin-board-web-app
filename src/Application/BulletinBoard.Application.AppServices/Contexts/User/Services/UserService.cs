using AutoMapper;
using BulletinBoard.Application.AppServices.Authentication.Constants;
using BulletinBoard.Application.AppServices.Authentication.Services;
using BulletinBoard.Application.AppServices.Contexts.User.Repositories;
using BulletinBoard.Application.AppServices.Cryptography.Helpers;
using BulletinBoard.Application.AppServices.Exceptions;
using BulletinBoard.Application.AppServices.Pagination.Helpers;
using BulletinBoard.Contracts.User;
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
        private readonly IEntityAuthorizationService _entityAuthorizationService;

        /// <summary>
        /// Инициализирует экземпляр <see cref="UserService"/>
        /// </summary>
        /// <param name="userRepository">Репозиторий.</param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="mapper">Маппер.</param>
        /// <param name="entityAuthorizationService"></param>
        public UserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper, IEntityAuthorizationService entityAuthorizationService)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _entityAuthorizationService = entityAuthorizationService;
        }

        /// <inheritdoc/>
        public Task<IReadOnlyCollection<UserDto>> GetAllAsync(int pageSize, int pageIndex, CancellationToken cancellationToken)
        {
            var modelCollection = _userRepository.GetAllAsync(cancellationToken).Result;
            var paginatedCollection = PaginationHelper<UserDto>.SplitByPages(modelCollection, pageSize, pageIndex);

            return Task.FromResult(paginatedCollection);
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
        public Task UpdateAsync(Guid id, UpdateUserDto dto, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetByPredicate(u => u.Id == id, cancellationToken).Result ?? throw new EntityNotFoundException();

            if (_entityAuthorizationService.ValidateUserOnly(_httpContextAccessor.HttpContext!.User, id, AuthRoles.Admin).Result)
                throw new EntityForbiddenException();

            var (Salt, Password) = PasswordHashHelper.HashPassword(dto.Password);

            user.Login = dto.Login;
            user.Name = dto.Name;
            user.Salt = Salt;
            user.HashedPassword = Password;
            user.Role = GetByIdAsync(id, cancellationToken).Result!.Role;

            return _userRepository.UpdateAsync(id, user, cancellationToken);
        }

        /// <inheritdoc/>
        public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetByPredicate(u => u.Id == id, cancellationToken).Result ?? throw new EntityNotFoundException();

            if (_entityAuthorizationService.ValidateUserOnly(_httpContextAccessor.HttpContext!.User, id, AuthRoles.Admin).Result)
                throw new EntityForbiddenException();

            return _userRepository.DeleteAsync(user, cancellationToken);
        }
    }
}

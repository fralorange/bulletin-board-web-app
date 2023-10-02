using AutoMapper;
using BulletinBoard.Application.AppServices.Authentication.Exceptions;
using BulletinBoard.Application.AppServices.Contexts.User.Repositories;
using BulletinBoard.Application.AppServices.Cryptography.Helpers;
using BulletinBoard.Contracts.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BulletinBoard.Application.AppServices.Authentication.Services
{
    /// <inheritdoc cref="IAuthService"/>
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;


        /// <summary>
        /// Инициализирует <see cref="AuthService"/>.
        /// </summary>
        /// <param name="userRepository">Репозиторий пользователей.</param>
        /// <param name="configuration">Конфигурация.</param>
        /// <param name="mapper">Маппер.</param>
        public AuthService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
        }


        /// <inheritdoc/>
        public async Task<string> Login(LoginUserDto dto, CancellationToken cancellationToken) 
        {
            var user = await _userRepository.GetByPredicate(elem => elem.Login == dto.Login, cancellationToken) ?? throw new LoginNotFoundException();

            var hashedPassword = PasswordHashHelper.HashPassword(dto.Password, user.Salt);
            if (hashedPassword != user.HashedPassword) throw new InvalidPasswordException();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken
            (
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn
            );

            var result = new JwtSecurityTokenHandler().WriteToken(token);

            return result;            
        }

        /// <inheritdoc/>
        public async Task<Guid> Register(CreateUserDto dto, CancellationToken cancellationToken)
        {
            if ((await _userRepository.GetByPredicate(elem => elem.Login == dto.Login, cancellationToken)) is not null)
            {
                throw new LoginAlreadyExistsException();
            }

            var user = _mapper.Map<Domain.User.User>(dto);
            var (Salt, Hash) = PasswordHashHelper.HashPassword(dto.Password);
            user.HashedPassword = Hash;
            user.Salt = Salt;
            user.Role = "Default";

            var result = await _userRepository.CreateAsync(user, cancellationToken);
            return result;
        }
    }
}

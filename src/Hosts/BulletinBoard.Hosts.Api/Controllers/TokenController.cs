using BulletinBoard.Contracts.User;
using BulletinBoard.Hosts.Api.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BulletinBoard.Hosts.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с токеном.
    /// </summary>
    [Obsolete("TokenController is obsolete. Use AuthController instead!")]
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Инициализация конфигурации.
        /// </summary>
        /// <param name="configuration">Конфигурация.</param>
        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Метод получения JWT.
        /// </summary>
        /// <param name="dto">Модель аутентификации.</param>
        /// <returns>Модель с данными для входа.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(LoginUserDto), StatusCodes.Status200OK)]
        public ActionResult Post(LoginUserDto dto)
        {
            // Check user credentials

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, dto.Login),
                new Claim("User", "User")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken
            (
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn
            );

            return Ok(new TokenDto { Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}

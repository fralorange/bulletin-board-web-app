using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;

namespace BulletinBoard.Application.AppServices.Authentication.Handlers
{
    /// <inheritdoc cref="AuthenticationHandler{TOptions}"/>
    public class JwtSchemeHandler : AuthenticationHandler<JwtSchemeOptions>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Обработчик аутентификации с помощью JWT (проверяем наличие токена и его валидность).
        /// Если токен валидный, то аутентифицируем пользователя.
        /// </summary>
        public JwtSchemeHandler(
            IOptionsMonitor<JwtSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration) : base(options, logger, encoder, clock)
        {
            _contextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        /// <inheritdoc/>
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Получение токена из заголовка (Cookies) запроса.

            bool tokenPresented = _contextAccessor.HttpContext
                .Request.Cookies.TryGetValue("token", out var token);

            // Проверяем наличие токена, если токена нет - не можем аутентифицировать пользователя.
            if (!tokenPresented)
            {
                return Task.FromResult(AuthenticateResult.Fail("Fail. Token not found!"));
            }

            // Нужно сформировать токен (пример: https://jwt.io/)
            #region Проверка токена
            // Проверка токена (пример: https://stackoverflow.com/questions/38725038/c-sharp-how-to-verify-signature-on-jwt-token)

            var handler = new JwtSecurityTokenHandler();

            var parts = token!.Split(".".ToCharArray());

            // Разбираем токен на части

            var header = parts[0];
            var payload = parts[1];
            var signature = parts[2];

            var bytesToSign = Encoding.UTF8.GetBytes($"{header}.{payload}");
            var secret = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var alg = new HMACSHA256(secret);
            var hash = alg.ComputeHash(bytesToSign);

            // Вычисляем подпись 
            var calculatedSignature = Base64UrlEncode(hash);

            // Сравниваем подпись с подписью из токена (проверяем, что токен был зашифрован с таким же ключём)
            if (!calculatedSignature.Equals(signature))
            {
                return Task.FromResult(AuthenticateResult.Fail("Fail. Invalid Token!"));
            }
            #endregion

            var jwtToken = handler.ReadJwtToken(token);

            // Забираем данные пользователя из токена.

            var claims = jwtToken.Claims;

            // Аутентифицируем пользователя.

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }

        // Можно добавить отдельный Helper вместо приватного метода
        private static string Base64UrlEncode(byte[] bytes)
        {
            var value = Convert.ToBase64String(bytes);

            value = value.Split('=')[0];
            value = value.Replace('+', '-');
            value = value.Replace('/', '_');

            return value;
        }
    }
}

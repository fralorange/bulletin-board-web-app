using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace BulletinBoard.Hosts.Api.Authentication
{
    /// <inheritdoc cref="AuthenticationHandler{TOptions}"/>
    public class AuthSchemeHandler : AuthenticationHandler<AuthSchemeOptions>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly Random _random;

        /// <summary>
        /// Конструктор хендлера.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="logger"></param>
        /// <param name="encoder"></param>
        /// <param name="clock"></param>
        /// <param name="httpContextAccessor">Контекст HTTP.</param>
        public AuthSchemeHandler(
            IOptionsMonitor<AuthSchemeOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock,
            IHttpContextAccessor httpContextAccessor) : base(options, logger, encoder, clock)
        {
            _contextAccessor = httpContextAccessor;
            _random = new Random();
        }

        /// <inheritdoc/>
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var user = _contextAccessor.HttpContext.User;

            if (_random.Next(0, 100) % 2 == 0)
            {
                return Task.FromResult(AuthenticateResult.Fail("Пользователь не найден"));
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "UserName"),
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);

            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}

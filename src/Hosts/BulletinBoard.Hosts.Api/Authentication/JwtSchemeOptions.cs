using Microsoft.AspNetCore.Authentication;

namespace BulletinBoard.Hosts.Api.Authentication
{
    /// <summary>
    /// Настройки JWT-Схемы.
    /// </summary>
    public class JwtSchemeOptions : AuthenticationSchemeOptions
    {
        /// <summary>
        /// Активность токена.
        /// </summary>
        public bool IsActive { get; set; }
    }
}

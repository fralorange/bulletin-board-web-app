namespace BulletinBoard.Hosts.Api.Dto
{
    /// <summary>
    /// Модель аутентификации.
    /// </summary>
    public class AuthDto
    {
        /// <summary>
        /// Логин.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        public string Password { get; set; }
    }
}

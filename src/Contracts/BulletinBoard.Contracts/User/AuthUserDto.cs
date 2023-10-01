namespace BulletinBoard.Hosts.Api.Dto
{
    /// <summary>
    /// Модель аутентификационных данных пользователя.
    /// </summary>
    public class AuthUserDto
    {
        /// <summary>
        /// Проверка на аутентификацию.
        /// </summary>
        public bool IsAuthenticated { get; set; }
        /// <summary>
        /// Схема.
        /// </summary>
        public string? Scheme { get; set; }
        /// <summary>
        /// Заявки.
        /// </summary>
        public List<object> Claims { get; set; } = new List<object>();
    }
}

namespace BulletinBoard.Domain.User
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    public class User : Base.BaseEntity
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Логин пользователя.
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Пароль пользователя в хешированном виде.
        /// </summary>
        public string PasswordHash { get; set; }
        /// <summary>
        /// Роль пользователя в системе.
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// Объявления пользователя.
        /// </summary>
        public virtual IEnumerable<Ad.Ad>? Adverts { get; set; }
    }
}

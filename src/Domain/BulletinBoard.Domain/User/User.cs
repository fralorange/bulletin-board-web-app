namespace BulletinBoard.Domain.User
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    public class User : Base.BaseEntity
    {
        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Логин.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя в хешированном виде.
        /// </summary>
        public string HashedPassword { get; set; }

        /// <summary>
        /// Соль.
        /// </summary>
        public string Salt { get; set; }

        /// <summary>
        /// Роль пользователя в системе.
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Объявления.
        /// </summary>
        public virtual IEnumerable<Ad.Ad>? Adverts { get; set; }
    }
}

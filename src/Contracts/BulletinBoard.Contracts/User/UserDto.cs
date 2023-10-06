namespace BulletinBoard.Contracts.User
{
    /// <summary>
    /// Модель пользователя.
    /// </summary>
    public class UserDto : Base.BaseDto
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Роль пользователя.
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Объявления пользователя.
        /// </summary>
        
        public IEnumerable<Ad.InfoAdDto>? Adverts { get; set; }
    }
}

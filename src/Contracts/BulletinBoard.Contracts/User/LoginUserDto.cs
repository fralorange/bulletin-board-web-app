using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Contracts.User
{
    /// <summary>
    /// Модель для аутентификации пользователя.
    /// </summary>
    public class LoginUserDto
    {
        /// <summary>
        /// Логин.
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Login { get; init; }

        /// <summary>
        /// Пароль.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; init; }
    }
}

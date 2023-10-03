using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Contracts.User
{
    /// <summary>
    /// Модель для обновления пользователя.
    /// </summary>
    public class UpdateUserDto
    {
        /// <summary>
        /// Имя.
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Name { get; init; }

        /// <summary>
        /// Пароль.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; init; }
    }
}

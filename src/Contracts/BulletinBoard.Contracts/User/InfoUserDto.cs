using BulletinBoard.Contracts.Base;

namespace BulletinBoard.Contracts.User
{
    /// <summary>
    /// Информационная модель User.
    /// </summary>
    public class InfoUserDto : BaseDto
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Роль пользователя.
        /// </summary>
        public string Role { get; set; }
    }
}

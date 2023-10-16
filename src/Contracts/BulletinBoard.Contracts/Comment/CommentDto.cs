using BulletinBoard.Contracts.Ad;
using BulletinBoard.Contracts.Base;
using BulletinBoard.Contracts.User;

namespace BulletinBoard.Contracts.Comment
{
    /// <summary>
    /// Модель комментария.
    /// </summary>
    public class CommentDto : BaseDto
    {
        /// <summary>
        /// Содержимое
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Объявление.
        /// </summary>
        public virtual InfoAdDto Ad { get; set; }

        /// <summary>
        /// Пользователь.
        /// </summary>
        public virtual InfoUserDto User { get; set; }

        /// <summary>
        /// Оценка.
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// Дата публикации.
        /// </summary>
        public DateTime PublishedAt { get; set; }
    }
}

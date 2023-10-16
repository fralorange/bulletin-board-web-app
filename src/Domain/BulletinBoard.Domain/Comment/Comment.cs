using BulletinBoard.Domain.Base;

namespace BulletinBoard.Domain.Comment
{
    /// <summary>
    /// Комментарий.
    /// </summary>
    public class Comment : BaseEntity
    {
        /// <summary>
        /// Содержимое
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Объявление.
        /// </summary>
        public virtual Ad.Ad Ad { get; set; }

        /// <summary>
        /// Идентификатор объявления.
        /// </summary>
        public Guid AdId { get; set; }

        /// <summary>
        /// Пользователь.
        /// </summary>
        public virtual User.User User { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; set; }

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

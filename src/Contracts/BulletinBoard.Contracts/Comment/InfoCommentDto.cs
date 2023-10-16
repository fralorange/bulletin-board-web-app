using BulletinBoard.Contracts.Base;

namespace BulletinBoard.Contracts.Comment
{
    /// <summary>
    /// Информационная модель Comment.
    /// </summary>
    public class InfoCommentDto : BaseDto
    {
        /// <summary>
        /// Содержимое
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Оценка.
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// Идентификатор объявления.
        /// </summary>
        public Guid AdId { get; set; }

        /// <summary>
        /// Дата публикации.
        /// </summary>
        public DateTime PublishedAt { get; set; }
    }
}

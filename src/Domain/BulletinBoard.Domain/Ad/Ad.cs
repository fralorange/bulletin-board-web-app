using BulletinBoard.Domain.Base;

namespace BulletinBoard.Domain.Ad
{
    /// <summary>
    /// Объявление.
    /// </summary>
    public class Ad : BaseEntity
    {
        /// <summary>
        /// Заголовок.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Изображения.
        /// </summary>
        public virtual ICollection<Attachment.Attachment> Attachments { get; set; }

        /// <summary>
        /// Комментарии.
        /// </summary>
        public virtual ICollection<Comment.Comment> Comments { get; set; }

        /// <summary>
        /// Цена.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Категория.
        /// </summary>
        public virtual Category.Category Category { get; set; }

        /// <summary>
        /// Идентификатор категории.
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Пользователь.
        /// </summary>
        public virtual User.User User { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; set; }
    }
}

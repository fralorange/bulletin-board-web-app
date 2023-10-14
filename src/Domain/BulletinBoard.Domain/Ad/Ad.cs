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
        public required string Title { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        public required string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //public required Guid CategoryId { get; init; }

        /// <summary>
        /// Категория.
        /// </summary>
        //public Category.Category Category { get; init; }

        /// <summary>
        /// Изображения.
        /// </summary>
        public virtual required ICollection<Attachment.Attachment> Attachments { get; set; }

        /// <summary>
        /// Цена.
        /// </summary>
        public required decimal Price { get; set; }

        /// <summary>
        /// Пользователь.
        /// </summary>
        public virtual required User.User User { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public required Guid UserId { get; set; }
    }
}

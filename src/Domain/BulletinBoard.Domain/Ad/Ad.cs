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
        public required string Title { get; init; }

        /// <summary>
        /// Описание.
        /// </summary>
        public required string Description { get; init; }

        /// <summary>
        /// 
        /// </summary>
        public required Guid CategoryId { get; init; }

        /// <summary>
        /// Категория.
        /// </summary>
        public Category.Category Category { get; init; }

        /// <summary>
        /// Изображения.
        /// </summary>
        public required IReadOnlyCollection<Attachment.Attachment> Attachments { get; init; }

        /// <summary>
        /// Цена.
        /// </summary>
        public required decimal Price { get; init; }
    }
}

using BulletinBoard.Domain.Ad;

namespace BulletinBoard.Domain.Base
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
        /// Название категории.
        /// </summary>
        public required Guid CategoryId { get; init; }

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

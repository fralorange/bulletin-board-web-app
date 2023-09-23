using BulletinBoard.Contracts.Attachment;
using BulletinBoard.Contracts.Base;

namespace BulletinBoard.Contracts.Ad
{
    /// <summary>
    /// Объявление.
    /// </summary>
    public class AdDto : BaseDto
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
        //public required string CategoryName { get; init; }

        /// <summary>
        /// Изображения.
        /// </summary>
        //public required IReadOnlyCollection<AttachmentDto> Attachments { get; init; }

        /// <summary>
        /// Цена.
        /// </summary>
        public required decimal Price { get; init; }
    }
}

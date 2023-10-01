using BulletinBoard.Contracts.Attachment;
using BulletinBoard.Contracts.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Contracts.Ad
{
    /// <summary>
    /// Создание объявления.
    /// </summary>
    public class CreateAdDto
    {
        /// <summary>
        /// Заголовок.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(50)]
        public string Title { get; init; }

        /// <summary>
        /// Описание.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(150)]
        public string Description { get; init; }

        /// <summary>
        /// Идентификатор категории.
        /// </summary>
        //public required Guid CategoryId { get; init; }

        /// <summary>
        /// Изображения.
        /// </summary>
        //[Limit(1, 5)]
        //public required IReadOnlyCollection<AttachmentDto> Attachments { get; init; }

        /// <summary>
        /// Цена.
        /// </summary>
        [Range(0, long.MaxValue)]
        public required decimal Price { get; init; }
    }
}

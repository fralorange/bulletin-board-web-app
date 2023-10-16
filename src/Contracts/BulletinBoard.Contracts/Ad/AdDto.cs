using BulletinBoard.Contracts.Attachment;
using BulletinBoard.Contracts.Base;
using BulletinBoard.Contracts.Comment;
using BulletinBoard.Contracts.User;
using System.Text.Json.Serialization;

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
        public string Title { get; init; }

        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; init; }

        /// <summary>
        /// Изображения.
        /// </summary>
        public IReadOnlyCollection<AttachmentDto> Attachments { get; set; }

        /// <summary>
        /// Комментарии.
        /// </summary>
        public IReadOnlyCollection<CommentDto> Comments { get; set; }

        /// <summary>
        /// Цена.
        /// </summary>
        public decimal Price { get; init; }

        /// <summary>
        /// Пользователь.
        /// </summary>
        public UserDto User { get; init; }
    }
}

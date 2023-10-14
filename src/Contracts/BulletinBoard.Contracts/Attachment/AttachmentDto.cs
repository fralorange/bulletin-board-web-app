using BulletinBoard.Contracts.Ad;
using BulletinBoard.Contracts.Base;

namespace BulletinBoard.Contracts.Attachment
{
    /// <summary>
    /// Вложение.
    /// </summary>
    public class AttachmentDto : BaseDto
    {
        /// <summary>
        /// Содержимое вложения в виде массива байтов.
        /// </summary>
        public required byte[] Content { get; set; }

        /// <summary>
        /// Объявление.
        /// </summary>
        public required InfoAdDto Ad { get; set; }
    }
}

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
        public required byte[] Content { get; init; }

        /// <summary>
        /// Идентификатор объявления.
        /// </summary>
        public required Guid AdId { get; init; }
    }
}

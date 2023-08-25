using BulletinBoard.Domain.Ad;

namespace BulletinBoard.Domain.Attachment
{
    /// <summary>
    /// Вложение.
    /// </summary>
    public class Attachment : BaseEntity
    {
        /// <summary>
        /// Содержимое вложения в виде массива байтов.
        /// </summary>
        public required byte[] Content { get; init; }
    }
}

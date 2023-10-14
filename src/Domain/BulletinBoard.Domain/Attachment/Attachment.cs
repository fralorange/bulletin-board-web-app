using BulletinBoard.Domain.Base;

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
        public required byte[] Content { get; set; }

        /// <summary>
        /// Идентификатор объявления.
        /// </summary>
        public required Guid AdId { get; set; }

        /// <summary>
        /// Навигационное свойство <see cref="Ad.Ad"/>.
        /// </summary>
        public virtual required Ad.Ad Ad { get; set; }
    }
}

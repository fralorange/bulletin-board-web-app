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
        public byte[] Content { get; set; }

        /// <summary>
        /// Идентификатор объявления.
        /// </summary>
        public Guid AdId { get; set; }

        /// <summary>
        /// Навигационное свойство <see cref="Ad.Ad"/>.
        /// </summary>
        public virtual Ad.Ad Ad { get; set; }
    }
}

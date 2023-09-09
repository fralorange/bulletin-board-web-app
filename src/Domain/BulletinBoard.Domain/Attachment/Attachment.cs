﻿using BulletinBoard.Domain.Base;

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

        /// <summary>
        /// Идентификатор объявления.
        /// </summary>
        public required Guid AdId { get; init; }
    }
}

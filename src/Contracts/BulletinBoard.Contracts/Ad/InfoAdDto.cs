using BulletinBoard.Contracts.Base;

namespace BulletinBoard.Contracts.Ad
{
    /// <summary>
    /// Информационная модель Ad.
    /// </summary>
    public class InfoAdDto : BaseDto
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
        /// Цена.
        /// </summary>
        public required decimal Price { get; init; }
    }
}

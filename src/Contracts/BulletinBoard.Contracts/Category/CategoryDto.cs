using BulletinBoard.Contracts.Ad;
using BulletinBoard.Contracts.Base;

namespace BulletinBoard.Contracts.Category
{
    /// <summary>
    /// Модель пользователя.
    /// </summary>
    public class CategoryDto : BaseDto
    {
        /// <summary>
        /// Название категории.
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Идентификатор родительской категории.
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// Дочерние категории.
        /// </summary>
        public IEnumerable<InfoCategoryDto>? Children { get; set; }

        /// <summary>
        /// Объявления принадлежащие категории.
        /// </summary>
        public IEnumerable<InfoAdDto>? Adverts { get; set; }
    }
}

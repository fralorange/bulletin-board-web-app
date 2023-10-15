using BulletinBoard.Contracts.Base;

namespace BulletinBoard.Contracts.Category
{
    /// <summary>
    /// Информационная модель Category.
    /// </summary>
    public class InfoCategoryDto : BaseDto
    {
        /// <summary>
        /// Название категории.
        /// </summary>
        public string CategoryName { get; init; }

        /// <summary>
        /// Идентификатор родительской категории.
        /// </summary>
        public Guid? ParentId { get; init; }
    }
}

using BulletinBoard.Domain.Base;

namespace BulletinBoard.Domain.Category
{
    /// <summary>
    /// Категория объявления.
    /// </summary>
    public class Category : BaseEntity
    {
        /// <summary>
        /// Название категории.
        /// </summary>
        public required string CategoryName { get; init; }

        /// <summary>
        /// Идентификатор родительской категории.
        /// </summary>
        public required Guid? ParentCategoryId { get; init; }

        /// <summary>
        /// Массив дочерних идентификаторов категории.
        /// </summary>
        public required Guid[] ChildCategoryIds { get; init; }
    }
}

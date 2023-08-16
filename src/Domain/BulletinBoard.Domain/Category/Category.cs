namespace BulletinBoard.Domain.Category
{
    /// <summary>
    /// Категория
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Уникальный номер категории
        /// </summary>
        public required Guid Id { get; init; }
        /// <summary>
        /// Уникальный номер родительской категории
        /// </summary>
        public required Guid? ParentId { get; init; }
        /// <summary>
        /// Родительская категория
        /// </summary>
        public required Category? Parent { get; init; }
        /// <summary>
        /// Название категории
        /// </summary>
        public required string Name { get; init; }
        /// <summary>
        /// Дочерние категории
        /// </summary>
        public required IEnumerable<Category>? Children { get; init; }
    }
}

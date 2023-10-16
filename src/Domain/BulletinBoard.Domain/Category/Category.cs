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
        public string CategoryName { get; set; }

        /// <summary>
        /// Родительская категория.
        /// </summary>
        public virtual Category? Parent { get; set; }

        /// <summary>
        /// Идентификатор родительской категории.
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// Дочерние категории.
        /// </summary>
        public virtual IEnumerable<Category>? Children { get; set; }

        /// <summary>
        /// Объявления принадлежащие категории.
        /// </summary>
        public virtual IEnumerable<Ad.Ad>? Adverts { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Contracts.Category
{
    /// <summary>
    /// Модель для создания категории.
    /// </summary>
    public class CreateCategoryDto
    {
        /// <summary>
        /// Название категории.
        /// </summary>
        [Required]
        [StringLength(30, MinimumLength = 4)]
        public string CategoryName { get; set; }

        /// <summary>
        /// Идентификатор родительской категории.
        /// </summary>
        public Guid? ParentId { get; set; }
    }
}

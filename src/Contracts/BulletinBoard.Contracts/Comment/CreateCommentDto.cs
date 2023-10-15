using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Contracts.Comment
{
    /// <summary>
    /// Создание комментария.
    /// </summary>
    public class CreateCommentDto
    {
        /// <summary>
        /// Содержимое
        /// </summary>
        [Required]
        [StringLength(maximumLength: 1000)]
        public string Content { get; set; }

        /// <summary>
        /// Идентификатор объявления.
        /// </summary>
        [Required]
        public Guid AdId { get; set; }

        /// <summary>
        /// Оценка.
        /// </summary>
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
    }
}

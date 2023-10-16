using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Contracts.Comment
{
    /// <summary>
    /// Обновление комментария.
    /// </summary>
    public class UpdateCommentDto
    {
        /// <summary>
        /// Содержимое
        /// </summary>
        [Required]
        [StringLength(maximumLength: 1000)]
        public string Content { get; set; }

        /// <summary>
        /// Оценка.
        /// </summary>
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
    }
}

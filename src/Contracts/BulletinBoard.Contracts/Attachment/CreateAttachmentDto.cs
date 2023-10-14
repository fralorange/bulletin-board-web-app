using BulletinBoard.Contracts.ValidationAttributes;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Contracts.Attachment
{
    /// <summary>
    /// Создание изображения.
    /// </summary>
    public class CreateAttachmentDto
    {
        /// <summary>
        /// Содержимое вложения в виде массива байтов.
        /// </summary>
        [Display(Name = "Изображение")]
        [Required(ErrorMessage = "Выберите изображение!")]
        [AllowedExtensions(".jpg", ".jpeg", ".png")]
        public IFormFile File { get; set; }

        /// <summary>
        /// Идентификатор объявления.
        /// </summary>
        [Required]
        public Guid AdId { get; set; }
    }
}

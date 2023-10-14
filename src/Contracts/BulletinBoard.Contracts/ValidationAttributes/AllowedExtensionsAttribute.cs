using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Contracts.ValidationAttributes
{
    /// <summary>
    /// Определяет разрешенные расширения файлов.
    /// </summary>
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly HashSet<string> _allowedExtensions;

        /// <summary>
        /// Инициализирует атрибут <see cref="AllowedExtensionsAttribute"/>.
        /// </summary>
        /// <param name="allowedExtensions">Разрешённые расширения файлов.</param>
        public AllowedExtensionsAttribute(params string[] allowedExtensions)
        {
            _allowedExtensions = new HashSet<string>(allowedExtensions, StringComparer.OrdinalIgnoreCase);
        }

        /// <inheritdoc/>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!_allowedExtensions.Contains(extension))
                    return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage() => "Тип файла вашего изображения не является правильным";
    }
}

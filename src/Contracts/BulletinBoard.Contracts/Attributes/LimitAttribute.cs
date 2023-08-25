using BulletinBoard.Contracts.Attachment;
using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Contracts.Attributes
{
    /// <summary>
    /// Определяет минимальный и максимальный размер коллекции.
    /// </summary>
    public class LimitAttribute : ValidationAttribute
    {
        private readonly int _minSize;
        private readonly int _maxSize;

        /// <summary>
        /// Инициализирует размеры коллекции.
        /// </summary>
        /// <param name="minSize">Минимальная длина коллекции.</param>
        /// <param name="maxSize">Максимальная длина коллекции.</param>
        public LimitAttribute(int minSize, int maxSize)
        {
            _minSize = minSize;
            _maxSize = maxSize;
        }

        /// <summary>
        /// Валидирует длину коллекции по минимальному и максимальному полю.
        /// </summary>
        /// <param name="value">Поле для валидации.</param>
        /// <param name="validationContext">Контекст валидации.</param>
        /// <returns>Результат валидации.</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var collection = value as IReadOnlyCollection<AttachmentDto>;
            if (_minSize >= _maxSize)
                return new ValidationResult("Минимальный размер коллекции должен быть меньше максимального");
            if (collection!.Count > _maxSize || collection.Count < _minSize)
                return new ValidationResult($"Размер коллекции должен быть больше {_minSize} и меньше {_maxSize}");

            return ValidationResult.Success;
        }
    }
}

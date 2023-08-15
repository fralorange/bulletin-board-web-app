using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Domain.Attributes.MaxElementsAttribute
{
    public class MaxElementsAttribute : ValidationAttribute
    {
        private readonly int _maxElements;
        public MaxElementsAttribute(int maxElements) => _maxElements = maxElements;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IEnumerable collection)
            {
                int count = 0;
                foreach (var item in collection)
                {
                    count++;
                    if (count > _maxElements) return new ValidationResult($"Количество элементов не может превышать {_maxElements}");
                }
            }

            return ValidationResult.Success;
        }
    }
}

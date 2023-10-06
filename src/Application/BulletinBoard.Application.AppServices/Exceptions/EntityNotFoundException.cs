namespace BulletinBoard.Application.AppServices.Exceptions
{
    /// <summary>
    /// Исключение выбрасываемое при неудачном поиске модели.
    /// </summary>
    public class EntityNotFoundException : Exception
    {
        /// <summary>
        /// Сообщение по-умолчанию.
        /// </summary>
        public EntityNotFoundException()
            : base("Сущность не найдена!")
        {
        }

        /// <summary>
        /// Кастомное сообщение.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        public EntityNotFoundException(string message)
            : base(message)
        {
        }
    }
}

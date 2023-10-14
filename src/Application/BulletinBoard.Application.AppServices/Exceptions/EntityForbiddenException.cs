namespace BulletinBoard.Application.AppServices.Exceptions
{
    /// <summary>
    /// Исключение выбрасываемое при неудачной авторизации сущности.
    /// </summary>
    public class EntityForbiddenException : Exception
    {
        /// <summary>
        /// Сообщение по-умолчанию.
        /// </summary>
        public EntityForbiddenException()
            : base("У пользователя нет прав редактировать данную сущность.")
        {
        }

        /// <summary>
        /// Кастомное сообщение.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        public EntityForbiddenException(string message)
            : base(message)
        {
        }
    }
}

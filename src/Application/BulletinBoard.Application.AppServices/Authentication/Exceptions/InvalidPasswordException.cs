namespace BulletinBoard.Application.AppServices.Authentication.Exceptions
{
    /// <summary>
    /// Исключение свидетельствующее о неправильности пароля.
    /// </summary>
    public class InvalidPasswordException : Exception
    {
        /// <summary>
        /// Сообщение по-умолчанию.
        /// </summary>
        public InvalidPasswordException()
            : base("Неправильный пароль!")
        { 
        }

        /// <summary>
        /// Кастомное сообщение.
        /// </summary>
        /// <param name="message"></param>
        public InvalidPasswordException(string message)
            :base(message) 
        { 
        }
    }
}

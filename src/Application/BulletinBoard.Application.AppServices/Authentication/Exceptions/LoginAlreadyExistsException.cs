namespace BulletinBoard.Application.AppServices.Authentication.Exceptions
{
    /// <summary>
    /// Исключение свидетельствующее о существовании активного логина.
    /// </summary>
    public class LoginAlreadyExistsException : Exception
    {
        /// <summary>
        /// Сообщение по умолчанию.
        /// </summary>
        public LoginAlreadyExistsException()
            : base("Пользователь с данным логином уже существует.")
        {
        }

        /// <summary>
        /// Кастомное сообщение.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        public LoginAlreadyExistsException(string message) 
            : base(message) 
        { 
        }
    }
}

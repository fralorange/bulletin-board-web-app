namespace BulletinBoard.Application.AppServices.Authentication.Exceptions
{
    /// <summary>
    /// Исключение свидетельствующее о том, что пользователь с данным логином не найден.
    /// </summary>
    public class LoginNotFoundException : Exception
    {
        /// <summary>
        /// Сообщение по-умолчанию.
        /// </summary>
        public LoginNotFoundException()
            : base("Пользователь с данным логином не найден!")
        {
        }

        /// <summary>
        /// Кастомное сообщение.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        public LoginNotFoundException(string message) 
            : base(message)
        {
        }
    }
}

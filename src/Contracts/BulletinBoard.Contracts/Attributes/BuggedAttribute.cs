namespace BulletinBoard.Contracts.Attributes
{
    /// <summary>
    /// Атрибут демонстрирующий, что метод забагован.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class BuggedAttribute : Attribute
    {
        /// <summary>
        /// Сообщение.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Конструктор по-умолчанию.
        /// </summary>
        public BuggedAttribute() => Message = string.Empty;

        /// <summary>
        /// Кастомный конструктор.
        /// </summary>
        /// <param name="message">Кастомное сообщение.</param>
        public BuggedAttribute(string message) => Message = message;
    }
}

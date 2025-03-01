namespace ViewModel.Implementations.Tests
{
    /// <summary>
    /// Класс аргументов события уведомления.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="EventArgs"/>.
    /// </remarks>
    public class NotificationEventArgs : EventArgs
    {
        /// <summary>
        /// Возвращает описание.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Возвращает заголовок.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Создаёт экземпяр класса <see cref="NotificationEventArgs"/>.
        /// </summary>
        /// <param name="description">Описание.</param>
        /// <param name="title">Заголовок.</param>
        public NotificationEventArgs(string description, string title)
        {
            Description = description;
            Title = title;
        }
    }
}

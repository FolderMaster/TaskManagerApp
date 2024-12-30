namespace ViewModel.Interfaces.AppStates
{
    /// <summary>
    /// Интерфейс менеджера уведомлений.
    /// </summary>
    public interface INotificationManager
    {
        /// <summary>
        /// Отправляет уведомление.
        /// </summary>
        /// <param name="description">Описание.</param>
        /// <param name="title">Заголовок.</param>
        public void SendNotification(string description, string title);
    }
}

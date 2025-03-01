using ViewModel.Interfaces.AppStates;

namespace ViewModel.Implementations.Tests
{
    /// <summary>
    /// Класс-заглушка менеджера уведомлений.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="INotificationManager"/>.
    /// </remarks>
    public class MockNotificationManager : INotificationManager
    {
        /// <summary>
        /// Событие, которое возникает при отправке уведомления.
        /// </summary>
        public event EventHandler<NotificationEventArgs> NotificationSended;

        /// <inheritdoc/>
        public void SendNotification(string description, string title) =>
            NotificationSended?.Invoke(this, new NotificationEventArgs(description, title));
    }
}

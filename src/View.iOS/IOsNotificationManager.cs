using UserNotifications;

using ViewModel.Interfaces.AppStates;

namespace View.iOS
{
    /// <summary>
    /// Класс менеджера уведомлений iOS.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="INotificationManager"/>.
    /// </remarks>
    public class IOsNotificationManager : INotificationManager
    {
        /// <summary>
        /// Индетификатор уведомления.
        /// </summary>
        private int _notificationId;

        /// <inheritdoc/>
        public void SendNotification(string description, string title)
        {
            var notificationContent = new UNMutableNotificationContent
            {
                Title = title,
                Body = description
            };
            var notificationTrigger = UNTimeIntervalNotificationTrigger.CreateTrigger(0, false);
            var notificationRequest = UNNotificationRequest.FromIdentifier
                ((_notificationId++).ToString(), notificationContent, notificationTrigger);

            var center = UNUserNotificationCenter.Current;
            center.RequestAuthorizationAsync(UNAuthorizationOptions.Alert |
                UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound);
        }
    }
}

using UserNotifications;

using ViewModel.Interfaces.AppStates;

namespace View.iOS
{
    public class IOsNotificationManager : INotificationManager
    {
        private int _notificationId;

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

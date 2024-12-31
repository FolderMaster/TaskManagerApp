using Android.App;
using Android.Content;
using Android.OS;

using ViewModel.Interfaces.AppStates;

namespace View.Android
{
    /// <summary>
    /// Класс менеджера уведомлений Android.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="INotificationManager"/>.
    /// </remarks>
    public class AndroidNotificationManager : INotificationManager
    {
        /// <summary>
        /// Индетификатор уведомления.
        /// </summary>
        private int _notificationId;

        /// <summary>
        /// Контекст.
        /// </summary>
        private Context _context;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="AndroidNotificationManager"/> по умолчанию.
        /// </summary>
        public AndroidNotificationManager()
        {
            _context = Application.Context;
        }

        /// <inheritdoc/>
        public void SendNotification(string description, string title)
        {
            var notificationManager = (NotificationManager)_context.
                GetSystemService(Context.NotificationService);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channelId = "default_channel";
                var channelName = "Default Channel";
                var channelDescription = "Default Channel Description";
                var channel = new NotificationChannel(channelId, channelName,
                    NotificationImportance.Default)
                {
                    Description = channelDescription
                };
                notificationManager.CreateNotificationChannel(channel);
            }

            var notification = new Notification();
            var notificationBuilder = new Notification.Builder(_context, "default_channel")
                .SetContentTitle(title)
                .SetContentText(description)
                .SetSmallIcon(Resource.Drawable.Icon)
                .SetAutoCancel(true);

            notificationManager.Notify(_notificationId++, notificationBuilder.Build());
        }
    }
}

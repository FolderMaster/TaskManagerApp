﻿using Android.App;
using Android.Content;
using Android.OS;
using ViewModel;

namespace View.Android
{
    public class AndroidNotificationManager : INotificationManager
    {
        private Context _context;

        public AndroidNotificationManager()
        {
            _context = Application.Context;
        }

        public void SendNotification(string content, string title)
        {
            var notificationManager = (NotificationManager)_context.GetSystemService(Context.NotificationService);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channelId = "default_channel";
                var channelName = "Default Channel";
                var channelDescription = "Default Channel Description";
                var channel = new NotificationChannel(channelId, channelName, NotificationImportance.Default)
                {
                    Description = channelDescription
                };
                notificationManager.CreateNotificationChannel(channel);
            }

            var notificationId = 100;
            var notification = new Notification();
            var notificationBuilder = new Notification.Builder(_context, "default_channel")
                .SetContentTitle(title)
                .SetContentText(content)
                .SetSmallIcon(Resource.Drawable.Icon)
                .SetAutoCancel(true);

            notificationManager.Notify(notificationId, notificationBuilder.Build());
        }
    }
}
using System;
using System.Diagnostics;
using System.IO;

using ViewModel.Interfaces.AppStates;

namespace View.Desktop.Linux
{
    public class LinuxNotificationManager : INotificationManager
    {
        private static string _appName = "TaskManager";

        private static string _iconPath = Path.Combine(AppContext.BaseDirectory, "icon.png");

        public void SendNotification(string description, string title)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "notify-send",
                Arguments = $"-a \"{_appName}\" -i \"{_iconPath}\" \"{title}\" \"{description}\"",
                RedirectStandardOutput = false,
                UseShellExecute = true,
                CreateNoWindow = true
            });
        }
    }
}

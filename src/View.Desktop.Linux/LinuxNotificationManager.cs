using System;
using System.Diagnostics;
using System.IO;

using ViewModel.Interfaces.AppStates;

namespace View.Desktop.Linux
{
    /// <summary>
    /// Класс менеджера уведомлений Linux.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="INotificationManager"/>.
    /// </remarks>
    public class LinuxNotificationManager : INotificationManager
    {
        /// <summary>
        /// Название приложения.
        /// </summary>
        private static string _appName = "TaskManager";

        /// <summary>
        /// Путь к иконке.
        /// </summary>
        private static string _iconPath = Path.Combine(AppContext.BaseDirectory, "icon.png");

        /// <inheritdoc/>
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

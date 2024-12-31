using System.Diagnostics;

using ViewModel.Interfaces.AppStates;

namespace View.Desktop.macOS
{
    /// <summary>
    /// Класс менеджера уведомлений macOS.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="INotificationManager"/>.
    /// </remarks>
    public class MacOsNotificationManager : INotificationManager
    {
        private static string _appName = "TaskManager";

        /// <inheritdoc/>
        public void SendNotification(string description, string title)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "osascript",
                Arguments = $"-e 'display notification \"{description}\" with title \"{title}\" " +
                    $"subtitle \"{_appName}\"'",
                RedirectStandardOutput = false,
                UseShellExecute = true,
                CreateNoWindow = true,
            });
        }
    }
}

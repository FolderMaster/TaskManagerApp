using System.Diagnostics;

using ViewModel.Interfaces.AppStates;

namespace View.Desktop.macOS
{
    public class MacOsNotificationManager : INotificationManager
    {
        public void SendNotification(string description, string title)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "osascript",
                Arguments = $"-e 'display notification \"{description}\" with title \"{title}\"'",
                RedirectStandardOutput = false,
                UseShellExecute = true,
                CreateNoWindow = true,
            });
        }
    }
}

using System.Diagnostics;

using ViewModel.Interfaces.AppStates;

namespace View.Desktop.Linux
{
    public class LinuxNotificationManager : INotificationManager
    {
        public void SendNotification(string description, string title)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "notify-send",
                Arguments = $"\"{title}\" \"{description}\"",
                RedirectStandardOutput = false,
                UseShellExecute = true,
                CreateNoWindow = true
            });
        }
    }
}

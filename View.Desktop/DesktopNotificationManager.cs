using Microsoft.Toolkit.Uwp.Notifications;
using ViewModel.Interfaces;

namespace View.Desktop
{
    public class DesktopNotificationManager : INotificationManager
    {
        public void SendNotification(string content, string title)
        {
            var toast = new ToastContentBuilder()
                .AddArgument("action", "viewConversation")
                .AddArgument("conversationId", 300)
                .AddText(title)
                .AddText(content);
            toast.Show();
        }
    }
}

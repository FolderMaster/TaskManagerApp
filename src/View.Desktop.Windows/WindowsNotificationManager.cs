using Microsoft.Toolkit.Uwp.Notifications;

using ViewModel.Interfaces.AppStates;

namespace View.Desktop.Windows
{
    public class WindowsNotificationManager : INotificationManager
    {
        public void SendNotification(string description, string title)
        {
            var toast = new ToastContentBuilder().
                AddArgument("action", "viewConversation").
                AddArgument("conversationId", 300).
                AddText(title).AddText(description).
                SetToastScenario(ToastScenario.Alarm);
        }
    }
}

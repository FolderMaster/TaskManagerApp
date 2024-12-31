using Microsoft.Toolkit.Uwp.Notifications;

using ViewModel.Interfaces.AppStates;

namespace View.Desktop.Windows
{
    /// <summary>
    /// Класс менеджера уведомлений Windows.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="INotificationManager"/>.
    /// </remarks>
    public class WindowsNotificationManager : INotificationManager
    {
        /// <inheritdoc/>
        public void SendNotification(string description, string title)
        {
            var toast = new ToastContentBuilder().
                AddArgument("action", "viewConversation").
                AddArgument("conversationId", 300).
                AddText(title).AddText(description).
                SetToastScenario(ToastScenario.Alarm);
            toast.Show();
        }
    }
}

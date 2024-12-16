using ViewModel.Interfaces.AppStates;

namespace ViewModel.Implementations.Mocks
{
    public class MockNotificationManager : INotificationManager
    {
        public event EventHandler<NotificationEventArgs> NotificationSended;

        public void SendNotification(string description, string title) =>
            NotificationSended?.Invoke(this, new NotificationEventArgs(description, title));
    }
}

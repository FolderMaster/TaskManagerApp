namespace ViewModel.Interfaces.AppStates
{
    public interface INotificationManager
    {
        public void SendNotification(string content, string title);
    }
}

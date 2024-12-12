namespace ViewModel.Interfaces.AppStates
{
    public interface INotificationManager
    {
        public void SendNotification(string description, string title);
    }
}

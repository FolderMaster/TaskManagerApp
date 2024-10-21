namespace ViewModel
{
    public class MockNotificationManager : INotificationManager
    {
        public void SendNotification(string content, string title)
        {
            throw new NotImplementedException();
        }
    }
}

using System.Diagnostics;

namespace ViewModel
{
    public class MockNotificationManager : INotificationManager
    {
        public void SendNotification(string content, string title)
        {
            Debug.WriteLine(content, title);
        }
    }
}

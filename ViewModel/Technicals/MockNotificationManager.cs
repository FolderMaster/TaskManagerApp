using System.Diagnostics;

namespace ViewModel.Technicals
{
    public class MockNotificationManager : INotificationManager
    {
        public void SendNotification(string content, string title)
        {
            Debug.WriteLine(content, title);
        }
    }
}

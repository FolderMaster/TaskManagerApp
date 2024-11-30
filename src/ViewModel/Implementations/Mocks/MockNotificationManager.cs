using System.Diagnostics;
using ViewModel.Interfaces;

namespace ViewModel.Implementations.Mocks
{
    public class MockNotificationManager : INotificationManager
    {
        public void SendNotification(string content, string title)
        {
            Debug.WriteLine(content, title);
        }
    }
}

using System.Diagnostics;
using ViewModel.Interfaces.AppStates;

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

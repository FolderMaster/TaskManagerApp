namespace ViewModel.Implementations.Mocks
{
    public class NotificationEventArgs : EventArgs
    {
        public string Description { get; private set; }

        public string Title { get; private set; }

        public NotificationEventArgs(string description, string title)
        {
            Description = description;
            Title = title;
        }
    }
}

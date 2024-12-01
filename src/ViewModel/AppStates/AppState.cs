using TrackableFeatures;

namespace ViewModel.AppStates
{
    public class AppState : TrackableObject
    {
        private Session _session;

        private Settings _settings;

        public event EventHandler<object>? ItemSessionChanged;

        public Session Session
        {
            get => _session;
            set => UpdateProperty(ref _session, value, UpdateSessionItems);
        }

        public Settings Settings
        {
            get => _settings;
            set => UpdateProperty(ref _settings, value);
        }

        public ServicesCollection Services { get; private set; }

        public AppState(Session session, Settings settings, ServicesCollection services)
        {
            _session = session;
            Settings = settings;
            Services = services;
        }

        public void UpdateSessionItems() => ItemSessionChanged?.Invoke(this, new());
    }
}

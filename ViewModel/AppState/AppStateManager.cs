using Model.Technicals;

namespace ViewModel.AppState
{
    public class AppStateManager : TrackableObject
    {
        private Session _session;

        public event EventHandler<object>? ItemSessionChanged;

        public Session Session
        {
            get => _session;
            set => UpdateProperty(ref _session, value, UpdateSessionItems);
        }

        public ServicesCollection Services { get; private set; }

        public AppStateManager(Session session, ServicesCollection services)
        {
            _session = session;
            Services = services;
        }

        public void UpdateSessionItems() => ItemSessionChanged?.Invoke(this, new());
    }
}

using TrackableFeatures;

using ViewModel.Interfaces;

namespace ViewModel.AppStates
{
    public class AppState : TrackableObject
    {
        private ISession _session;

        private Settings _settings;

        public ISession Session => _session;

        public Settings Settings
        {
            get => _settings;
            set => UpdateProperty(ref _settings, value);
        }

        public ServicesCollection Services { get; private set; }

        public AppState(ISession session, Settings settings, ServicesCollection services)
        {
            _session = session;
            Settings = settings;
            Services = services;
        }
    }
}

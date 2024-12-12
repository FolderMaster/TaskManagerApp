using TrackableFeatures;

using ViewModel.Interfaces.AppStates;
using ViewModel.Interfaces.AppStates.Sessions;

namespace ViewModel.Implementations.AppStates
{
    public class AppState : TrackableObject
    {
        private ISession _session;

       private IAppLifeState _appLifeState;

        private Settings _settings;

        public IAppLifeState AppLifeState => _appLifeState;

        public ISession Session => _session;

        public Settings Settings
        {
            get => _settings;
            set => UpdateProperty(ref _settings, value);
        }

        public ServicesCollection Services { get; private set; }

        public AppState(ISession session, Settings settings, ServicesCollection services,
            IAppLifeState appLifeState)
        {
            _session = session;
            _appLifeState = appLifeState;
            Settings = settings;
            Services = services;
        }
    }
}

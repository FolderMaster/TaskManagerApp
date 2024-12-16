using ViewModel.Interfaces.AppStates;

namespace ViewModel.Implementations.Mocks
{
    public class MockAppLifeState : IAppLifeState
    {
        public event EventHandler AppDeactivated;

        public void DeactivateApp() => AppDeactivated?.Invoke(this, EventArgs.Empty);
    }
}

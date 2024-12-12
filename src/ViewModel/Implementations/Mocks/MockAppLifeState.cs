using ViewModel.Interfaces.AppStates;

namespace ViewModel.Implementations.Mocks
{
    public class MockAppLifeState : IAppLifeState
    {
        public event EventHandler AppDeactivated;
    }
}

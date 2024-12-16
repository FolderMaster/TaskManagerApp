using ViewModel.Interfaces.AppStates.Settings;

namespace ViewModel.Implementations.Mocks
{
    public class MockThemeManager : IThemeManager
    {
        public IEnumerable<object> Themes => throw new NotImplementedException();

        public object ActualTheme
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
    }
}

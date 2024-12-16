using System.Globalization;

using ViewModel.Interfaces.AppStates.Settings;

namespace ViewModel.Implementations.Mocks
{
    public class MockLocalizationManager : ILocalizationManager
    {
        public IEnumerable<CultureInfo> Localizations => throw new NotImplementedException();

        public CultureInfo ActualLocalization
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
    }
}

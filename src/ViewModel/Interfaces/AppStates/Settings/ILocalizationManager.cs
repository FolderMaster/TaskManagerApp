using System.Globalization;

namespace ViewModel.Interfaces.AppStates.Settings
{
    public interface ILocalizationManager
    {
        public IEnumerable<CultureInfo> Localizations { get; }

        public CultureInfo ActualLocalization { get; set; }
    }
}

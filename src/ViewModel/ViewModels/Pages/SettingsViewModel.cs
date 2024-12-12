using System.Globalization;

using ViewModel.Implementations.AppStates;

namespace ViewModel.ViewModels.Pages
{
    public partial class SettingsViewModel : PageViewModel
    {
        private readonly AppState _appState;

        public IEnumerable<object> Themes => _appState.Settings.ThemeManager.Themes;

        public object SelectedTheme
        {
            get => _appState.Settings.ThemeManager.ActualTheme;
            set => _appState.Settings.ThemeManager.ActualTheme = value;
        }

        public IEnumerable<CultureInfo> Localizations =>
            _appState.Settings.LocalizationManager.Localizations;

        public CultureInfo SelectedLocalization
        {
            get => _appState.Settings.LocalizationManager.ActualLocalization;
            set => _appState.Settings.LocalizationManager.ActualLocalization = value;
        }

        public SettingsViewModel(AppState appState)
        {
            _appState = appState;

            Metadata = _appState.Services.ResourceService.GetResource("SettingsPageMetadata");
        }
    }
}

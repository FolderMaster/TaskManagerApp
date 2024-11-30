using ViewModel.Interfaces;

namespace ViewModel.AppStates
{
    public class Settings
    {
        public IThemeManager ThemeManager { get; private set; }

        public ILocalizationManager LocalizationManager { get; private set; }

        public Settings(IThemeManager themeManager, ILocalizationManager localizationManager)
        {
            ThemeManager = themeManager;
            LocalizationManager = localizationManager;
        }
    }
}

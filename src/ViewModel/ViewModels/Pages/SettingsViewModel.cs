using ReactiveUI;
using ReactiveUI.SourceGenerators;

using ViewModel.Interfaces.AppStates;
using ViewModel.Interfaces.AppStates.Settings;
using ViewModel.Technicals;

namespace ViewModel.ViewModels.Pages
{
    /// <summary>
    /// Класс контроллера страницы настроек.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="BaseViewModel"/>.
    /// </remarks>
    public partial class SettingsViewModel : BasePageViewModel
    {
        /// <summary>
        /// Настройки.
        /// </summary>
        private ISettings _settings;

        /// <summary>
        /// Менеджер тем.
        /// </summary>
        private IThemeManager _themeManager;

        /// <summary>
        /// Менеджер локализаций.
        /// </summary>
        private ILocalizationManager _localizationManager;

        /// <summary>
        /// Конфигурация.
        /// </summary>
        [Reactive]
        public AppConfiguration _configuration;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="SettingsViewModel"/>.
        /// </summary>
        /// <param name="settings">Настройки.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="themeManager">Менеджер тем.</param>
        /// <param name="localizationManager">Менеджер локализаций.</param>
        public SettingsViewModel(ISettings settings, IResourceService resourceService,
            IThemeManager themeManager, ILocalizationManager localizationManager)
        {
            _settings = settings;
            _themeManager = themeManager;
            _localizationManager = localizationManager;

            Configuration = new AppConfiguration(_settings.Configuration,
                _localizationManager.Localizations, _themeManager.Themes);

            this.WhenAnyValue(x => x._settings.Configuration).Subscribe(c => Configuration =
                new AppConfiguration(c, _localizationManager.Localizations, _themeManager.Themes));

            Metadata = resourceService.GetResource("SettingsPageMetadata");
        }
    }
}

using ReactiveUI;
using ReactiveUI.SourceGenerators;

using ViewModel.Interfaces.AppStates;
using ViewModel.Interfaces.AppStates.Settings;

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
        /// Конфигурация.
        /// </summary>
        [Reactive]
        public object _configuration;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="SettingsViewModel"/>.
        /// </summary>
        /// <param name="settings">Настройки.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        public SettingsViewModel(ISettings settings, IResourceService resourceService)
        {
            _settings = settings;
            Configuration = _settings.Configuration;

            this.WhenAnyValue(x => x._settings.Configuration).Subscribe(c => Configuration = c);

            Metadata = resourceService.GetResource("SettingsPageMetadata");
        }
    }
}

using System.ComponentModel;

using TrackableFeatures;

using ViewModel.Interfaces.AppStates;
using ViewModel.Interfaces.AppStates.Sessions;
using ViewModel.Interfaces.AppStates.Settings;
using ViewModel.Technicals;

namespace ViewModel.Implementations.AppStates.Settings
{
    /// <summary>
    /// Класс настроек приложения.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="TrackableObject"/>.
    /// Реализует <see cref="ISettings"/>.
    /// </remarks>
    public class AppSettings : TrackableObject, ISettings
    {
        /// <summary>
        /// Путь к файлу.
        /// </summary>
        private static readonly string _filePath = "settings.json";

        /// <summary>
        /// Полный путь к файлу.
        /// </summary>
        private readonly string _fullFilePath;

        /// <summary>
        /// Файловый сервис.
        /// </summary>
        private IFileService _fileService;

        /// <summary>
        /// Сериализатор.
        /// </summary>
        private ISerializer _serializer;

        /// <summary>
        /// Менеджер тем.
        /// </summary>
        private IThemeManager _themeManager;

        /// <summary>
        /// Менеджер локализаций.
        /// </summary>
        private ILocalizationManager _localizationManager;

        /// <summary>
        /// Сессия.
        /// </summary>
        private ISession _session;

        /// <summary>
        /// Логгирование.
        /// </summary>
        private ILogger _logger;

        /// <summary>
        /// Конфигурация.
        /// </summary>
        private AppConfiguration _configuration = new();

        /// <inheritdoc/>
        public object Configuration
        {
            get => _configuration;
            private set => UpdateProperty(ref _configuration, (AppConfiguration)value,
                OnConfigurationUpdated);
        }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="AppSettings"/>.
        /// </summary>
        /// <param name="themeManager">Менеджер тем.</param>
        /// <param name="localizationManager">Менеджер локализаций.</param>
        /// <param name="session">Сессия.</param>
        /// <param name="fileService">Файловый сервис.</param>
        /// <param name="serializer">Сериализатор.</param>
        /// <param name="logger">Логгирование.</param>
        public AppSettings(IThemeManager themeManager, ILocalizationManager localizationManager,
            ISession session, IFileService fileService, ISerializer serializer, ILogger logger)
        {
            _themeManager = themeManager;
            _localizationManager = localizationManager;
            _session = session;
            _fileService = fileService;
            _serializer = serializer;
            _logger = logger;

            _fullFilePath = _fileService.CombinePath
                (_fileService.PersonalDirectoryPath, _filePath);

            InitializeConfiguration();
        }

        /// <inheritdoc/>
        public async Task Save()
        {
            try
            {
                var directoryPath = _fileService.GetDirectoryPath(_fullFilePath);
                if (!_fileService.IsPathExists(directoryPath))
                {
                    _fileService.CreateDirectory(directoryPath);
                }
                var format = new AppConfigurationFormat()
                {
                    Theme = _configuration.ActualTheme.ToString(),
                    Localization = _configuration.ActualLocalization,
                    SavePath = _configuration.SavePath
                };
                var data = _serializer.Serialize(format);
                await _fileService.Save(_fullFilePath, data);
            }
            catch (Exception ex)
            {
                _logger.Log(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task Load()
        {
            if (!_fileService.IsPathExists(_fullFilePath))
            {
                return;
            }
            try
            {
                var bytes = await _fileService.Load(_fullFilePath);
                var format = _serializer.Deserialize<AppConfigurationFormat>(bytes);
                if (format != null)
                {
                    Configuration = new AppConfiguration()
                    {
                        Localizations = _localizationManager.Localizations,
                        Themes = _themeManager.Themes,
                        ActualLocalization = format.Localization ??
                            _configuration.ActualLocalization,
                        ActualTheme = _themeManager.Themes.FirstOrDefault
                            (t => t.ToString() == format.Theme) ?? _configuration.ActualTheme,
                        SavePath = format.SavePath ?? _configuration.SavePath
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Log(ex.Message);
            }
        }

        /// <summary>
        /// Инициализирует конфигурацию.
        /// </summary>
        protected void InitializeConfiguration()
        {
            _configuration = new AppConfiguration
            {
                SavePath = _session.SavePath,
                Localizations = _localizationManager.Localizations,
                ActualLocalization = _localizationManager.ActualLocalization,
                Themes = _themeManager.Themes,
                ActualTheme = _themeManager.ActualTheme
            };

            _configuration.PropertyChanged += Configuration_PropertyChanged;
        }

        /// <summary>
        /// Вызывается при обновлении конфигурации.
        /// </summary>
        /// <param name="oldConfiguration">Старая конфигурация.</param>
        /// <param name="newConfiguration">Новая конфигурация.</param>
        protected void OnConfigurationUpdated(AppConfiguration oldConfiguration,
            AppConfiguration newConfiguration)
        {
            oldConfiguration.PropertyChanged -= Configuration_PropertyChanged;
            _themeManager.ActualTheme = _configuration.ActualTheme;
            _localizationManager.ActualLocalization = _configuration.ActualLocalization;
            _session.SavePath = _configuration.SavePath;
            newConfiguration.PropertyChanged += Configuration_PropertyChanged;
        }

        private void Configuration_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(AppConfiguration.ActualTheme):
                    _themeManager.ActualTheme = _configuration.ActualTheme;
                    break;
                case nameof(AppConfiguration.ActualLocalization):
                    _localizationManager.ActualLocalization = _configuration.ActualLocalization;
                    break;
                case nameof(AppConfiguration.SavePath):
                    _session.SavePath = _configuration.SavePath;
                    break;
            }
        }
    }
}

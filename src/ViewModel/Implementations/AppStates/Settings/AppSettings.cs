using System.ComponentModel;

using TrackableFeatures;

using ViewModel.Interfaces.AppStates;
using ViewModel.Interfaces.AppStates.Sessions;
using ViewModel.Interfaces.AppStates.Settings;
using ViewModel.Technicals;

namespace ViewModel.Implementations.AppStates.Settings
{
    public class AppSettings : TrackableObject, ISettings
    {
        private static readonly string _filePath = "settings.json";

        private readonly string _fullFilePath;

        private IFileService _fileService;

        private ISerializer _serializer;

        private IThemeManager _themeManager;

        private ILocalizationManager _localizationManager;

        private ISession _session;

        private ILogger _logger;

        private AppConfiguration _configuration = new();

        public object Configuration
        {
            get => _configuration;
            private set => UpdateProperty(ref _configuration, (AppConfiguration)value,
                OnConfigurationUpdated);
        }

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

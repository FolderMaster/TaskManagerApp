using System.Collections;
using System.Collections.Specialized;

using TrackableFeatures;

using ViewModel.Interfaces.AppStates;
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
        /// Частичный путь к файлу.
        /// </summary>
        private static readonly string _partFilePath = "settings.json";

        /// <summary>
        /// Настраиваемые объекты.
        /// </summary>
        private readonly IEnumerable<IConfigurable> _configurables;

        /// <summary>
        /// Файловый сервис.
        /// </summary>
        private readonly IFileService _fileService;

        /// <summary>
        /// Сериализатор.
        /// </summary>
        private readonly ISerializer _serializer;

        /// <summary>
        /// Логгирование.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Конфигурация.
        /// </summary>
        private TrackableDictionary<ConfigurableKey, object> _configuration = new();

        /// <summary>
        /// Возвращает и задаёт путь к файлу.
        /// </summary>
        public string FilePath { get; set; }

        /// <inheritdoc/>
        public IDictionary Configuration
        {
            get => _configuration;
            private set => UpdateProperty(ref _configuration,
                (TrackableDictionary<ConfigurableKey, object>)value, OnConfigurationUpdated);
        }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="AppSettings"/>.
        /// </summary>
        /// <param name="configurables">Настраиваемые объекты.</param>
        /// <param name="fileService">Файловый сервис.</param>
        /// <param name="serializer">Сериализатор.</param>
        /// <param name="logger">Логгирование.</param>
        public AppSettings(IEnumerable<IConfigurable> configurables,
            IFileService fileService, ISerializer serializer, ILogger logger)
        {
            _configurables = configurables;
            _fileService = fileService;
            _serializer = serializer;
            _logger = logger;

            FilePath = _fileService.CombinePath
                (_fileService.PersonalDirectoryPath, _partFilePath);

            InitializeConfiguration();
        }

        /// <inheritdoc/>
        public async Task Save()
        {
            try
            {
                var directoryPath = _fileService.GetDirectoryPath(FilePath);
                if (!_fileService.IsPathExists(directoryPath))
                {
                    _fileService.CreateDirectory(directoryPath);
                }
                var data = _serializer.Serialize(_configuration);
                await _fileService.Save(FilePath, data);
            }
            catch (Exception ex)
            {
                _logger.Log(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task Load()
        {
            if (!_fileService.IsPathExists(FilePath))
            {
                return;
            }
            try
            {
                var bytes = await _fileService.Load(FilePath);
                var format = _serializer.Deserialize<Dictionary<ConfigurableKey, object>>(bytes);
                if (format != null)
                {
                    foreach (var configuration in format)
                    {
                        var key = configuration.Key;
                        if (_configuration.ContainsKey(key))
                        {
                            _configuration[key] = configuration.Value;
                        }
                    }
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
            var dictionary = new TrackableDictionary<ConfigurableKey, object>();
            foreach (var configurable in _configurables)
            {
                dictionary.Add((ConfigurableKey)configurable.SettingsKey, configurable.Settings);
            }
            _configuration = dictionary;
            _configuration.CollectionChanged += Configuration_CollectionChanged;
        }

        /// <summary>
        /// Вызывается при обновлении конфигурации.
        /// </summary>
        /// <param name="oldConfiguration">Старая конфигурация.</param>
        /// <param name="newConfiguration">Новая конфигурация.</param>
        protected void OnConfigurationUpdated
            (TrackableDictionary<ConfigurableKey, object> oldConfiguration,
            TrackableDictionary<ConfigurableKey, object> newConfiguration)
        {
            oldConfiguration.CollectionChanged -= Configuration_CollectionChanged;
            newConfiguration.CollectionChanged += Configuration_CollectionChanged;
        }

        private void Configuration_CollectionChanged(object? sender,
            NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Reset:
                    throw new NotImplementedException();
                case NotifyCollectionChangedAction.Replace:
                    var keyValuePair = (KeyValuePair<ConfigurableKey, object>)e.NewItems[0];
                    var configurable = _configurables.First
                        (v => (ConfigurableKey)v.SettingsKey == keyValuePair.Key);
                    configurable.Settings = keyValuePair.Value;
                    break;
            }
        }
    }
}

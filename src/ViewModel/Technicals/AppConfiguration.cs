using System.Collections;
using System.Globalization;

namespace ViewModel.Technicals
{
    /// <summary>
    /// Класс конфигурации приложения.
    /// </summary>
    public class AppConfiguration
    {
        /// <summary>
        /// Возвращает настройки.
        /// </summary>
        private readonly IDictionary _settings;

        /// <summary>
        /// Возвращает локализации.
        /// </summary>
        public IEnumerable<CultureInfo> Localizations { get; private set; }

        /// <summary>
        /// Возвращает и задаёт актуальную локализацию.
        /// </summary>
        public CultureInfo ActualLocalization
        {
            get => (CultureInfo)_settings[ConfigurableKey.Localization];
            set => _settings[ConfigurableKey.Localization] = value;
        }

        /// <summary>
        /// Возвращает темы.
        /// </summary>
        public IEnumerable<object> Themes { get; private set; }

        /// <summary>
        /// Возвращает и задаёт актуальную тему.
        /// </summary>
        public object ActualTheme
        {
            get => _settings[ConfigurableKey.Theme];
            set => _settings[ConfigurableKey.Theme] = value;
        }

        /// <summary>
        /// Возвращает и задаёт строку подключения.
        /// </summary>
        public string ConnectionString
        {
            get => (string)_settings[ConfigurableKey.DataBase];
            set => _settings[ConfigurableKey.DataBase] = value;
        }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="AppConfiguration"/>.
        /// </summary>
        /// <param name="settings">Настройки.</param>
        /// <param name="localizations">Локализации.</param>
        /// <param name="themes">Темы.</param>
        public AppConfiguration(IDictionary settings, IEnumerable<CultureInfo> localizations,
            IEnumerable<object> themes)
        {
            _settings = settings;
            Localizations = localizations;
            Themes = themes;
        }
    }
}

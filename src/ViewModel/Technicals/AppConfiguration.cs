using System.Globalization;

using TrackableFeatures;

namespace ViewModel.Technicals
{
    /// <summary>
    /// Класс конфигурации приложения.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="TrackableObject"/>.
    /// </remarks>
    public class AppConfiguration : TrackableObject
    {
        /// <summary>
        /// Актуальная локализация.
        /// </summary>
        private CultureInfo _actualLocalization;

        /// <summary>
        /// Актуальная тема.
        /// </summary>
        private object _actualTheme;

        /// <summary>
        /// Путь сохранения.
        /// </summary>
        private string _savePath;

        /// <summary>
        /// Возвращает и задаёт актуальную локализацию.
        /// </summary>
        public CultureInfo ActualLocalization
        {
            get => _actualLocalization;
            set => UpdateProperty(ref _actualLocalization, value);
        }

        /// <summary>
        /// Возвращает и задаёт локализации.
        /// </summary>
        public IEnumerable<CultureInfo> Localizations { get; set; } =
            Enumerable.Empty<CultureInfo>();

        /// <summary>
        /// Возвращает и задаёт актуальную тему.
        /// </summary>
        public object ActualTheme
        {
            get => _actualTheme;
            set => UpdateProperty(ref _actualTheme, value);
        }

        /// <summary>
        /// Возвращает и задаёт темы.
        /// </summary>
        public IEnumerable<object> Themes { get; set; } = Enumerable.Empty<object>();

        /// <summary>
        /// Возвращает и задаёт путь сохранения.
        /// </summary>
        public string SavePath
        {
            get => _savePath;
            set => UpdateProperty(ref _savePath, value);
        }
    }
}

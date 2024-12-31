using System.Globalization;

namespace ViewModel.Implementations.AppStates.Settings
{
    /// <summary>
    /// Класс формата конфигурации приложении.
    /// </summary>
    public class AppConfigurationFormat
    {
        /// <summary>
        /// Возвращает и задаёт локализацию.
        /// </summary>
        public CultureInfo? Localization { get; set; }

        /// <summary>
        /// Возвращает и задаёт тему.
        /// </summary>
        public string? Theme { get; set; }

        /// <summary>
        /// Возвращает и задаёт путь к сохранению.
        /// </summary>
        public string? SavePath { get; set; }
    }
}

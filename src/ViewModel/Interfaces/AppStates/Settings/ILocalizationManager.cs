using System.Globalization;

namespace ViewModel.Interfaces.AppStates.Settings
{
    /// <summary>
    /// Интерфейс менеджера локализаций.
    /// </summary>
    public interface ILocalizationManager
    {
        /// <summary>
        /// Возвращает локализации.
        /// </summary>
        public IEnumerable<CultureInfo> Localizations { get; }

        /// <summary>
        /// Возвращает и задаёт актуальную локализацию.
        /// </summary>
        public CultureInfo ActualLocalization { get; set; }
    }
}

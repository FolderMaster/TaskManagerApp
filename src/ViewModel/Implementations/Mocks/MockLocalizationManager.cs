using System.Globalization;

using ViewModel.Interfaces.AppStates.Settings;

namespace ViewModel.Implementations.Mocks
{
    /// <summary>
    /// Класс-заглушка менеджера локализаций.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="ILocalizationManager"/>.
    /// </remarks>
    public class MockLocalizationManager : ILocalizationManager
    {
        /// <summary>
        /// Локализации.
        /// </summary>
        private IEnumerable<CultureInfo> _localizations;

        /// <inheritdoc/>
        public IEnumerable<CultureInfo> Localizations
        {
            get => _localizations;
            set
            {
                _localizations = value;
                ActualLocalization = _localizations.First();
            }
        }

        /// <inheritdoc/>
        public CultureInfo ActualLocalization { get; set; }
    }
}

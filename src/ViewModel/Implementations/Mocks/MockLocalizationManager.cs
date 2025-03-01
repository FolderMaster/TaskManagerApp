using System.Globalization;
using ViewModel.Interfaces.AppStates.Settings;
using ViewModel.Technicals;

namespace ViewModel.Implementations.Mocks
{
    /// <summary>
    /// Класс-заглушка менеджера локализаций.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="ILocalizationManager"/> и <see cref="IConfigurable"/>.
    /// </remarks>
    public class MockLocalizationManager : ILocalizationManager, IConfigurable
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

        /// <inheritdoc/>
        public object SettingsKey => ConfigurableKey.Localization;

        /// <inheritdoc/>
        public Type SettingsType => typeof(CultureInfo);

        /// <inheritdoc/>
        public object Settings
        {
            get => ActualLocalization;
            set => ActualLocalization = (CultureInfo)value;
        }
    }
}

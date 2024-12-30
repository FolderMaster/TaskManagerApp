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
        /// <inheritdoc/>
        public IEnumerable<CultureInfo> Localizations => throw new NotImplementedException();

        /// <inheritdoc/>
        public CultureInfo ActualLocalization
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
    }
}

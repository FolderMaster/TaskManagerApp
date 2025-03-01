using ViewModel.Interfaces.AppStates.Settings;
using ViewModel.Technicals;

namespace ViewModel.Implementations.Mocks
{
    /// <summary>
    /// Класс-заглушка менеджера тем.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IThemeManager"/> и <see cref="IConfigurable"/>.
    /// </remarks>
    public class MockThemeManager : IThemeManager, IConfigurable
    {
        /// <summary>
        /// Темы.
        /// </summary>
        private IEnumerable<object> _themes;

        /// <inheritdoc/>
        public IEnumerable<object> Themes
        {
            get => _themes;
            set
            {
                _themes = value;
                ActualTheme = _themes.First();
            }
        }

        /// <inheritdoc/>
        public object ActualTheme { get; set; }

        /// <inheritdoc/>
        public object SettingsKey => ConfigurableKey.Theme;

        /// <inheritdoc/>
        public Type SettingsType => typeof(object);

        /// <inheritdoc/>
        public object Settings
        {
            get => ActualTheme;
            set => ActualTheme = value;
        }
    }
}

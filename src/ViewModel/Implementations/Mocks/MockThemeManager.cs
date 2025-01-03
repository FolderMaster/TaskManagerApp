using ViewModel.Interfaces.AppStates.Settings;

namespace ViewModel.Implementations.Mocks
{
    /// <summary>
    /// Класс-заглушка менеджера тем.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IThemeManager"/>.
    /// </remarks>
    public class MockThemeManager : IThemeManager
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
    }
}

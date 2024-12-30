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
        /// <inheritdoc/>
        public IEnumerable<object> Themes => throw new NotImplementedException();

        /// <inheritdoc/>
        public object ActualTheme
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
    }
}

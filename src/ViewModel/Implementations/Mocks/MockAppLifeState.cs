using ViewModel.Interfaces.AppStates;

namespace ViewModel.Implementations.Mocks
{
    /// <summary>
    /// Класс-заглушка управления состоянием жизненного цикла приложения.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IAppLifeState"/>.
    /// </remarks>
    public class MockAppLifeState : IAppLifeState
    {
        /// <inheritdoc/>
        public event EventHandler AppDeactivated;

        /// <summary>
        /// Деактивирует приложение.
        /// </summary>
        public void DeactivateApp() => AppDeactivated?.Invoke(this, EventArgs.Empty);
    }
}

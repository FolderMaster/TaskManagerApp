using System;

using ViewModel.Interfaces.AppStates;

namespace View.Desktop.macOS
{
    /// <summary>
    /// Класс для управления состоянием жизненного цикла desktop-приложения.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IAppLifeState"/>.
    /// </remarks>
    public class DesktopAppLifeState : IAppLifeState
    {
        /// <inheritdoc/>
        public event EventHandler AppDeactivated;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="DesktopAppLifeState"/> по умолчанию.
        /// </summary>
        public DesktopAppLifeState()
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
        }

        private void CurrentDomain_ProcessExit(object? sender, EventArgs e) =>
            AppDeactivated?.Invoke(this, e);
    }
}

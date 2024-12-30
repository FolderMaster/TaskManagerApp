namespace ViewModel.Interfaces.AppStates
{
    /// <summary>
    /// Интерфейс для управления состоянием жизненного цикла приложения.
    /// </summary>
    public interface IAppLifeState
    {
        /// <summary>
        /// Событие, которое возникает, когда приложение деактивируется.
        /// </summary>
        public event EventHandler AppDeactivated;
    }
}

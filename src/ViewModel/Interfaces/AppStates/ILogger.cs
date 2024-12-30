namespace ViewModel.Interfaces.AppStates
{
    /// <summary>
    /// Интерфейс сервиса логирования сообщений.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Записывает сообщение в лог.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        public void Log(string message);
    }
}

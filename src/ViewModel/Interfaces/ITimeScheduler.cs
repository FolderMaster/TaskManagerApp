namespace ViewModel.Interfaces
{
    /// <summary>
    /// Интерфейс планировщика времени, который отслеживает достижение временных меток.
    /// </summary>
    public interface ITimeScheduler
    {
        /// <summary>
        /// Возращает список временных меток.
        /// </summary>
        public IList<DateTime> Timepoints { get; }

        /// <summary>
        /// Событие, которое возникает при достижении временной метки.
        /// </summary>
        /// <remarks>
        /// Параметр передаёт время достигнутой метки.
        /// </remarks>
        public event EventHandler<DateTime> TimepointReached;
    }
}

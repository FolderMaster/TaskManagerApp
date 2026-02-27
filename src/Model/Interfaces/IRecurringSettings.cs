namespace Model.Interfaces
{
    /// <summary>
    /// Интерфейс настройки повторения.
    /// </summary>
    public interface IRecurringSettings
    {
        /// <summary>
        /// Рассчитыввает повторения в периоде.
        /// </summary>
        /// <param name="startDate">Дата начала периода.</param>
        /// <param name="endDate">Дата конца периода.</param>
        /// <returns>Возвращает временные метки повторений в периоде.</returns>
        public IEnumerable<DateTime> CalculateOccurrences(DateTime startDate, DateTime endDate);
    }
}

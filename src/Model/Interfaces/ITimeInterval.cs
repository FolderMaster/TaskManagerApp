namespace Model.Interfaces
{
    /// <summary>
    /// Интерфейс временного интервала.
    /// </summary>
    public interface ITimeInterval
    {
        /// <summary>
        /// Возвращает длительность.
        /// </summary>
        public TimeSpan Duration { get; }
    }
}

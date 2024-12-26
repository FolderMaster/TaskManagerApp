namespace Model.Interfaces
{
    /// <summary>
    /// Интерфейс элементарного временного интервала.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="ITimeInterval"/>.
    /// </remarks>
    public interface ITimeIntervalElement : ITimeInterval
    {
        /// <summary>
        /// Возвращает и задаёт начало.
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// Возвращает и задаёт конец.
        /// </summary>
        public DateTime End { get; set; }
    }
}

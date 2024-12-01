namespace Model.Interfaces
{
    /// <summary>
    /// Интерфейс элементарного временного интервала. Наследует <see cref="ITimeInterval"/>.
    /// </summary>
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

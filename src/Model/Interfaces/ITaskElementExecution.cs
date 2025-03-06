namespace Model.Interfaces
{
    /// <summary>
    /// Интерфейс выполнения элементарной задачи.
    /// </summary>
    public interface ITaskElementExecution
    {
        /// <summary>
        /// Возвращает и задаёт прогресс.
        /// </summary>
        public double Progress { get; set; }

        /// <summary>
        /// Возвращает и задаёт статус.
        /// </summary>
        public TaskStatus Status { get; set; }

        /// <summary>
        /// Возвращает и задаёт потраченное время.
        /// </summary>
        public TimeSpan SpentTime { get; set; }

        /// <summary>
        /// Возвращает и задаёт выполненный реальный показатель.
        /// </summary>
        public double ExecutedReal { get; set; }

        /// <summary>
        /// Возвращает список временных интервалов.
        /// </summary>
        public ITimeIntervalList TimeIntervals { get; }

        /// <summary>
        /// Возращает дату создания.
        /// </summary>
        public DateTime CreatedDate { get; }
    }
}

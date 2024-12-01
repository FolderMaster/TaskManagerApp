namespace Model.Interfaces
{
    /// <summary>
    /// Интерфейс элементарной задачи. Наследует <see cref="ITask"/>.
    /// </summary>
    public interface ITaskElement : ITask
    {
        /// <summary>
        /// Возвращает и задаёт сложность.
        /// </summary>
        public new int Difficult { get; set; }

        /// <summary>
        /// Возвращает и задаёт приоритет.
        /// </summary>
        public new int Priority { get; set; }

        /// <summary>
        /// Возвращает и задаёт статус.
        /// </summary>
        public new TaskStatus Status { get; set; }

        /// <summary>
        /// Возвращает и задаёт срок.
        /// </summary>
        public new DateTime? Deadline { get; set; }

        /// <summary>
        /// Возвращает и задаёт прогресс.
        /// </summary>
        public new double Progress { get; set; }

        /// <summary>
        /// Возвращает и задаёт запланированное время.
        /// </summary>
        public new TimeSpan PlannedTime { get; set; }

        /// <summary>
        /// Возвращает и задаёт потраченное время.
        /// </summary>
        public new TimeSpan SpentTime { get; set; }

        /// <summary>
        /// Возвращает и задаёт запланированный реальный показатель.
        /// </summary>
        public double PlannedReal { get; set; }

        /// <summary>
        /// Возвращает и задаёт выполненный реальный показатель.
        /// </summary>
        public double ExecutedReal { get; set; }

        /// <summary>
        /// Возвращает список временных интервалов.
        /// </summary>
        public ITimeIntervalList TimeIntervals { get; }
    }
}
